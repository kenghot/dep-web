using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Organization
{
    public partial class OrganizationRequestForm : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IRegisterService service { get; set; }
        public IServices.IProjectInfoService ProjectInfoService
        {
            get;
            set;
        }

        public Boolean IsDeleteRole
        {
            get
            {
                bool isTrue = UserInfo.Roles.Contains(Common.FunctionCode.MANAGE_ORGANIZATION);
                return isTrue;
            }
        }

        public String RegisPrefix
        {
            get
            {
                //try
                //{
                //    entryId = Decimal.Parse(Request.QueryString["ID"]);
                //}
                //catch { }

                if (ViewState["RegisPrefix"] == null)
                {
                    //string prefix = "regis/" + entryId;
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

        private Decimal? entryId = null;

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.MANAGE_ORGANIZATION };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                entryId = Decimal.Parse(Request.QueryString["ID"]);
            }
            catch { }

            if (!IsPostBack)
            {
                BindRadioButtonOrganiaztionType();
                if (entryId.HasValue)
                {
                    LoadData(entryId.Value);
                }
            }
        }

        private void LoadData(Decimal entryId)
        {
            var result = service.GetRegisteredOrganization(entryId);
            if (result.IsCompleted)
            {
                var data = result.Data;
                string mobile;

                LabelAddressNo.Text = Common.Web.WebUtility.DisplayInHtml(data.Address, "", "-");
                LabelBuilding.Text = Common.Web.WebUtility.DisplayInHtml(data.Building, "", "-");
                LabelDistrict.Text = Common.Web.WebUtility.DisplayInHtml(data.District, "", "-");
                LabelEmail.Text = Common.Web.WebUtility.DisplayInHtml(data.EmailUser, "", "-");
                LabelEmailOrganization.Text = Common.Web.WebUtility.DisplayInHtml(data.EmailOrganization, "", "-");
                LabelFax.Text = Common.Web.WebUtility.DisplayInHtml(data.Fax, "", "-");
                LabelMoo.Text = Common.Web.WebUtility.DisplayInHtml(data.Moo, "", "-");
                LabelOrganizationNameEN.Text = Common.Web.WebUtility.DisplayInHtml(data.OrganizationNameEN, "", "-");
                LabelOrganizationNameTH.Text = Common.Web.WebUtility.DisplayInHtml(data.OrganizationNameTH, "", "-");
                LabelOrgUnderSupport.Text = Common.Web.WebUtility.DisplayInHtml(data.OrgUnderSupport, "", "-");
                LabelPersonalID.Text = Common.Web.WebUtility.DisplayIDCardNoInHtml(data.PersonalID, "-"); 
                LabelPosition.Text = Common.Web.WebUtility.DisplayInHtml(data.Position, "", "-"); 
                LabelPostCode.Text = Common.Web.WebUtility.DisplayInHtml(data.PostCode, "", "-");
                LabelProvince.Text = data.Province;
                
                LabelRequesterFirstName.Text = data.FirstName;
                LabelRequesterLastName.Text = data.LastName;
                LabelSoi.Text = Common.Web.WebUtility.DisplayInHtml(data.Soi, "", "-");
                LabelStreet.Text = Common.Web.WebUtility.DisplayInHtml(data.Road, "", "-");
                LabelSubDistrict.Text = Common.Web.WebUtility.DisplayInHtml(data.SubDistrict, "", "-");
                LabelTelephoneOrganization.Text = Common.Web.WebUtility.DisplayInHtml(data.TelephoneNoOrganization, "", "-");

                mobile = String.IsNullOrEmpty(data.MobileOrganization) ? null : data.MobileOrganization;
                LabelMobileOrganization.Text = Common.Web.WebUtility.DisplayInHtml(mobile, "", "-");
                LabelTelephoneUser.Text = Common.Web.WebUtility.DisplayInHtml(data.TelephoneNoUser, "", "-");

                mobile = String.IsNullOrEmpty(data.MobileUser)? null : data.MobileUser;
                LabelMobileUser.Text = Common.Web.WebUtility.DisplayInHtml(mobile, "", "-"); 

                SetSelectedOrganizationType(data.OrganizationType, data.OrganizationTypeEtc);

                #region Org Year
                if (!String.IsNullOrEmpty(data.OrganizationYear))
                {
                    DateTime tmpDate = new DateTime(Convert.ToInt32(data.OrganizationYear), 1, 1, 0, 0, 0, Common.Constants.CULTUREINFO.Calendar);
                    LabelRegisterYear.Text = Common.Web.WebUtility.DisplayInHtml(tmpDate.ToString("yyyy", Common.Constants.UI_CULTUREINFO), "", "-"); 
                }

                if (data.OrganizationDate.HasValue)
                {
                    OrganizationRegisterDateLabel.Visible = true;
                    OrganizationRegisterDateControl.Visible = true;
                    DateTime tmpDate = (DateTime)data.OrganizationDate;
                    LabelRegisterDate.Text = Common.Web.WebUtility.DisplayInHtml(tmpDate.ToString(Common.Constants.UI_FORMAT_DATE, Common.Constants.UI_CULTUREINFO), "", "-");
                }
                #endregion Org Year

                #region Attachment
                List<ServiceModels.KendoAttachment> list;
                if(data.IdentityAttachment != null){
                    list = new List<ServiceModels.KendoAttachment>();
                    list.Add(data.IdentityAttachment);
                    PersonalIDAttachment.ExistingFiles = list;
                }

                if(data.OrgIdentityAttachment != null){
                    LabelOrgIdentityAttachment.Visible = false;
                    list = new List<ServiceModels.KendoAttachment>();
                    list.Add(data.OrgIdentityAttachment);
                    OrgIdentityAttachment.ExistingFiles = list;
                }
                #endregion Attachment


                if (data.ApprovedDate.HasValue)
                {
                    ButtonApprove.Visible = false;
                    ButtonDelete.Visible = false;
                }
                else
                {
                    var hasRole = UserInfo.Roles.Contains(Common.FunctionCode.MANAGE_ORGANIZATION);
                    if (UserInfo.IsProvinceOfficer)
                    {
                        hasRole = (hasRole && (data.ProvinceID == UserInfo.ProvinceID));
                        if (hasRole)
                        {
                            ButtonApprove.Visible = hasRole;
                            ButtonDelete.Visible = hasRole;
                        }
                        else
                        {
                            UpdatePanelRegister.Visible = false;
                        }

                       
                    }
                    else
                    {
                        ButtonApprove.Visible = hasRole;
                        ButtonDelete.Visible = hasRole;
                    }                    
                }
            }
            else
            {
                ShowResultMessage(result.Message);
            }
        }

        private void SetSelectedOrganizationType(Decimal id, String etc)
        {
            RadioButtonOrganizationType1.Checked = id == Common.OrganizationTypeID.สังกัดกรม;
            RadioButtonOrganizationType2.Checked = id == Common.OrganizationTypeID.กระทรวง;
            RadioButtonOrganizationType3.Checked = id == Common.OrganizationTypeID.องค์กรปกครองส่วนท้องถิ่น;
            RadioButtonOrganizationType4.Checked = id == Common.OrganizationTypeID.องค์กรด้านคนพิการ;
            RadioButtonOrganizationType5.Checked = id == Common.OrganizationTypeID.องค์กรชุมชน;
            RadioButtonOrganizationType6.Checked = id == Common.OrganizationTypeID.องค์กรธุรกิจ;
            RadioButtonOrganizationType7.Checked = id == Common.OrganizationTypeID.อื่นๆ;

            if (RadioButtonOrganizationType1.Checked)
            {
                TextBoxDepartmentName.Text = etc;
            }
            else if (RadioButtonOrganizationType2.Checked)
            {
                TextBoxMinistryName.Text = etc;
            }
            else if (RadioButtonOrganizationType7.Checked)
            {
                TextBoxOrganzationTypeETC.Text = etc;
            }
        }

        protected void ButtonApprove_Click(object sender, EventArgs e)
        {
            if (entryId.HasValue)
            {
                var result = service.ApproveRegisteredOrganization(entryId.Value);
                if (result.IsCompleted)
                {
                    ShowResultMessage(Resources.Message.SaveSuccess);
                    ButtonApprove.Visible = false;
                    ButtonDelete.Visible = false;

                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }
        }

        private void BindRadioButtonOrganiaztionType()
        {
            string strType1ID = Common.OrganizationTypeID.สังกัดกรม.ToString();
            string strType2ID = Common.OrganizationTypeID.กระทรวง.ToString();
            string strType3ID = Common.OrganizationTypeID.องค์กรปกครองส่วนท้องถิ่น.ToString();

            string strType4ID = Common.OrganizationTypeID.องค์กรด้านคนพิการ.ToString();
            string strType5ID = Common.OrganizationTypeID.องค์กรชุมชน.ToString();
            string strType6ID = Common.OrganizationTypeID.องค์กรธุรกิจ.ToString();
            string strType7ID = Common.OrganizationTypeID.อื่นๆ.ToString();

            var result = ProjectInfoService.GetOrganizationType();
            if (result.IsCompleted)
            {
                String orgTypeName;
                var orgTypes = result.Data;

                //สังกัดกรม
                orgTypeName = orgTypes.Where(x => x.Value == strType1ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType1.Text = orgTypeName;

                //กระทรวง
                orgTypeName = orgTypes.Where(x => x.Value == strType2ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType2.Text = orgTypeName;

                //องค์กรปกครองส่วนท้องถิ่น
                orgTypeName = orgTypes.Where(x => x.Value == strType3ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType3.Text = orgTypeName;

                //องค์กรด้านคนพิการ
                orgTypeName = orgTypes.Where(x => x.Value == strType4ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType4.Text = orgTypeName;

                //องค์กรชุมชน
                orgTypeName = orgTypes.Where(x => x.Value == strType5ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType5.Text = orgTypeName;

                //องค์กรธุรกิจ
                orgTypeName = orgTypes.Where(x => x.Value == strType6ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType6.Text = orgTypeName;

                //อื่นๆ
                orgTypeName = orgTypes.Where(x => x.Value == strType7ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType7.Text = orgTypeName;
            }

            TextBoxDepartmentName.Text = "";
            TextBoxMinistryName.Text = "";
            TextBoxOrganzationTypeETC.Text = "";

        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                entryId = Decimal.Parse(Request.QueryString["ID"]);
            }
            catch { }
            if (entryId.HasValue)
            {
                var result = service.RemoveRegisteredOrganization((decimal)entryId);
                if (result.IsCompleted)
                {
                    Response.Redirect(Page.ResolveClientUrl("~/Organization/OrganizationRequestList?isDeleteSuccess=true"));
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }

            }
        }
    }
}