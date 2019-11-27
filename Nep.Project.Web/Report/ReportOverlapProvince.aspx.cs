using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Nep.Project.ServiceModels;
using Nep.Project.Common;
using Nep.Project.Resources;

namespace Nep.Project.Web.Report
{
    public partial class ReportOverlapProvince : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IProviceService _provinceService { get; set; }
        public IServices.IReportOverlapProvinceService _service { get; set; }
        public IServices.IListOfValueService LovService { get; set; }
        
        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.VIEW_APPROVING_REPORT, Common.FunctionCode.VIEW_REDUNDANCY_REPORT, Common.FunctionCode.VIEW_TRACKINGING_REPORT };
            Functions = functions;
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            SuppressExportButton(ReportViewerOverlapProvince, "WORDOPENXML");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownList();
            }
        }

        protected void ComboBoxProvince_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<ServiceModels.GenericDropDownListData> listProvince = new List<ServiceModels.GenericDropDownListData>();
                ReturnQueryData<ServiceModels.GenericDropDownListData> resultProvince = null;
                if (DropDownListRegion.SelectedIndex >= 0)
                {
                    if (DropDownListRegion.SelectedValue.ToString() != "-1")
                    {
                        resultProvince = _provinceService.ListOrgProvinceSection(Convert.ToInt32(DropDownListRegion.SelectedValue));
                    }
                    else {
                        resultProvince = _provinceService.ListOrgProvinceSection(null);
                        listProvince.Insert(0, new ServiceModels.GenericDropDownListData { Value = "-1", Text = Nep.Project.Resources.UI.DropdownAll });
                    }
                }

                if (resultProvince != null && resultProvince.IsCompleted)
                {
                    listProvince.AddRange(resultProvince.Data);
                }

                ComboBoxProvince.DataSource = listProvince;
                ComboBoxProvince.DataBind();
                ComboBoxProvince.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "ReportOverlapProvince", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        protected void ComboBoxSection_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<ServiceModels.GenericDropDownListData> listSection = new List<ServiceModels.GenericDropDownListData>();
                ReturnQueryData<ServiceModels.GenericDropDownListData> resultSection = null;
                string value = ComboBoxProvince.SelectedValue;
                if (ComboBoxProvince.SelectedIndex >= 0)
                {
                    if (ComboBoxProvince.SelectedValue.ToString() != "-1")
                    {
                        resultSection = _provinceService.ListSection(Convert.ToInt32(ComboBoxProvince.SelectedValue));
                    }
                    else {
                        resultSection = _provinceService.ListSection(null);
                        listSection.Insert(0, new ServiceModels.GenericDropDownListData { Value = "-1", Text = Nep.Project.Resources.UI.DropdownAll });
                    }
                } 

                if (resultSection != null && resultSection.IsCompleted)
                {
                    listSection.AddRange(resultSection.Data);
                }

                DropDownListRegion.DataSource = listSection;
                DropDownListRegion.DataBind();
                DropDownListRegion.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "ReportOverlapProvince", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        protected void BindDropDownList()
        {
            // Region
            string sectionName = UserInfo.SectionName;
            bool isCenter = UserInfo.IsCenterOfficer;

            List<ServiceModels.GenericDropDownListData> listRegion = new List<ServiceModels.GenericDropDownListData>();
            int? provinceId = null;
            if (UserInfo.ProvinceID.HasValue)
            {
                provinceId = (int) UserInfo.ProvinceID.Value;
            }
            ReturnQueryData<ServiceModels.GenericDropDownListData> resultRegion = null;

            if (isCenter)
            {
                resultRegion = _provinceService.ListSection(null);
                listRegion.Insert(0, new ServiceModels.GenericDropDownListData { Value = "-1", Text = Nep.Project.Resources.UI.DropdownAll });
            }
            else
            {
                resultRegion = _provinceService.ListSection(provinceId);
            }

            if (resultRegion != null && resultRegion.IsCompleted)
            {
                listRegion.AddRange(resultRegion.Data);
            }

            DropDownListRegion.DataSource = listRegion;
            DropDownListRegion.DataBind();
            DropDownListRegion.SelectedIndex = 0;

            // Province
            List<ServiceModels.GenericDropDownListData> listProvince = new List<ServiceModels.GenericDropDownListData>();
            Array eProvince = Enum.GetValues(typeof(ServiceModels.Report.TempEnumProvince));
            ServiceModels.GenericDropDownListData pEmpty = new ServiceModels.GenericDropDownListData();

            string provinceName = UserInfo.ProvinceName;
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

            ComboBoxProvince.DataSource = listProvince;
            ComboBoxProvince.DataBind();
            ComboBoxProvince.SelectedIndex = 0;

        }

        protected void BindReport()
        {
            ReportViewerOverlapProvince.LocalReport.DataSources.Clear();
            List<ServiceModels.Report.ReportOverlapProvince> list = GetTemp();
            ReportViewerOverlapProvince.LocalReport.LoadReportDefinition(Common.Web.WebUtility.LoadReportFile("ReportOverlapProvince.rdlc"));
            //Set DataSet
            var dataset1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet");
            dataset1.Value = list;

            if ((list != null) && list.Count > 0)
            {
                ReportViewerOverlapProvince.LocalReport.DataSources.Add(dataset1);
                ReportViewerOverlapProvince.DataBind();
                ReportViewerOverlapProvince.Visible = true;
            }
            else
            {
                ReportViewerOverlapProvince.Visible = false;
                ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
            }
        }

        private List<ServiceModels.Report.ReportOverlapProvince> GetTemp()
        {
            List<ServiceModels.Report.ReportOverlapProvince> list = new List<ServiceModels.Report.ReportOverlapProvince>();

            List<ServiceModels.Report.ReportOverlapProvince> data = new List<ServiceModels.Report.ReportOverlapProvince>();
            ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOverlapProvince> result = new ServiceModels.ReturnQueryData<ServiceModels.Report.ReportOverlapProvince>();
            var p = Nep.Project.Common.Web.NepHelper.ToQueryParameter(CreateFilter(), 0, Int32.MaxValue, "Region", SortDirection.Ascending);
            result = this._service.ListReportOverlapProvince(p);
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
            if (ComboBoxProvince.SelectedIndex > 0)
            {
                Decimal.TryParse(ComboBoxProvince.SelectedItem.Value, out valueId);
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

            // ภาค
            if (DropDownListRegion.SelectedIndex > 0)
            {
                Decimal.TryParse(DropDownListRegion.SelectedItem.Value, out valueId);
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "RegionID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = valueId
                });
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
            if (Page.IsValid)
            {
                BindReport();
            }
        }

    }
}