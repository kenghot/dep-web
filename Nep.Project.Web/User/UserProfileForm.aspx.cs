using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;

namespace Nep.Project.Web.User
{
    public partial class UserProfileForm : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IUserProfileService _service { get; set; }
        public IServices.IProviceService _provinceService { get; set; }
        public Decimal? UserID
        {
            get
            {
                decimal? id = (decimal?)null;
                if(ViewState["UserID"] != null){
                    id = Convert.ToDecimal(ViewState["UserID"]);
                }else{
                    string strID = Request.QueryString["UserID"];
                    decimal temID = 0;
                    Decimal.TryParse(strID, out temID);
                    id = (temID > 0) ? temID : (decimal?)null;
                    ViewState["UserID"] = id;
                }
                
                return id;
            }
        }

        public int ProvinceSelectedIndex
        {
            get { 
                int i = -1;
                if (ViewState["ProvinceSelectedIndex"] != null)
                {
                    i = Convert.ToInt32(ViewState["ProvinceSelectedIndex"]);
                }
                return i;
            }
            set { ViewState["ProvinceSelectedIndex"] = value; }
        }

        public Boolean IsCreateMode
        {
            get
            {
                bool isTrue = (!UserID.HasValue);
                return isTrue;
            }
        }

        public Boolean IsDeleteRole
        {
            get
            {
                bool isTrue = (UserInfo.Roles.Contains(Common.FunctionCode.MANAGE_USER) && UserInfo.IsCenterOfficer);
                return isTrue;
            }
        }

        public Int32 AdmimistratorRoleID
        {
            get
            {
                return Convert.ToInt32(ViewState["AdmimistratorRoleID"]);
            }

            set
            {
                ViewState["AdmimistratorRoleID"] = value;
            }
        }

        public Int32 ProvinceRoleID
        {
            get
            {
                return Convert.ToInt32(ViewState["ProvinceRoleID"]);
            }

            set
            {
                ViewState["ProvinceRoleID"] = value;
            }
        }

        public List<ServiceModels.GenericDropDownListData> CenterProvince
        {
            get {
                List<ServiceModels.GenericDropDownListData> list = null;
                if(ViewState["CenterProvince"] != null){
                    list = (List<ServiceModels.GenericDropDownListData>)ViewState["CenterProvince"];
                }else{
                    var result = _provinceService.ListOrgProvince("ส่วนกลาง");
                    if(result.IsCompleted){
                        ViewState["CenterProvince"] = result.Data;
                        list = result.Data;
                    }else{
                        ShowErrorMessage(result.Message);
                    }
                }

                return list;
            }
            set { ViewState["CenterProvince"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.MANAGE_USER };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UserID.HasValue && Request.QueryString["Success"] == "true")
                {
                    ShowResultMessage(Resources.Message.SaveSuccess);
                }                
                BindData();
            }

