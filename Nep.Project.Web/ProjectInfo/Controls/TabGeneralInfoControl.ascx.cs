using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using AjaxControlToolkit;
using Nep.Project.Common;
using Nep.Project.ServiceModels;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabGeneralInfoControl : Nep.Project.Web.Infra.BaseUserControl
    {
        public IServices.IProjectInfoService _service { get; set; }
        public IServices.IProviceService _provinceService { get; set; }

        public Decimal ProjectID
        {
            get
            {
                if (ViewState["ProjectID"] == null)
                {
                    string stID = Request.QueryString["id"];
                    decimal id = 0;
                    Decimal.TryParse(stID, out id);
                    ViewState["ProjectID"] = id;
                }


                return (decimal)ViewState["ProjectID"];
            }
        }

        public List<String> RequiredSubmitData
        {
            get
            {
                List<string> dataName = null;
                if (ViewState["RequiredSubmitData"] != null)
                {
                    dataName = (List<string>)ViewState["RequiredSubmitData"];
                }

                return dataName;
            }

            set
            {
                ViewState["RequiredSubmitData"] = value;
            }
        }

        public void BindData()
        {
            ComboBoxOrganizationProvince.Enabled = UserInfo.IsAdministrator;
            ComboBoxOrganizationNameTH.Enabled = UserInfo.IsAdministrator;
            BindRadioButtonOrganiaztionType();
            OrgAssistanceControl.DataBind();
            CommitteeControl.DataBind();
           
            ButtonSendProjectInfo.Text = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Nep.Project.Resources.UI.ButtonSubmit : Nep.Project.Resources.UI.ButtonSendProjectInfo;

            decimal projectID = ProjectID;
            if (projectID > 0)
            {
               
                ServiceModels.ProjectInfo.OrganizationInfo model = new ServiceModels.ProjectInfo.OrganizationInfo();
                var result = _service.GetProjectGeneralInfoByProjectID(projectID);
                if (result.IsCompleted)
                {
                    model = result.Data;
                 
                    BindOrganizationNameTH(model.ProvinceID);
                   
                    SetDefaultDisplay(model);
                   
                }
            }
            CommitteeControl.RefreshPosition();
            RegisterClientScriptBlock();
        }

     
        #region Server Validate
        protected void CustomValidatorOrganizationProvince_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int selectedIndex = ComboBoxOrganizationProvince.SelectedIndex;
            args.IsValid = (selectedIndex < 0) ? false : true;
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
        public List<ServiceModels.GenericDropDownListData> GetOrganizationProvince()
        {
            String filter = ComboBoxOrganizationProvince.Text;

            var result = _provinceService.ListOrgProvince(filter);
            if (result.IsCompleted)
            {
                return result.Data;
            }
            else
            {
                return new List<ServiceModels.GenericDropDownListData>();
            }
        }

        private void BindOrganizationNameTH(decimal provinceID)
        {
            List<ServiceModels.DecimalDropDownListData> list = new List<ServiceModels.DecimalDropDownListData>();
            list = _service.ListOrganization(provinceID);
            ComboBoxOrganizationNameTH.DataSource = list;
            ComboBoxOrganizationNameTH.DataBind();
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

        public List<ServiceModels.GenericDropDownListData> GetDistrict()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            string filter = ComboBoxDistrict.Text;
            string provinceValue = ComboBoxProvince.SelectedValue;
            int? provinceId = null;

            if (!string.IsNullOrEmpty(provinceValue))
            {
                provinceId = Convert.ToInt32(provinceValue);
                var result = _provinceService.ListDistrict(provinceId, filter);
                if (result.IsCompleted)
                    list = result.Data;
            }


            return list;
        }

        public List<ServiceModels.GenericDropDownListData> GetSubDistrict()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            string filter = ComboBoxSubDistrict.Text;
            string districtValue = ComboBoxDistrict.SelectedValue;
            int? districtId = null;

            if (!string.IsNullOrEmpty(districtValue))
            {
                districtId = Convert.ToInt32(districtValue);
                var result = _provinceService.ListSubDistrict(districtId, filter);
                if (result.IsCompleted)
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
                //Validate Committee

                //Page.Validate("SaveGeneralInfo");

                if (Page.IsValid)
                {                  
                   
                    decimal projectID = ProjectID;

                    if (projectID > 0)
                    {
                        var mainData = _service.GetProjectGeneralInfoByProjectID(projectID);
                        if (mainData.IsCompleted)
                        {
                            model.Committees = CommitteeControl.GetDataEditingCommittee();

                            // Get Assistance
                            List<ServiceModels.ProjectInfo.OrganizationAssistance> listAssistance = OrgAssistanceControl.GetDataEditingOrgAssistance();
                            if (listAssistance.Count > 0)
                            {
                                model.Assistances = listAssistance;
                            }
                            else
                            {
                                model.Assistances = null;
                            }

                            ServiceModels.ProjectInfo.OrganizationInfo d = mainData.Data;
                            model.ProjectID = projectID;
                            model.OrganizationID = d.OrganizationID;
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

                        decimal provinceId = Convert.ToDecimal(ComboBoxOrganizationProvince.SelectedValue);
                        bool supportFlag = (rbLstIsFirstFlag.SelectedItem.Value == "Yes") ? true : false;
                        decimal? supportTime = null;
                        if (!string.IsNullOrEmpty(TextBoxPromoteAmount.Text.Trim()))
                            supportTime = Convert.ToDecimal(TextBoxPromoteAmount.Text.Trim());

                        string purpose = TextBoxOrganizationObjective.Text.TrimEnd();
                        string currentProject = TextBoxActivityCurrent.Text.TrimEnd();
                        string currentProjectResult = TextBoxWorkingYear.Text.TrimEnd();
                        string gotSupportYear = (DatePickerPromoteYear.SelectedDate.HasValue) ? ((DateTime)DatePickerPromoteYear.SelectedDate).Year.ToString() : null;
                        string toGotSupportYear = (DatePickerTogotSupportYear.SelectedDate.HasValue) ? ((DateTime)DatePickerTogotSupportYear.SelectedDate).Year.ToString() : null;
                        string gotSupportLastProject = TextBoxProjectLasted.Text.Trim();
                        string gotSupportLastResult = TextBoxProjectLastedResult.Text.TrimEnd();
                        string gotSupportLastProblems = TextBoxProblem.Text.TrimEnd();
                        decimal orgId = Convert.ToDecimal(ComboBoxOrganizationNameTH.SelectedValue);
                        model.ProvinceID = provinceId;
                        model.OrganizationID = orgId;
                        model.Purpose = purpose;
                        model.CurrentProject = currentProject;
                        model.CurrentProjectResult = currentProjectResult;
                        model.GotSupportFlag = supportFlag;
                        if(supportFlag){
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

                      
                        string projectNameEN = TextBoxOrganizationNameEN.Text.Trim();

                        var resutl = _service.Update(model);
                        if (resutl.IsCompleted)
                        {
                            ShowResultMessage(resutl.Message);

                            if (!supportFlag)
                            {
                                DatePickerPromoteYear.Clear();
                                DatePickerTogotSupportYear.Clear();
                                TextBoxPromoteAmount.Text = "";
                                TextBoxProjectLasted.Text = "";
                                TextBoxProjectLastedResult.Text = "";
                                TextBoxProblem.Text = "";
                            }
                        }
                        else
                        {
                            ShowErrorMessage(resutl.Message);
                        }
                    }
                    else
                    {
                        string err = string.Format(Resources.Error.RequiredField, "ProjectID");
                        ShowErrorMessage(err);
                    }
                   
                }

                RegisterClientScriptBlock();
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Decimal projectId = ProjectID;                
                if (projectId > 0)
                {
                    var result = _service.DeleteProject(projectId);
                    if (result.IsCompleted)
                    {
                        Response.Redirect(Page.ResolveClientUrl("~/ProjectInfo/ProjectInfoList.aspx?isDeleteSuccess=true"));
                    }
                    else
                        ShowErrorMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        private void SetDefaultDisplay(ServiceModels.ProjectInfo.OrganizationInfo model)
        {
            
                //Manage Function
                bool canSendProjectInfo = (model.RequiredSubmitData == null);
            RequiredSubmitData = model.RequiredSubmitData;
            

                List<Common.ProjectFunction> functions = _service.GetProjectFunction(model.ProjectID).Data;
            //kenghot
            //bool cansave = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData));
            var master = (this.Page.Master as MasterPages.SiteMaster);
            bool cansave = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData)
                || master.UserInfo.IsAdministrator);
            
          
                //end kenghot
                ButtonSave.Visible = cansave;
                CommitteeControl.IsEnabled = cansave;
                ButtonDraft.Visible = ButtonSave.Visible;
                ButtonSendProjectInfo.Visible = (functions.Contains(Common.ProjectFunction.SaveDarft) && canSendProjectInfo);
                ButtonDelete.Visible = functions.Contains(Common.ProjectFunction.Delete);
                HyperLinkPrint.Visible = (functions.Contains(Common.ProjectFunction.PrintDataForm) && canSendProjectInfo);
                ButtonReject.Visible =   functions.Contains(Common.ProjectFunction.Reject);
                int organizationYear = 0;
                Int32.TryParse(model.OrganizationYear, out organizationYear);

                ComboBoxOrganizationProvince.SelectedValue = model.ProvinceID.ToString();
                ComboBoxOrganizationNameTH.SelectedValue = model.OrganizationID.ToString();
                TextBoxOrganizationNameEN.Text = model.OrganizationNameEN;
                DisplayRadioButtonOrganiaztionType(model.OrganizationTypeID, model.OrganizationTypeEtc);
                TextBoxOrgUnderSupport.Text = model.OrgUnderSupport;
                TextBoxRegisterYear.Text = (organizationYear > 0) ? (organizationYear + 543).ToString() : model.OrganizationYear;

                if (model.OrgEstablishedDate.HasValue)
                {
                    OrganizationRegisterDateLabel.Visible = true;
                    OrganizationRegisterDateControl.Visible = true;
                    DateTime tmpDate = (DateTime)model.OrgEstablishedDate;
                    TextBoxRegisterDate.Text = Common.Web.WebUtility.DisplayInHtml(tmpDate.ToString(Common.Constants.UI_FORMAT_DATE, Common.Constants.UI_CULTUREINFO), "", "-");
                }

                TextBoxAddressNo.Text = model.Address;
                TextBoxBuilding.Text = model.Building;
                TextBoxMoo.Text = model.Moo;
                TextBoxSoi.Text = model.Soi;
                TextBoxStreet.Text = model.Road;
                ComboBoxProvince.SelectedValue = model.AddressProvinceID.ToString();
            ComboBoxDistrict.DataSource = GetDistrict2((int)model.AddressProvinceID);
           //ComboBoxDistrict.DataSource = GetDistrict();
            ComboBoxDistrict.DataBind();
            ComboBoxDistrict.SelectedValue = model.DistrictID.ToString();


            ComboBoxSubDistrict.DataSource = GetSubDistrict2((int)model.DistrictID);
            //ComboBoxSubDistrict.DataSource = GetSubDistrict();
            ComboBoxSubDistrict.DataBind();
            ComboBoxSubDistrict.SelectedValue = model.SubDistrictID.ToString();
           // ComboBoxDistrict.SelectedValue = model.DistrictID.ToString();
              //  ComboBoxSubDistrict.SelectedValue = model.SubDistrictID.ToString();
                TextBoxPostCode.Text = model.Postcode;
                TextBoxTelephone.Text = model.Telephone;
                TextBoxTelephone.Text = model.Telephone;
                TextBoxMobile.Text = model.Mobile;
                TextBoxFax.Text = model.Fax;
                TextBoxEmail.Text = model.Email;
                TextBoxOrganizationObjective.Text = model.Purpose;
                TextBoxActivityCurrent.Text = model.CurrentProject;
                TextBoxWorkingYear.Text = model.CurrentProjectResult;
                rbLstIsFirstFlag.SelectedValue = (model.GotSupportFlag == true) ? "Yes" : "No";
                DatePickerPromoteYear.SelectedDate = (!String.IsNullOrEmpty(model.GotSupportYear)) ? new DateTime(Int32.Parse(model.GotSupportYear), 1, 1) : (DateTime?)null;
                DatePickerTogotSupportYear.SelectedDate = (!String.IsNullOrEmpty(model.TogotSupportYear)) ? new DateTime(Int32.Parse(model.TogotSupportYear), 1, 1) : (DateTime?)null;
                TextBoxPromoteAmount.Text = model.GotSupportTimes.ToString();
                TextBoxProjectLasted.Text = model.GotSupportLastProject;
                TextBoxProjectLastedResult.Text = model.GotSupportLastResult;
                TextBoxProblem.Text = model.GotSupportLastProblems;

                CommitteeControl.BindRepeaterCommittee(model.Committees, model.OrganizationID, (decimal)model.OrganizationTypeID);
                OrgAssistanceControl.BindRepeaterOrgAssistance(model.Assistances);

       
      
        
            
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

        private Boolean ValidateOrganizationCommittee(List<ServiceModels.ProjectInfo.Committee> list)
        {
            bool result = false;
            List<ServiceModels.ProjectInfo.Committee> listCommittee = new List<ServiceModels.ProjectInfo.Committee>();
            bool hasPosition1 = false;
            bool hasPosition2 = false;
            bool hasPosition3 = false;

            listCommittee = list;
            foreach (var item in listCommittee)
            {
                if (item.CommitteePosition == "1")
                    hasPosition1 = true;

                if (item.CommitteePosition == "2")
                    hasPosition2 = true;

                if (item.CommitteePosition == "3")
                    hasPosition3 = true;
            }

            if (hasPosition1 == true && hasPosition2 == true && hasPosition3 == true)
                result = true;

            return result;
        }

        protected void ButtonSendProjectInfo_Click(object sender, EventArgs e)
        {            
            decimal projectID = ProjectID;
            if(projectID > 0){
                //var result = _service.ValidateSubmitData(projectID);
                //if (result.IsCompleted)
                //{
                    var sendDataToReviewResult = _service.SendDataToReview(projectID,Request.UserHostAddress);
                    if (sendDataToReviewResult.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelGeneralInfo");

                        string message = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Message.SubmitDataToReviewSuccess : Message.SendDataToReviewSuccess;
                        ShowResultMessage(message);                       

                    }
                    else
                    {
                        ShowErrorMessage(sendDataToReviewResult.Message[0]);
                    }
                //}
                //else
                //{
                //    ShowErrorMessage(result.Message);
                //}
            }

            RegisterClientScriptBlock();
        }

        protected void CustomValidatorSupport_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string value = rbLstIsFirstFlag.SelectedValue;
            bool isValid = true;
            if((value.ToLower() == "yes") && (String.IsNullOrEmpty(args.Value))){
                isValid = false;
            }

            args.IsValid = isValid;
        }

        protected void CustomValidatorSupportDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DatePickerPromoteYear.SelectedDate.HasValue && DatePickerTogotSupportYear.SelectedDate.HasValue)
            {
                int startY = ((DateTime)DatePickerPromoteYear.SelectedDate).Year;
                int endY = ((DateTime)DatePickerTogotSupportYear.SelectedDate).Year;
                args.IsValid = (endY >= startY);
            }
        }


        private void RegisterClientScriptBlock()
        {

            String script = @"
                 $(function () {      
                    SetTabHeader(" + Common.Web.WebUtility.ToJSON(RequiredSubmitData) + @");
                    handelProjectIsGotSupport();
                 });
            
            ";
            ScriptManager.RegisterStartupScript(
                      UpdatePanelGeneralInfoControl,
                      this.GetType(),
                      "UpdatePanelGeneralInfoControl",
                      script,
                      true);
        }
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

                    if (data.OrgEstablishedDate.HasValue)
                    {
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

                    ComboBoxDistrict.DataSource = GetDistrict2((int)data.AddressProvinceID);
                    //ComboBoxDistrict.DataSource = GetDistrict();
                    ComboBoxDistrict.DataBind();
                    ComboBoxDistrict.SelectedValue = data.DistrictID.ToString();


                    ComboBoxSubDistrict.DataSource = GetSubDistrict2((int)data.DistrictID);
                    //ComboBoxSubDistrict.DataSource = GetSubDistrict();
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
        public List<ServiceModels.GenericDropDownListData> GetDistrict2(int provinceID)
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

        public List<ServiceModels.GenericDropDownListData> GetSubDistrict2(int districtID)
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
        protected void ComboBoxOrganizationProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxOrganizationProvince.Enabled = UserInfo.IsAdministrator;
            ComboBoxOrganizationNameTH.Enabled = UserInfo.IsAdministrator;
            BindRadioButtonOrganiaztionType();
            OrgAssistanceControl.DataBind();
            CommitteeControl.DataBind();

            ButtonSendProjectInfo.Text = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Nep.Project.Resources.UI.ButtonSubmit : Nep.Project.Resources.UI.ButtonSendProjectInfo;

            decimal projectID = ProjectID;
            if (projectID > 0)
            {

                ServiceModels.ProjectInfo.OrganizationInfo model = new ServiceModels.ProjectInfo.OrganizationInfo();
                var result = _service.GetProjectGeneralInfoByProjectID(projectID);
                if (result.IsCompleted)
                {
                    model = result.Data;
                    decimal d = 0;
                    decimal.TryParse(ComboBoxOrganizationProvince.SelectedValue,out d);
                    BindOrganizationNameTH(d);

                    //SetDefaultDisplay(model);

                }
            }
            CommitteeControl.RefreshPosition();
            RegisterClientScriptBlock();
        }

        protected void ComboBoxOrganizationNameTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxOrganizationProvince.Enabled = UserInfo.IsAdministrator;
            ComboBoxOrganizationNameTH.Enabled = UserInfo.IsAdministrator;
            BindRadioButtonOrganiaztionType();
            OrgAssistanceControl.DataBind();
            CommitteeControl.DataBind();

            ButtonSendProjectInfo.Text = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Nep.Project.Resources.UI.ButtonSubmit : Nep.Project.Resources.UI.ButtonSendProjectInfo;

            string selectedValue = ComboBoxOrganizationNameTH.SelectedValue;
            decimal organizationID = 0;
            bool tryParse = Decimal.TryParse(selectedValue, out organizationID);
            decimal? orgId = (tryParse) ? (decimal?)organizationID : (decimal?)null;

            BindOrganizationInfo(orgId);
            CommitteeControl.RefreshPosition();
            RegisterClientScriptBlock();
        }
    }
}