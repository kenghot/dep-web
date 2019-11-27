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
    public partial class TabProsecuteControl : Nep.Project.Web.Infra.BaseUserControl
    {
        public IServices.IProjectInfoService _service { get; set; }
        public IServices.IProviceService _provinceService { get; set; }
        private string QuestionareGroup = "PROSECUTE";

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
                if (ViewState["ProsecuteViewAttachmentPrefix"] == null)
                {
                    string prefix = "Project/" + ProjectID;
                    ViewState["ProsecuteViewAttachmentPrefix"] = prefix;
                }
                return ViewState["ProsecuteViewAttachmentPrefix"].ToString();
            }

            set
            {
                ViewState["ProsecuteViewAttachmentPrefix"] = value;
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
            if (!Page.IsPostBack)
            {
                List<ServiceModels.GenericDropDownListData> listPrefix = GetPrefix();
                //Title Name Prefix
                DropDownListPrefix1.DataSource = listPrefix;
                DropDownListPrefix1.DataTextField = "Text";
                DropDownListPrefix1.DataValueField = "Value";
                DropDownListPrefix1.DataBind();

                List<ServiceModels.GenericDropDownListData> listPros = new List<GenericDropDownListData>();
                listPros.Add(new GenericDropDownListData { Text = "ใช้เงินผิดวัตถุประสงค์ของโครงการ", Value = "0" });
                listPros.Add(new GenericDropDownListData { Text = "ไม่ส่งแบบรายงานผลการปฏิบัติงาน", Value = "1" });
                listPros.Add(new GenericDropDownListData { Text = "อื่นๆ", Value = "9" });

                ddlProsecute.DataSource = listPros;
                ddlProsecute.DataTextField = "Text";
                ddlProsecute.DataValueField = "Value";
                ddlProsecute.DataBind();

                //----------------------------------------------
            }
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

    
            #endregion for old data


           // RegisterClientScript();
        }
       
        
        public void BindData()
        {
            decimal projectID = ProjectID;
            if (projectID > 0)
            {
                HiddenProjectID.Value = projectID.ToString();
                ButtonSendProjectInfo.Text = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Nep.Project.Resources.UI.ButtonSubmit : Nep.Project.Resources.UI.ButtonSendProjectInfo;
                var db = _service.GetDB();


                var proj = (from p in db.ProjectGeneralInfoes where p.ProjectID == ProjectID select p).FirstOrDefault();
                var info = (from p in db.ProjectInformations where p.ProjectID == ProjectID select p).FirstOrDefault();
                if (info != null)
                {
                    LabelProjectName.Text = info.ProjectNameTH;
                }
                if (proj != null)
                {
                    LabelORGName.Text = proj.OrganizationNameTH;
                }
                //var result = _service.GetProjectPersonalByProjectID(projectID);
                //if (result.IsCompleted)
                //{
                //    ServiceModels.ProjectInfo.TabPersonal model = result.Data;
                //    if (model != null)
                //    {
                //        DisplayIDCardNo(model.IDCardNo);
                //        DisplayDataPersonal(model);
                //    }
                //}
                //else
                //{
                //    ShowErrorMessage(result.Message);
                //}
                var att = new Business.AttachmentService(db);
                var f = att.GetAttachmentOfTable("PROSECUTE", "PROSECUTE1", ProjectID);
                FileUploadProsecute.ClearChanges();
                FileUploadProsecute.ExistingFiles = f;
                FileUploadProsecute.DataBind();
                f = att.GetAttachmentOfTable("PROSECUTE", "PROSECUTE2", ProjectID);
                FileUploadProsecute2.ClearChanges();
                FileUploadProsecute2.ExistingFiles = f;
                FileUploadProsecute2.DataBind();
                RegisterClientScript();
                var qnh = (from q in db.PROJECTQUESTIONHDs where q.PROJECTID == ProjectID && q.QUESTGROUP == QuestionareGroup select q).FirstOrDefault();
                //var oper = (from op in db.ProjectOperations where op.ProjectID == ProjectID select op).FirstOrDefault();
                //var con = (from c in db.ProjectContracts where c.ProjectID == ProjectID select c).FirstOrDefault();
                //var inf = (from i in db.ProjectInformations where i.ProjectID == ProjectID select i).FirstOrDefault();
                bool isReported = false; // (model.FollowupStatusCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว);
                if (qnh != null)
                {
                    isReported = (qnh.ISREPORTED == "1") ? true : false;
                }
                string script = "";
                
                script = @"<script type=""text/javascript"" src=""../../Scripts/Vue/Prosecute.js?v=" + DateTime.Now.Ticks.ToString() + @"""></script>";
                script += @"<script type=""text/javascript"" src=""../../Scripts/Vue/VueQN.js?v=" + DateTime.Now.Ticks.ToString() + @"""></script>";
                script += @"<script type=""text/javascript"" src=""../../Scripts/file-upload-helper.js?v=" + DateTime.Now.Ticks.ToString() + @"""></script>";
                var setVueParam = @"$( document ).ready(function() { 
                                console.log('set');
                                appVueQN.param.projID = " + ProjectID.ToString() + @";
                                appVueQN.param.qnGroup = '" + QuestionareGroup + @"';
                                appVueQN.param.IsReported = '" + ((isReported) ? "1" : "0") + @"';
                                //console.log(appVueQN.param);
                                appVueQN.getData(setComboBox);
                                })
                                ";

                //if (inf != null)
                //{
                //    setVueParam += string.Format("Vue.set(appVueQN.extend,'projectName','{0}');", inf.ProjectNameTH);
                //}
                //if (con != null)
                //{
                //    setVueParam += string.Format("Vue.set(appVueQN.extend,'organization','{0}');", con.ProjectGeneralInfo.OrganizationNameTH);
                //    setVueParam += string.Format("Vue.set(appVueQN.extend,'contractEndDate','{0:dd/MM/yyyy}');", con.ContractEndDate);
                //}
                //if (oper != null)
                //{
                //    setVueParam += string.Format("Vue.set(appVueQN.extend,'startDate','{0:dd/MM/yyyy}');", oper.StartDate);
                //}
               


                ScriptManager.RegisterStartupScript(this, this.GetType(), "QuestionareJSFile", script, false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "QNInitialData" + this.ClientID,
                       setVueParam, true);
            }           
           
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
                        page.RebindData("TabProsecute");
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

        protected void Test(object sender, EventArgs e) {
            var a = FileUploadProsecute.AddedFiles.ToList();
            var r = FileUploadProsecute.RemovedFiles.ToList();

              _service.SaveAttachFile(ProjectID, Common.LOVCode.Attachmenttype.PROJECT_INFORMATION, r, a, "PROSECUTE", "PROSECUTE1");
             a = FileUploadProsecute2.AddedFiles.ToList();
            r = FileUploadProsecute2.RemovedFiles.ToList();

            _service.SaveAttachFile(ProjectID, Common.LOVCode.Attachmenttype.PROJECT_INFORMATION, r, a, "PROSECUTE", "PROSECUTE2");
            Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
            page.RebindData("TabProsecute");
          
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
                        page.RebindData("TabProsecute");
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
         
            //model.SupportPlace1 = TextBoxSupportPlace1.Text.Trim();
            //model.SupportOrgName1 = TextBoxSupportOrgName1.Text.Trim();

            decimal? instructorAmt2 = null;
            //string strInstructorAmt2 = TextBoxInstructorAmt2.Text.Trim();
            //if (!string.IsNullOrEmpty(strInstructorAmt2))
            //    instructorAmt2 = Convert.ToDecimal(strInstructorAmt2);

            //model.InstructorAmt2 = instructorAmt2;
            //model.SupportOrgName2 = TextBoxSupportOrgName2.Text.Trim();

            addedFiles = FileUploadProsecute.AddedFiles;
            removedFiles = FileUploadProsecute.RemovedFiles;
            model.AddedInstructorAttachment = (addedFiles.Count() > 0) ? addedFiles.First() : null;
            model.RemovedInstructorAttachment = (removedFiles.Count() > 0) ? removedFiles.First() : null;
            //kenghot
            model.AddedInstructorAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            model.RemovedInstructorAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;
            //end kenghot

            decimal? supportBudgetAmt3 = null;
            //string strSuppportBudgetAmt3 = TextBoxSupportBudgetAmt3.Text.Trim();
            //if (!string.IsNullOrEmpty(strSuppportBudgetAmt3))
            //    supportBudgetAmt3 = Convert.ToDecimal(strSuppportBudgetAmt3);

            //model.SupportBudgetAmt3 = supportBudgetAmt3;
            //model.SupportOrgName3 = TextBoxSupportOrgName3.Text.Trim();
            //model.SupportEquipment4 = TextBoxSupportEquipment4.Text.Trim();
            //model.SupportOrgName4 = TextBoxSupportOrgName4.Text.Trim();

            //decimal? supportDrinkFoodAmt5 = null;
            //string strSupportDrinkFoodAmt5 = TextBoxSupportDrinkFoodAmt5.Text.Trim();
            //if (!string.IsNullOrEmpty(strSupportDrinkFoodAmt5))
            //    supportDrinkFoodAmt5 = Convert.ToDecimal(strSupportDrinkFoodAmt5);

            //model.SupportDrinkFoodAmt5 = supportDrinkFoodAmt5;
            //model.SupportOrgName5 = TextBoxSupportOrgName5.Text.Trim();
            //model.SupportOrgName6 = TextBoxSupportOrgName6.Text.Trim();

            //addedFiles = FileUploadVehicle.AddedFiles;
            //removedFiles = FileUploadVehicle.RemovedFiles;
            //model.AddedVehicleAttachment= (addedFiles.Count() > 0) ? addedFiles.First() : null;
            //model.RemovedVehicleAttachment = (removedFiles.Count() > 0) ? removedFiles.First() : null;
            ////kenghot
            //model.AddedVehicleAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            //model.RemovedVehicleAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;
            ////end kenghot
            //decimal? supportValunteerAmt7 = null;
            //string strSupportValunteerAmt7 = TextBoxSupportValunteerAmt7.Text.Trim();
            //if (!string.IsNullOrEmpty(strSupportValunteerAmt7))
            //    supportValunteerAmt7 = Convert.ToDecimal(strSupportValunteerAmt7);

            //model.SupportValunteerAmt7 = supportValunteerAmt7;
            //model.SupportOrgName7 = TextBoxSupportOrgName7.Text.Trim();

            //addedFiles = FileUploadValunteer.AddedFiles;
            //removedFiles = FileUploadValunteer.RemovedFiles;
            //model.AddedValunteerAttachment = (addedFiles.Count() > 0) ? addedFiles.First() : null;
            //model.RemovedValunteerAttachment = (removedFiles.Count() > 0) ? removedFiles.First() : null;
            ////kenghot
            //model.AddedValunteerAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            //model.RemovedValunteerAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;
            ////end kenghot
            //model.SupportOther8 = TextBoxSupportOther8.Text.Trim();
            //model.SupportOrgName8 = TextBoxSupportOrgName8.Text.Trim();

            return model;
        }

        private string KeepIDCardNo()
        {
            string result = string.Empty;
            result = TextBoxPersonalMainIDCardNo.Text.Trim();
            result = result.Replace("-", "");
            return result;
        }

        private ServiceModels.ProjectInfo.AddressTabPersonal1 KeepAddressTabPersonal1()
        {
            ServiceModels.ProjectInfo.AddressTabPersonal1 result = new ServiceModels.ProjectInfo.AddressTabPersonal1();
            result.Prefix1 = Convert.ToDecimal(DropDownListPrefix1.SelectedValue);
            result.Firstname1 = TextBoxFirstName1.Text.Trim();
            result.Lastname1 = TextBoxLastName1.Text.Trim();
            result.Address1 = TextBoxAddress1.Text.Trim();
            result.Moo1 = TextBoxMoo1.Text.Trim();
            result.Building1 = TextBoxBuilding1.Text.Trim();
            result.Soi1 = TextBoxSoi1.Text.Trim();
            result.Road1 = TextBoxRoad1.Text.Trim();
            result.SubDistrictID1 = Convert.ToDecimal(DdlSubDistrict1.Text);
            result.DistrictID1 = Convert.ToDecimal(DdlDistrict1.Text);
            result.ProvinceID1 = Convert.ToDecimal(DdlProvince1.Text);
            result.PostCode1 = TextBoxPostCode1.Text.Trim();
            result.Telephone1 = TextBoxTelephone1.Text.Trim();
            result.Mobile1 = TextBoxMobile1.Text.Trim();
            result.Fax1 = TextBoxFax1.Text.Trim();
            result.Email1 = TextBoxEmail1.Text.Trim();
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
            ButtonSendProjectInfo.Visible = (functions.Contains(Common.ProjectFunction.SaveDarft) && canSendProjectInfo);
            ButtonDelete.Visible = functions.Contains(Common.ProjectFunction.Delete);
            HyperLinkPrint.Visible = (functions.Contains(Common.ProjectFunction.PrintDataForm) && canSendProjectInfo);
  
            ButtonReject.Visible = functions.Contains(Common.ProjectFunction.Reject);
  
            FileUploadProsecute.Enabled = isEditable;

            //----------------------------
            
            if (model.Address1 != null)
            {
                DropDownListPrefix1.SelectedValue = model.Address1.Prefix1.ToString();
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

            //kenghot
            //List<ServiceModels.KendoAttachment> instructorFiles = new List<ServiceModels.KendoAttachment>();
      
            List<ServiceModels.KendoAttachment> instructorFiles = model.InstructorAttachments;
            if (model.InstructorAttachment != null)
            {
                instructorFiles.Add(model.InstructorAttachment);
            }
            //end kenghot
            FileUploadProsecute.ClearChanges();
            FileUploadProsecute.ExistingFiles = instructorFiles;
            FileUploadProsecute.DataBind();

       
            List<ServiceModels.KendoAttachment> vehicleFiles = model.VehicleAttachments;
            if (model.VehicleAttachment != null)
            {
                vehicleFiles.Add(model.VehicleAttachment);
            }
       
            //kenghot
            //List<ServiceModels.KendoAttachment> valunteerFiles = new List<ServiceModels.KendoAttachment>();
            List<ServiceModels.KendoAttachment> valunteerFiles = model.ValunteerAttachments ;
            if (model.ValunteerAttachment != null)
            {
                valunteerFiles.Add(model.ValunteerAttachment);
            }
       
                            
        }


        private void RegisterClientScript()
        {
            //String projectInfoTargetAmountChangeScript = @"
            //    function projectPersonalAddress3Validate(sender, args) {
            //        var comboBoxProvince3ID = '" + DdlProvince3.ClientID +@"';
            //        var comboBoxDistrict3ID = '" + DdlDistrict3.ClientID +@"';
            //        var comboBoxSubDistrict3ID = '" + DdlSubDistrict3.ClientID +@"';

            //        var controlToValidate = sender.controltovalidate;

            //        var firstName3 = $('#" + TextBoxFirstName3.ClientID +@"').val();
            //        firstName3 = $.trim(firstName3);
            //        var isValid = true;
            //        if ((firstName3 != '') && ((args.Value == '') || 
            //            (((controlToValidate == comboBoxProvince3ID) || (controlToValidate == comboBoxDistrict3ID) || (controlToValidate == comboBoxSubDistrict3ID)) && (args.Value == '')))) {
            //            isValid = false;
            //        }               
            //        args.IsValid = isValid;
            //    }
            //    ";
            //ScriptManager.RegisterClientScriptBlock(
            //           UpdatePanelPersonal,
            //           this.GetType(),
            //           "ProjectPersonalAddress3ValidateScript",
            //           projectInfoTargetAmountChangeScript,
            //           true);



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



            string provinceList = Nep.Project.Common.Web.WebUtility.ToJSON(GetProvince());

            String script = @"
                $(function () {                 
                    // Address 1
                    //setComboBox();
                 });
                    function setComboBox() {
                    console.log('commbo');
                    c2x.createLocalCombobox({                       
                        ControlID: '" + DdlProvince1.ClientID + @"',
                        Placeholder: '" + Nep.Project.Resources.UI.DropdownPleaseSelect + @"',
                        Enable:true,
                        TextField: 'Text',
                        ValueField: 'Value',
                        ServerFiltering: false,                        
                        Change: function(e){c2x.onProvinceComboboxChange('" + DdlDistrict1.ClientID + @"', '" + DdlSubDistrict1.ClientID + @"',e);
                        appVueQN.items[appVueQN.field.ddlProvince].v = e.sender._old;},
                       // Value: '" + province1Selected + @"',   
                        Value: appVueQN.items[appVueQN.field.ddlProvince].v,
                        Data:{Data:" + provinceList+@", TotalRow:0}                  
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
                        Change: function(e){c2x.onDistrictComboboxChange('" + DdlSubDistrict1.ClientID + @"',e);
                        appVueQN.items[appVueQN.field.ddlDistrict].v = e.sender._old;},
                       // Value: '" + district1Selected + @"',
                        Value: appVueQN.items[appVueQN.field.ddlDistrict].v,
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
                        Change: function(e) {appVueQN.items[appVueQN.field.ddlSubDistrict].v = e.sender._old;},
                        ReadUrl: './ComboboxHandler/GetSubDistrict',      
                       // Value: '" + subDistrict1Selected + @"', 
                        Value: appVueQN.items[appVueQN.field.ddlSubDistrict].v,
                        Param: function(){return c2x.getProvinceComboboxParam('" + DdlDistrict1.ClientID + @"');},           
                     });  

                    // Address 2

                //};
                   
                //})
                
            ";
              script += "scriptupload_" + FileUploadProsecute.ClientID + "();";
              script += "scriptupload_" + FileUploadProsecute2.ClientID + "();";
            script += "};";
            script += @"          
                function SaveAttachmentFiles() {
                   SaveAttachmentToDB(" + ProjectID.ToString() + "," +  FileUploadProsecute.Controls[1].ClientID + "," +  FileUploadProsecute.Controls[0].ClientID + @", 'PROSECUTE', 'PROSECUTE1');
                   SaveAttachmentToDB(" + ProjectID.ToString() + "," + FileUploadProsecute2.Controls[1].ClientID + "," + FileUploadProsecute2.Controls[0].ClientID + @", 'PROSECUTE', 'PROSECUTE2');
                 };
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