            if (UserID.HasValue)
            {
                Page.Title = "แก้ไขผู้ใช้งาน";
            } else
            {
                Page.Title = "สร้างผู้ใช้งาน";
            }


            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ClientRegisterScript();
        }


        private List<ServiceModels.GenericDropDownListData> GetCenterProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = _provinceService.ListOrgProvince("ส่วนกลาง");
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

        private List<ServiceModels.GenericDropDownListData> GetProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = _provinceService.ListProvince(null);
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

        //private void BindDdlProvince()
        //{
        //    ComboBoxProvince.DataSource = GetProvince();
        //    ComboBoxProvince.DataBind();
        //    ComboBoxProvince.SelectedIndex = 0;
        //}

        private void BindData()
        {
            var adminRoleResult = _service.GetUserAdministratorRoleID();
            if (adminRoleResult.IsCompleted)
            {
                AdmimistratorRoleID = adminRoleResult.Data;
            }
            else
            {
                ShowErrorMessage(adminRoleResult.Message);
                AdmimistratorRoleID = -1;
            }

            var provinceRoleResult = _service.GetUserProvicnceRoleID();
            if(provinceRoleResult.IsCompleted){
                ProvinceRoleID = provinceRoleResult.Data;
            }else{
                ShowErrorMessage(adminRoleResult.Message);
                ProvinceRoleID = -1;
            }

            if (UserID.HasValue)
            {
                
                
                var userInfoResult = _service.GetUserProfile((decimal)UserID);
                if(userInfoResult.IsCompleted){
                    ServiceModels.UserProfile user = userInfoResult.Data;

                    TxtFirstName.Text = user.FirstName;
                    TxtLastName.Text = user.LastName;
                    DdlRoleID.SelectedValue = user.GroupID.ToString();
                    TxtEmail.Text = user.Email;
                    TxtTelephoneNo.Text = user.TelephoneNo;
                    DdlProvince.Value = user.ProvinceID.ToString();                    
                    TxtPosition.Text = user.Position;
                    IsActive.Checked = (user.IsActive == "1");

                    TxtEmail.Enabled = false;
                    ButtonDelete.Visible = IsDeleteRole;

                    if(user.GroupID == AdmimistratorRoleID){
                        LabelProvince.Visible = false;
                        DivComboBoxProvince.Visible = false;                        
                        CustomValidatorProvince.Enabled = false;
                      
                    }

                    if (user.UserName.ToLower() == Common.Constants.SYSTEM_USERNAME.ToLower())
                    {
                        ButtonDelete.Visible = false;
                        DdlRoleID.Enabled = false;                      
                        CustomValidatorProvince.Enabled = false;
                        TxtPosition.Enabled = false;
                        IsActive.Enabled = false;

                    }                    
                }
                else
                {
                    ShowErrorMessage(userInfoResult.Message);
                }



            }
            else
            {                
                IsActive.Checked = true;
            }

            ButtonSave.Visible = IsDeleteRole;
        }

        public List<ServiceModels.GenericDropDownListData> DdlRole_GetData()
        {
            DdlRoleID.Items.Clear();

            var listRole = _service.ListInternalRole();
            
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();

            if (listRole.IsCompleted)
            {
                list = listRole.Data;                
                list.Insert(0,new ServiceModels.GenericDropDownListData(){Text = UI.DropdownPleaseSelect,Value = ""});
            }
            else {
                ShowErrorMessage(listRole.Message);
            }

            return list;

        }        
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            
            if (Page.IsValid)
            {
                
                decimal groupID = Convert.ToDecimal(DdlRoleID.SelectedValue);
                decimal? provinceID = (groupID == AdmimistratorRoleID)?(decimal?)null : Convert.ToDecimal(DdlProvince.Value);

                ServiceModels.UserProfile user = new ServiceModels.UserProfile();

                user.FirstName = TxtFirstName.Text.Trim();
                user.LastName = TxtLastName.Text.Trim();
                user.GroupID = groupID;
                user.Email = TxtEmail.Text.Trim();
                user.TelephoneNo = TxtTelephoneNo.Text.Trim();
                user.ProvinceID = provinceID;
                user.Position = TxtPosition.Text.Trim();
                user.IsActive = (IsActive.Checked) ? "1" : "0";




                if(IsCreateMode){
                    var createResult = _service.CreateInternalUser(user);
                    if (createResult.IsCompleted)
                    {
                        Response.Redirect(Page.ResolveClientUrl("~/User/UserProfileForm?Success=true&UserID="+ createResult.Data.UserID));
                    }
                    else
                    {
                        ShowErrorMessage(createResult.Message);
                    }
                }
                else
                {
                    user.UserID = (decimal)UserID;
                    var updateResult = _service.UpdateInternalUser(user);
                    if (updateResult.IsCompleted)
                    {
                        Response.Redirect(Page.ResolveClientUrl("~/User/UserProfileForm?Success=true&UserID=" + user.UserID));
                    }
                    else
                    {
                        ShowErrorMessage(updateResult.Message);
                    }
                }               
            }

        }

        #region BindComboBox
        //private List<ServiceModels.GenericDropDownListData> GetProvince()
        //{
            
        //    DdlProvince.Value = "";
        //    string selectedRole = DdlRoleID.SelectedValue;
        //    Int32 roleID = 0;
        //    Int32.TryParse(selectedRole, out roleID);
        //    List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
        //    if (roleID == ProvinceRoleID)
        //    {
        //        var result = _provinceService.ListProvince(null);
        //        if (result.IsCompleted)
        //        {
        //            list = result.Data;
        //            list.Insert(0, new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.DropdownPleaseSelect, Value = "" });
        //        }
        //        else
        //        {
        //            ShowErrorMessage(result.Message);
        //        }
        //    }
        //    else
        //    {
        //        var result = _provinceService.ListOrgProvince("ส่วนกลาง");
        //        if (result.IsCompleted)
        //        {
        //            list = result.Data;
        //            list.Insert(0, new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.DropdownPleaseSelect, Value = "" });
        //        }
        //        else
        //        {
        //            ShowErrorMessage(result.Message);
        //        }
        //    }           

        //    return list;
        //}
        #endregion

        #region Server Validate
        //protected void CustomValidatorProvince_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    int selectedIndex = ComboBoxProvince.SelectedIndex;
        //    args.IsValid = (selectedIndex < 0) ? false : true;
        //}
        #endregion

        protected void CustomValidatorRole_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int selectedIndex = DdlRoleID.SelectedIndex;
            args.IsValid = (selectedIndex < 0) ? false : true;
        }
               

        private List<ServiceModels.GenericDropDownListData> GetProviceOrganizationList(decimal provinceID)
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var orgResult = _service.ListProvinceOrganization(provinceID);
            if (orgResult.IsCompleted)
            {
                list = orgResult.Data;
                if(list == null){
                    list = new List<ServiceModels.GenericDropDownListData>();
                }
            }
            else
            {
                ShowErrorMessage(orgResult.Message);
            }


            list.Insert(0, new ServiceModels.GenericDropDownListData {Text=Nep.Project.Resources.UI.DropdownPleaseSelect, Value="" });
            return list;   
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (UserID.HasValue)
            {
                var result = _service.DeleteUser((decimal)UserID);
                if (result.IsCompleted)
                {
                    Response.Redirect(Page.ResolveClientUrl("~/User/UserProfileList?isDeleteSuccess=true"));
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }

            }
        }

        protected void DdlRoleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = DdlRoleID.SelectedValue;
            int roleID = 0;
            Int32.TryParse(selectedValue, out roleID);
            if (roleID == AdmimistratorRoleID)
            {
                LabelProvince.Visible = false;
                DivComboBoxProvince.Visible = false;
                CustomValidatorProvince.Enabled = false;
            }
            else
            {
                LabelProvince.Visible = true;
                DivComboBoxProvince.Visible = true;
                CustomValidatorProvince.Enabled = true;
            }

            
            if(roleID != ProvinceRoleID){
                ProvinceSelectedIndex = 1;
                DdlProvince.Value = "";
            }

        
        }

        private void ClientRegisterScript()
        {
            string provinceSelected = (!String.IsNullOrEmpty(DdlProvince.Value)) ? DdlProvince.Value : "null";
            String script = "";

            string selectedValue = DdlRoleID.SelectedValue;
            int roleID = 0;
            Int32.TryParse(selectedValue, out roleID);

            if ((selectedValue != "") && roleID != ProvinceRoleID)
            {
                script = @"
                $(function () {                 
                    
                    c2x.createLocalCombobox({ 
                        Data:{Data:" + Nep.Project.Common.Web.WebUtility.ToJSON(GetCenterProvince()) + @", TotalRow:1, IsCompleted:true},
                        ControlID: '" + DdlProvince.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',                        
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,                                         
                        Value: " + provinceSelected + @", 
                        SelectedIndex: 0                 
                     });  
                });";
            }
            else
            {
                script = @"
                $(function () {                 
                    
                    c2x.createLocalCombobox({ 
                        Data:{Data:" + Nep.Project.Common.Web.WebUtility.ToJSON(GetProvince()) + @", TotalRow:0, IsCompleted:true},
                        ControlID: '" + DdlProvince.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',                        
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetProvince',                       
                        Value: " + provinceSelected + @",                     
                     });  
                });";
            }

            ScriptManager.RegisterStartupScript(
                     this.Page,
                     this.GetType(),
                     "ManageProvince",
                     script,
                     true);
        }

        protected void CustomValidatorCombobox_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String value = DdlProvince.Value;
            int id = 0;
            Int32.TryParse(value, out id);

            args.IsValid = (id > 0);
        }
    }
}