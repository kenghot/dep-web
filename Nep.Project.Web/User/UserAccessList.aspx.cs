using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using Nep.Project.Resources;

namespace Nep.Project.Web.User
{
    public partial class UserAccessList : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IUserProfileService _service { get; set; }
        public IServices.IProviceService _provinceService { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.MANAGE_USER };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindDdlProvince();
                BindUserRole();

 

            }          

        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ClientRegisterScript();
        }




        public Boolean IsDeleteRole
        {
            get
            {
                bool isTrue = (UserInfo.Roles.Contains(Common.FunctionCode.MANAGE_USER) && UserInfo.IsCenterOfficer);
                return isTrue;
            }
        }

        protected void BindUserRole()
        {
            ddlRole.Items.Clear();
            var roleResult = _service.ListRole();
            if (roleResult.IsCompleted)
            {
                List<ServiceModels.GenericDropDownListData> ddlList = roleResult.Data;
                ddlList.Insert(0, new ServiceModels.GenericDropDownListData { Value="", Text=Nep.Project.Resources.UI.DropdownAll});
                ddlRole.DataSource = ddlList;
                ddlRole.DataBind();
            }
            else
            {
                ShowErrorMessage(roleResult.Message);
            }
        }

        public void ButtonSearch_Click(object sender, EventArgs e)
        {
            List<ServiceModels.IFilterDescriptor> userNameFields = new List<ServiceModels.IFilterDescriptor>();            
            if (!String.IsNullOrEmpty(txtProfileName.Text))
            {
                userNameFields.Add(new ServiceModels.FilterDescriptor() { Field = "FirstName", Operator = ServiceModels.FilterOperator.Contains, Value = txtProfileName.Text.Trim() });

                userNameFields.Add(new ServiceModels.FilterDescriptor() { Field = "LastName", Operator = ServiceModels.FilterOperator.Contains, Value = txtProfileName.Text.Trim() });
            }

            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
            if (!String.IsNullOrEmpty(ddlRole.SelectedValue))
            {
                decimal roleId = Convert.ToDecimal(ddlRole.SelectedValue);
                fields.Add(new ServiceModels.FilterDescriptor() { Field = "RoleID", Operator = ServiceModels.FilterOperator.IsEqualTo, Value = roleId });
            }

            if(!String.IsNullOrEmpty(txtEmail.Text)){
                fields.Add(new ServiceModels.FilterDescriptor() { Field = "Email", Operator = ServiceModels.FilterOperator.Contains, Value = txtEmail.Text.Trim() });
            }

            if (!String.IsNullOrEmpty(txtOrgName.Text))
            {
                fields.Add(new ServiceModels.FilterDescriptor() { Field = "OrganizationName", Operator = ServiceModels.FilterOperator.Contains, Value = txtOrgName.Text.Trim() });
            }
            DateTime date;
            if ((DatePickerStart.SelectedDate.HasValue) && (DatePickerEnd.SelectedDate.HasValue))
            {
                date = ((DateTime)DatePickerStart.SelectedDate);
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "AccessDateTime",
                    Operator = ServiceModels.FilterOperator.IsGreaterThanOrEqualTo,
                    Value = date.Date
                });

                date = ((DateTime)DatePickerEnd.SelectedDate);
                date = date.AddDays(1);
                date = date.AddSeconds(-1);
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "AccessDateTime",
                    Operator = ServiceModels.FilterOperator.IsLessThanOrEqualTo,
                    Value = date
                });
            }
            //จังหวัด
            if (DdlProvince.Value != "")
            {
                decimal valueId = 0;
                Decimal.TryParse(DdlProvince.Value, out valueId);
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProvinceID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = valueId
                });
            }

            List<ServiceModels.IFilterDescriptor> filterComposite = new List<ServiceModels.IFilterDescriptor>();
            if (userNameFields.Count > 0)
            {
                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.Or,
                    FilterDescriptors = userNameFields

                });
            }

            if (fields.Count > 0)
            {
                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = fields
                });
            }

            List<ServiceModels.IFilterDescriptor> filters = new List<ServiceModels.IFilterDescriptor>();  
            if(filterComposite.Count > 0){
                filters.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = filterComposite
                });
            }            
                  

            this.UserProfileGrid.FilterDescriptors = filters;
            this.UserProfileGrid.DataBind();

        }
       
        public List<ServiceModels.UserList> UserProfileGrid_GetData(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {
            var result = _service.ListAccessWithCriteria(UserProfileGrid.QueryParameter);
            List<ServiceModels.UserList> data = new List<ServiceModels.UserList>();
            totalRowCount = 0;

            if (result.IsCompleted)
            {
                data = result.Data;
                totalRowCount = result.TotalRow;
                UserProfileGrid.TotalRows = result.TotalRow;

                if (totalRowCount == 0)
                {
                    ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
                }
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return data;

        }

        protected void UserProfileGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{                
            //    if(e.Row.DataItem != null){
            //        ServiceModels.UserList user = (ServiceModels.UserList)e.Row.DataItem;
            //        HyperLink link = (HyperLink)e.Row.Cells[0].FindControl("lnkUserName");

            //        string  roleCode = (!String.IsNullOrEmpty(user.RoleCode)) ? user.RoleCode : "";


            //        String url = (roleCode == Common.UserGroupCode.องค์กรภายนอก) ? "~/User/UserProfileExternalForm" : "~/User/UserProfileForm";
            //        url = ResolveUrl(url + "?UserID=" + user.UserID);
            //        link.NavigateUrl = url;                   
            //    }                
            //}
        }

        protected void UserProfileGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //String key = e.CommandArgument.ToString();
            //decimal id = 0;
            //Decimal.TryParse(key, out id);

            //if ((e.CommandName == "del") && (id > 0))
            //{
            //    var result = _service.DeleteUser(id);
            //    if (result.IsCompleted)
            //    {
            //        UserProfileGrid.DataBind();
            //        ShowResultMessage(result.Message);
            //    }
            //    else
            //    {
            //        ShowErrorMessage(result.Message);
            //    }
            //}

        }


        //private void BindDdlProvince()
        //{
        //    var result = _provinceService.ListOrgProvince(null);
        //    List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
        //    if (result.IsCompleted)
        //    {
        //        list = result.Data;
        //        list.Insert(0, new ServiceModels.GenericDropDownListData { Value = "", Text = Nep.Project.Resources.UI.DropdownAll });
        //        list.Insert(1, new ServiceModels.GenericDropDownListData { Value = "0", Text = Nep.Project.Resources.UI.LabelNotProvinceName });
        //    }
        //    else
        //    {
        //        ShowErrorMessage(result.Message);
        //    }

        //    //ComboBoxProvince.DataSource = list;
        //    //ComboBoxProvince.DataBind();
        //    //ComboBoxProvince.SelectedIndex = 0;
        //}

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            txtEmail.Text = String.Empty;
            txtProfileName.Text = String.Empty;
            txtOrgName.Text = String.Empty;

            ddlRole.ClearSelection();
            DdlProvince.Value = "";
 
        }

        private List<ServiceModels.GenericDropDownListData> GetOrgProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = _provinceService.ListOrgProvince(null);
            if (result.IsCompleted)
            {
                list = result.Data;
                list.Insert(1, new ServiceModels.GenericDropDownListData { Value = "0", Text = Nep.Project.Resources.UI.LabelNotProvinceName });
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return list;
        }

        private void ClientRegisterScript()
        {
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
                     "ManageUpdatePanelRegister",
                     script,
                     true);
        }
    }
}