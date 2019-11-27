using Nep.Project.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.User
{
    public partial class UserRequestForm : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IUserProfileService _service { get; set; }
        public IServices.IRegisterService _regist { get; set; }
        public IServices.IProviceService _provinceService { get; set; }
  
        public Decimal? UserID
        {
            get
            {
                decimal? id = null;
                if (ViewState["UserID"] != null)
                {
                    id = Convert.ToDecimal(ViewState["UserID"]);
                }
                else
                {
                    string strID = Request.QueryString["UserID"];
                    decimal temID = 0;
                    Decimal.TryParse(strID, out temID);
                    id = (temID > 0) ? temID : (decimal?)null;
                    ViewState["UserID"] = id;
                }

                return id;
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

        public String RegisPrefix
        {
            get
            {
                if (ViewState["RegisPrefix"] == null)
                {
                    //string prefix = "regis/" + UserID;
                    string prefix = "regis";
                    ViewState["RegisPrefix"] = prefix;
                }
                return ViewState["RegisPrefix"].ToString();
            }

            set
            {
                ViewState["RegisPrefix"] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.MANAGE_USER };

            if (this.UserInfo.UserGroupCode == Common.UserGroupCode.องค์กรภายนอก)
            {
                functions = new String[] { Common.FunctionCode.MANAGE_USER, Common.FunctionCode.REQUEST_PROJECT };
                ButtonSendMail.Visible = false;
            }
            Functions = functions;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDdlProvince();
                BindData();
            }
        }

        private void BindDdlProvince()
        {
            ComboBoxProvince.DataSource = GetProvince();
            ComboBoxProvince.DataBind();
            ComboBoxProvince.SelectedIndex = 0;
        }

        private void BindData()
        {
            if (this.UserInfo.UserGroupCode == Common.UserGroupCode.องค์กรภายนอก)
            {
                var userInfoResult = _service.GetUserRequest(this.UserInfo.UserID.Value);
                if (userInfoResult.IsCompleted)
                {
                    var user = userInfoResult.Data;
                    TextBoxOrganizationName.Text = user.OrganizationName;
                    ComboBoxProvince.SelectedValue = user.ProvinceID.ToString();
                }
                    

                return;
            }
            if (UserID.HasValue)
            {
                var userInfoResult = _service.GetUserRequest(UserID.Value);
                if (userInfoResult.IsCompleted)
                {
                    var user = userInfoResult.Data;
                    //if user is not external user redirect to UserProfileForm
                    //if (user.GroupCode != Common.UserGroupCode.องค์กรภายนอก)
                    //{
                    //    Page.Response.Redirect("UserProfileForm?UserID=" + UserID);
                    //}
                    TxtFirstName.Text = user.FirstName;
                    TxtLastName.Text = user.LastName;
                    TextBoxIDCardNo.Text = user.IDNO;
                    
                    PersonalIDAttachment.ExistingFiles = null;
                    TextBoxOrganizationName.Text = user.OrganizationName;
                    DatePickerRegisterDate.SelectedDate = user.RegisterDate;
                    TxtPosition.Text = user.Position;
                    TxtTelephoneNo.Text = user.TelephoneNo;
                    ComboBoxProvince.SelectedValue = user.ProvinceID.ToString();
                    TxtEmail.Text = user.Email;
                   // CheckBoxIsActive.Checked = user.IsActive == Common.Constants.BOOLEAN_TRUE;

                    if(user.IDCardFileID.HasValue){
                        List<ServiceModels.KendoAttachment> attachList = new List<ServiceModels.KendoAttachment>();
                        attachList.Add(user.IDCardAttachment);
                        PersonalIDAttachment.ExistingFiles = attachList;
                    }

                    if(user.EmpployeeCardFileID.HasValue){
                        List<ServiceModels.KendoAttachment> attachList = new List<ServiceModels.KendoAttachment>();
                        attachList.Add(user.EmpployeeCardAttachment);
                        OrgIdentityAttachment.ExistingFiles = attachList;
                    }

                    ButtonDelete.Visible = IsDeleteRole;
                }
                else
                {
                    ShowErrorMessage(userInfoResult.Message);
                }
            }
        }
        protected void ButtonSendMail_Click(object sender, EventArgs e)
        {
            Business.MailService _mailService = new Business.MailService(new DBModels.Model.NepProjectDBEntities());
            _mailService.SendUserRegistrationNotify(UserID.Value);
           
            ShowResultMessage("ส่ง email เรียบร้อย");

        }
        private void CreateUser()
        {
            //var objModel = new ServiceModels.RegisterEntry();

            //string orgValue = DdlOrganization.Value;
            //int orgID = 0;
            //Int32.TryParse(orgValue, out orgID);

            //objModel.OrganizationID = orgID;
            //objModel.TelephoneNo = TxtTelephoneNo.Text.Trim();
            //objModel.Mobile = TextBoxMobileUser.Text.Trim();
            //objModel.Email = TxtEmail.Text.Trim();
            //objModel.Position = TxtPosition.Text.Trim();
            //objModel.FirstName = TxtFirstName.Text.Trim();
            //objModel.LastName = TxtLastName.Text.Trim();
        
            //objModel.PersonalID = TextBoxPersonalID.Text.Replace("-", "").Trim();

            //var personIDCardAttachment = PersonalIDAttachment.AllFiles.First();
            //var employeeIDCardAttachment = OrgIdentityAttachment.AllFiles != null ? OrgIdentityAttachment.AllFiles.FirstOrDefault() : null;

            //var result = service.CreateRegisterEntry(objModel, personIDCardAttachment, employeeIDCardAttachment);
            //if (result.IsCompleted)
            //{
            //    Response.Redirect(Page.ResolveUrl("~/Register/RegisterComplete"));
            //}
            //else
            //{
            //    ShowErrorMessage(result.Message);
            //}
        }
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }
            //Decimal? _provinceId = null;
            //try
            //{
            //    _provinceId = Decimal.Parse(ComboBoxProvince.SelectedValue);
            //}
            //catch
            //{
            //    _provinceId = null;
            //}
            if (this.UserInfo.UserGroupCode == Common.UserGroupCode.องค์กรภายนอก)
            {
                CreateUser();
            }
            var data = new ServiceModels.UserProfile();
            data.UserID = UserID.Value;
            data.FirstName = TxtFirstName.Text.Trim();
            data.LastName = TxtLastName.Text.Trim();
            data.TelephoneNo = TxtTelephoneNo.Text.Trim();
            data.Position = TxtPosition.Text.Trim();
            data.Email = TxtEmail.Text.Trim();
            var personIDCardAttachment = PersonalIDAttachment.AddedFiles.FirstOrDefault();
            var employeeIDCardAttachment = OrgIdentityAttachment.AddedFiles != null ? OrgIdentityAttachment.AddedFiles.FirstOrDefault() : null;

            data.IDCardAttachment = personIDCardAttachment;
            data.EmpployeeCardAttachment = employeeIDCardAttachment;

            //data.ProvinceID = _provinceId;
            data.IDNO = TextBoxIDCardNo.Text.Replace("-", "").Trim();
           // data.IsActive = CheckBoxIsActive.Checked ? Common.Constants.BOOLEAN_TRUE : Common.Constants.BOOLEAN_FALSE;
            var result = _service.UpdateRequestUser(data);
            if (result.IsCompleted)
            {
                BindData();
                ShowResultMessage(result.Message);
            }
            else
            {
                ShowErrorMessage(result.Message);
            }
        }

        #region BindComboBox
        private List<ServiceModels.GenericDropDownListData> GetProvince()
        {
            String filter = ComboBoxProvince.Text;
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();

            var result = _provinceService.ListOrgProvince(filter);
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
        #endregion

        #region Server Validate
        protected void CustomValidatorProvince_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int selectedIndex = ComboBoxProvince.SelectedIndex;
            args.IsValid = (selectedIndex < 0) ? false : true;
        }
        #endregion

        private List<ServiceModels.GenericDropDownListData> GetProviceOrganizationList(decimal provinceID)
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var orgResult = _service.ListProvinceOrganization(provinceID);
            if (orgResult.IsCompleted)
            {
                list = orgResult.Data;
                if (list == null)
                {
                    list = new List<ServiceModels.GenericDropDownListData>();
                }
            }
            else
            {
                ShowErrorMessage(orgResult.Message);
            }


            list.Insert(0, new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.DropdownPleaseSelect, Value = "" });
            return list;
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (UserID.HasValue)
            {
                var result = _service.DeleteRequestUser((decimal)UserID);
                if (result.IsCompleted)
                {
                    Response.Redirect(Page.ResolveClientUrl("~/User/NewUserProfileList?isDeleteSuccess=true"));
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }

            }
        }
    }
}