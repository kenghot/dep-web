using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace Nep.Project.Web.Report
{
    public partial class ReportEvaluationSummary : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IReportEvaluationService ReportEvaluationService { get; set; }
        protected Random rnd = new Random();
        public IServices.IProjectInfoService ProjectService { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] {Common.FunctionCode.VIEW_APPROVING_REPORT, Common.FunctionCode.VIEW_REDUNDANCY_REPORT, Common.FunctionCode.VIEW_TRACKINGING_REPORT };
            Functions = functions;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BindReport();
            }

        }

        protected void BindReport()
        {
            ReportViewerEvaluationSummary.LocalReport.DataSources.Clear();
            
            List<ServiceModels.Report.ReportEvaluationSummary> list = GetTemp();
            ReportViewerEvaluationSummary.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportEvaluationSummary.rdlc"));
            
            //Set DataSet
            var reportEvaluationDataSet = new Microsoft.Reporting.WebForms.ReportDataSource("ReportEvaluationDataSet");
            reportEvaluationDataSet.Value = list;

            if (list != null) 
            {
                if (list.Count > 0)
                {
                    ReportViewerEvaluationSummary.LocalReport.DataSources.Add(reportEvaluationDataSet);
                    ReportViewerEvaluationSummary.DataBind();
                    ReportViewerEvaluationSummary.Visible = true;
                }
                else
                {
                    ReportViewerEvaluationSummary.Visible = false;
                    ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
                }
            }
        } 



        private List<ServiceModels.IFilterDescriptor> CreateFilter()
        {
            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
          
            //เลขทะเบียนโครงการ
            if (!String.IsNullOrEmpty(TextBoxProjectNo.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectNo",
                    Operator = ServiceModels.FilterOperator.Contains,
                    Value = TextBoxProjectNo.Text.Trim()
                });
            }

            //ปีงบประมาณ
            if (DatePickerStartBudgetYear.SelectedDate.HasValue)
            {
                decimal BudgetYear = ((DateTime)DatePickerStartBudgetYear.SelectedDate).Year;  
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "BudgetYear",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = BudgetYear
                });
            }

            //ชื่อองค์กร
            if (!String.IsNullOrEmpty(TextBoxContractOrgName.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "Organization",
                    Operator = ServiceModels.FilterOperator.Contains,
                    Value = TextBoxContractOrgName.Text.Trim()
                });
            }

            //ชื่อหน่วยงาน
            if (!String.IsNullOrEmpty(TextBoxProjectName.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectName",
                    Operator = ServiceModels.FilterOperator.Contains,
                    Value = TextBoxProjectName.Text.Trim()
                });
            }

            // จังหวัด
            Decimal? provinceIDFilter = (UserInfo.IsProvinceOfficer) ? UserInfo.ProvinceID : (Decimal?)null;
            if (provinceIDFilter.HasValue)
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProvinceID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = provinceIDFilter
                });
            }

            // Create CompositeFilterDescriptor

            List<ServiceModels.IFilterDescriptor> filters = new List<ServiceModels.IFilterDescriptor>();
            filters.Add(new ServiceModels.CompositeFilterDescriptor()
            {
                LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                FilterDescriptors = fields
            });

            return filters;
        }

        private List<ServiceModels.Report.ReportEvaluationSummary> GetTemp()
        {
            List<ServiceModels.Report.ReportEvaluationSummary> data = new List<ServiceModels.Report.ReportEvaluationSummary>();
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportEvaluationSummary> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportEvaluationSummary>();
            var p = Nep.Project.Common.Web.NepHelper.ToQueryParameter(CreateFilter(), 0, Int32.MaxValue, "Organization", SortDirection.Ascending);
            result = this.ReportEvaluationService.ListReportEvaluationSummary(p);
            if (result.IsCompleted)
            {
                data = result.Data;
            }
            else
            {
                data = null;
                ShowErrorMessage(result.Message[0]);
            }

            return data;
        }


    }
}