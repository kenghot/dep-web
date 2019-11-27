using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Nep.Project.Resources;
using Nep.Project.ServiceModels;
using System.Web.ModelBinding;
using System.Security;
using System.Security.Permissions;
using System.Reflection;
using System.Security.Policy;

namespace Nep.Project.Web.Report
{
    public partial class ReportSummaryTracing : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IListOfValueService LovService { get; set; }
        public IServices.IProviceService _provinceService { get; set; }
        public IServices.IReportSummaryTrackingService ReportSummaryTrackingService { get; set; }

        protected Random rand = new Random();

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
                BindReport();
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
            List<string> errors = new List<string>();

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

            #region Bind data to สถานะ
            List<ServiceModels.ListOfValue> followupStatusList = new List<ServiceModels.ListOfValue>();
            var followupStatusListResult = this.LovService.ListAll(Common.LOVGroup.FollowupStatus);
            if (followupStatusListResult.IsCompleted)
            {
                followupStatusList = followupStatusListResult.Data;
                followupStatusList.Insert(0, new ServiceModels.ListOfValue() { LovID = -1, LovName = UI.DropdownAll });
                followupStatusList.Insert(1, new ServiceModels.ListOfValue() { LovID = 0, LovName = "ยังไม่ติดตาม" });
                DropDownListStatus.DataSource = followupStatusList;
                DropDownListStatus.DataBind();
            }
            else
            {
                errors.Add(followupStatusListResult.Message[0]);
            }
            #endregion Bind data to สถานะ
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            BindReport();
        }

        private void BindReport()
        {
            ReportViewerSummaryTracing.LocalReport.DataSources.Clear();
            List<ServiceModels.Report.ReportSummaryTracking> list = GetTemp();

            ReportViewerSummaryTracing.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportSummaryTracing.rdlc"));
            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.AllFlags));
            ReportViewerSummaryTracing.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
            Assembly asmSystemDrawing = Assembly.Load("Nep.Project.Common.Report, Version=1.0.0.0, Culture=neutral, PublicKeyToken=af066dcbb193094a");
            AssemblyName asmNameSystemDrawing = asmSystemDrawing.GetName();
            ReportViewerSummaryTracing.LocalReport.AddFullTrustModuleInSandboxAppDomain(new StrongName(new StrongNamePublicKeyBlob(asmNameSystemDrawing.GetPublicKeyToken()), asmNameSystemDrawing.Name, asmNameSystemDrawing.Version));

            var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
            dataset1.Value = list;

            if (list.Count > 0)
            {
                ReportViewerSummaryTracing.LocalReport.DataSources.Add(dataset1);
                ReportViewerSummaryTracing.DataBind();
                ReportViewerSummaryTracing.Visible = true;
            }
            else
            {
                ReportViewerSummaryTracing.Visible = false;
                ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
            }
        }

        private List<ServiceModels.IFilterDescriptor> CreateFilter()
        {
            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
            decimal statusId;

            //จังหวัด
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


            //เดิอนปี
            if (DatePickerBudgetYear.SelectedDate.HasValue)
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "YearTracing",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = ((DateTime)DatePickerBudgetYear.SelectedDate).Year
                });
            }
            if (DatePickerBudgetYear.SelectedDate.HasValue)
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "MonthTracing",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = ((DateTime)DatePickerBudgetYear.SelectedDate).Month
                });
            }

            //ชื่อโครงการ
            if (!String.IsNullOrEmpty(TextBoxProjectName.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectName",
                    Operator = ServiceModels.FilterOperator.Contains,
                    Value = TextBoxProjectName.Text.Trim()
                });
            }

            //ชื่อองค์กร
            if (!String.IsNullOrEmpty(TextBoxOrganizationName.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "OrganizationName",
                    Operator = ServiceModels.FilterOperator.Contains,
                    Value = TextBoxOrganizationName.Text.Trim()
                });
            }

            //สถานะ
            if (DropDownListStatus.SelectedIndex >= 0)
            {
                if (DropDownListStatus.SelectedValue != "-1")
                {
                    Decimal.TryParse(DropDownListStatus.SelectedItem.Value, out statusId);
                    fields.Add(new ServiceModels.FilterDescriptor()
                    {
                        Field = "StatusID",
                        Operator = ServiceModels.FilterOperator.IsEqualTo,
                        Value = statusId
                    });
                }
            }



            // Create CompositeFilterDescriptor

            List<ServiceModels.IFilterDescriptor> filters = null;
            if(fields.Count > 0){
                filters = new List<ServiceModels.IFilterDescriptor>();
                filters.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = fields
                });
            }
                

            return filters;
        }



        private List<ServiceModels.Report.ReportSummaryTracking> GetTemp()
        {
            List<ServiceModels.Report.ReportSummaryTracking> data = new List<ServiceModels.Report.ReportSummaryTracking>();
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportSummaryTracking> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportSummaryTracking>();
            var p = Nep.Project.Common.Web.NepHelper.ToQueryParameter(CreateFilter(), 0, Int32.MaxValue, "BudgetYear", SortDirection.Ascending);
            result = this.ReportSummaryTrackingService.ListReportSummaryTracking(p);
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