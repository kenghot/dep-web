using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace Nep.Project.Web.Report
{
    public partial class ReportBudgetSummary : Nep.Project.Web.Infra.BasePage
    {

        public IServices.IReportBudgetSummaryService _service { get; set; }        

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            SuppressExportButton(ReportViewerBudgetSummary, "WORDOPENXML");
        }
       
        protected void BindReport()
        {
            ReportViewerBudgetSummary.LocalReport.DataSources.Clear();
            ReportViewerBudgetSummary.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportBudgetSummary.rdlc"));
            var dataset = new Microsoft.Reporting.WebForms.ReportDataSource("ReprtBudgetSummary");

            decimal? provinceID = (UserInfo.IsProvinceOfficer) ? UserInfo.ProvinceID : (decimal?)null;             

            ServiceModels.QueryParameter parameterQuery = CreateQueryParameter();
            var result = _service.ListReportBudgetSummary(parameterQuery, provinceID);
            if (result.IsCompleted)
            {
                string startYear = ((DateTime)DatePickerStartBudgetYear.SelectedDate).ToString("yyyy", Common.Constants.UI_CULTUREINFO);
                string endYear = ((DateTime)DatePickerEndBudgetYear.SelectedDate).ToString("yyyy", Common.Constants.UI_CULTUREINFO);

                ReportViewerBudgetSummary.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("ParamStartYear", startYear));
                ReportViewerBudgetSummary.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("ParamEndYear", endYear));

                dataset.Value = result.Data;
                ReportViewerBudgetSummary.LocalReport.DataSources.Add(dataset);
                ReportViewerBudgetSummary.DataBind();
                ReportViewerBudgetSummary.Visible = true;
            }
            else
            {
                ShowErrorMessage(result.Message);
                ReportViewerBudgetSummary.Visible = true;
            }           
            
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            BindReport();
        }

        private List<ServiceModels.Report.ReportBudgetSummary> GetTemp()
        {
            List<ServiceModels.Report.ReportBudgetSummary> list = new List<ServiceModels.Report.ReportBudgetSummary>();
            
            for (int i = 0; i < 11; i++)
            {
                 ServiceModels.Report.ReportBudgetSummary data = new ServiceModels.Report.ReportBudgetSummary();
              

                list.Add(data);
            }

            return list;
        }

        private ServiceModels.QueryParameter CreateQueryParameter()
        {
            ServiceModels.QueryParameter p = new ServiceModels.QueryParameter();
            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
            Decimal year = 0;

            if ((DatePickerStartBudgetYear.SelectedDate.HasValue) && (DatePickerEndBudgetYear.SelectedDate.HasValue))
            {
                year = ((DateTime)DatePickerStartBudgetYear.SelectedDate).Year;
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "BudgetYear",
                    Operator = ServiceModels.FilterOperator.IsGreaterThanOrEqualTo,
                    Value = year
                });

                year = ((DateTime)DatePickerEndBudgetYear.SelectedDate).Year;
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "BudgetYear",
                    Operator = ServiceModels.FilterOperator.IsLessThanOrEqualTo,
                    Value = year
                });
            }


            

            // Create CompositeFilterDescriptor
            List<ServiceModels.IFilterDescriptor> filterComposite = new List<ServiceModels.IFilterDescriptor>();

            filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
            {
                LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                FilterDescriptors = fields
            });


            p = Nep.Project.Common.Web.NepHelper.ToQueryParameter(filterComposite, 0, Int32.MaxValue, "BudgetYear", SortDirection.Ascending);

            return p;
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