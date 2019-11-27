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
    public partial class ReportProjectResult : Nep.Project.Web.Infra.BasePrintPage
    {
        public IServices.IProjectInfoService _service { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindReport();
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            SuppressExportButton(ReportViewerProjectResult, "WORDOPENXML");
            SuppressExportButton(ReportViewerProjectResult, "EXCELOPENXML");
        }

        protected void BindReport()
        {
            ReportViewerProjectResult.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportProjectResult.rdlc"));
            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.AllFlags));
            ReportViewerProjectResult.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
            Assembly asmSystemDrawing = Assembly.Load("Nep.Project.Common.Report, Version=1.0.0.0, Culture=neutral, PublicKeyToken=af066dcbb193094a");
            AssemblyName asmNameSystemDrawing = asmSystemDrawing.GetName();
            ReportViewerProjectResult.LocalReport.AddFullTrustModuleInSandboxAppDomain(new StrongName(new StrongNamePublicKeyBlob(asmNameSystemDrawing.GetPublicKeyToken()), asmNameSystemDrawing.Name, asmNameSystemDrawing.Version));

            String strProjectID = Request.QueryString["projectID"];
            decimal projectID = 0;
            List<String> errorMessages = new List<string>();

            if (Decimal.TryParse(strProjectID, out projectID))
            {
                var genInfoResult = _service.GetReportProjectResult(projectID);
                if (genInfoResult.IsCompleted)
                {
                    ServiceModels.Report.ReportProjectResult.GeneralProjectInfo genInfo = genInfoResult.Data;
                    List<ServiceModels.Report.ReportProjectResult.GeneralProjectInfo> genInfoList = new List<ServiceModels.Report.ReportProjectResult.GeneralProjectInfo>();
                    genInfoList.Add(genInfo);

                   ReportViewerProjectResult.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("ParamBudgetYear", genInfo.BudgetYearThai.ToString()));
                   ReportViewerProjectResult.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("ParamOperationLevelDescCode", Common.LOVCode.Operationlevel.สูงกว่าเป้าหมาย_เพราะ));

                    var genInfoDataset = new Microsoft.Reporting.WebForms.ReportDataSource("ReportProjectResultGeneralProjectInfo");
                    var pTypeDataset = new Microsoft.Reporting.WebForms.ReportDataSource("ReportProjectResultProjectType");                    
                    var operationResultDataset = new Microsoft.Reporting.WebForms.ReportDataSource("ReportProjectResultSummaryProjectResultOperationResult");
                    var operationLevelDataset = new Microsoft.Reporting.WebForms.ReportDataSource("ReportProjectResultSummaryProjectResultOperationLevel");

                    genInfoDataset.Value = genInfoList;
                    ReportViewerProjectResult.LocalReport.DataSources.Add(genInfoDataset);

                    // ProjectbType
                    var projectTypeResult = _service.GetListReportProjectResultProjectType(projectID);
                    if (projectTypeResult.IsCompleted)
                    {
                        pTypeDataset.Value = projectTypeResult.Data;
                        ReportViewerProjectResult.LocalReport.DataSources.Add(pTypeDataset);
                    }
                    else
                    {
                        errorMessages.Add("ข้อมูลประเภทโปรเจค : " + projectTypeResult.Message[0]); 
                    }

                    //Operation Result
                    var operationResult = _service.GetListReportProjectResultSummaryProjectResult(projectID, Common.LOVGroup.OperationResult);
                    if (operationResult.IsCompleted)
                    {
                        operationResultDataset.Value = operationResult.Data;
                        ReportViewerProjectResult.LocalReport.DataSources.Add(operationResultDataset);
                    }
                    else
                    {
                        errorMessages.Add("ข้อมูลเปรียบเทียบกับวัตถุประสงค์ : " + projectTypeResult.Message[0]); 
                    }

                    //Operation Level
                    var operationLevelResult = _service.GetListReportProjectResultSummaryProjectResult(projectID, Common.LOVGroup.OperationLevel);
                    if (operationLevelResult.IsCompleted)
                    {
                        operationLevelDataset.Value = operationLevelResult.Data;
                        ReportViewerProjectResult.LocalReport.DataSources.Add(operationLevelDataset);
                    }
                    else
                    {
                        errorMessages.Add("ข้อมูลเปรียบเทียบกับเป้าหมาย: " + operationLevelResult.Message[0]);
                    }
                }
                else
                {
                    errorMessages.Add("ข้อทั่วไป : " + genInfoResult.Message[0]);       
                }
            }
            else
            {
                errorMessages.Add(Nep.Project.Resources.Message.NoRecord);
            }

            if (errorMessages.Count > 0)
            {
                ShowErrorMessage(errorMessages);
                ReportViewerProjectResult.Visible = false;
            }
        }
    }
}