using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using Nep.Project.Resources;

namespace Nep.Project.Web.Organization
{
    public partial class OrganizationRequestList : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IRegisterService service { get; set; }
        public IServices.IProviceService provinceService { get; set; }

        public Boolean IsDeleteRole
        {
            get
            {
                bool isTrue = UserInfo.Roles.Contains(Common.FunctionCode.MANAGE_ORGANIZATION);
                return isTrue;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.MANAGE_ORGANIZATION };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OrganizationGrid.Sort("RegisterDate", SortDirection.Descending);

                if ((Request.QueryString["isDeleteSuccess"] != null) && (Request.QueryString["isDeleteSuccess"].ToString() == "true"))
                {
                    ShowResultMessage(Nep.Project.Resources.Message.DeleteSuccess);
                }

                if (UserInfo.IsProvinceOfficer)
                {
                    SearchData();
                }
            }

            if (UserInfo.IsProvinceOfficer)
            {
                LabelProvince.Visible = false;
                DivComboBoxProvince.Visible = false;
            }
            else
            {
                LabelProvince.Visible = true;
                DivComboBoxProvince.Visible = true;
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
                        Data:{Data:" + Nep.Project.Common.Web.WebUtility.ToJSON(GetProvince()) + @", TotalRow:0, IsCompleted:true},                   
                        Value: " + provinceSelected + @",                     
                     });  
                });";

            ScriptManager.RegisterStartupScript(
                     this.Page,
                     this.GetType(),
                     "ManageUpdatePanelRegister",
                     script,
                     true);
        }

        public IQueryable<ServiceModels.DecimalDropDownListData> DropDownListProvince_SelectMethod()
        {
            return service.ListProvince();
        }

        protected String FormatAddress(ServiceModels.RegisteredOrganizationList item)
        {
            var sb = new System.Text.StringBuilder();
            if (!String.IsNullOrWhiteSpace(item.Address))
            {
                sb.AppendFormat(" บ้านเลขที่ {0}", item.Address);
            }

            if (!String.IsNullOrWhiteSpace(item.Building))
            {
                sb.AppendFormat(" อาคาร {0}", item.Building);
            }

            if (!String.IsNullOrWhiteSpace(item.Moo))
            {
                sb.AppendFormat(" หมู่ที่ {0}", item.Moo);
            }

            if (!String.IsNullOrWhiteSpace(item.Soi))
            {
                sb.AppendFormat(" ซอย{0}", item.Soi);
            }

            if (!String.IsNullOrWhiteSpace(item.Road))
            {
                sb.AppendFormat(" ถนน{0}", item.Road);
            }

            if (!String.IsNullOrWhiteSpace(item.SubDistrict))
            {
                sb.AppendFormat(" {0}", item.SubDistrict);
            }

            if (!String.IsNullOrWhiteSpace(item.District))
            {
                sb.AppendFormat(" {0}", item.District);
            }

            if (!String.IsNullOrWhiteSpace(item.Province))
            {
                sb.AppendFormat(" {0}", item.Province);
            }

            if (!String.IsNullOrWhiteSpace(item.PostCode))
            {
                sb.AppendFormat(" {0}", item.PostCode);
            }

            if (sb.Length >= 1)
            {
                sb.Remove(0, 1);
            }

            return sb.ToString();
        }

        public List<ServiceModels.RegisteredOrganizationList> OrganizationGrid_GetData(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {
            ServiceModels.ReturnQueryData<ServiceModels.RegisteredOrganizationList> result;
            if (rdAllApprove.Checked)
            {
                result = service.ListRegisteredOrganization(OrganizationGrid.QueryParameter);
            }else
            {
                result = service.ListRegisteredOrganization(OrganizationGrid.QueryParameter,rdApproved.Checked);
            }
           
            List<ServiceModels.RegisteredOrganizationList> data = new List<ServiceModels.RegisteredOrganizationList>();
            totalRowCount = 0;

            if (result.IsCompleted)
            {
                data = result.Data;
                totalRowCount = result.TotalRow;
                OrganizationGrid.TotalRows = result.TotalRow;

                if (totalRowCount == 0)
                {
                    ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
                }
                else
                {
                    // @TODO Clear Message
                }
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return data;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void SearchData()
        {
            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
            if (!String.IsNullOrEmpty(TextBoxOrganizationName.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor() { Field = "OrganizationName", Operator = ServiceModels.FilterOperator.Contains, Value = TextBoxOrganizationName.Text.Trim() });
            }

            if (UserInfo.IsProvinceOfficer)
            {
                fields.Add(new ServiceModels.FilterDescriptor() { Field = "ProvinceID", Operator = ServiceModels.FilterOperator.IsEqualTo, Value = UserInfo.ProvinceID });
            }
            else if (!String.IsNullOrEmpty(DdlProvince.Value))
            {
                fields.Add(new ServiceModels.FilterDescriptor() { Field = "ProvinceID", Operator = ServiceModels.FilterOperator.IsEqualTo, Value = Decimal.Parse(DdlProvince.Value) });
            }
            //if (chbApproved.Checked && !chbNotApproved.Checked){
            //    fields.Add(new ServiceModels.FilterDescriptor() { Field = "ApproveDate", Operator = ServiceModels.FilterOperator.IsEqualTo, Value = new DateTime?()});
            //}
            //if (!chbApproved.Checked && chbNotApproved.Checked)
            //{
            //    fields.Add(new ServiceModels.FilterDescriptor() { Field = "ApproveDate", Operator = ServiceModels.FilterOperator.IsGreaterThan, Value = new DateTime?() });
            //}
            List<ServiceModels.IFilterDescriptor> filterComposite = new List<ServiceModels.IFilterDescriptor>();
            if (fields.Count > 0)
            {
                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = fields
                });
            }


            this.OrganizationGrid.FilterDescriptors = filterComposite;
            this.OrganizationGrid.DataBind();
        }

        protected void OrganizationGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String key = e.CommandArgument.ToString();
            decimal organizationEntryID = 0;
            Decimal.TryParse(key, out organizationEntryID);

            if ((e.CommandName == "del") && (organizationEntryID > 0))
            {
                var result = service.RemoveRegisteredOrganization(organizationEntryID);
                if (result.IsCompleted)
                {
                    OrganizationGrid.DataBind();
                    ShowResultMessage(result.Message);
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }
        }

        private List<ServiceModels.GenericDropDownListData> GetProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = provinceService.ListProvince(null);
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