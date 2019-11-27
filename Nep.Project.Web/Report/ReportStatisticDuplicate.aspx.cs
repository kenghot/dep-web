using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using Microsoft.Reporting.WebForms;

namespace Nep.Project.Web.Report
{
    public partial class ReportStatisticDuplicate : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IProjectInfoService _projectService { get; set; }
        public IServices.IReportStatisticService _peportStatisticService { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.VIEW_APPROVING_REPORT, Common.FunctionCode.VIEW_REDUNDANCY_REPORT, Common.FunctionCode.VIEW_TRACKINGING_REPORT };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            SuppressExportButton(ReportViewerStatisticDuplicate, "WORDOPENXML");           
        }

        private void BindData()
        {
            try
            {
                List<String> errors = new List<string>();
                ServiceModels.QueryParameter pCompareDuplicated = CreateQueryParameter(true);
                ServiceModels.QueryParameter pAnalyzeProject = CreateQueryParameter(false);
                int countResult = 0;

                Decimal? provinceIDFilter = (UserInfo.IsProvinceOfficer) ? UserInfo.ProvinceID : (Decimal?)null;

                //CompareDuplicatedSupport
                var compareDuplicatedSupportResult = _peportStatisticService.ListCompareDuplicatedSupport(pCompareDuplicated);
                if (compareDuplicatedSupportResult.IsCompleted)
                {
                    countResult = compareDuplicatedSupportResult.Data.Count();
                    string startYear = ((DateTime)DatePickerStartBudgetYear.SelectedDate).ToString("yyyy", Common.Constants.UI_CULTUREINFO);
                    string endYear = ((DateTime)DatePickerEndBudgetYear.SelectedDate).ToString("yyyy", Common.Constants.UI_CULTUREINFO); ;


                    ReportViewerStatisticDuplicate.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportStatisticDuplicate.rdlc"));
                    ReportViewerStatisticDuplicate.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("ParamStartYear", startYear));
                    ReportViewerStatisticDuplicate.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("ParamEndYear", endYear));

                    var compareDupDataset = new Microsoft.Reporting.WebForms.ReportDataSource("CompareDuplicatedSupport");
                    var analyzeByTypeDataset = new Microsoft.Reporting.WebForms.ReportDataSource("AnalyzeProjectByType");
                    var analyzeByStrategicDataset = new Microsoft.Reporting.WebForms.ReportDataSource("AnalyzeProjectByStrategic");

                    compareDupDataset.Value = compareDuplicatedSupportResult.Data;
                    ReportViewerStatisticDuplicate.LocalReport.DataSources.Add(compareDupDataset);

                    //AnalyzeProjectByType

                    var analyzeProjectByTypeResult = _peportStatisticService.ListAnalyzeProjectByType(pAnalyzeProject, provinceIDFilter);
                    if (analyzeProjectByTypeResult.IsCompleted)
                    {
                        analyzeByTypeDataset.Value = analyzeProjectByTypeResult.Data;
                        ReportViewerStatisticDuplicate.LocalReport.DataSources.Add(analyzeByTypeDataset);
                    }
                    else
                    {
                        errors.Add(compareDuplicatedSupportResult.Message[0]);
                    }

                    //AnalyzeProjectByStrategic
                    var analyzeProjectByStrategicResult = _peportStatisticService.ListAnalyzeProjectByStrategic(pAnalyzeProject, provinceIDFilter);
                    if (analyzeProjectByStrategicResult.IsCompleted)
                    {
                        analyzeByStrategicDataset.Value = analyzeProjectByStrategicResult.Data;
                        ReportViewerStatisticDuplicate.LocalReport.DataSources.Add(analyzeByStrategicDataset);
                    }
                    else
                    {
                        errors.Add(compareDuplicatedSupportResult.Message[0]);
                    }

                }
                else
                {
                    errors.Add(compareDuplicatedSupportResult.Message[0]);
                }

                if (errors.Count == 0)
                {
                    if (countResult > 0)
                    {
                        ReportViewerStatisticDuplicate.DataBind();
                        ReportViewerStatisticDuplicate.LocalReport.Refresh();
                        ReportViewerStatisticDuplicate.Visible = true;
                    }
                    else
                    {
                        ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
                        ReportViewerStatisticDuplicate.Visible = false;
                    }                    
                }
                else
                {
                    ShowErrorMessage(errors);
                    ReportViewerStatisticDuplicate.Visible = false;
                }

                
            }
            catch (Exception ex)
            {
                ReportViewerStatisticDuplicate.Visible = false;
                ShowErrorMessage(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.ServiceError, "Project Statistic Duplicate", ex);
            }           

        }

        private ServiceModels.QueryParameter CreateQueryParameter(bool isFilterProvince)
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


            if (isFilterProvince && UserInfo.IsProvinceOfficer)
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectProvinceID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = UserInfo.ProvinceID
                });
            }

            //fields.Add(new ServiceModels.FilterDescriptor()
            //{
            //    Field = "CountIDCardNo",
            //    Operator = ServiceModels.FilterOperator.IsGreaterThan,
            //    Value = 1
            //});
         

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
          

            Int32 startYear = (DatePickerStartBudgetYear.SelectedDate.HasValue)? ((DateTime)DatePickerStartBudgetYear.SelectedDate).Year : 0;
            Int32 endYear = (DatePickerEndBudgetYear.SelectedDate.HasValue) ? ((DateTime)DatePickerEndBudgetYear.SelectedDate).Year : 0;           

            if((startYear > 0) && (endYear > 0) && (endYear < startYear)){
                args.IsValid = false;
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if(Page.IsValid){
                BindData();
            }
        }
    }
}