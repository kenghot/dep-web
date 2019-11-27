using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using AjaxControlToolkit;
using Nep.Project.Common;

namespace Nep.Project.Web.ProjectInfo
{
    public partial class Create : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IProjectInfoService _service { get; set; }
        public IServices.IProviceService _provinceService { get; set; }
        public IServices.IOrganizationService _orgService { get; set; }

        public Int32 CenterProvinceID
        {
            get
            {
                return Convert.ToInt32(ViewState["CenterProvinceID"]);
            }
            set
            {
                ViewState["CenterProvinceID"] = value;
            }
        }

        public bool OrganizationNameTHEnable
        {
            get
            {
                bool isTrue = true;
                if (ViewState["OrganizationNameTHEnable"] != null)
                {
                    isTrue = Convert.ToBoolean(ViewState["OrganizationNameTHEnable"]);
                }
                return isTrue;
            }
            set
            {
                ViewState["OrganizationNameTHEnable"] = value;
            }
        }

        public bool OrganizationProvinceEnable
        {
            get
            {
                bool isTrue = false;
                if (ViewState["OrganizationProvinceEnable"] != null)
                {
                    isTrue = Convert.ToBoolean(ViewState["OrganizationProvinceEnable"]);
                }
                return isTrue;
            }
            set
            {
                ViewState["OrganizationProvinceEnable"] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.MANAGE_PROJECT, Common.FunctionCode.REQUEST_PROJECT};
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var centerProvinceResult = _provinceService.GetCenterProvinceID();
                if(centerProvinceResult.IsCompleted){
                    CenterProvinceID = centerProvinceResult.Data;
                }else{
                    ShowErrorMessage(centerProvinceResult.Message);
                }


                BindRadioButtonOrganiaztionType();
                OrgAssistanceControl.DataBind();
                CommitteeControl.DataBind();
                CommitteeControl.RefreshPosition();
                //BindComboBoxOrganization(UserInfo.ProvinceID);
                //ComboBoxOrganizationProvince.SelectedValue = (UserInfo.ProvinceID.HasValue) ? UserInfo.ProvinceID.ToString() : "";
                DdlOrganizationProvince.Text = (UserInfo.ProvinceID.HasValue) ? UserInfo.ProvinceID.ToString() : "";
               

               if (UserInfo.OrganizationID.HasValue)
                {
                    //kenghot
                    var isBlackList = _orgService.IsBlackList(UserInfo.OrganizationID,UserInfo.ProvinceID);
                    if (isBlackList.IsCompleted && isBlackList.Data)
                    {
                        //var err = "องค์กรของท่านไม่สามารถเพิ่มโครงการได้ เนื่องจากองค์กรถูกจัดไว้ในสถานะ Blacklist";
                        //ShowErrorMessage(err);
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" +err+
                        //    "');window.location.href='/ProjectInfo/ProjectInfoList.aspx' ", true);
                        //ButtonSave.Visible = false;
                        Response.Redirect("~/ProjectInfo/NotAllowToCreate.aspx");
                    }
                    //end kenghot
                    DdlOrganizationNameTH.Text = UserInfo.OrganizationID.ToString();
                    OrganizationNameTHEnable = false;
                    BindOrganizationInfo(UserInfo.OrganizationID);
                }
               
            }

            string parameter = Request["__EVENTARGUMENT"];
            if (!String.IsNullOrEmpty(parameter) && (parameter == "loadorginfo"))
            {
                LoadOrgInformation();
            }

