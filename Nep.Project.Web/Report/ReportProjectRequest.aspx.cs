using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Report
{
    public partial class ReportProjectRequest : Nep.Project.Web.Infra.BasePrintPage
    {
        public IServices.IProjectInfoService _service { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack){
                BindReport();
            }
        }

        protected void BindReport()
        {
            ReportViewerProjectRequest.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportProjectRequest.rdlc"));
            
            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.AllFlags));
            ReportViewerProjectRequest.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
            Assembly asmSystemDrawing = Assembly.Load("Nep.Project.Common.Report, Version=1.0.0.0, Culture=neutral, PublicKeyToken=af066dcbb193094a");
            AssemblyName asmNameSystemDrawing = asmSystemDrawing.GetName();            
            ReportViewerProjectRequest.LocalReport.AddFullTrustModuleInSandboxAppDomain(new StrongName(new StrongNamePublicKeyBlob(asmNameSystemDrawing.GetPublicKeyToken()), asmNameSystemDrawing.Name, asmNameSystemDrawing.Version));

            String strProjectID = Request.QueryString["projectID"];
            decimal projectID = 0;
            List<String> errorMessages = new List<string>();

            if (Decimal.TryParse(strProjectID, out projectID))
            {     
                var genInfoResult = _service.GetReportProjectRequest(projectID);
                if (genInfoResult.IsCompleted)
                {
                    var orgTypeResult = _service.GetOrganizationType();                   
                    List<Microsoft.Reporting.WebForms.ReportParameter> paramList = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                    if(orgTypeResult.IsCompleted){
                        List<ServiceModels.GenericDropDownListData> orgTypes = orgTypeResult.Data;
                        var p = new Microsoft.Reporting.WebForms.ReportParameter("OrgType1ID", orgTypes[0].Value);
                        ReportViewerProjectRequest.LocalReport.SetParameters(p);
                        ReportViewerProjectRequest.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("OrgType2ID", orgTypes[1].Value));
                        ReportViewerProjectRequest.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("OrgType3ID", orgTypes[2].Value));
                        ReportViewerProjectRequest.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("OrgType4ID", orgTypes[3].Value));
                        ReportViewerProjectRequest.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("OrgType5ID", orgTypes[4].Value));
                        ReportViewerProjectRequest.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("OrgType6ID", orgTypes[5].Value));
                        ReportViewerProjectRequest.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("OrgType7ID", orgTypes[6].Value));
                    }

                    var genInfoDataset = new Microsoft.Reporting.WebForms.ReportDataSource("GeneralProjectInfo");
                    var orgAssDataset = new Microsoft.Reporting.WebForms.ReportDataSource("OrganizationAssistance");
                    var attDataset = new Microsoft.Reporting.WebForms.ReportDataSource("ProjectAttachment");
                    var budgetDataset = new Microsoft.Reporting.WebForms.ReportDataSource("ProjectBudget");
                    var commDataset = new Microsoft.Reporting.WebForms.ReportDataSource("ProjectCommittee");
                    var targetDataset = new Microsoft.Reporting.WebForms.ReportDataSource("ProjectTargetGroup");
                    var opAddressDataset = new Microsoft.Reporting.WebForms.ReportDataSource("ProjectOperationAddress");

                    List<ServiceModels.Report.ReportProjectRequest.GeneralProjectInfo> genInfoList = new List<ServiceModels.Report.ReportProjectRequest.GeneralProjectInfo>();
                    genInfoList.Add(genInfoResult.Data);
                    genInfoDataset.Value = genInfoList;
                    ReportViewerProjectRequest.LocalReport.DataSources.Add(genInfoDataset);

                    var orgAssResult = _service.GetListOrganizationAssistance(projectID);
                    if (orgAssResult.IsCompleted)
                    {
                        orgAssDataset.Value = orgAssResult.Data;
                        ReportViewerProjectRequest.LocalReport.DataSources.Add(orgAssDataset);
                    }else
	                {
                        errorMessages.Add("ข้อมูลแหล่งความช่วยเหลือที่องค์กรได้รับในปัจจุบัน : " + orgAssResult.Message[0]);        
	                }

                    var attListResult = _service.GetListProjectAttachment(projectID);
                    if (attListResult.IsCompleted)
                    {
                        attDataset.Value = attListResult.Data;
                        ReportViewerProjectRequest.LocalReport.DataSources.Add(attDataset);
                    }
                    else
                    {
                        errorMessages.Add("ข้อมูลเอกสารแนบ : " + attListResult.Message[0]);
                    }

                    var budgetListResult = _service.GetListProjectBudget(projectID);
                    if (budgetListResult.IsCompleted)
                    {
                        budgetDataset.Value = budgetListResult.Data;
                        ReportViewerProjectRequest.LocalReport.DataSources.Add(budgetDataset);
                    }
                    else
                    {
                        errorMessages.Add("ข้อมูลงบประมาณ : " + budgetListResult.Message[0]);
                    }

                    var commListResult = _service.GetListProjectCommitteet(projectID);
                    if (commListResult.IsCompleted)
                    {
                        commDataset.Value = commListResult.Data;
                        ReportViewerProjectRequest.LocalReport.DataSources.Add(commDataset);
                    }
                    else
                    {
                        errorMessages.Add("ข้อมูลรายชื่อคณะกรรมการ : " + commListResult.Message[0]);
                    }

                    var targetListResult = _service.GetListProjectTargetGroup(projectID);
                    if (targetListResult.IsCompleted)
                    {
                        targetDataset.Value = targetListResult.Data;
                        ReportViewerProjectRequest.LocalReport.DataSources.Add(targetDataset);
                    }
                    else
                    {
                        errorMessages.Add("ข้อมูลกลุ่มเป้าหมายโครงการ : " + targetListResult.Message[0]);
                    }

                    var operationAddressListResult = _service.GetListProjectOperationAddres(projectID);
                    if (operationAddressListResult.IsCompleted)
                    {
                        opAddressDataset.Value = operationAddressListResult.Data;
                        ReportViewerProjectRequest.LocalReport.DataSources.Add(opAddressDataset);
                    }
                    else
                    {
                        errorMessages.Add("ข้อมูลสถานที่ดำเนินโครงการ : " + operationAddressListResult.Message[0]);
                    }

                    if(errorMessages.Count == 0){
                        ReportViewerProjectRequest.DataBind();
                        ReportViewerProjectRequest.LocalReport.Refresh();
                        ReportViewerProjectRequest.Visible = true;
                    }
                   
                }
                else
                {
                    errorMessages.Add("ข้อทั่วไป : " + genInfoResult.Message[0]);                   
                }                
            }else{                
                errorMessages.Add(Nep.Project.Resources.Message.NoRecord);
            }

            if (errorMessages.Count > 0)
            {
                ShowErrorMessage(errorMessages);
                ReportViewerProjectRequest.Visible = false;
            }

            //Set DataSet
            //var genInfoDataset = new Microsoft.Reporting.WebForms.ReportDataSource("GeneralProjectInfo");
            //
            //dataset1.Value = GetTemp();
            //ReportViewer3.LocalReport.DataSources.Add(dataset1);

            //ReportViewer3.DataBind();
            //ReportViewer3.LocalReport.Refresh();
            //ReportViewer3.Visible = true;
        }
    }
}