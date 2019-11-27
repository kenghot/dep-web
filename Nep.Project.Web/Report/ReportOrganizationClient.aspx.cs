using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Nep.Project.ServiceModels;
using Nep.Project.Common;

namespace Nep.Project.Web.Report
{
    public partial class ReportOrganizationClient : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IProviceService _provinceService { get; set; }
        public IServices.IReportOrganizationClientService _service { get; set; }
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
                });";

            ScriptManager.RegisterStartupScript(
                     this.Page,
                     this.GetType(),
                     "ManageComboboxScript",
                     script,
                     true);
        }

 
        protected void BindDropDownList()
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

        protected void BindReport()
        {

            ReportViewerOrganizationClient.LocalReport.DataSources.Clear();
            List<ServiceModels.Report.ReportOrganizationClient> list = GetTemp();
            ReportViewerOrganizationClient.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportOrganizationClient.rdlc"));
            //Set DataSet
            var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("ReportOrganizationClient");

            dataset1.Value = list;

            if (list != null) 
            {
                if (list.Count > 0)
                {
                    ReportViewerOrganizationClient.LocalReport.DataSources.Add(dataset1);
                    ReportViewerOrganizationClient.DataBind();
                    ReportViewerOrganizationClient.Visible = true;
                }
                else
                {
                    ReportViewerOrganizationClient.Visible = false;
                    ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
                }
             }
        }

        private List<ServiceModels.Report.ReportOrganizationClient> GetTemp()
        {
            List<ServiceModels.Report.ReportOrganizationClient> list = new List<ServiceModels.Report.ReportOrganizationClient>();


            List<ServiceModels.Report.ReportOrganizationClient> data = new List<ServiceModels.Report.ReportOrganizationClient>();
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOrganizationClient> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOrganizationClient>();
            var p = Nep.Project.Common.Web.NepHelper.ToQueryParameter(CreateFilter(), 0, Int32.MaxValue, "ProjectName", SortDirection.Ascending);
            result = this._service.ListReportOrganizationClient(p);
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

        private List<ServiceModels.IFilterDescriptor> CreateFilter()
        {
            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
            decimal valueId;
            bool isProvince = UserInfo.IsProvinceOfficer;

            // ปีงบประมาณ
            if (DatePickerBudgetYear.SelectedDate.HasValue)
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "BudgetYear",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = (Decimal)((DateTime)DatePickerBudgetYear.SelectedDate).Year
                });
            }

            //จังหวัด
            if (DdlProvince.Value != "")
            {
                Decimal.TryParse(DdlProvince.Value, out valueId);
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProvinceID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = valueId
                });
            }
            else
            {
                if (isProvince)
                {
                    fields.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "ProvinceID",
                        Operator = ServiceModels.FilterOperator.IsEqualTo,
                        Value = UserInfo.ProvinceID
                    });
                }
            }


            // Create CompositeFilterDescriptor

            List<ServiceModels.IFilterDescriptor> filters = null;
            if (fields.Count > 0)
            {
                filters = new List<ServiceModels.IFilterDescriptor>();
                filters.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = fields
                });
            }


            return filters;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            BindReport();
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

    }
}