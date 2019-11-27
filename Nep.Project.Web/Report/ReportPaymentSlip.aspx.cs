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
    public partial class ReportPaymentSlip : Nep.Project.Web.Infra.BasePrintPage
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
            ReportViewerPaymentSlip.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportPaymentSlip.rdlc"));
            
            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.AllFlags));
            ReportViewerPaymentSlip.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
            Assembly asmSystemDrawing = Assembly.Load("Nep.Project.Common.Report, Version=1.0.0.0, Culture=neutral, PublicKeyToken=af066dcbb193094a");
            AssemblyName asmNameSystemDrawing = asmSystemDrawing.GetName();
            ReportViewerPaymentSlip.LocalReport.AddFullTrustModuleInSandboxAppDomain(new StrongName(new StrongNamePublicKeyBlob(asmNameSystemDrawing.GetPublicKeyToken()), asmNameSystemDrawing.Name, asmNameSystemDrawing.Version));

            String strProjectID = Request.QueryString["projectID"];
            decimal projectID = 0;
            List<String> errorMessages = new List<string>();

            if (Decimal.TryParse(strProjectID, out projectID))
            {     
                var genPayResult = _service.GetPaymentSlip(projectID);
                if (genPayResult.IsCompleted)
                {
  

                    var payDataset = new Microsoft.Reporting.WebForms.ReportDataSource("PaymentSlipDataset");
               

                    List<ServiceModels.Report.ReportPaymentSlip> payList = new List<ServiceModels.Report.ReportPaymentSlip>();
                    payList.Add(genPayResult.Data);
                    payDataset.Value = payList;
                    ReportViewerPaymentSlip.LocalReport.DataSources.Add(payDataset);

              


                    if(errorMessages.Count == 0){
                        ReportViewerPaymentSlip.DataBind();
                        ReportViewerPaymentSlip.LocalReport.Refresh();
                        ReportViewerPaymentSlip.Visible = true;
                    }
                   
                }
                else
                {
                    errorMessages.Add("ข้อทั่วไป : " + genPayResult.Message[0]);                   
                }                
            }else{                
                errorMessages.Add(Nep.Project.Resources.Message.NoRecord);
            }

            if (errorMessages.Count > 0)
            {
                ShowErrorMessage(errorMessages);
                ReportViewerPaymentSlip.Visible = false;
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