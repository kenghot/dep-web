using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Nep.Project.ServiceModels;
using Nep.Project.Common.Web;
using System.Text;

namespace Nep.Project.Web.Report
{
    public partial class ReportFormatContract : Nep.Project.Web.Infra.BasePrintPage
    {
        public IServices.IProjectInfoService _service { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReport();               
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            SuppressExportButton(ReportViewerContract, "EXCELOPENXML");
            //SuppressExportButton(ReportViewerContract, "WORDOPENXML");
        }

        private void BindReport()
        {
            List<ServiceModels.Report.ReportFormatContract> list = new List<ServiceModels.Report.ReportFormatContract>();
            ServiceModels.Report.ReportFormatContract data = new ServiceModels.Report.ReportFormatContract();
            String strProjectID = Request.QueryString["projectID"];
            decimal projectID = 0;
            if (Decimal.TryParse(strProjectID, out projectID))
            {
                var reportResult = _service.GetReportFormatContract(projectID);

                if (reportResult.IsCompleted)
                {

                    ServiceModels.Report.ReportFormatContract contract = reportResult.Data;
                    contract.ContractNo = WebUtility.ParseToThaiNumber(contract.ContractNo);
                    contract.ReceiverAddressNo = WebUtility.ParseToThaiNumber(contract.ReceiverAddressNo);
                    contract.ContractDate = WebUtility.ParseToThaiNumber(contract.ContractDate);
                    contract.Amount = WebUtility.ParseToThaiNumber(contract.Amount);
                    contract.ProjectName = WebUtility.ParseToThaiNumber(contract.ProjectName);
                    contract.AttachPage1 = string.IsNullOrEmpty(contract.AttachPage1) ? "๐ (ศูนย์)" : $" {WebUtility.ParseToThaiNumber(contract.AttachPage1)} ({WebUtility.ToThaiBath(decimal.Parse(contract.AttachPage1)).Replace("บาทถ้วน", "")}) ";
                    contract.AttachPage2 = string.IsNullOrEmpty(contract.AttachPage2) ? "๐ (ศูนย์)" : $" {WebUtility.ParseToThaiNumber(contract.AttachPage2)} ({WebUtility.ToThaiBath(decimal.Parse(contract.AttachPage2)).Replace("บาทถ้วน", "")}) ";
                    contract.AttachPage3 = string.IsNullOrEmpty(contract.AttachPage3) ? "๐ (ศูนย์)" : $" {WebUtility.ParseToThaiNumber(contract.AttachPage3)} ({WebUtility.ToThaiBath(decimal.Parse(contract.AttachPage3)).Replace("บาทถ้วน", "")}) ";
                    contract.MeetingText = $" {contract.MeetingNo} ";
                    var obj = _service.GetProjectApprovalResult(projectID);
                    if (obj.IsCompleted)
                    {
                        contract.MeetingText = $"<u>{obj.Data.ApprovalNo} / " +
                        $"{int.Parse(obj.Data.ApprovalYear) + 543}</u> เมื่อวันที่ <u>{Common.Web.WebUtility.ToBuddhaDateFormat(obj.Data.ApprovalDate, "d MMMM yyyy")}</u>";

                    }
                    
                  
                    //contract.MeetingText += $" จำนวน {contract.AttachPage3} หน้า";
                    contract.MeetingText = WebUtility.ParseToThaiNumber(contract.MeetingText);
                    //contract.ContractNo = Nep.Project.Common.Web.WebUtility.ParseToThaiNumber(contract.ContractNo);

                    String reportName = (contract.IsCenterContract) ? "ReportFormatContractCenter.rdlc" : "ReportFormatContractProvince.rdlc";

                    ReportViewerContract.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile(reportName));

                    list.Add(contract);
                   
                    //Set DataSet
                    var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
                    dataset1.Value = list;
                    ReportViewerContract.LocalReport.DataSources.Add(dataset1);

                    string msgAuthorizeFlag = "";
                    if (list.Count > 0)
                    {
                        if(list[0].AuthorizeFlag)
                            msgAuthorizeFlag = string.Format("ผู้มีอำนาจลงนามผูกพันปรากฏตามหนังสือมอบอำนาจ ฉบับลงวันที่ {0}", list[0].AttorneyDate);
                    }

                    ReportViewerContract.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("MsgAuthorizeFlag", msgAuthorizeFlag));
                    if(!contract.IsCenterContract){
                        ReportViewerContract.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("SupportGivenDesc", CreateSupportGivenDesc(contract)));
                    }

                    ReportViewerContract.DataBind();
                    ReportViewerContract.LocalReport.Refresh();
                    ReportViewerContract.Visible = true;
                }
                else
                {
                    ShowErrorMessage(reportResult.Message);
                    ReportViewerContract.Visible = false;
                }
            }
            else
            {
                ShowErrorMessage(Nep.Project.Resources.Message.NoRecord);
                ReportViewerContract.Visible = false;
            }   
        }

        private String CreateSupportGivenDesc(ServiceModels.Report.ReportFormatContract contract)
        {
            StringBuilder desc = new StringBuilder();
            if (!String.IsNullOrEmpty(contract.DirectiveNo) && (!String.IsNullOrEmpty(contract.DirectProvinceNo)))
            {
                desc.AppendFormat(" ผู้ได้รับมอบอำนาจให้ปฏิบัติราชการแทนตามคำสั่ง กรมส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการที่ <u>{0}</u> ลงวันที่ <u>{1}</u>", contract.DirectiveNo, contract.DirectiveDate);
                desc.AppendFormat(" และคำสั่งจังหวัด <u>{0}</u> ที่ <u>{1}</u>  ลงวันที่ <u>{2}</u> ", contract.DirectiveProvince, contract.DirectProvinceNo,  contract.DirectProvinceDate);

            }else if(!String.IsNullOrEmpty(contract.DirectiveNo)){
                desc.AppendFormat(" ผู้ได้รับมอบอำนาจให้ปฏิบัติราชการแทนตามคำสั่ง กรมส่งเสริมและพัฒนาคุณภาพชีวิตคนพิการที่ <u>{0}</u> ลงวันที่ <u>{1}</u> ", contract.DirectiveNo, contract.DirectiveDate);
            }
            else if (!String.IsNullOrEmpty(contract.DirectProvinceNo))
            {
                desc.AppendFormat(" ผู้ได้รับมอบอำนาจให้ปฏิบัติราชการแทนตามคำสั่งจังหวัด <u>{0}</u> ที่ <u>{1}</u>  ลงวันที่ <u>{2}</u> ", contract.DirectiveProvince, contract.DirectProvinceNo, contract.DirectProvinceDate);
            }

           
            return WebUtility.ParseToThaiNumber(desc.ToString());
        }
        
    }
}