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
    public partial class ReportBudgetType : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IReportsService _service { get; set; }
        public IServices.IProviceService _provinceService { get; set; }
        public IServices.IProjectInfoService _projectInfoService { get; set; }
        public IServices.IListOfValueService _lov { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.VIEW_APPROVING_REPORT, Common.FunctionCode.VIEW_REDUNDANCY_REPORT, Common.FunctionCode.VIEW_TRACKINGING_REPORT };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownList();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string provinceSelected = (!String.IsNullOrEmpty(DdlProvince.Value)) ? DdlProvince.Value : "null";
            string budgetTypeSelected = (!String.IsNullOrEmpty(DdlBudgetType.Value)) ? DdlBudgetType.Value : "null";
            String script = @"
                $(function () {                 
                    
                    c2x.createLocalCombobox({                       
                        ControlID: '" + DdlProvince.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownAll + @"',                        
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        Data:{Data:" + Nep.Project.Common.Web.WebUtility.ToJSON(GetOrgProvince()) + @", TotalRow:0, IsCompleted:true},                   
                        Value: " + provinceSelected + @",                     
                     });  
                    c2x.createLocalCombobox({                       
                        ControlID: '" + DdlBudgetType.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownAll + @"',                        
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        Data:{Data:" + Nep.Project.Common.Web.WebUtility.ToJSON(GetBudgetType()) + @", TotalRow:0, IsCompleted:true},                   
                        Value: " + budgetTypeSelected + @",                     
                     });  
                });";

            ScriptManager.RegisterStartupScript(
                     this.Page,
                     this.GetType(),
                     "ManageComboboxScript",
                     script,
                     true);
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BindReport();
            }
        }

        private void BindDropDownList()
        {
            // Province
            List<ServiceModels.GenericDropDownListData> listProvince = new List<ServiceModels.GenericDropDownListData>();
            Array eProvince = Enum.GetValues(typeof(ServiceModels.Report.TempEnumProvince));
            ServiceModels.GenericDropDownListData pEmpty = new ServiceModels.GenericDropDownListData();

            string provinceName = UserInfo.ProvinceName;
            bool isCenter = UserInfo.IsCenterOfficer;
            ReturnQueryData<ServiceModels.GenericDropDownListData> resultProvince = null;

            if (isCenter)
            {
                resultProvince = _provinceService.ListOrgProvince(string.Empty);
                listProvince.Insert(0, new ServiceModels.GenericDropDownListData { Value = "-1", Text = Nep.Project.Resources.UI.DropdownAll });
            }
            else
            {
                resultProvince = _provinceService.ListProvince(provinceName);
            }

            if (resultProvince != null && resultProvince.IsCompleted)
            {
                listProvince.AddRange(resultProvince.Data);
            }

            //ComboBoxProvince.DataSource = listProvince;
            //ComboBoxProvince.DataBind();
            //ComboBoxProvince.SelectedIndex = 0;
        }

        private void BindReport()
        {
            try
            {
                ReportViewer4.LocalReport.DataSources.Clear();
                ServiceModels.QueryParameter p = CreateQueryParameter();
                var result = _service.ListApprovedReport(p);
                if (result.IsCompleted)
                {
                    var list = result.Data;
                    var y = ((DateTime)DatePickerBudgetYear.SelectedDate).Year + 543;
                    //result.Data.Year = y.ToString();
                 
  
                    ReportViewer4.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportBudgetType.rdlc"));
                    ReportViewer4.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("BudgetYear", y.ToString()));

                    var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
                    dataset1.Value = list;

                    if (list != null && list.Count > 0)
                    {
                        ReportViewer4.LocalReport.DataSources.Add(dataset1);
                        ReportViewer4.DataBind();
                        ReportViewer4.Visible = true;
                    }
                    else
                    {
                        ReportViewer4.Visible = false;
                        ShowResultMessage(Resources.Message.NoRecord);
                    }
                }
                else
                {
                    //ReportViewer4.Visible = false;
                    ShowErrorMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                //ReportViewer4.Visible = false;
                ShowErrorMessage(ex.Message);
                Common.Logging.LogError(Common.Logging.ErrorType.WebError, "Report4", ex);
            }
        }

        private ServiceModels.QueryParameter CreateQueryParameter()
        {
            ServiceModels.QueryParameter p = new ServiceModels.QueryParameter();
            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
            DateTime? date = null;

            if (DdlProvince.Value != "") 
            {
                decimal Id = decimal.Parse(DdlProvince.Value);
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProvinceId",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = Id
                });
            }
            else
            {
                string provinceName = UserInfo.ProvinceName;
                bool isCenter = UserInfo.IsCenterOfficer;
                if (!isCenter)
                {
                    fields.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProvinceId",
                        Operator = ServiceModels.FilterOperator.IsEqualTo,
                        Value = UserInfo.ProvinceID
                    });
                }
            }
            if (DdlBudgetType.Value != "")
            {
                decimal Id = decimal.Parse(DdlBudgetType.Value);
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "BudgetTypeId",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = Id
                });
            }

            if (DatePickerBudgetYear.SelectedDate.HasValue)
            {
                int approvalYear = ((DateTime)DatePickerBudgetYear.SelectedDate).Year;
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "BudgetYear",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = approvalYear
                });
            }

           
            // Create CompositeFilterDescriptor
            List<ServiceModels.IFilterDescriptor> filterComposite = new List<ServiceModels.IFilterDescriptor>();
            if (fields.Count > 0)
            {
                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = fields
                });
            }

            p = Nep.Project.Common.Web.NepHelper.ToQueryParameter(filterComposite, 0, Int32.MaxValue, string.Empty, SortDirection.Ascending);

            return p;
        }

        private List<ServiceModels.Report.Report4> GetReportValue(List<ServiceModels.Report.Report4> listReport4)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("th-TH");
            List<ServiceModels.Report.Report4> list = listReport4;
            int i = 0;
            string groupApprove = string.Empty;

            list.All(x =>
                    {
                        if ((x.ApproveNo + "/" + x.ApproveYear) != groupApprove)
                        {
                            groupApprove = x.ApproveNo + "/" + x.ApproveYear;
                            i = 0;
                        }

                        x.ApproveNo = x.ApproveNo;
                        x.ApproveYear = (int.Parse(x.ApproveYear) + 543).ToString();
                        i = i + 1;
                        x.No = i.ToString();
                        x.ProjectName = x.ProjectName ?? "-";
                        x.OrganizationName = x.OrganizationName ?? "-";
                        x.Amount = x.Amount;
                        x.ContractDate = x.ContractDate;
                        x.ContractDateStr = x.ContractDate.HasValue ? ((DateTime)x.ContractDate).ToString("d MMM yy", format) : "-";
                        x.BudgetYear = x.BudgetYear + 543;
                        x.StartDate = x.StartDate;
                        x.StartDateStr = x.StartDate.HasValue ? x.StartDate.Value.ToString("d MMM yy", format) : " ";
                        x.EndDate = x.EndDate;
                        x.EndDateStr = x.EndDate.HasValue ? x.EndDate.Value.ToString("d MMM yy", format) : " ";
                        x.Status = x.Status;
                        return true;
                    });

            return list;
        }

        private List<ServiceModels.GenericDropDownListData> GetOrgProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = _provinceService.ListOrgProvince(null);
            if (result.IsCompleted)
            {
                list = result.Data;
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return list;
        }
        private List<ServiceModels.GenericDropDownListData> GetBudgetType()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = _lov.ListBudgetType(UserInfo.ProvinceID.HasValue? UserInfo.ProvinceID.Value: 161, null);
            if (result.IsCompleted)
            {
                list = result.Data.Select(s => new GenericDropDownListData { Text = s.LovName, Value = s.LovID.ToString() }).ToList();
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return list;
        }
    }
}