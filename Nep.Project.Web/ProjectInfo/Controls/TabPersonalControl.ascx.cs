using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.ServiceModels;
using Nep.Project.Common;
using AjaxControlToolkit;
using System.IO;
using Nep.Project.Resources;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabPersonalControl : Nep.Project.Web.Infra.BaseUserControl
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

            set
            {
                ViewState["ProjectID"] = value;
            }
        }

        public Decimal BudgetYear
        {
            get
            {
                decimal budgetYear = 0;
                if (ViewState["BudgetYear"] != null)
                {
                    budgetYear = Decimal.Parse(ViewState["BudgetYear"].ToString());
                }


                return budgetYear;
            }

            set
            {
                ViewState["BudgetYear"] = value;
            }
        }

        public String FollowupViewAttachmentPrefix
        {
            get
            {
                if (ViewState["PersonalViewAttachmentPrefix"] == null)
                {
                    string prefix = "Project/" + ProjectID;
                    ViewState["PersonalViewAttachmentPrefix"] = prefix;
                }
                return ViewState["PersonalViewAttachmentPrefix"].ToString();
            }

            set
            {
                ViewState["PersonalViewAttachmentPrefix"] = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            //string compare = "TabPersonal_PersonalControl";
            //string target = Request["__EVENTTARGET"];

            
            //string compareCheckBoxDupData1 = "CheckBoxDupData1";
            //string compareCheckBoxDupData2 = "CheckBoxDupData2";
            
            //string scriptManager = Request.Form["ctl00$ScriptManager1"];

            //if ((IsPostBack) && ((!String.IsNullOrEmpty(target) && (target.Contains(compare))) ||
            //    (scriptManager != null && (scriptManager.Contains(compareCheckBoxDupData1) || scriptManager.Contains(compareCheckBoxDupData2)))))
            //{
            //    RegisterRequiredData();                
            //}
            
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            #region for old data
            bool isRequired = (BudgetYear > 2016) ? true : false;            
            LabelIDCardNoSign.Visible = isRequired;
            CustomValidatorIDCardNo.Enabled = isRequired;

            LabelEmail1Sign.Visible = isRequired;
            RequiredFieldValidatorEmail1.Enabled = isRequired;

            LabelEmail2Sign.Visible = isRequired;
            RequiredFieldValidatorEmail2.Enabled = isRequired;

            CustomValidatorEmail3.Enabled = isRequired;
            #endregion for old data


            RegisterClientScript();
        }
       

        public void BindData()
        {
            decimal projectID = ProjectID;
            if (projectID > 0)
            {
                HiddenProjectID.Value = projectID.ToString();
                ButtonSendProjectInfo.Text = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Nep.Project.Resources.UI.ButtonSubmit : Nep.Project.Resources.UI.ButtonSendProjectInfo;

                List<ServiceModels.GenericDropDownListData> listPrefix = GetPrefix();
                //Title Name Prefix
                DropDownListPrefix1.DataSource = listPrefix;
                DropDownListPrefix1.DataTextField = "Text";
                DropDownListPrefix1.DataValueField = "Value";
                DropDownListPrefix1.DataBind();

                DropDownListPrefix2.DataSource = listPrefix;
                DropDownListPrefix2.DataTextField = "Text";
                DropDownListPrefix2.DataValueField = "Value";
                DropDownListPrefix2.DataBind();

                DropDownListPrefix3.DataSource = listPrefix;
                DropDownListPrefix3.DataTextField = "Text";
                DropDownListPrefix3.DataValueField = "Value";
                DropDownListPrefix3.DataBind();
                //----------------------------------------------

              
                var result = _service.GetProjectPersonalByProjectID(projectID);
                if (result.IsCompleted)
                {
                    ServiceModels.ProjectInfo.TabPersonal model = result.Data;
                    if (model != null)
                    {
                        DisplayIDCardNo(model.IDCardNo);
                        DisplayDataPersonal(model);
                    }
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }           
           
        }

        protected void ProjectPersonalAddress3Validate(object source, ServerValidateEventArgs args)
        {
           
            String firstName3 = TextBoxFirstName3.Text;
            args.IsValid = (!String.IsNullOrEmpty(firstName3) && (String.IsNullOrEmpty(args.Value)))? false : true;

            //if (String.IsNullOrEmpty(firstName3) && (!String.IsNullOrEmpty(args.Value)))
            //{
            //    CustomValidatorFirstName3.IsValid = false;
            //}

            //CustomValidatorFirstName3.IsValid = false;
            //CustomValidatorLastName3.IsValid = false;
            //CustomValidatorProvince3.IsValid = false;
            //CustomValidatorSubDistrict3.IsValid = false;
            //CustomValidatorDistrict3.IsValid = false;
            //CustomValidatorPostCode3.IsValid = false;
        }

        protected void ButtonSendProjectInfo_Click(object sender, EventArgs e)
        {
            
            decimal projectID = ProjectID;
            if (projectID > 0)
            {
                //var result = _service.ValidateSubmitData(projectID);
                //if (result.IsCompleted)
                //{
                    var sendDataToReviewResult = _service.SendDataToReview(projectID,Request.UserHostAddress);
                    if (sendDataToReviewResult.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPersonal");
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
        }

        #region Management ComboBox
        public List<ServiceModels.GenericDropDownListData> GetPrefix()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            try
            {
                list = _service.ListPrefix();
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
                ShowErrorMessage(ex.Message);
            }

            return list;
        }

        public List<ServiceModels.GenericDropDownListData> GetProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            try
            {               

                var result = _provinceService.ListProvince(null);
                if (result.IsCompleted)
                    list = result.Data;
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
                ShowErrorMessage(ex.Message);
            }

            return list;
        }

        //public List<ServiceModels.GenericDropDownListData> GetProvince1()
        //{
        //    List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
        //    try
        //    {
        //        string filter = ComboBoxProvince1.Text;

        //        var result = _provinceService.ListProvince(filter);
        //        if (result.IsCompleted)                    
        //            list = result.Data;
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
        //        ShowErrorMessage(ex.Message);
        //    }

        //    return list;
        //}

        //public List<ServiceModels.GenericDropDownListData> GetProvince2()
        //{
        //    List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
        //    try
        //    {
        //        string filter = ComboBoxProvince2.Text;

        //        var result = _provinceService.ListProvince(filter);
        //        if (result.IsCompleted)                    
        //            list = result.Data;
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
        //        ShowErrorMessage(ex.Message);
        //    }

        //    return list;
        //}

        //public List<ServiceModels.GenericDropDownListData> GetProvince3()
        //{
        //    List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
        //    try
        //    {
        //        string filter = ComboBoxProvince3.Text;

        //        var result = _provinceService.ListProvince(filter);
        //        if (result.IsCompleted)                    
        //            list = result.Data;
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
        //        ShowErrorMessage(ex.Message);
        //    }

        //    return list;
        //}

        //protected void ComboBoxProvince1_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = ComboBoxProvince1.SelectedIndex;
        //        string value = ComboBoxProvince1.SelectedValue;
        //        int provinceID = 0;
        //        bool tryParseId = Int32.TryParse(value, out provinceID);

        //        if ((selectedIndex >= 0) && (tryParseId))
        //        {
        //            BindComboBoxDistrict1(provinceID);
        //        }
        //        else
        //        {
        //            ComboBoxDistrict1.SelectedIndex = -1;
        //            ComboBoxDistrict1.Enabled = false;

        //            ComboBoxSubDistrict1.SelectedIndex = -1;
        //            ComboBoxSubDistrict1.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
        //        ShowErrorMessage(ex.Message);
        //    }
        //}

        //protected void ComboBoxProvince2_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = ComboBoxProvince2.SelectedIndex;
        //        string value = ComboBoxProvince2.SelectedValue;
        //        int provinceID = 0;
        //        bool tryParseId = Int32.TryParse(value, out provinceID);

        //        if ((selectedIndex >= 0) && (tryParseId))
        //        {
        //            BindComboBoxDistrict2(provinceID);
        //        }
        //        else
        //        {
        //            ComboBoxDistrict2.SelectedIndex = -1;
        //            ComboBoxDistrict2.Enabled = false;

        //            ComboBoxSubDistrict2.SelectedIndex = -1;
        //            ComboBoxSubDistrict2.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
        //        ShowErrorMessage(ex.Message);
        //    }
        //}

        //protected void ComboBoxProvince3_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = ComboBoxProvince3.SelectedIndex;
        //        string value = ComboBoxProvince3.SelectedValue;
        //        int provinceID = 0;
        //        bool tryParseId = Int32.TryParse(value, out provinceID);

        //        if ((selectedIndex >= 0) && (tryParseId))
        //        {
        //            BindComboBoxDistrict3(provinceID);
        //        }
        //        else
        //        {
        //            ComboBoxDistrict3.SelectedIndex = -1;
        //            ComboBoxDistrict3.Enabled = false;

        //            ComboBoxSubDistrict3.SelectedIndex = -1;
        //            ComboBoxSubDistrict3.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
        //        ShowErrorMessage(ex.Message);
        //    }
        //}

        //protected void ComboBoxDistrict1_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = ComboBoxDistrict1.SelectedIndex;
        //        string value = ComboBoxDistrict1.SelectedValue;
        //        int districtID = 0;
        //        bool tryParseId = Int32.TryParse(value, out districtID);

        //        if ((selectedIndex >= 0) && (tryParseId))
        //        {
        //            BindComboBoxSubDistrict1(districtID);
        //        }
        //        else
        //        {
        //            ComboBoxSubDistrict1.SelectedIndex = -1;
        //            ComboBoxSubDistrict1.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
        //        ShowErrorMessage(ex.Message);
        //    }
        //}

        //protected void ComboBoxDistrict2_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = ComboBoxDistrict2.SelectedIndex;
        //        string value = ComboBoxDistrict2.SelectedValue;
        //        int districtID = 0;
        //        bool tryParseId = Int32.TryParse(value, out districtID);

        //        if ((selectedIndex >= 0) && (tryParseId))
        //        {
        //            BindComboBoxSubDistrict2(districtID);
        //        }
        //        else
        //        {
        //            ComboBoxSubDistrict2.SelectedIndex = -1;
        //            ComboBoxSubDistrict2.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
        //        ShowErrorMessage(ex.Message);
        //    }
        //}

        //protected void ComboBoxDistrict3_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = ComboBoxDistrict3.SelectedIndex;
        //        string value = ComboBoxDistrict3.SelectedValue;
        //        int districtID = 0;
        //        bool tryParseId = Int32.TryParse(value, out districtID);

        //        if ((selectedIndex >= 0) && (tryParseId))
        //        {
        //            BindComboBoxSubDistrict3(districtID);
        //        }
        //        else
        //        {
        //            ComboBoxSubDistrict3.SelectedIndex = -1;
        //            ComboBoxSubDistrict3.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
        //        ShowErrorMessage(ex.Message);
        //    }
        //}

        //private void BindComboBoxDistrict1(Int32 provinceId)
        //{
        //    var result = _provinceService.ListDistrict(provinceId, string.Empty);
        //    if (result.IsCompleted)
        //    {
        //        ComboBoxDistrict1.Enabled = true;
        //        ComboBoxDistrict1.DataSource = result.Data;
        //        ComboBoxDistrict1.DataBind();

        //        ComboBoxSubDistrict1.SelectedIndex = -1;
        //        ComboBoxSubDistrict1.Enabled = false;
        //    }
        //}

        //private void BindComboBoxDistrict2(Int32 provinceId)
        //{
        //    var result = _provinceService.ListDistrict(provinceId, string.Empty);
        //    if (result.IsCompleted)
        //    {
        //        ComboBoxDistrict2.Enabled = true;
        //        ComboBoxDistrict2.DataSource = result.Data;
        //        ComboBoxDistrict2.DataBind();

        //        ComboBoxSubDistrict2.SelectedIndex = -1;
        //        ComboBoxSubDistrict2.Enabled = false;
        //    }
        //}

        //private void BindComboBoxDistrict3(Int32 provinceId)
        //{
        //    var result = _provinceService.ListDistrict(provinceId, string.Empty);
        //    if (result.IsCompleted)
        //    {
        //        ComboBoxDistrict3.Enabled = true;
        //        ComboBoxDistrict3.DataSource = result.Data;
        //        ComboBoxDistrict3.DataBind();

        //        ComboBoxSubDistrict3.SelectedIndex = -1;
        //        ComboBoxSubDistrict3.Enabled = false;
        //    }
        //}

        //private void BindComboBoxSubDistrict1(Int32 districtId)
        //{
        //    var result = _provinceService.ListSubDistrict(districtId, string.Empty);
        //    if (result.IsCompleted)
        //    {
        //        ComboBoxSubDistrict1.Enabled = true;
        //        ComboBoxSubDistrict1.DataSource = result.Data;
        //        ComboBoxSubDistrict1.DataBind();
        //    }
        //}

        //private void BindComboBoxSubDistrict2(Int32 districtId)
        //{
        //    var result = _provinceService.ListSubDistrict(districtId, string.Empty);
        //    if (result.IsCompleted)
        //    {
        //        ComboBoxSubDistrict2.Enabled = true;
        //        ComboBoxSubDistrict2.DataSource = result.Data;
        //        ComboBoxSubDistrict2.DataBind();
        //    }
        //}

        //private void BindComboBoxSubDistrict3(Int32 districtId)
        //{
        //    var result = _provinceService.ListSubDistrict(districtId, string.Empty);
        //    if (result.IsCompleted)
        //    {
        //        ComboBoxSubDistrict3.Enabled = true;
        //        ComboBoxSubDistrict3.DataSource = result.Data;
        //        ComboBoxSubDistrict3.DataBind();
        //    }
        //}
        #endregion

        protected void CheckBoxDupData1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBoxDupData1.Checked)
                {
                    DropDownListPrefix2.SelectedValue = DropDownListPrefix1.SelectedValue;
                    TextBoxPrefixOther2.Text = TextBoxPrefixOther1.Text;
                    TextBoxFirstName2.Text = TextBoxFirstName1.Text;
                    TextBoxLastName2.Text = TextBoxLastName1.Text;
                    TextBoxAddress2.Text = TextBoxAddress1.Text;
                    TextBoxMoo2.Text = TextBoxMoo1.Text;
                    TextBoxBuilding2.Text = TextBoxBuilding1.Text;
                    TextBoxSoi2.Text = TextBoxSoi1.Text;
                    TextBoxRoad2.Text = TextBoxRoad1.Text;
                    TextBoxPostCode2.Text = TextBoxPostCode1.Text;
                    TextBoxTelephone2.Text = TextBoxTelephone1.Text;
                    TextBoxFax2.Text = TextBoxFax1.Text;
                    TextBoxEmail2.Text = TextBoxEmail1.Text;

                    DdlProvince2.Text = DdlProvince1.Text;
                    DdlDistrict2.Text = DdlDistrict1.Text;
                    DdlSubDistrict2.Text = DdlSubDistrict1.Text;

                    //DisplayComboBoxAddress(2, ComboBoxProvince1.SelectedValue, ComboBoxDistrict1.SelectedValue);

                    //if (ComboBoxDistrict2.Items.Count > 0)
                    //    ComboBoxDistrict2.SelectedValue = ComboBoxDistrict1.SelectedValue;

                    //if (ComboBoxSubDistrict2.Items.Count > 0)
                    //    ComboBoxSubDistrict2.SelectedValue = ComboBoxSubDistrict1.SelectedValue;
                    
                }
                else
                {
                    DropDownListPrefix2.SelectedIndex = 0;
                    TextBoxPrefixOther2.Text = string.Empty;
                    TextBoxFirstName2.Text = string.Empty;
                    TextBoxLastName2.Text = string.Empty;
                    TextBoxAddress2.Text = string.Empty;
                    TextBoxMoo2.Text = string.Empty;
                    TextBoxBuilding2.Text = string.Empty;
                    TextBoxSoi2.Text = string.Empty;
                    TextBoxRoad2.Text = string.Empty;
                    DdlProvince2.Text = "";
                    DdlDistrict2.Text = "";
                    DdlSubDistrict2.Text = "";
                    TextBoxPostCode2.Text = string.Empty;
                    TextBoxTelephone2.Text = string.Empty;
                    TextBoxFax2.Text = string.Empty;
                    TextBoxEmail2.Text = string.Empty;
                }               
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        protected void CheckBoxDupData2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBoxDupData2.Checked)
                {
                    DropDownListPrefix3.SelectedValue = DropDownListPrefix1.SelectedValue;
                    TextBoxPrefixOther3.Text = TextBoxPrefixOther1.Text;
                    TextBoxFirstName3.Text = TextBoxFirstName1.Text;
                    TextBoxLastName3.Text = TextBoxLastName1.Text;
                    TextBoxAddress3.Text = TextBoxAddress1.Text;
                    TextBoxMoo3.Text = TextBoxMoo1.Text;
                    TextBoxBuilding3.Text = TextBoxBuilding1.Text;
                    TextBoxSoi3.Text = TextBoxSoi1.Text;
                    TextBoxRoad3.Text = TextBoxRoad1.Text;
                    TextBoxPostCode3.Text = TextBoxPostCode1.Text;
                    TextBoxTelephone3.Text = TextBoxTelephone1.Text;
                    TextBoxFax3.Text = TextBoxFax1.Text;
                    TextBoxEmail3.Text = TextBoxEmail1.Text;

                    DdlProvince3.Text = DdlProvince1.Text;
                    DdlDistrict3.Text = DdlDistrict1.Text;
                    DdlSubDistrict3.Text = DdlSubDistrict1.Text;
                    
                    //DisplayComboBoxAddress(3, DdlProvince1.Text, DdlDistrict1.Text);

                    //if (ComboBoxDistrict3.Items.Count > 0)
                    //    ComboBoxDistrict3.SelectedValue = ComboBoxDistrict1.SelectedValue;

                    //if (ComboBoxSubDistrict3.Items.Count > 0)
                    //    ComboBoxSubDistrict3.SelectedValue = ComboBoxSubDistrict1.SelectedValue;

                    //if (ComboBoxDistrict1.SelectedIndex > 0)
                    //    ComboBoxDistrict1.Enabled = true;

                    //if (ComboBoxDistrict2.SelectedIndex > 0)
                    //    ComboBoxDistrict2.Enabled = true;

                    //if (ComboBoxSubDistrict1.SelectedIndex > 0)
                    //    ComboBoxSubDistrict1.Enabled = true;

                    //if (ComboBoxSubDistrict2.SelectedIndex > 0)
                    //    ComboBoxSubDistrict1.Enabled = true;
                }
                else
                {
                    DropDownListPrefix3.SelectedIndex = 0;
                    TextBoxPrefixOther2.Text = string.Empty;
                    TextBoxFirstName3.Text = string.Empty;
                    TextBoxLastName3.Text = string.Empty;
                    TextBoxAddress3.Text = string.Empty;
                    TextBoxMoo3.Text = string.Empty;
                    TextBoxBuilding3.Text = string.Empty;
                    TextBoxSoi3.Text = string.Empty;
                    TextBoxRoad3.Text = string.Empty;
                    DdlProvince3.Text = "";
                    DdlDistrict3.Text = "";
                    DdlSubDistrict3.Text = "";
                    TextBoxPostCode3.Text = string.Empty;
                    TextBoxTelephone3.Text = string.Empty;
                    TextBoxFax3.Text = string.Empty;
                    TextBoxEmail3.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        protected void ButtonSavePersonal_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    ServiceModels.ProjectInfo.TabPersonal model = MapControlToTabPersonal();


                    var result = _service.SaveProjectPersonal(model);
                    if (result.IsCompleted)
                    {
                        RequiredSubmitData = result.Data.RequiredSubmitData;
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPersonal");
                        ShowResultMessage(result.Message);
                    }
                    else
                    {
                        ShowErrorMessage(result.Message);
                    }
                }
                catch (Exception ex)
                {
                    Common.Logging.LogError(Logging.ErrorType.WebError, "Personal", ex);
                    ShowErrorMessage(ex.Message);
                }
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Decimal projectId = 0;
                decimal.TryParse(HiddenProjectID.Value, out projectId);
                if (projectId > 0)
                {
                    var result = _service.DeleteProject(projectId);
                    if (result.IsCompleted)
                        Response.Redirect(Page.ResolveClientUrl("~/ProjectInfo/ProjectInfoList.aspx?isDeleteSuccess=true"));
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

        private ServiceModels.ProjectInfo.TabPersonal MapControlToTabPersonal()
        {
            IEnumerable<ServiceModels.KendoAttachment> addedFiles;
            IEnumerable<ServiceModels.KendoAttachment> removedFiles;

            ServiceModels.ProjectInfo.TabPersonal model = new ServiceModels.ProjectInfo.TabPersonal();
            model.ProjectID = Convert.ToDecimal(HiddenProjectID.Value);
            model.IDCardNo = KeepIDCardNo();
            model.Address1 = KeepAddressTabPersonal1();
            model.Address2 = KeepAddressTabPersonal2();
            model.Address3 = KeepAddressTabPersonal3();
            model.SupportPlace1 = TextBoxSupportPlace1.Text.Trim();
            model.SupportOrgName1 = TextBoxSupportOrgName1.Text.Trim();

            decimal? instructorAmt2 = null;
            string strInstructorAmt2 = TextBoxInstructorAmt2.Text.Trim();
            if (!string.IsNullOrEmpty(strInstructorAmt2))
                instructorAmt2 = Convert.ToDecimal(strInstructorAmt2);

            model.InstructorAmt2 = instructorAmt2;
            model.SupportOrgName2 = TextBoxSupportOrgName2.Text.Trim();

            addedFiles = FileUploadInstructor.AddedFiles;
            removedFiles = FileUploadInstructor.RemovedFiles;
            model.AddedInstructorAttachment = (addedFiles.Count() > 0) ? addedFiles.First() : null;
            model.RemovedInstructorAttachment = (removedFiles.Count() > 0) ? removedFiles.First() : null;
            //kenghot
            model.AddedInstructorAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            model.RemovedInstructorAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;
            //end kenghot

            decimal? supportBudgetAmt3 = null;
            string strSuppportBudgetAmt3 = TextBoxSupportBudgetAmt3.Text.Trim();
            if (!string.IsNullOrEmpty(strSuppportBudgetAmt3))
                supportBudgetAmt3 = Convert.ToDecimal(strSuppportBudgetAmt3);

            model.SupportBudgetAmt3 = supportBudgetAmt3;
            model.SupportOrgName3 = TextBoxSupportOrgName3.Text.Trim();
            model.SupportEquipment4 = TextBoxSupportEquipment4.Text.Trim();
            model.SupportOrgName4 = TextBoxSupportOrgName4.Text.Trim();

            decimal? supportDrinkFoodAmt5 = null;
            string strSupportDrinkFoodAmt5 = TextBoxSupportDrinkFoodAmt5.Text.Trim();
            if (!string.IsNullOrEmpty(strSupportDrinkFoodAmt5))
                supportDrinkFoodAmt5 = Convert.ToDecimal(strSupportDrinkFoodAmt5);

            model.SupportDrinkFoodAmt5 = supportDrinkFoodAmt5;
            model.SupportOrgName5 = TextBoxSupportOrgName5.Text.Trim();
            model.SupportOrgName6 = TextBoxSupportOrgName6.Text.Trim();

            addedFiles = FileUploadVehicle.AddedFiles;
            removedFiles = FileUploadVehicle.RemovedFiles;
            model.AddedVehicleAttachment= (addedFiles.Count() > 0) ? addedFiles.First() : null;
            model.RemovedVehicleAttachment = (removedFiles.Count() > 0) ? removedFiles.First() : null;
            //kenghot
            model.AddedVehicleAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            model.RemovedVehicleAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;
            //end kenghot
            decimal? supportValunteerAmt7 = null;
            string strSupportValunteerAmt7 = TextBoxSupportValunteerAmt7.Text.Trim();
            if (!string.IsNullOrEmpty(strSupportValunteerAmt7))
                supportValunteerAmt7 = Convert.ToDecimal(strSupportValunteerAmt7);

            model.SupportValunteerAmt7 = supportValunteerAmt7;
            model.SupportOrgName7 = TextBoxSupportOrgName7.Text.Trim();

            addedFiles = FileUploadValunteer.AddedFiles;
            removedFiles = FileUploadValunteer.RemovedFiles;
            model.AddedValunteerAttachment = (addedFiles.Count() > 0) ? addedFiles.First() : null;
            model.RemovedValunteerAttachment = (removedFiles.Count() > 0) ? removedFiles.First() : null;
            //kenghot
            model.AddedValunteerAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            model.RemovedValunteerAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;
            //end kenghot
            model.SupportOther8 = TextBoxSupportOther8.Text.Trim();
            model.SupportOrgName8 = TextBoxSupportOrgName8.Text.Trim();

            return model;
        }

        private string KeepIDCardNo()
        {
            string result = string.Empty;
            result = TextBoxPersonalMainIDCardNo.Text.Trim();
            result = result.Replace("-", "");
            result = result.Replace("_", "");
            return result;
        }

        private ServiceModels.ProjectInfo.AddressTabPersonal1 KeepAddressTabPersonal1()
        {
            ServiceModels.ProjectInfo.AddressTabPersonal1 result = new ServiceModels.ProjectInfo.AddressTabPersonal1();
            result.Prefix1 = Convert.ToDecimal(DropDownListPrefix1.SelectedValue);
            result.PrefixOther = TextBoxPrefixOther1.Text.Trim();
            result.Firstname1 = TextBoxFirstName1.Text.Trim() ;
            result.Lastname1 = TextBoxLastName1.Text.Trim() ;
            result.Address1 = TextBoxAddress1.Text.Trim() ;
            result.Moo1 = TextBoxMoo1.Text.Trim();
            result.Building1 = TextBoxBuilding1.Text.Trim();
            result.Soi1 = TextBoxSoi1.Text.Trim();
            result.Road1 = TextBoxRoad1.Text.Trim();
            // save draft
            if (!string.IsNullOrEmpty(DdlSubDistrict1.Text))
            {
                result.SubDistrictID1 = Convert.ToDecimal(DdlSubDistrict1.Text);
            }
            if (!string.IsNullOrEmpty(DdlDistrict1.Text))
            {
                result.DistrictID1 = Convert.ToDecimal(DdlDistrict1.Text);
            }
            //save draft
            if (string.IsNullOrEmpty(DdlProvince1.Text))
            {
                DdlProvince1.Text = "80";
            }
            result.ProvinceID1 = Convert.ToDecimal(DdlProvince1.Text);
            result.PostCode1 = TextBoxPostCode1.Text.Trim();
            result.Telephone1 = TextBoxTelephone1.Text.Trim();
            result.Mobile1 = TextBoxMobile1.Text.Trim();
            result.Fax1 = TextBoxFax1.Text.Trim();
            result.Email1 = TextBoxEmail1.Text.Trim();
            return result;
        }

        private ServiceModels.ProjectInfo.AddressTabPersonal2 KeepAddressTabPersonal2()
        {
            ServiceModels.ProjectInfo.AddressTabPersonal2 result = new ServiceModels.ProjectInfo.AddressTabPersonal2();
            result.Prefix2 = Convert.ToDecimal(DropDownListPrefix2.SelectedValue);
            result.PrefixOther = TextBoxPrefixOther2.Text.Trim();
            result.Firstname2 = TextBoxFirstName2.Text.Trim() == "" ? string.Empty : TextBoxFirstName2.Text.Trim();
            result.Lastname2 = TextBoxLastName2.Text.Trim() == "" ? string.Empty : TextBoxLastName2.Text.Trim();
            result.Address2 = TextBoxAddress2.Text.Trim() == "" ? string.Empty : TextBoxAddress2.Text.Trim();
            result.Moo2 = TextBoxMoo2.Text.Trim();
            result.Building2 = TextBoxBuilding2.Text.Trim();
            result.Soi2 = TextBoxSoi2.Text.Trim();
            result.Road2 = TextBoxRoad2.Text.Trim();
            //save draft
            if (!string.IsNullOrEmpty(DdlSubDistrict2.Text))
            {
                result.SubDistrictID2 = Convert.ToDecimal(DdlSubDistrict2.Text);
            }
            if (!string.IsNullOrEmpty(DdlDistrict2.Text))
            {
                result.DistrictID2 = Convert.ToDecimal(DdlDistrict2.Text);
            }
            //save draft
            if (string.IsNullOrEmpty(DdlProvince2.Text))
            {
                DdlProvince2.Text = "80";
            }
            result.ProvinceID2 = Convert.ToDecimal(DdlProvince2.Text);            
            result.PostCode2 = TextBoxPostCode2.Text.Trim();
            result.Telephone2 = TextBoxTelephone2.Text.Trim();
            result.Fax2 = TextBoxFax2.Text.Trim();
            result.Email2 = TextBoxEmail2.Text.Trim();
            return result;
        }

        private ServiceModels.ProjectInfo.AddressTabPersonal3 KeepAddressTabPersonal3()
        {
            ServiceModels.ProjectInfo.AddressTabPersonal3 result = null;
            
            string firstname3 = TextBoxFirstName3.Text.Trim();
            string lastname3 = TextBoxLastName3.Text.Trim();
            string address3 = TextBoxAddress3.Text.Trim();
            string subDistrict3 = DdlSubDistrict3.Text;
            string district3 = DdlDistrict3.Text;
            string province3 = DdlProvince3.Text;
            string postcode3 = TextBoxPostCode3.Text.Trim();
            string telephone3 = TextBoxTelephone3.Text.Trim();
            string email3 = TextBoxEmail3.Text.Trim();

            
            if (!string.IsNullOrEmpty(firstname3) && !string.IsNullOrEmpty(lastname3) && !string.IsNullOrEmpty(address3) && !string.IsNullOrEmpty(subDistrict3)
                && !string.IsNullOrEmpty(district3) && !string.IsNullOrEmpty(province3) && !string.IsNullOrEmpty(postcode3) && !string.IsNullOrEmpty(telephone3)
                && !string.IsNullOrEmpty(email3))
            {
                result = new ServiceModels.ProjectInfo.AddressTabPersonal3();
                result.Prefix3 = Convert.ToDecimal(DropDownListPrefix3.SelectedValue);
                result.PrefixOther = TextBoxPrefixOther3.Text.Trim();
                result.Firstname3 = firstname3;
                result.Lastname3 = lastname3;
                result.Address3 = address3;
                result.Moo3 = TextBoxMoo3.Text.Trim();
                result.Building3 = TextBoxBuilding3.Text.Trim();
                result.Soi3 = TextBoxSoi3.Text.Trim();
                result.Road3 = TextBoxRoad3.Text.Trim();
                result.SubDistrictID3 = Convert.ToDecimal(DdlSubDistrict3.Text);
                result.DistrictID3 = Convert.ToDecimal(DdlDistrict3.Text);
                result.ProvinceID3 = Convert.ToDecimal(DdlProvince3.Text);
                result.PostCode3 = postcode3;
                result.Telephone3 = telephone3;
                result.Fax3 = TextBoxFax3.Text.Trim();
                result.Email3 = email3;
            }
            
          

            //if (CheckBoxDupData2.Checked)
            //{
            
            
            //}
            return result;
        }


        private void DisplayIDCardNo(string iDCardNo)
        {
            TextBoxPersonalMainIDCardNo.Text = iDCardNo;
        }

        private void DisplayDataPersonal(ServiceModels.ProjectInfo.TabPersonal model)
        {
            BudgetYear = model.BudgetYear;
            bool canSendProjectInfo = (model.RequiredSubmitData == null);
            RequiredSubmitData = model.RequiredSubmitData;

            //Check Function
            List<Common.ProjectFunction> functions = _service.GetProjectFunction(model.ProjectID).Data;
            //kenghot
            //bool isEditable = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData));
            var master = (this.Page.Master as MasterPages.SiteMaster);
            bool isEditable = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData)
                || master.UserInfo.IsAdministrator);
            //end kenghot
            ButtonSavePersonal.Visible = isEditable;
            ButtonDraft.Visible = ButtonSavePersonal.Visible;
            ButtonSendProjectInfo.Visible = (functions.Contains(Common.ProjectFunction.SaveDarft) && canSendProjectInfo);
            ButtonDelete.Visible = functions.Contains(Common.ProjectFunction.Delete);
            HyperLinkPrint.Visible = (functions.Contains(Common.ProjectFunction.PrintDataForm) && canSendProjectInfo);
            CheckBoxDupData1.Enabled = isEditable;
            CheckBoxDupData2.Enabled = isEditable;
            ButtonReject.Visible = functions.Contains(Common.ProjectFunction.Reject);
            CheckBoxDupData1.Checked = false;
            CheckBoxDupData2.Checked = false;
            FileUploadInstructor.Enabled = isEditable;
            FileUploadVehicle.Enabled = isEditable;
            FileUploadValunteer.Enabled = isEditable;
            //----------------------------
            
            if (model.Address1 != null)
            {
                DropDownListPrefix1.SelectedValue = model.Address1.Prefix1.ToString();
                TextBoxPrefixOther1.Text = model.Address1.PrefixOther;
                TextBoxFirstName1.Text = model.Address1.Firstname1;
                TextBoxLastName1.Text = model.Address1.Lastname1;
                TextBoxAddress1.Text = model.Address1.Address1;
                TextBoxMoo1.Text = model.Address1.Moo1;
                TextBoxBuilding1.Text = model.Address1.Building1;
                TextBoxSoi1.Text = model.Address1.Soi1;
                TextBoxRoad1.Text = model.Address1.Road1;

                DdlProvince1.Text = model.Address1.ProvinceID1.ToString();
                DdlDistrict1.Text = model.Address1.DistrictID1.ToString();
                DdlSubDistrict1.Text = model.Address1.SubDistrictID1.ToString();
                
                //DisplayComboBoxAddress(1, model.Address1.ProvinceID1.ToString(), model.Address1.DistrictID1.ToString());

                //if (ComboBoxDistrict1.Items.Count > 0)
                //    ComboBoxDistrict1.SelectedValue = model.Address1.DistrictID1.ToString();

                //if (ComboBoxSubDistrict1.Items.Count > 0)
                //    ComboBoxSubDistrict1.SelectedValue = model.Address1.SubDistrictID1.ToString();

                TextBoxPostCode1.Text = model.Address1.PostCode1;
                TextBoxTelephone1.Text = model.Address1.Telephone1;
                TextBoxMobile1.Text = model.Address1.Mobile1;
                TextBoxFax1.Text = model.Address1.Fax1;
                TextBoxEmail1.Text = model.Address1.Email1;
            }

            if (model.Address2 != null)
            {
                DropDownListPrefix2.SelectedValue = model.Address2.Prefix2.ToString();
                TextBoxPrefixOther2.Text = model.Address2.PrefixOther;
                TextBoxFirstName2.Text = model.Address2.Firstname2;
                TextBoxLastName2.Text = model.Address2.Lastname2;
                TextBoxAddress2.Text = model.Address2.Address2;
                TextBoxMoo2.Text = model.Address2.Moo2;
                TextBoxBuilding2.Text = model.Address2.Building2;
                TextBoxSoi2.Text = model.Address2.Soi2;
                TextBoxRoad2.Text = model.Address2.Road2;

                DdlProvince2.Text = model.Address2.ProvinceID2.ToString();
                DdlDistrict2.Text = model.Address2.DistrictID2.ToString();
                DdlSubDistrict2.Text = model.Address2.SubDistrictID2.ToString();

                //ComboBoxProvince2.SelectedValue = model.Address2.ProvinceID2.ToString();
                //DisplayComboBoxAddress(2, model.Address2.ProvinceID2.ToString(), model.Address2.DistrictID2.ToString());

                //if(ComboBoxDistrict2.Items.Count > 0)
                //    ComboBoxDistrict2.SelectedValue = model.Address2.DistrictID2.ToString();

                //if (ComboBoxSubDistrict2.Items.Count > 0)
                //    ComboBoxSubDistrict2.SelectedValue = model.Address2.SubDistrictID2.ToString();
            
                TextBoxPostCode2.Text = model.Address2.PostCode2;
                TextBoxTelephone2.Text = model.Address2.Telephone2;
                TextBoxFax2.Text = model.Address2.Fax2;
                TextBoxEmail2.Text = model.Address2.Email2;
            }

            if (model.Address3 != null)
            {
                DropDownListPrefix3.SelectedValue = model.Address3.Prefix3.ToString();
                TextBoxPrefixOther3.Text = model.Address3.PrefixOther;
                TextBoxFirstName3.Text = model.Address3.Firstname3;
                TextBoxLastName3.Text = model.Address3.Lastname3;
                TextBoxAddress3.Text = model.Address3.Address3;
                TextBoxMoo3.Text = model.Address3.Moo3;
                TextBoxBuilding3.Text = model.Address3.Building3;
                TextBoxSoi3.Text = model.Address3.Soi3;
                TextBoxRoad3.Text = model.Address3.Road3;

                DdlProvince3.Text = model.Address3.ProvinceID3.ToString();
                DdlDistrict3.Text = model.Address3.DistrictID3.ToString();
                DdlSubDistrict3.Text = model.Address3.SubDistrictID3.ToString();

                //ComboBoxProvince3.SelectedValue = model.Address3.ProvinceID3.ToString();
                //DisplayComboBoxAddress(3, model.Address3.ProvinceID3.ToString(), model.Address3.DistrictID3.ToString());

                //if(ComboBoxDistrict3.Items.Count > 0)
                //    ComboBoxDistrict3.SelectedValue = model.Address3.DistrictID3.ToString();

                //if(ComboBoxSubDistrict3.Items.Count > 0)
                //    ComboBoxSubDistrict3.SelectedValue = model.Address3.SubDistrictID3.ToString();

                TextBoxPostCode3.Text = model.Address3.PostCode3;
                TextBoxTelephone3.Text = model.Address3.Telephone3;
                TextBoxFax3.Text = model.Address3.Fax3;
                TextBoxEmail3.Text = model.Address3.Email3;
            }
            else
            {
                DropDownListPrefix3.SelectedIndex = 0;
                TextBoxFirstName3.Text = string.Empty;
                TextBoxLastName3.Text = string.Empty;
                TextBoxAddress3.Text = string.Empty;
                TextBoxMoo3.Text = string.Empty;
                TextBoxBuilding3.Text = string.Empty;
                TextBoxSoi3.Text = string.Empty;
                TextBoxRoad3.Text = string.Empty;
                DdlProvince3.Text = "";
                DdlDistrict3.Text = "";
                DdlSubDistrict3.Text = "";
                TextBoxPostCode3.Text = string.Empty;
                TextBoxTelephone3.Text = string.Empty;
                TextBoxFax3.Text = string.Empty;
                TextBoxEmail3.Text = string.Empty;
            }

            TextBoxSupportPlace1.Text = model.SupportPlace1;
            TextBoxSupportOrgName1.Text = model.SupportOrgName1;

            TextBoxInstructorAmt2.Text = (model.InstructorAmt2.HasValue)? model.InstructorAmt2.ToString() : "";
            TextBoxSupportOrgName2.Text = model.SupportOrgName2;
            //kenghot
            //List<ServiceModels.KendoAttachment> instructorFiles = new List<ServiceModels.KendoAttachment>();
      
            List<ServiceModels.KendoAttachment> instructorFiles = model.InstructorAttachments;
            if (model.InstructorAttachment != null)
            {
                instructorFiles.Add(model.InstructorAttachment);
            }
            //end kenghot
            FileUploadInstructor.ClearChanges();
            FileUploadInstructor.ExistingFiles = instructorFiles;
            FileUploadInstructor.DataBind();

            TextBoxSupportBudgetAmt3.Text = (model.SupportBudgetAmt3.HasValue)? model.SupportBudgetAmt3.ToString() : "";
            TextBoxSupportOrgName3.Text = model.SupportOrgName3;

            TextBoxSupportEquipment4.Text = model.SupportEquipment4;
            TextBoxSupportOrgName4.Text = model.SupportOrgName4;

            TextBoxSupportDrinkFoodAmt5.Text = (model.SupportDrinkFoodAmt5.HasValue)? model.SupportDrinkFoodAmt5.ToString() : "";
            TextBoxSupportOrgName5.Text = model.SupportOrgName5;
            TextBoxSupportOrgName6.Text = model.SupportOrgName6;
            //kenghot
            //List<ServiceModels.KendoAttachment> vehicleFiles = new List<ServiceModels.KendoAttachment>();
            List<ServiceModels.KendoAttachment> vehicleFiles = model.VehicleAttachments;
            if (model.VehicleAttachment != null)
            {
                vehicleFiles.Add(model.VehicleAttachment);
            }
            //end kenghot
            FileUploadVehicle.ClearChanges();
            FileUploadVehicle.ExistingFiles = vehicleFiles;
            FileUploadVehicle.DataBind();

            TextBoxSupportValunteerAmt7.Text = (model.SupportValunteerAmt7.HasValue)? model.SupportValunteerAmt7.ToString() : "";
            TextBoxSupportOrgName7.Text = model.SupportOrgName7;
            //kenghot
            //List<ServiceModels.KendoAttachment> valunteerFiles = new List<ServiceModels.KendoAttachment>();
            List<ServiceModels.KendoAttachment> valunteerFiles = model.ValunteerAttachments ;
            if (model.ValunteerAttachment != null)
            {
                valunteerFiles.Add(model.ValunteerAttachment);
            }
            //end kenghot
            FileUploadValunteer.ClearChanges();
            FileUploadValunteer.ExistingFiles = valunteerFiles;
            FileUploadValunteer.DataBind();

            TextBoxSupportOther8.Text = model.SupportOther8;
            TextBoxSupportOrgName8.Text = model.SupportOrgName8;
                            
        }

        //private void DisplayComboBoxAddress(Int32 orderNo, String provinceId, String districtId)
        //{
        //    if ((!String.IsNullOrEmpty(provinceId)) && (!String.IsNullOrEmpty(districtId)))
        //    {
        //        switch (orderNo)
        //        {
        //            case 1:
        //                int province1Id = Convert.ToInt32(provinceId);
        //                int district1Id = Convert.ToInt32(districtId);
        //                DdlProvince1.Text = provinceId;
        //                //ComboBoxProvince1.SelectedValue = provinceId;
        //                if (province1Id > 0)
        //                {
        //                    //BindComboBoxDistrict1(province1Id);
        //                }
        //                else
        //                {
        //                    ComboBoxDistrict1.SelectedIndex = -1;
        //                    ComboBoxDistrict1.Enabled = false;

        //                    ComboBoxSubDistrict1.SelectedIndex = -1;
        //                    ComboBoxSubDistrict1.Enabled = false;
        //                }

        //                if (district1Id > 0)
        //                {
        //                    BindComboBoxSubDistrict1(district1Id);
        //                }
        //                else
        //                {
        //                    ComboBoxSubDistrict1.SelectedIndex = -1;
        //                    ComboBoxSubDistrict1.Enabled = false;
        //                }
        //                break;
        //            case 2:
        //                int province2Id = Convert.ToInt32(provinceId);
        //                int district2Id = Convert.ToInt32(districtId);
        //                ComboBoxProvince2.SelectedValue = provinceId;
        //                if (province2Id > 0)
        //                {
        //                    BindComboBoxDistrict2(province2Id);
        //                }
        //                else
        //                {
        //                    ComboBoxDistrict2.SelectedIndex = -1;
        //                    ComboBoxDistrict2.Enabled = false;

        //                    ComboBoxSubDistrict2.SelectedIndex = -1;
        //                    ComboBoxSubDistrict2.Enabled = false;
        //                }

        //                if (district2Id > 0)
        //                {
        //                    BindComboBoxSubDistrict2(district2Id);
        //                }
        //                else
        //                {
        //                    ComboBoxSubDistrict2.SelectedIndex = -1;
        //                    ComboBoxSubDistrict2.Enabled = false;
        //                }
        //                break;
        //            case 3:
        //                int province3Id = Convert.ToInt32(provinceId);
        //                int district3Id = Convert.ToInt32(districtId);
        //                ComboBoxProvince3.SelectedValue = provinceId;
        //                if (province3Id > 0)
        //                {
        //                    BindComboBoxDistrict3(province3Id);
        //                }
        //                else
        //                {
        //                    ComboBoxDistrict3.SelectedIndex = -1;
        //                    ComboBoxDistrict3.Enabled = false;

        //                    ComboBoxSubDistrict3.SelectedIndex = -1;
        //                    ComboBoxSubDistrict3.Enabled = false;
        //                }

        //                if (district3Id > 0)
        //                {
        //                    BindComboBoxSubDistrict3(district3Id);
        //                }
        //                else
        //                {
        //                    ComboBoxSubDistrict3.SelectedIndex = -1;
        //                    ComboBoxSubDistrict3.Enabled = false;
        //                }
        //                break;
        //        }
        //    }
            
        //}

        private void RegisterClientScript()
        {
            String projectInfoTargetAmountChangeScript = @"
                function projectPersonalAddress3Validate(sender, args) {
                    var comboBoxProvince3ID = '" + DdlProvince3.ClientID +@"';
                    var comboBoxDistrict3ID = '" + DdlDistrict3.ClientID +@"';
                    var comboBoxSubDistrict3ID = '" + DdlSubDistrict3.ClientID +@"';

                    var controlToValidate = sender.controltovalidate;

                    var firstName3 = $('#" + TextBoxFirstName3.ClientID +@"').val();
                    firstName3 = $.trim(firstName3);
                    var isValid = true;
                    if ((firstName3 != '') && ((args.Value == '') || 
                        (((controlToValidate == comboBoxProvince3ID) || (controlToValidate == comboBoxDistrict3ID) || (controlToValidate == comboBoxSubDistrict3ID)) && (args.Value == '')))) {
                        isValid = false;
                    }               
                    args.IsValid = isValid;
                }
                ";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelPersonal,
                       this.GetType(),
                       "ProjectPersonalAddress3ValidateScript",
                       projectInfoTargetAmountChangeScript,
                       true);



            RegisterRequiredData();
            RegisterComboboxScript();
          
        }

        private void RegisterRequiredData()
        {
            
                String script = @"
                 $(function () {      
                    SetTabHeader(" + Common.Web.WebUtility.ToJSON(RequiredSubmitData) + @");
                 });";


                ScriptManager.RegisterStartupScript(
                          UpdatePanelPersonal,
                          this.GetType(),
                          "UpdatePanelPersonalScript",
                          script,
                          true);

        }

        private void RegisterComboboxScript()
        {
            string province1Selected = (!String.IsNullOrEmpty(DdlProvince1.Text)) ? DdlProvince1.Text : "";
            string district1Selected = (!String.IsNullOrEmpty(DdlDistrict1.Text)) ? DdlDistrict1.Text : "";
            string subDistrict1Selected = (!String.IsNullOrEmpty(DdlSubDistrict1.Text)) ? DdlSubDistrict1.Text : "";

            string province2Selected = (!String.IsNullOrEmpty(DdlProvince2.Text)) ? DdlProvince2.Text : "";
            string district2Selected = (!String.IsNullOrEmpty(DdlDistrict2.Text)) ? DdlDistrict2.Text : "";
            string subDistrict2Selected = (!String.IsNullOrEmpty(DdlSubDistrict2.Text)) ? DdlSubDistrict2.Text : "";

            string province3Selected = (!String.IsNullOrEmpty(DdlProvince3.Text)) ? DdlProvince3.Text : "";
            string district3Selected = (!String.IsNullOrEmpty(DdlDistrict3.Text)) ? DdlDistrict3.Text : "";
            string subDistrict3Selected = (!String.IsNullOrEmpty(DdlSubDistrict3.Text)) ? DdlSubDistrict3.Text : "";

            string provinceList = Nep.Project.Common.Web.WebUtility.ToJSON(GetProvince());
            var prefix = _service.GetListOfValue("4", "Prefix");
            decimal otherId = 0;
            if (prefix != null)
            {
                otherId = prefix.LOVID;
            }
            String script = @"
                 function PrefixChange(sender,ref) { 
                        console.log(sender.value)
                        var obj = $('#' + ref)
                        console.log(obj)
                        if (sender.value != '" + otherId.ToString() + @"'){
                            obj.hide()
                        }else {
                            obj.show()
                        }
                }
                function RefreshPrefix() {
                    $('#" + DropDownListPrefix1.ClientID + @"').change()
                    $('#" + DropDownListPrefix2.ClientID + @"').change()
                    $('#" + DropDownListPrefix3.ClientID + @"').change()
                }
                $(function () {   
                    //prefix
                    $('#" + DropDownListPrefix1.ClientID + @"').change(function() {PrefixChange(this,'" + TextBoxPrefixOther1.ClientID + @"')})
                    $('#" + DropDownListPrefix2.ClientID + @"').change(function() {PrefixChange(this,'" + TextBoxPrefixOther2.ClientID + @"')})
                    $('#" + DropDownListPrefix3.ClientID + @"').change(function() {PrefixChange(this,'" + TextBoxPrefixOther3.ClientID + @"')})
                    RefreshPrefix()
                    // Address 1
                    c2x.createLocalCombobox({                       
                        ControlID: '" + DdlProvince1.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        Enable:true,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,                        
                        Change: function(e){c2x.onProvinceComboboxChange('" + DdlDistrict1.ClientID + @"', '" + DdlSubDistrict1.ClientID + @"',e);},
                        Value: '" + province1Selected + @"',   
                        Data:{Data:"+ provinceList+@", TotalRow:0}                  
                     });  

                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlDistrict1.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'" + DdlProvince1.ClientID + @"', 
                        AutoBind:false,
                        Enable:false,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetDistrict',
                        Change: function(e){c2x.onDistrictComboboxChange('" + DdlSubDistrict1.ClientID + @"',e);},
                        Value: '" + district1Selected + @"',
                        Param: function(){return c2x.getProvinceComboboxParam('" + DdlProvince1.ClientID + @"');}
                     });    

                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlSubDistrict1.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'" + DdlDistrict1.ClientID + @"', 
                        AutoBind:false,
                        Enable:false,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetSubDistrict',      
                        Value: '" + subDistrict1Selected + @"', 
                        Param: function(){return c2x.getProvinceComboboxParam('" + DdlDistrict1.ClientID + @"');},           
                     });  

                    // Address 2
                    c2x.createLocalCombobox({                       
                        ControlID: '" + DdlProvince2.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        Enable:true,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,                        
                        Change: function(e){c2x.onProvinceComboboxChange('" + DdlDistrict2.ClientID + @"', '" + DdlSubDistrict2.ClientID + @"',e);},
                        Value: '" + province2Selected + @"',   
                        Data:{Data:" + provinceList + @", TotalRow:0}                  
                     });  

                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlDistrict2.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'" + DdlProvince2.ClientID + @"', 
                        AutoBind:false,
                        Enable:false,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetDistrict',
                        Change: function(e){c2x.onDistrictComboboxChange('" + DdlSubDistrict2.ClientID + @"',e);},
                        Value: '" + district2Selected + @"',
                        Param: function(){return c2x.getProvinceComboboxParam('" + DdlProvince2.ClientID + @"');},
                     });    

                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlSubDistrict2.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'" + DdlDistrict2.ClientID + @"', 
                        AutoBind:false,
                        Enable:false,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetSubDistrict',      
                        Value: '" + subDistrict2Selected + @"', 
                        Param: function(){return c2x.getProvinceComboboxParam('" + DdlDistrict2.ClientID + @"');},           
                     });  

                    // Address 3
                    c2x.createLocalCombobox({                       
                        ControlID: '" + DdlProvince3.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        Enable:true,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,                        
                        Change: function(e){c2x.onProvinceComboboxChange('" + DdlDistrict3.ClientID + @"', '" + DdlSubDistrict3.ClientID + @"',e);},
                        Value: '" + province3Selected + @"',   
                        Data:{Data:" + provinceList + @", TotalRow:0}                  
                     });  

                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlDistrict3.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'" + DdlProvince3.ClientID + @"', 
                        AutoBind:false,
                        Enable:false,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetDistrict',
                        Change: function(e){c2x.onDistrictComboboxChange('" + DdlSubDistrict3.ClientID + @"',e);},
                        Value: '" + district3Selected + @"',
                        Param: function(){return c2x.getProvinceComboboxParam('" + DdlProvince3.ClientID + @"');},
                     });    

                    c2x.createComboboxCustom({                       
                        ControlID: '" + DdlSubDistrict3.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        ParentID:'" + DdlDistrict3.ClientID + @"', 
                        AutoBind:false,
                        Enable:false,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,
                        ReadUrl: './ComboboxHandler/GetSubDistrict',      
                        Value: '" + subDistrict3Selected + @"', 
                        Param: function(){return c2x.getProvinceComboboxParam('" + DdlDistrict3.ClientID + @"');},           
                     });                   
                   
                });
                 
              
                

            ";
            ScriptManager.RegisterStartupScript(
                      UpdatePanelPersonal,
                      this.GetType(),
                      "ManageUpdatePanelPersonalRegister",
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

        protected void CustomValidatorIDCardNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String str = args.Value.Trim();
            str = str.Replace("-", "").Replace("_", "");
            args.IsValid = (str != "");
        }
    }
}