            if (UserInfo.OrganizationID.HasValue || UserInfo.IsAdministrator)
            {                
                OrganizationProvinceEnable = true;
                //ComboBoxOrganizationProvince.Enabled = true;
            }

            
           
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            RegisterClientScriptBlock();
            RegisterComboboxScript();
        }

        #region ServerValidate
        //protected void ValidateOrganizationNameTH(object source, ServerValidateEventArgs args)
        //{
        //    int selectedIndex = ComboBoxOrganizationNameTH.SelectedIndex;
        //    args.IsValid = (selectedIndex > 0);
        //}

        //protected void CustomValidatorOrganizationProvince_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    int selectedIndex = ComboBoxOrganizationProvince.SelectedIndex;
        //    args.IsValid = (selectedIndex < 0) ? false : true;
        //}

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

        protected void CustomValidatorCommittee_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isOrgType1Checked = RadioButtonOrganizationType1.Checked; // สังกัดกรม
            bool isOrgType2Checked = RadioButtonOrganizationType2.Checked; // กระทรวง
            bool isOrgType3Checked = RadioButtonOrganizationType3.Checked; // องค์กรปกครองส่วนท้องถิ่น
           
            args.IsValid = true;
            if (!isOrgType1Checked && !isOrgType2Checked && !isOrgType3Checked)
            {
                String errorMsg = CommitteeControl.ValidateOrganizationCommittee();
                args.IsValid = String.IsNullOrEmpty(errorMsg);
                CustomValidator validator = source as CustomValidator;
                validator.ErrorMessage = errorMsg;
                validator.Text = errorMsg;
            } 
        }
        #endregion

        #region BindComboBox
        //public void onComboBoxOrganizationNameTHSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int selectedIndex = ComboBoxOrganizationNameTH.SelectedIndex;
        //    string selectedValue = ComboBoxOrganizationNameTH.SelectedValue;          

        //    int tmpOrgId = 0;
        //    decimal? orgId = (Int32.TryParse(selectedValue, out tmpOrgId)) ? (decimal?)tmpOrgId : (decimal?)null;
            
        //    BindOrganizationInfo(orgId);           
        //}

        private void BindOrganizationInfo(decimal? organizationID)
        {
            if (organizationID.HasValue)
            {
                int organizationYear = 0;
                var result = _service.GetOrganizationInfoByID((decimal)organizationID);
                if (result.IsCompleted)
                {
                    ServiceModels.ProjectInfo.OrganizationInfo data = result.Data;
                    Int32.TryParse(data.OrganizationYear, out organizationYear);

                    if(data.OrgEstablishedDate.HasValue){
                        OrganizationRegisterDateLabel.Visible = true;
                        OrganizationRegisterDateControl.Visible = true;
                        DateTime tmpDate = (DateTime)data.OrgEstablishedDate;
                        TextBoxRegisterDate.Text = Common.Web.WebUtility.DisplayInHtml(tmpDate.ToString(Common.Constants.UI_FORMAT_DATE, Common.Constants.UI_CULTUREINFO), "", "-");
                    }
                    TextBoxOrganizationObjective.Text = data.Purpose;

                    TextBoxOrganizationNameEN.Text = data.OrganizationNameEN;
                    DisplayRadioButtonOrganiaztionType(data.OrganizationTypeID, data.OrganizationTypeEtc);
                    TextBoxOrgUnderSupport.Text = data.OrgUnderSupport;
                    TextBoxRegisterYear.Text = (organizationYear > 0) ? (organizationYear + 543).ToString() : data.OrganizationYear;
                    TextBoxAddressNo.Text = data.Address;
                    TextBoxBuilding.Text = data.Building;
                    TextBoxMoo.Text = data.Moo;
                    TextBoxSoi.Text = data.Soi;
                    TextBoxStreet.Text = data.Road;
                    ComboBoxProvince.SelectedValue = data.AddressProvinceID.ToString();

                    ComboBoxDistrict.DataSource = GetDistrict((int)data.AddressProvinceID);
                    ComboBoxDistrict.DataBind();
                    ComboBoxDistrict.SelectedValue = data.DistrictID.ToString();
                    

                    ComboBoxSubDistrict.DataSource = GetSubDistrict((int)data.DistrictID);
                    ComboBoxSubDistrict.DataBind();
                    ComboBoxSubDistrict.SelectedValue = data.SubDistrictID.ToString();
                    
                    

                    TextBoxPostCode.Text = data.Postcode;
                    TextBoxTelephone.Text = data.Telephone;
                    TextBoxMobile.Text = data.Mobile;
                    TextBoxFax.Text = data.Fax;
                    TextBoxEmail.Text = data.Email;

                    
                    

                    CommitteeControl.BindRepeaterCommittee(data.Committees, (decimal)organizationID, (decimal)data.OrganizationTypeID);
                }
            }
            else
            {
                TextBoxOrganizationNameEN.Text = "";
                DisplayRadioButtonOrganiaztionType((decimal?)null, "");
                TextBoxOrgUnderSupport.Text = "";
                TextBoxRegisterYear.Text = "";
                TextBoxAddressNo.Text = "";
                TextBoxBuilding.Text = "";
                TextBoxMoo.Text = "";
                TextBoxSoi.Text = "";
                TextBoxStreet.Text = "";
                ComboBoxProvince.ClearSelection();
                ComboBoxDistrict.ClearSelection();
                ComboBoxSubDistrict.ClearSelection();
                TextBoxPostCode.Text = "";
                TextBoxTelephone.Text = "";
                TextBoxMobile.Text = "";
                TextBoxFax.Text = "";
                TextBoxEmail.Text = "";
            }
        }

        private void BindOrganizationObjective(decimal? organizationID)
        {

        }

        public List<ServiceModels.GenericDropDownListData> GetOrganizationProvince()
        {
            String filter = DdlOrganizationProvince.Text;


            var result = _service.GetListProjectProvince();
            if (result.IsCompleted)
            {
                return result.Data;
            }
            else
            {
                return new List<ServiceModels.GenericDropDownListData>();
            }
        }       

        public List<ServiceModels.GenericDropDownListData> GetProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            string filter = ComboBoxProvince.Text;
            
            var result = _provinceService.ListProvince(filter);
            if (result.IsCompleted)
                list = result.Data;

            return list;
        }

        public List<ServiceModels.GenericDropDownListData> GetDistrict(int provinceID)
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            //string filter = ComboBoxDistrict.Text;
            //string provinceValue = ComboBoxProvince.SelectedValue;
            //int? provinceId = null;

            //if (!string.IsNullOrEmpty(provinceValue))
            //{
                //provinceId = Convert.ToInt32(provinceValue);
                //var result = _provinceService.ListDistrict(provinceID, filter);
                //if (result.IsCompleted)
                //    list = result.Data;
            //}

            var result = _provinceService.ListDistrict(provinceID, null);
            if (result.IsCompleted)
            {
                list = result.Data;
            }
            return list;
        }

        public List<ServiceModels.GenericDropDownListData> GetSubDistrict(int districtID)
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            //string filter = ComboBoxSubDistrict.Text;
            //string districtValue = ComboBoxDistrict.SelectedValue;
            //int? districtId = null;

            //if (!string.IsNullOrEmpty(districtValue))
            //{
            //    districtId = Convert.ToInt32(districtValue);
            //    var result = _provinceService.ListSubDistrict(districtId, filter);
            //    if (result.IsCompleted)
            //        list = result.Data;
            //}

            var result = _provinceService.ListSubDistrict(districtID, null);
            if (result.IsCompleted)
            {
                list = result.Data;
            }

            return list;
        }
        #endregion

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            ServiceModels.ProjectInfo.OrganizationInfo model = new ServiceModels.ProjectInfo.OrganizationInfo();
            try
            {
                if (Page.IsValid)
                {

                    model.Committees = CommitteeControl.GetDataEditingCommittee();

                    List<ServiceModels.ProjectInfo.OrganizationAssistance> listAssistance = OrgAssistanceControl.GetDataEditingOrgAssistance();
                    if (listAssistance.Count > 0)
                    {
                        model.Assistances = listAssistance;
                    }

                    //decimal provinceId = Convert.ToDecimal(ComboBoxOrganizationProvince.SelectedValue);
                    //decimal orgId = Convert.ToDecimal(ComboBoxOrganizationNameTH.SelectedValue);
                    decimal provinceId = Convert.ToDecimal(DdlOrganizationProvince.Text);
                    decimal orgId = Convert.ToDecimal(DdlOrganizationNameTH.Text);
                    bool supportFlag = (rbLstIsFirstFlag.SelectedItem.Value == "Yes") ? true : false;
                    decimal? supportTime = null;
                    if (!string.IsNullOrEmpty(TextBoxSupportTime.Text.Trim()))
                        supportTime = Convert.ToDecimal(TextBoxSupportTime.Text.Trim());

                    string purpose = TextBoxOrganizationObjective.Text.TrimEnd();
                    string currentProject = TextBoxActivityCurrent.Text.TrimEnd();
                    string currentProjectResult = TextBoxWorkingYear.Text.TrimEnd();
                    string gotSupportYear = (DatePickerSupportYear.SelectedDate.HasValue) ? ((DateTime)DatePickerSupportYear.SelectedDate).Year.ToString() : null;
                    string toGotSupportYear = (DatePickerTogotSupportYear.SelectedDate.HasValue) ? ((DateTime)DatePickerTogotSupportYear.SelectedDate).Year.ToString() : null;
                    string gotSupportLastProject = TextBoxProjectLasted.Text.Trim();
                    string gotSupportLastResult = TextBoxProjectLastedResult.Text.TrimEnd();
                    string gotSupportLastProblems = TextBoxProblem.Text.TrimEnd();

                    model.ProvinceID = provinceId;
                    model.OrganizationID = orgId;
                    model.Purpose = purpose;
                    model.CurrentProject = currentProject;
                    model.CurrentProjectResult = currentProjectResult;
                    model.GotSupportFlag = supportFlag;
                    if (supportFlag)
                    {
                        model.GotSupportYear = gotSupportYear;
                        model.TogotSupportYear = toGotSupportYear;
                        model.GotSupportTimes = supportTime;
                        model.GotSupportLastProject = gotSupportLastProject;
                        model.GotSupportLastResult = gotSupportLastResult;
                        model.GotSupportLastProblems = gotSupportLastProblems;
                    }


                    var objOranizationInfo = _service.GetOrganizationInfoByID(model.OrganizationID);
                    if (objOranizationInfo.IsCompleted)
                    {
                        var d = objOranizationInfo.Data;
                        model.OrganizationNameTH = d.OrganizationNameTH;
                        model.OrganizationNameEN = d.OrganizationNameEN;
                        model.OrganizationTypeID = d.OrganizationTypeID;
                        model.OrganizationTypeEtc = d.OrganizationTypeEtc;
                        model.OrgUnderSupport = d.OrgUnderSupport;
                        model.OrganizationYear = d.OrganizationYear;
                        model.OrgEstablishedDate = d.OrgEstablishedDate;
                        model.Address = d.Address;
                        model.Building = d.Building;
                        model.Moo = d.Moo;
                        model.Soi = d.Soi;
                        model.Road = d.Road;
                        model.SubDistrictID = d.SubDistrictID;
                        model.SubDistrict = d.SubDistrict;
                        model.DistrictID = d.DistrictID;
                        model.District = d.District;
                        model.AddressProvinceID = d.AddressProvinceID;
                        model.Postcode = d.Postcode;
                        model.Telephone = d.Telephone;
                        model.Mobile = d.Mobile;
                        model.Fax = d.Fax;
                        model.Email = d.Email;
                    }

                    string projectNameTH = TextBoxProjectInfoNameTH.Text.Trim();
                    string projectNameEN = TextBoxProjectInfoNameEN.Text.Trim();

                    var result = _service.Create(model, projectNameTH, projectNameEN);
                    if (result.IsCompleted)
                    {
                        decimal id = result.Data.ProjectID;
                        Response.Redirect(Page.ResolveClientUrl("~/ProjectInfo/ProjectInfoForm?id=" + id + "&isSaveSuccess=true"));
                    }
                    else
                    {
                        ShowErrorMessage(result.Message);
                    }
                }                
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        private void DisplayRadioButtonOrganiaztionType(decimal? typeId, string typeEtc)
        {    
            RadioButtonOrganizationType1.Checked = false;
            RadioButtonOrganizationType2.Checked = false;
            RadioButtonOrganizationType3.Checked = false;
            RadioButtonOrganizationType4.Checked = false;
            RadioButtonOrganizationType5.Checked = false;
            RadioButtonOrganizationType6.Checked = false;
            RadioButtonOrganizationType7.Checked = false;

            TextBoxOrganizationType1.Text = "";
            TextBoxOrganizationType2.Text = "";
            TextBoxOrganizationType7.Text = "";

            if (typeId != null)
            {
                decimal id = (decimal)typeId;


                if (id == Common.OrganizationTypeID.สังกัดกรม)
                {
                    RadioButtonOrganizationType1.Checked = true;
                    TextBoxOrganizationType1.Text = typeEtc;
                }
                else if (id == Common.OrganizationTypeID.กระทรวง)
                {
                    RadioButtonOrganizationType2.Checked = true;
                    TextBoxOrganizationType2.Text = typeEtc;
                    CommitteeControl.IsEnabled = false;

                }
                else if (id == Common.OrganizationTypeID.องค์กรปกครองส่วนท้องถิ่น)
                {
                    RadioButtonOrganizationType3.Checked = true;
                }
                else if (id == Common.OrganizationTypeID.องค์กรด้านคนพิการ)
                {
                    RadioButtonOrganizationType4.Checked = true;
                }
                else if (id == Common.OrganizationTypeID.องค์กรชุมชน)
                {
                    RadioButtonOrganizationType5.Checked = true;
                }
                else if (id == Common.OrganizationTypeID.องค์กรธุรกิจ)
                {
                    RadioButtonOrganizationType6.Checked = true;
                }
                else if (id == Common.OrganizationTypeID.อื่นๆ)
                {
                    RadioButtonOrganizationType7.Checked = true;
                    TextBoxOrganizationType7.Text = typeEtc;
                }
            }            
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

            TextBoxOrganizationType1.Text = "";
            TextBoxOrganizationType2.Text = "";
            TextBoxOrganizationType7.Text = "";
           
        }

        //private void BindComboBoxOrganization(decimal? provinceID)
        //{            
        //    List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
        //    list = _service.ListOrganization(provinceID);
        //    list.Insert(0, new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.DropdownPleaseSelect, Value=""});
        //    ComboBoxOrganizationNameTH.DataSource = list;
        //    .ComboBoxOrganizationNameTH.DataBind();

        //    if (UserInfo.OrganizationID.HasValue)
        //    {
        //        ComboBoxOrganizationNameTH.SelectedValue = UserInfo.OrganizationID.ToString();
        //        ComboBoxOrganizationNameTH.Enabled = false;

        //        BindOrganizationInfo(UserInfo.OrganizationID);
        //    }
        //    else
        //    {
        //        ComboBoxOrganizationNameTH.SelectedIndex = 0;
        //    }
        //}

        protected void CustomValidatorSupport_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string value = rbLstIsFirstFlag.SelectedValue;
            bool isValid = true;
            if ((value.ToLower() == "yes") && (String.IsNullOrEmpty(args.Value)))
            {
                isValid = false;
            }

            args.IsValid = isValid;
        }

        protected void CustomValidatorSupportDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DatePickerSupportYear.SelectedDate.HasValue && DatePickerTogotSupportYear.SelectedDate.HasValue)
            {
                int startY = ((DateTime)DatePickerSupportYear.SelectedDate).Year;
                int endY = ((DateTime)DatePickerTogotSupportYear.SelectedDate).Year;                
                args.IsValid = (endY >= startY);
            }
        }

        //protected void ComboBoxOrganizationProvince_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string seletedProvince = ComboBoxOrganizationProvince.SelectedValue;
        //    decimal provinceId = 0;
        //    Decimal.TryParse(seletedProvince, out provinceId);
        //    if (provinceId > 0)
        //    {
        //        if(!UserInfo.OrganizationID.HasValue){
        //            BindComboBoxOrganization(provinceId);
        //            ComboBoxOrganizationNameTH.Enabled = true;
        //        }                
        //    }
        //    else
        //    {
        //        BindComboBoxOrganization((decimal?)null);
        //        ComboBoxOrganizationNameTH.Enabled = false;
        //    }            
        //}

        private void RegisterClientScriptBlock()
        {

            String script = @"
                 $(function () {
                    handelProjectIsGotSupport();
                });";

            ScriptManager.RegisterStartupScript(
                      UpdatePanelCreate,
                      this.GetType(),
                      "HandelProjectIsGotSupport",
                      script,
                      true);
        }

       

        //protected void DdlOrganizationProvince_TextChanged(object sender, EventArgs e)
        //{
        //    string seletedProvince = DdlOrganizationProvince.Text;
        //    decimal provinceId = 0;
        //    Decimal.TryParse(seletedProvince, out provinceId);
        //    if (provinceId > 0)
        //    {
        //        if (!UserInfo.OrganizationID.HasValue)
        //        {
        //            OrganizationNameTHEnable = true;
        //            //BindComboBoxOrganization(provinceId);
        //            //ComboBoxOrganizationNameTH.Enabled = true;
        //        }
        //    }
        //    else
        //    {
        //        OrganizationNameTHEnable = false;
        //        //BindComboBoxOrganization((decimal?)null);
        //        //ComboBoxOrganizationNameTH.Enabled = false;
        //    }  
        //}


        private void LoadOrgInformation()
        {
            string selectedValue = DdlOrganizationNameTH.Text;
            decimal organizationID = 0;
            bool tryParse = Decimal.TryParse(selectedValue, out organizationID);     
            decimal? orgId = (tryParse) ? (decimal?)organizationID : (decimal?)null;

            BindOrganizationInfo(orgId);   
        }


        private void RegisterComboboxScript()
        {
            string provinceOnChange = (UserInfo.OrganizationID.HasValue) ? "" : "Change: function(e){onDdlOrganizationProvince('" + DdlOrganizationNameTH.ClientID + "',e);},";
            string orgDataBound = (UserInfo.OrganizationID.HasValue) ? "DataBound:function (e) { isComboboxEditable('', false); }," : ""; 
            string provinceSelected = DdlOrganizationProvince.Text;
            string orgSelected = (UserInfo.OrganizationID.HasValue)? UserInfo.OrganizationID.ToString() : DdlOrganizationNameTH.Text;
            string orgAutoBind = (UserInfo.IsProvinceOfficer || (orgSelected != "")) ? "true" : "false";

            String script = @"
                $(function () {                 
                    
                    c2x.createLocalCombobox({                       
                        ControlID: '" + DdlOrganizationProvince.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        Enable:true,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,                      
                        " + provinceOnChange + @"
                        Value: '" + provinceSelected + @"',   
                        Data:{Data:" + Nep.Project.Common.Web.WebUtility.ToJSON(GetOrganizationProvince()) + @"},
                        Enable:" + Nep.Project.Common.Web.WebUtility.ToJSON(OrganizationProvinceEnable) + @" ,                                 
                     });  

                  c2x.createVirtualCombobox({      
                        AutoBind:" + orgAutoBind+@",                 
                        ControlID: '" + DdlOrganizationNameTH.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'"+DdlOrganizationProvince.ClientID+ @"',
                        ParentValue:'"+ provinceSelected + @"',
                        TextField: 'Text',
                        ValueField: 'Value',                       
                        ReadUrl: './ComboboxHandler/getorg',
                        VirtualUrl: './ComboboxHandler/orgvaluemapper',                     
                        " + orgDataBound + @"                       
                        Value: '"+ orgSelected + @"',                      
                     
                        Change: function(e){reloadPanelCreate('loadorginfo');},
                        Enable:" + Nep.Project.Common.Web.WebUtility.ToJSON((OrganizationNameTHEnable && (provinceSelected != ""))) + @",
                        Param: function(){return getOrgComboboxParam('" + DdlOrganizationProvince.ClientID + @"', '" + CenterProvinceID + @"');},
                     });  

                   
                });                

                function onDdlOrganizationProvince(ddlOrgID, e){
                    var item = e.sender.dataItem();
                    var ddlOrg = $('#' + ddlOrgID).data('kendoComboBox');                   

                    if (item != null) {
           
                        var provinceID = parseInt(item.Value, 10);            
                        var dataSource = ddlOrg.dataSource;            

                        ddlOrg.select(-1);
                        ddlOrg.enable(true);      
                          
                        ddlOrg.dataSource.read().then(function () {                            
                            ddlOrg.focus();
                        });

                       
                    } else {           

                        ddlOrg.select(-1);
                        ddlOrg.enable(false);
                    }
                }

                function isComboboxEditable(comboboxID, enable){
                     var ddl = $('#' + comboboxID).data('kendoComboBox');
                     if(ddl != null){
                        ddl.enable(enable);
                     }
                     
                }

                function getOrgComboboxParam(parentId, provinceCenter){
                     var ddl = $('#' + parentId).data('kendoComboBox');
                     var selectItem = ddl.dataItem();                    
                     if (selectItem != null) {
                        var value = (selectItem.Value != provinceCenter)? selectItem.Value : '';
                        return { parentid: value};
                     }
                }

                function reloadPanelCreate(param) {                               
                    
                    var p = (typeof(param) != 'undefined')? param : '';
                    __doPostBack('" + UpdatePanelCreate.UniqueID + @"', p);
                } 

            ";
            ScriptManager.RegisterStartupScript(
                      UpdatePanelCreate,
                      this.GetType(),
                      "ManageUpdatePanelCreate",
                      script,
                      true);   
        }

        protected void CustomValidatorCombobox_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String value = args.Value;
            int id = 0;
            Int32.TryParse(value, out id);

            args.IsValid = (id > 0);
        }       
       
    }
}