using Nep.Project.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Account
{
    public partial class EditOrganizationData : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IAuthenticationService service { get; set; }

        public IServices.IOrganizationService OrganizationService { get; set; }
        public IServices.IProviceService ProviceService
        {
            get;
            set;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.IsAllowAnonymous = false;
        }
        public Decimal? OrganizationID
        {
            get
            {
                decimal organizationId = 0;
                if (UserInfo.OrganizationID != null)
                {
                    organizationId = (decimal)UserInfo.OrganizationID;
                }

                if (organizationId > 0)
                {
                    return organizationId;
                }
                else
                {
                    return (decimal?)null;
                }

            }
        }
        public Boolean IsDeleteable
        {
            get
            {
                bool isTrue = true;
                if (ViewState["IsDeleteable"] != null)
                {
                    isTrue = (Boolean)ViewState["IsDeleteable"];
                }
                return isTrue;
            }
            set { ViewState["IsDeleteable"] = value; }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            RegisterClientScript();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();

                if (OrganizationID.HasValue && Request.QueryString["success"] == "true")
                {
                    ShowResultMessage(Resources.Message.SaveSuccess);
                }
                if ((!OrganizationID.HasValue) && (UserInfo.ProvinceID.HasValue))
                {
                    DdlProvince.Value = UserInfo.ProvinceID.ToString();
                }
            }


            if (!this.IsPostBack)
            {
                string jsonFilePath = Server.MapPath("~/Content/Files/bank.json");
                string json = File.ReadAllText(jsonFilePath);
                object result = JsonConvert.DeserializeObject<object>(json.Replace("{\"result\":", "").Replace("}", ""));
                JToken[] jArray = ((result as JArray) as JToken).ToArray();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem { Text = UI.DropdownPleaseSelect, Value = "" });
                for (int i = 1; i < jArray.Length; i++)
                {
                    items.Add(new ListItem { Text = jArray[i][1].ToString().Replace("\"", ""), Value = jArray[i][2].ToString().Replace("\"", "") });
                }
                DdlBank.DataSource = items;
                DdlBank.DataTextField = "Text";
                DdlBank.DataValueField = "Value";
                DdlBank.DataBind();
            }
        }
        private void LoadData()
        {
            if (!OrganizationID.HasValue)
                return;



            var result = OrganizationService.Get(OrganizationID.Value);
            if (result.IsCompleted)
            {

                var data = result.Data;
                DdlProvince.Value = data.ProvinceID.ToString();
                DdlDistrict.Value = data.DistrictID.ToString();
                DdlSubDistrict.Value = data.SubDistrictID.ToString();

                IsDeleteable = data.IsDeleteable;
               
                //ComboBoxDistrict.Enabled = true;
                //ComboBoxSubDistrict.Enabled = true;
                //ComboBoxProvince.SelectedValue = data.ProvinceID.ToString();

                //ComboBoxDistrict.DataSource = GetDistrict((int)data.ProvinceID);
                //ComboBoxDistrict.DataBind();
                //ComboBoxDistrict.SelectedValue = data.DistrictID.ToString();

                //ComboBoxSubDistrict.DataSource = GetSubDistrict((int)data.DistrictID);
                //ComboBoxSubDistrict.DataBind();
                //ComboBoxSubDistrict.SelectedValue = data.SubDistrictID.ToString();

                TextBoxAddressNo.Text = data.Address;
                TextBoxBuilding.Text = data.Building;
                TextBoxEmail.Text = data.Email;
                TextBoxFax.Text = data.Fax;
                TextBoxMoo.Text = data.Moo;
                TextBoxOrganizationNameEN.Text = data.OrganizationNameEN;
                TextBoxOrganizationNameTH.Text = data.OrganizationNameTH;
                TextBoxOrgUnderSupport.Text = data.OrganizationUnder;
                TextBoxPostCode.Text = data.PostCode;
                DatePickerRegisterYear.SelectedDate = new DateTime(Int32.Parse(data.OrganizationYear), 1, 1);
                DatePickerRegisterDate.SelectedDate = data.OrgEstablishedDate;

                TextBoxSoi.Text = data.Soi;
                TextBoxStreet.Text = data.Road;
                TextBoxTelephone.Text = data.Telephone;
                TextBoxMobileOrganization.Text = data.Mobile;
                if (data.ExtendData != null)
                {
                    TextBoxAccountName.Text = data.ExtendData.AccountName;
                    TextBoxAccountNo.Text = data.ExtendData.AccountNo;
                    DdlBank.SelectedValue = data.ExtendData.BankNo;
                    TextBoxBranchNo.Text = data.ExtendData.BranchNo;
                }
                SetSelectedOrganizationType(data.OrganizationType, data.OrganizationTypeEtc);
            }
            else
            {
                ShowResultMessage(result.Message);
            }
        }
        protected void ButtonSave_Click(Object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            ServiceModels.OrganizationProfile entry = null;

            if (this.OrganizationID.HasValue)
            {
                var getResult = OrganizationService.Get(this.OrganizationID.Value);
                if (getResult.IsCompleted)
                {
                    entry = getResult.Data;
                }
                else
                {
                    ShowErrorMessage(getResult.Message);
                    return;
                }
            }
            else
            {
                entry = new ServiceModels.OrganizationProfile();
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
            else
            {
                entry.OrganizationTypeEtc = "";
            }

            entry.OrganizationUnder = TextBoxOrgUnderSupport.Text.Trim();

            int orgYear = DatePickerRegisterYear.SelectedDate.Value.Year;
            int currYear = DateTime.Today.Year - 1;

            entry.OrganizationYear = orgYear.ToString();
            entry.OrgEstablishedDate = (orgYear >= currYear) ? DatePickerRegisterDate.SelectedDate : (DateTime?)null;

            entry.OrganizationYear = DatePickerRegisterYear.SelectedDate.Value.Year.ToString();

            entry.Address = TextBoxAddressNo.Text.Trim();
            entry.Building = TextBoxBuilding.Text.Trim();
            entry.Moo = TextBoxMoo.Text.Trim();
            entry.Soi = TextBoxSoi.Text.Trim();
            entry.Road = TextBoxStreet.Text.Trim();
            entry.SubDistrictID = subDistID;
            entry.DistrictID = distID;
            entry.ProvinceID = provID;
            entry.PostCode = TextBoxPostCode.Text.Trim();
            entry.Telephone = TextBoxTelephone.Text.Trim();
            entry.Mobile = TextBoxMobileOrganization.Text.Trim();
            entry.Fax = TextBoxFax.Text.Trim();
            entry.Email = TextBoxEmail.Text.Trim();
            if (DdlBank.SelectedValue.Trim() == "" && TextBoxBranchNo.Text.Trim() == "" && TextBoxAccountNo.Text.Trim() == "" && TextBoxAccountName.Text.Trim() == "")
            {
                entry.ExtendData = null;
            }
            else
            {
                entry.ExtendData = new ServiceModels.OrganizationExtend
                {
                    AccountName = TextBoxAccountName.Text.Trim(),
                    AccountNo = TextBoxAccountNo.Text.Trim(),
                    BankNo = DdlBank.SelectedValue.Trim(),
                    BranchNo = TextBoxBranchNo.Text.Trim()
                };
            }
            ServiceModels.ReturnObject<ServiceModels.OrganizationProfile> result = null;

            if (this.OrganizationID.HasValue)
            {
                result = this.OrganizationService.Update(entry);
            }
            else
            {
                result = this.OrganizationService.Create(entry);
            }

            if (result.IsCompleted)
            {
                var id = result.Data.OrganizationID;
                Page.Response.Redirect("EditOrganizationData?success=true&OrganizationID=" + id);
            }
            else
            {
                this.ShowErrorMessage(result.Message);
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

        protected void ComboBoxProvince_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //String value = ComboBoxProvince.SelectedValue;
            //int provinceID = 0;
            //bool tryParseId = Int32.TryParse(value, out provinceID);

            //if (tryParseId)
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
            
        }
        private List<ServiceModels.GenericDropDownListData> GetDistrict(int proviceID)
        {
            var result = ProviceService.ListDistrict(proviceID, null);

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
            var result = ProviceService.ListSubDistrict(districtID, null);

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

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (OrganizationID.HasValue)
            {
                var result = OrganizationService.Remove((decimal)OrganizationID);
                if (result.IsCompleted)
                {
                    Response.Redirect(Page.ResolveClientUrl("~/Organization/OrganizationList?isDeleteSuccess=true"));
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }

            }
        }

        protected void CustomValidatorRegisterDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DatePickerRegisterYear.SelectedDate.HasValue)
            {
                int year = ((DateTime)DatePickerRegisterYear.SelectedDate).Year;
                int currentYear = DateTime.Today.Year - 1;

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
                        Enable:" + Nep.Project.Common.Web.WebUtility.ToJSON(IsDeleteable) + @",
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetProvince',
                        Change: function(e){c2x.onProvinceComboboxChange('" + DdlDistrict.ClientID + @"', '" + DdlSubDistrict.ClientID + @"',e);},
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
    }
}