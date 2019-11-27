using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Microsoft.Reporting.WebForms;
using Nep.Project.ServiceModels;
using Nep.Project.Common.Report;

namespace Nep.Project.Web.Report
{
    public partial class ReportStatisticClient : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IReportStatisticClientService _service { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.VIEW_APPROVING_REPORT, Common.FunctionCode.VIEW_REDUNDANCY_REPORT, Common.FunctionCode.VIEW_TRACKINGING_REPORT };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            BindReport();
        }

        private void BindReport()
        {
            ReportViewerStatisticClient.LocalReport.DataSources.Clear();

            try
            {
                int startYear = ((DateTime)DatePickerStartYear.SelectedDate).Year;
                int endYear = ((DateTime)DatePickerEndYear.SelectedDate).Year;

                ReportViewerStatisticClient.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportStatisticClient.rdlc"));

                ReportViewerStatisticClient.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("StringDateToday", Utility.ToBuddhaDateFormat(DateTime.Today, "dd MMMM yyyy")));
                //Set DataSet
                var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
                var dataset2 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2");
                var dataset3 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet3");
                var dataset4 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet4");

                var resultClientSupport = _service.GetCompareClientSupports(startYear, endYear);
                if (resultClientSupport.IsCompleted)
                    dataset1.Value = resultClientSupport.Data;

                var resultByTypes = _service.GetAnalyzeProjectByTypes(startYear, endYear);
                if (resultByTypes.IsCompleted)
                    dataset2.Value = resultByTypes.Data;

                var resultStrategics = _service.GetAnalyzeProjectByStrategics(startYear, endYear);
                if (resultStrategics.IsCompleted)
                    dataset3.Value = resultStrategics.Data;

                var resultTarget = _service.GetAnalyzeProjectByTargetGroups(startYear, endYear);
                if (resultTarget.IsCompleted)
                    dataset4.Value = resultTarget.Data;

                if (resultClientSupport.TotalRow > 0 || resultByTypes.TotalRow > 0 || resultStrategics.TotalRow > 0 || resultTarget.TotalRow > 0)
                {
                    if(dataset1.Value != null)
                        ReportViewerStatisticClient.LocalReport.DataSources.Add(dataset1);

                    if(dataset2.Value != null)
                        ReportViewerStatisticClient.LocalReport.DataSources.Add(dataset2);

                    if(dataset3.Value != null)
                        ReportViewerStatisticClient.LocalReport.DataSources.Add(dataset3);

                    if(dataset4.Value != null)
                        ReportViewerStatisticClient.LocalReport.DataSources.Add(dataset4);

                    ReportViewerStatisticClient.DataBind();
                    ReportViewerStatisticClient.LocalReport.Refresh();
                    ReportViewerStatisticClient.Visible = true;
                }
                else
                {
                    ShowResultMessage(Resources.Message.NoRecord);
                    ReportViewerStatisticClient.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
                ReportViewerStatisticClient.Visible = false;
                Common.Logging.LogError(Common.Logging.ErrorType.WebError, "ReportStatisticClient", ex);
            }
        }
    }
}