using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Report
{
    public partial class ReportSummaryProjectInfo : Nep.Project.Web.Infra.BasePrintPage
    {
        public IServices.IProjectInfoService _service { get; set; }
        public IServices.IOrganizationParameterService _orgParamService { get; set; }
        public IServices.IListOfValueService _lovService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindReport();
            }
        }

        protected void BindReport()
        {
            String strProjectID = Request.QueryString["id"];
            String provinceabbr = Request.QueryString["provinceabbr"];
            decimal projectID = 0;

            List<String> errorMessages = new List<string>();

            if (Decimal.TryParse(strProjectID, out projectID) && (!String.IsNullOrEmpty(provinceabbr)))
            {
                var orgParamResult = _orgParamService.GetOrganizationParameter();
                if (orgParamResult.IsCompleted)
                {
                    
                    String reportName = (provinceabbr == orgParamResult.Data.CenterProvinceAbbr) ? "ReportSummaryProjectInfoCenter.rdlc" : "ReportSummaryProjectInfoProvince.rdlc";

                    ReportViewerProjectSummaryInfo.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile(reportName));

                    var genInfoResult = _service.GetReportSummaryProjectInfo(projectID);
                    if (genInfoResult.IsCompleted)
                    {
                        ServiceModels.Report.ReportSummaryProjectInfo genInfo = genInfoResult.Data;
                        String disabledCommittee = genInfo.DisabledCommitteePrositionName;
                        bool hasBudgetDetail = false;

                        var genInfoDataset = new Microsoft.Reporting.WebForms.ReportDataSource("ReportSummaryProjectInfo");
                        var budgetDetailDataset = new Microsoft.Reporting.WebForms.ReportDataSource("ReportSummaryProjectInfoBudgetDetail");

                        List<ServiceModels.Report.ReportSummaryProjectInfo> genInfoList = new List<ServiceModels.Report.ReportSummaryProjectInfo>();
                        genInfoList.Add(genInfo);                        
                        genInfoDataset.Value = genInfoList;
                        ReportViewerProjectSummaryInfo.LocalReport.DataSources.Add(genInfoDataset);

                        var budgetResult = _service.GetListReportSummaryProjectInfoBudgetDetail(projectID);
                        if (budgetResult.IsCompleted)
                        {
                            hasBudgetDetail = ((budgetResult.Data != null) && (budgetResult.Data.Count > 0)) ? true : false;
                            budgetDetailDataset.Value = budgetResult.Data;
                            ReportViewerProjectSummaryInfo.LocalReport.DataSources.Add(budgetDetailDataset);
                        }
                        else
                        {
                            errorMessages.Add(budgetResult.Message[0]);
                        }

                        ReportViewerProjectSummaryInfo.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("DisabledCommittee", disabledCommittee));
                        ReportViewerProjectSummaryInfo.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("HasBudgetDetail", hasBudgetDetail.ToString()));
                    }
                    else
                    {
                        errorMessages.Add(genInfoResult.Message[0]);
                    }
                }
                else
                {
                    errorMessages.Add(orgParamResult.Message[0]); 
                }                
            }
            else
            {
                errorMessages.Add(Nep.Project.Resources.Message.NoRecord);
            }

            if (errorMessages.Count == 0)
            {
                ReportViewerProjectSummaryInfo.DataBind();
                ReportViewerProjectSummaryInfo.LocalReport.Refresh();
                ReportViewerProjectSummaryInfo.Visible = true;
            }
            else
            {
                ShowErrorMessage(errorMessages);
                ReportViewerProjectSummaryInfo.Visible = false;
            }
           
        }
    }
}