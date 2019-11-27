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
    public partial class ReportStatisticSupport : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IReportStatisticSupportService _service { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            BindReport();
        }

        private void BindReport()
        {
            ReportViewerStatisticSupport.LocalReport.DataSources.Clear();

            string startYear = ((DateTime)DatePickerStartBudgetYear.SelectedDate).ToString("yyyy", Common.Constants.UI_CULTUREINFO);
            string endYear = ((DateTime)DatePickerEndBudgetYear.SelectedDate).ToString("yyyy", Common.Constants.UI_CULTUREINFO);

            int istartYear = Convert.ToInt32(startYear) - 543;
            int iendYear = Convert.ToInt32(endYear) - 543;

            ReportViewerStatisticSupport.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportStatisticSupport.rdlc"));

            ReportViewerStatisticSupport.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("StringDateToday", Utility.ToBuddhaDateFormat(DateTime.Today, "dd MMMM yyyy")));
            //Set DataSet
            var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
            var dataset2 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2");
            var dataset3 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet3");
            var dataset4 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet4");
            List<ServiceModels.Report.ReportStatisticSupport.CompareSupport> _s1 = _service.GetCompareSupports(istartYear, iendYear);
            List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByType> _s2 = _service.GetAnalyzeProjectByTypes(istartYear, iendYear);
            List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByStrategic> _s3 = _service.GetAnalyzeProjectByStrategics(istartYear, iendYear);
            List<ServiceModels.Report.ReportStatisticSupport.AnalyzeProjectByProjectType> _s4 = _service.GetAnalyzeProjectByProjectTypes(istartYear, iendYear);

            if (_s1.Count == 0 && _s2.Count == 0 && _s3.Count == 0 && _s4.Count == 0)
            {
                ReportViewerStatisticSupport.Visible = false;
                ShowResultMessage(Resources.Message.NoRecord);
            }
            else
            {
                dataset1.Value = _s1;
                dataset2.Value = _s2;
                dataset3.Value = _s3;
                dataset4.Value = _s4;

                ReportViewerStatisticSupport.LocalReport.DataSources.Add(dataset1);
                ReportViewerStatisticSupport.LocalReport.DataSources.Add(dataset2);
                ReportViewerStatisticSupport.LocalReport.DataSources.Add(dataset3);
                ReportViewerStatisticSupport.LocalReport.DataSources.Add(dataset4);

                ReportViewerStatisticSupport.DataBind();
                ReportViewerStatisticSupport.LocalReport.Refresh();
                ReportViewerStatisticSupport.Visible = true;
            }
        }

        protected void CustomValidatorEndBudgetYear_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Int32 startYear = (DatePickerStartBudgetYear.SelectedDate.HasValue) ? ((DateTime)DatePickerStartBudgetYear.SelectedDate).Year : 0;
            Int32 endYear = (DatePickerEndBudgetYear.SelectedDate.HasValue) ? ((DateTime)DatePickerEndBudgetYear.SelectedDate).Year : 0;

            if ((startYear > 0) && (endYear > 0) && (endYear < startYear))
            {
                args.IsValid = false;
            }
        }
    }
}