using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Register
{
    public partial class RegisterCompany : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IProjectInfoService _service
        {
            get;
            set;
        }
        public IServices.IListOfValueService lovService
        {
            get;
            set;
        }
        public IServices.IProviceService proviceService
        {
            get;
            set;
        }
        public IServices.IRegisterService registerService
        {
            get;
            set;
        }

        public String RegisPrefix
        {
            get
            {
                if (ViewState["RegisPrefix"] == null)
                {
                    string prefix = "regis/";
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
            this.IsAllowAnonymous = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRadioButtonOrganiaztionType();
                //BindRadioButtonListOrganizationType();
                //BindRadioButtonListGovType();
                //BindRadioButtonListPersonType();
            }        
            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            RegisterClientScript();
        }

        protected void OrganizationTypeValidate(object source, ServerValidateEventArgs args)
        {
            bool isValid = false;
            if (RadioButtonOrganizationType1.Checked)
            {
                isValid = true;
            }
            else if (RadioButtonOrganizationType2.Checked)
            {
                isValid = true;
            }
            else if (RadioButtonOrganizationType3.Checked)
            {
                isValid = true;
            }
            else if (RadioButtonOrganizationType4.Checked)
            {
                isValid = true;
            }
            else if (RadioButtonOrganizationType5.Checked)
            {
                isValid = true;
            }
            else if (RadioButtonOrganizationType6.Checked)
            {
                isValid = true;
            }
            else if (RadioButtonOrganizationType7.Checked)
            {
                isValid = true;
            }

            args.IsValid = isValid;
        }

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            string provValue = DdlProvince.Value;
            int provID = 0;
            Int32.TryParse(provValue, out provID);

            string distValue = DdlDistrict.Value;
            int distID = 0;
            Int32.TryParse(distValue, out distID);

            string subDistValue = DdlSubDistrict.Value;
            int subDistID = 0;
            Int32.TryParse(subDistValue, out subDistID);

            ServiceModels.OrganizationRegisterEntry entry = new ServiceModels.OrganizationRegisterEntry();
            entry.Position = TextBoxPosition.Text.Trim();
            entry.FirstName = TextBoxRequesterFirstName.Text.Trim();
            entry.LastName = TextBoxRequesterLastName.Text.Trim();
            entry.Position = TextBoxPosition.Text.Trim();
            entry.PersonalID = TextBoxPersonalID.Text.Replace("-", "").Trim();
            entry.EmailUser = TextBoxEmail.Text.Trim();
            entry.TelephoneNoUser = TextBoxTelephoneUser.Text.Trim();
            entry.MobileUser = TextBoxMobileUser.Text.Trim();

            var personIDCardAttachment = PersonalIDAttachment.AllFiles != null ? PersonalIDAttachment.AllFiles.FirstOrDefault() : null;
            var employeeIDCardAttachment = OrgIdentityAttachment.AllFiles != null ? OrgIdentityAttachment.AllFiles.FirstOrDefault() : null;


            entry.OrganizationNameTH = TextBoxOrganizationNameTH.Text.Trim();
            entry.OrganizationNameEN = TextBoxOrganizationNameEN.Text.Trim();
            entry.OrganizationType = GetSelectedOrganizationType();
            if (entry.OrganizationType == Common.OrganizationTypeID.อื่นๆ)
            {
                entry.OrganizationTypeEtc = TextBoxOrganzationTypeETC.Text.Trim();
            }
            else if (entry.OrganizationType == Common.OrganizationTypeID.สังกัดกรม)
            {
                entry.OrganizationTypeEtc = TextBoxDepartmentName.Text.Trim();
            }
            else if (entry.OrganizationType == Common.OrganizationTypeID.กระทรวง)
            {
                entry.OrganizationTypeEtc = TextBoxMinistryName.Text.Trim();
            }

            int orgYear = DatePickerRegisterYear.SelectedDate.Value.Year;
            int currYear = DateTime.Today.Year - 1;

            entry.OrganizationYear = orgYear.ToString();
            entry.OrganizationDate = (orgYear >= currYear)? DatePickerRegisterDate.SelectedDate : (DateTime?)null;

            entry.Address = TextBoxAddressNo.Text.Trim();
            entry.Building = TextBoxBuilding.Text.Trim();
            entry.Moo = TextBoxMoo.Text.Trim();
            entry.Soi = TextBoxSoi.Text.Trim();
            entry.Road = TextBoxStreet.Text.Trim();
            entry.SubDistrictID = subDistID;
            entry.DistrictID = distID;
            entry.ProvinceID = provID;
            entry.PostCode = TextBoxPostCode.Text.Trim();
            entry.TelephoneNoOrganization = TextBoxTelephoneOrganization.Text.Trim();
            entry.MobileOrganization = TextBoxMobileOrganization.Text.Trim();
            entry.Fax = TextBoxFax.Text.Trim();
            entry.EmailOrganization = TextBoxEmailOrganization.Text.Trim();

            var result = registerService.CreateOrganizationRegisterEntry(entry, personIDCardAttachment, employeeIDCardAttachment);
            if (result.IsCompleted)
            {

                Page.Response.Redirect("RegisterComplete");
            }
            else
            {
                this.ShowErrorMessage(result.Message);
            }
        }

        protected void ComboBoxProvince_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //int selectedIndex = ComboBoxProvince.SelectedIndex;
            //string value = ComboBoxProvince.SelectedValue;
            //int provinceID = 0;
            //bool tryParseId = Int32.TryParse(value, out provinceID);

            //if ((selectedIndex >= 0) && (tryParseId))
            //{
            //    ComboBoxDistrict.Enabled = true;
            //    ComboBoxDistrict.DataSource = GetDistrict(provinceID);
            //    ComboBoxDistrict.DataBind();

            //    ComboBoxSubDistrict.SelectedIndex = -1;
            //    ComboBoxSubDistrict.Enabled = false;
            //}
            //else
            //{
            //    ComboBoxDistrict.SelectedIndex = -1;
            //    ComboBoxDistrict.Enabled = false;

            //    ComboBoxSubDistrict.SelectedIndex = -1;
            //    ComboBoxSubDistrict.Enabled = false;
            //}
        }

        protected void ComboBoxDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //int selectedIndex = ComboBoxDistrict.SelectedIndex;
            //string value = ComboBoxDistrict.SelectedValue;
            //int districtID = 0;
            //bool tryParseId = Int32.TryParse(value, out districtID);

            //if ((selectedIndex >= 0) && (tryParseId))
            //{
            //    Int32.TryParse(value, out districtID);
            //    ComboBoxSubDistrict.Enabled = true;
            //    ComboBoxSubDistrict.DataSource = GetSubDistrict(districtID);
            //    ComboBoxSubDistrict.DataBind();
            //}
            //else
            //{
            //    ComboBoxSubDistrict.SelectedIndex = -1;
            //    ComboBoxSubDistrict.Enabled = false;
            //}
        }

        public List<ServiceModels.GenericDropDownListData> GetProvince()
        {
            //String filter = ComboBoxProvince.Text;

            //var result = proviceService.ListProvince(filter);

            //if (result.IsCompleted)
            //{
            //    return result.Data;
            //}
            //else
            //{
            //    this.ShowErrorMessage(result.Message);
            //    return new List<ServiceModels.GenericDropDownListData>();
            //}
            return null;
        }

        private List<ServiceModels.GenericDropDownListData> GetDistrict(int proviceID)
        {
            var result = proviceService.ListDistrict(proviceID, null);

            if (result.IsCompleted)
            {
                return result.Data;
            }
            else
            {
                this.ShowErrorMessage(result.Message);
                return new List<ServiceModels.GenericDropDownListData>();
            }
        }

        private List<ServiceModels.GenericDropDownListData> GetSubDistrict(int districtID)
        {
            var result = proviceService.ListSubDistrict(districtID, null);

            if (result.IsCompleted)
            {
                return result.Data;
            }
            else
            {
                this.ShowErrorMessage(result.Message);
                return new List<ServiceModels.GenericDropDownListData>();
            }
        }

        protected void RadioButtonOrganizationType_CheckedChanged(object sender, EventArgs e)
        {
            RequiredFieldValidatorDepartmentName.Enabled = RadioButtonOrganizationType1.Checked;
            RequiredFieldValidatorMinistryName.Enabled = RadioButtonOrganizationType2.Checked;
            RequiredFieldValidatorOrganzationTypeETC.Enabled = RadioButtonOrganizationType7.Checked;
        }

        private Decimal GetSelectedOrganizationType()
        {
            Decimal result = 0;

            if (RadioButtonOrganizationType1.Checked)
            {
                result = Common.OrganizationTypeID.สังกัดกรม;
            }
            else if (RadioButtonOrganizationType2.Checked)
            {
                result = Common.OrganizationTypeID.กระทรวง;
            }
            else if (RadioButtonOrganizationType3.Checked)
            {
                result = Common.OrganizationTypeID.องค์กรปกครองส่วนท้องถิ่น;
            }
            else if (RadioButtonOrganizationType4.Checked)
            {
                result = Common.OrganizationTypeID.องค์กรด้านคนพิการ;
            }
            else if (RadioButtonOrganizationType5.Checked)
            {
                result = Common.OrganizationTypeID.องค์กรชุมชน;
            }
            else if (RadioButtonOrganizationType6.Checked)
            {
                result = Common.OrganizationTypeID.องค์กรธุรกิจ;
            }
            else if (RadioButtonOrganizationType7.Checked)
            {
                result = Common.OrganizationTypeID.อื่นๆ;
            }

            return result;
        }


        private void BindRadioButtonOrganiaztionType()
        {
            string typeName = string.Empty;
            string strType1ID = Common.OrganizationTypeID.สังกัดกรม.ToString();
            string strType2ID = Common.OrganizationTypeID.กระทรวง.ToString();
            string strType3ID = Common.OrganizationTypeID.องค์กรปกครองส่วนท้องถิ่น.ToString();

            string strType4ID = Common.OrganizationTypeID.องค์กรด้านคนพิการ.ToString();
            string strType5ID = Common.OrganizationTypeID.องค์กรชุมชน.ToString();
            string strType6ID = Common.OrganizationTypeID.องค์กรธุรกิจ.ToString();
            string strType7ID = Common.OrganizationTypeID.อื่นๆ.ToString();

            var result = _service.GetOrganizationType();
            List<ServiceModels.GenericDropDownListData> orgTypes = new List<ServiceModels.GenericDropDownListData>();
            String orgTypeName;
            if (result.IsCompleted)
            {
                orgTypes = result.Data;

                //สังกัดกรม
                orgTypeName = result.Data.Where(x => x.Value == strType1ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType1.Text = orgTypeName;
                RadioButtonOrganizationType1.Attributes.Add("value", strType1ID);

                //กระทรวง
                orgTypeName = result.Data.Where(x => x.Value == strType2ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType2.Text = orgTypeName;
                RadioButtonOrganizationType2.Attributes.Add("value", strType2ID);

                //องค์กรปกครองส่วนท้องถิ่น
                orgTypeName = result.Data.Where(x => x.Value == strType3ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType3.Text = orgTypeName + "เช่น องค์การบริหารส่วนจังหวัด เทศบาล องค์การบริหารส่วนตำบล เป็นต้น";
                RadioButtonOrganizationType3.Attributes.Add("value", strType3ID);

                //องค์กรด้านคนพิการ
                orgTypeName = result.Data.Where(x => x.Value == strType4ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType4.Text = orgTypeName;
                RadioButtonOrganizationType4.Attributes.Add("value", strType4ID);

                //องค์กรชุมชน
                orgTypeName = result.Data.Where(x => x.Value == strType5ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType5.Text = orgTypeName;
                RadioButtonOrganizationType5.Attributes.Add("value", strType5ID);

                //องค์กรธุรกิจ
                orgTypeName = result.Data.Where(x => x.Value == strType6ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType6.Text = orgTypeName;
                RadioButtonOrganizationType6.Attributes.Add("value", strType6ID);

                //อื่นๆ
                orgTypeName = result.Data.Where(x => x.Value == strType7ID).Select(x => x.Text).FirstOrDefault();
                RadioButtonOrganizationType7.Text = orgTypeName;
                RadioButtonOrganizationType7.Attributes.Add("value", strType7ID);
            }

            TextBoxDepartmentName.Text = "";
            TextBoxMinistryName.Text = "";
            TextBoxOrganzationTypeETC.Text = "";

        }

        protected void CustomValidatorRegisterDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DatePickerRegisterYear.SelectedDate.HasValue)
            {                
                int year = ((DateTime)DatePickerRegisterYear.SelectedDate).Year;
                int currentYear = DateTime.Today.Year -1 ;

                if ((year >= currentYear) && (!DatePickerRegisterDate.SelectedDate.HasValue))
                {
                    args.IsValid = false;
                }
                else if ((year >= currentYear) && (DatePickerRegisterDate.SelectedDate.HasValue))
                {
                    DateTime selectedDate = (DateTime)DatePickerRegisterDate.SelectedDate;
                    DateTime dateCompare = DateTime.Today.AddDays(-180);
                    args.IsValid = (selectedDate <= dateCompare);
                }
            }
        }

        private void RegisterClientScript()
        {
            string provinceSelected = (!String.IsNullOrEmpty(DdlProvince.Value)) ? DdlProvince.Value : "null";
            string districtSelected = (!String.IsNullOrEmpty(DdlDistrict.Value)) ? DdlDistrict.Value : "null";
            string subDistrictSelected = (!String.IsNullOrEmpty(DdlSubDistrict.Value)) ? DdlSubDistrict.Value : "null";

            String script = @"
                $(function () {                 
                    
                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlProvince.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        Enable:true,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetProvince',
                        Change: function(e){c2x.onProvinceComboboxChange('" + DdlDistrict.ClientID + @"', '"+ DdlSubDistrict.ClientID +@"',e);},
                        Value: " + provinceSelected + @",                     
                     });  

                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlDistrict.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'" + DdlProvince.ClientID + @"', 
                        AutoBind:false,
                        Enable:false,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetDistrict',
                        Change: function(e){c2x.onDistrictComboboxChange('" + DdlSubDistrict.ClientID + @"',e);},
                        Value: " + districtSelected + @",
                        Param: function(){return c2x.getProvinceComboboxParam('" + DdlProvince.ClientID + @"');},
                     });    

                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlSubDistrict.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'" + DdlDistrict.ClientID + @"', 
                        AutoBind:false,
                        Enable:false,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetSubDistrict',      
                        Value: " + subDistrictSelected + @",     
                        Param: function(){return c2x.getProvinceComboboxParam('" + DdlDistrict.ClientID + @"');},           
                     });                   
                   
                });
                 

                function showOrgRegisterDate() {
                    var orgYearPicker = $find('DatePickerRegisterYear');
                    if (orgYearPicker != null) {
                        stopShowOrgDateInterval();
                        var orgSelectedDate = orgYearPicker.get_selectedDate();
                        var orgYear = kendo.toString(orgSelectedDate, 'yyyy');
                        orgYear = parseInt(orgYear, 10);

                        var currentYear = kendo.toString(kendo.parseDate(new Date()), 'yyyy');
                        currentYear = parseInt(currentYear, 10) - 1;
                        if (orgYear >= currentYear) {
                            $('.org-register-date').each(function (item) {
                                $(this).css('visibility', 'visible');
                            });

                        } else {
                            $('.org-register-date').each(function (item) {
                                $(this).css('visibility', 'hidden');
                            });

                            $('#" + CustomValidatorRegisterDate.ClientID+ @"').css('visibility', 'hidden');
                        }
                    }            
                }

                function stopShowOrgDateInterval() {
                    clearInterval(showOrgDateInterval);
                }

                var showOrgDateInterval = setInterval(function(){ showOrgRegisterDate() }, 1000);
                
                

            ";
            ScriptManager.RegisterStartupScript(
                      UpdatePanelRegister,
                      this.GetType(),
                      "ManageUpdatePanelRegister",
                      script,
                      true);   
       

            //$(function () {                 
                    
            //        c2x.createComboboxCustom({                       
            //            ControlID: '" + DdlProvince.ClientID + @"',
            //            Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect+ @"',
            //            TextField: 'Text',
            //            ValueField: 'Value',
            //            ServerFiltering: false,
            //            ReadUrl: './ComboboxHandler/GetProvince',
            //            Change: function(e){c2x.onProvinceComboboxChange('" + DdlDistrict.ClientID + @"',e);},
            //            Value: " + provinceSelected + @",                     
            //         });  

            //        c2x.createComboboxCustom({                       
            //            ControlID: '" + DdlDistrict.ClientID + @"',
            //            Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
            //            AutoBind:false,
            //            Enable:false,
            //            TextField: 'Text',
            //            ValueField: 'Value',
            //            ServerFiltering: false,
            //            ReadUrl: './ComboboxHandler/GetDistrict',
            //            Change: function(e){c2x.onDistrictComboboxChange('" + DdlSubDistrict.ClientID + @"',e);},
            //            Value: " + districtSelected + @",
            //            Param: function(){return c2x.getProvinceComboboxParam('" + DdlProvince.ClientID + @"');},
            //         });    

            //        c2x.createComboboxCustom({                       
            //            ControlID: '" + DdlSubDistrict.ClientID + @"',
            //            Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
            //            AutoBind:false,
            //            Enable:false,
            //            TextField: 'Text',
            //            ValueField: 'Value',
            //            ServerFiltering: false,
            //            ReadUrl: './ComboboxHandler/GetSubDistrict',      
            //            Value: " + subDistrictSelected + @",     
            //            Param: function(){return c2x.getProvinceComboboxParam('" + DdlDistrict.ClientID + @"');},           
            //         });                   
                   
            //    });


        }

        protected void CustomValidatorProvince_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String value = DdlProvince.Value;
            int id = 0;
            Int32.TryParse(value, out id);

            args.IsValid = (id > 0);
        }

        protected void CustomValidatorDistrict_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String value = DdlDistrict.Value;
            int id = 0;
            Int32.TryParse(value, out id);

            args.IsValid = (id > 0);
        }

        protected void CustomValidatorSubDistrict_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String value = DdlSubDistrict.Value;
            int id = 0;
            Int32.TryParse(value, out id);

            args.IsValid = (id > 0);
        }

        protected void CustomValidatorIDCardNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String str = args.Value.Trim();
            str = str.Replace("-", "").Replace("_", "");
            args.IsValid = (str != "");
        }
    }
}