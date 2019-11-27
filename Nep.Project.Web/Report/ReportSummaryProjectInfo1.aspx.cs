using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Report
{
    public partial class ReportSummaryProjectInfo1 : Nep.Project.Web.Infra.BasePrintPage
    {
        public IServices.IProjectInfoService _service { get; set; }
        public IServices.IOrganizationParameterService _orgParamService { get; set; }
        public IServices.IListOfValueService _lovService { get; set; }
        public IServices.IProviceService _provinceService { get; set; }

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

                    String reportName = "ReportSummaryProjectInfo.rdlc";

                    ReportViewerProjectSummaryInfo1.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile(reportName));

                    var genInfoResult = _service.GetReportSummaryProjectInfo1(projectID);
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
                        ReportViewerProjectSummaryInfo1.LocalReport.DataSources.Add(genInfoDataset);

                        var budgetResult = _service.GetListReportSummaryProjectInfoBudgetDetail(projectID);
                        decimal requestValue = 0;
                        if (budgetResult.IsCompleted)
                        {
                            hasBudgetDetail = ((budgetResult.Data != null) && (budgetResult.Data.Count > 0)) ? true : false;
                             if (genInfo.ProjectProvinceID != _provinceService.GetCenterProvinceID().Data) {
                                 foreach (ServiceModels.Report.ReportSummaryProjectInfoBudgetDetail budget in budgetResult.Data)
                                 {
                                     budget.ReviseValue2 = budget.ReviseValue1;
                                 }
                             }
                            budgetDetailDataset.Value = budgetResult.Data;
                            foreach (var item in budgetResult.Data)
                            {
                               requestValue +=  item.RequestValue.Value;
                            } 
                            ReportViewerProjectSummaryInfo1.LocalReport.DataSources.Add(budgetDetailDataset);
                        }
                        else
                        {
                            errorMessages.Add(budgetResult.Message[0]);
                        }
                        ReportViewerProjectSummaryInfo1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("RequestValue", String.Format("{0:##,#0.#0}", requestValue )));
                        ReportViewerProjectSummaryInfo1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("DisabledCommittee", disabledCommittee));
                        ReportViewerProjectSummaryInfo1.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("HasBudgetDetail", hasBudgetDetail.ToString()));
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
                ReportViewerProjectSummaryInfo1.DataBind();
                ReportViewerProjectSummaryInfo1.LocalReport.Refresh();
                ReportViewerProjectSummaryInfo1.Visible = true;
            }
            else
            {
                ShowErrorMessage(errorMessages);
                ReportViewerProjectSummaryInfo1.Visible = false;
            }

        }
    }
}