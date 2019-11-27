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
    public partial class ReportOverlap : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IProviceService _provinceService { get; set; }
        public IServices.IReportOverlapService _service { get; set; }
        public IServices.IProjectInfoService ProjectService { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.VIEW_APPROVING_REPORT, Common.FunctionCode.VIEW_REDUNDANCY_REPORT, Common.FunctionCode.VIEW_TRACKINGING_REPORT };
            Functions = functions;
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            SuppressExportButton(ReportViewerOverlap, "WORDOPENXML");
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
                listProvince.Insert(0, new ServiceModels.GenericDropDownListData { Value = "", Text = Nep.Project.Resources.UI.DropdownAll });
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


        //protected void ComboBoxProvince1_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = ComboBoxProvince.SelectedIndex;
        //        string value = ComboBoxProvince.SelectedValue;
        //        int provinceID = 0;
        //        bool tryParseId = Int32.TryParse(value, out provinceID);

        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "ReportOverlap", ex);
        //        ShowErrorMessage(ex.Message);
        //    }
        //}

        public List<ServiceModels.GenericDropDownListData> ComboBoxBudgetYear_GetData()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = this.ProjectService.ApprovalYear();
            if (result.IsCompleted)
            {
                list = result.Data;
                list.Insert(0, new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.DropdownPleaseSelect, Value = "" });
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return list;
        }

        protected void BindReport()
        {

            ReportViewerOverlap.LocalReport.DataSources.Clear();
            List<ServiceModels.Report.ReportOverlap> list = GetTemp();
            ReportViewerOverlap.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportOverlap.rdlc"));
            //Set DataSet
            var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1");
            
            dataset1.Value = list;

            if (list.Count > 0)
            {
                ReportViewerOverlap.LocalReport.DataSources.Add(dataset1);
                ReportViewerOverlap.DataBind();
                ReportViewerOverlap.Visible = true;
            }
            else
            {
                ReportViewerOverlap.Visible = false;
                ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
            }
        }


        private List<ServiceModels.Report.ReportOverlap> GetTemp()
        {
            //List<ServiceModels.Report.ReportOverlap> list = new List<ServiceModels.Report.ReportOverlap>();

            List<ServiceModels.Report.ReportOverlap> data = new List<ServiceModels.Report.ReportOverlap>();
            //ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOverlap> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOverlap>();
            //var p = Nep.Project.Common.Web.NepHelper.ToQueryParameter(CreateFilter(), 0, Int32.MaxValue, "Name", SortDirection.Ascending);
            //result = this._service.ListReportOverlap(p);
            decimal valueId;
            bool isProvince = UserInfo.IsProvinceOfficer;
            decimal? budgetYear = null;
            decimal? provinceId = null;
            String name = null;
            String idcardno = null;

            //ปีงบประมาณ
            if (DatePickerStartBudgetYear.SelectedDate.HasValue)
            {
                budgetYear = ((DateTime)DatePickerStartBudgetYear.SelectedDate).Year;
            }

            //จังหวัด
            if (DdlProvince.Value != "")
            {
                if (Decimal.TryParse(DdlProvince.Value, out valueId))
                {
                    provinceId = valueId;
                }
            }
            else
            {
                if (isProvince)
                {
                    provinceId = UserInfo.ProvinceID;
                }
            }

            //ชื่อ นามสกุล
            if (!String.IsNullOrEmpty(TextBoxName.Text))
            {
                name = TextBoxName.Text.Trim();
            }

            //เลขที่บัตรประชากร
            if (!String.IsNullOrEmpty(TextBoxIdCardNo.Text))
            {
                idcardno = TextBoxIdCardNo.Text.Trim();
            }
            var result = this._service.ListReportOverlap(budgetYear, provinceId, name, idcardno);
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

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BindReport();
            }
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