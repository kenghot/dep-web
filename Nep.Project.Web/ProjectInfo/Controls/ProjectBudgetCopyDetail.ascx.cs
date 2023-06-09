﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using Nep.Project.Common;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class ProjectBudgetCopyDetail : Nep.Project.Web.Infra.BaseUserControl
    {
        private string BUDGET_DETAIL_LIST_VIEWSTATE_KEY = "BUDGET_DETAIL_LIST";   
        private string BUDGET_DETAIL_ACTIVITY_VIEWSTATE_KEY = "BUDGET_DETAIL_ACTIVITY_VIEWSTATE_KEY";
        public IServices.IProjectInfoService _service { get; set; }
        private string QuestionareGroup = "BUDGET";
        private decimal NewCopyActivityID = 0;
        private List<Project.ServiceModels.ProjectInfo.BudgetActivity> GridViewActivityData {
            get
            {
                var obj = Session[BUDGET_DETAIL_ACTIVITY_VIEWSTATE_KEY];
                List<ServiceModels.ProjectInfo.BudgetActivity> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetActivity>)obj : new List<ServiceModels.ProjectInfo.BudgetActivity>();
                
                return list;
            } set
            {
                Session[BUDGET_DETAIL_ACTIVITY_VIEWSTATE_KEY] = value;
            }
        }
        public Boolean IsEditableProjectBudget
        {
            get
            {
                bool isEdit = false;
                if (ViewState["IsEditableProjectBudget"] != null)
                {
                    isEdit = Convert.ToBoolean(ViewState["IsEditableProjectBudget"]);
                }

                return isEdit;
            }
            set
            {
                ViewState["IsEditableProjectBudget"] = value;
            }
        }

        public Boolean IsApproved
        {
            get
            {
                bool isEdit = false;
                if (ViewState["IsProjectApproved"] != null)
                {
                    isEdit = Convert.ToBoolean(ViewState["IsProjectApproved"]);
                }

                return isEdit;
            }
            set
            {
                ViewState["IsProjectApproved"] = value;
            }
        }

        public Boolean IsRequestCenterBudget
        {
            get
            {
                bool isEdit = false;
                if (ViewState["IsRequestCenterBudget"] != null)
                {
                    isEdit = Convert.ToBoolean(ViewState["IsRequestCenterBudget"]);
                }

                return isEdit;
            }
            set
            {
                ViewState["IsRequestCenterBudget"] = value;
            }
        }

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
        public Decimal ActivityID
        {
            get
            {
                if (ViewState["ActivityID"] == null)
                {
                    string stID = Request.QueryString["actid"];
                    decimal id = 0;
                    Decimal.TryParse(stID, out id);
                    ViewState["ActivityID"] = id;
                }


                return (decimal)ViewState["ActivityID"];
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
            string compareScript = "ButtonAddBudgetDetail";

            string scriptManager = Request.Form["ctl00$ScriptManager1"];

            if ((IsPostBack) && ((scriptManager != null) && (scriptManager.Contains(compareScript))))
            {
                RegisterRequiredData();
               
            }


            
        }

        public void BindData()
        {
            //ButtonSendProjectInfo.Text = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Nep.Project.Resources.UI.ButtonSubmit : Nep.Project.Resources.UI.ButtonSendProjectInfo;

            BindDisplay();
            BindGridViewBudgetDetail();
            RegisterClientScript();
        }
        protected override void OnPreRender(EventArgs e)
        {

            base.OnPreRender(e);

            string scriptTag = Business.QuestionareHelper.CommonScript(UpdatePanelProjectBudget.ClientID, hdfQViewModel.ClientID, "");
           
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "QuestionareCommon" + this.ClientID, scriptTag, true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "QuestionareCommon" + this.ClientID,
                             scriptTag, true);
            //RegisterScriptToDoc();
        }
        private void RegisterScriptToDoc()
        {

            string doc = @" 
                function SelectBudgetPanel()
                {   console.log('select');
                    rdo = $('input[type=radio][name=BudgetType]');
                    var pn1 = $('#divBudgetType1')[0];
                    var pn2 = $('#divBudgetType2')[0];
                    pn2.style.display = 'none';
                    pn1.style.display = 'none'; 
                    if ($('div.divSupportBudgetType1')[0].style.display == 'none') 
                    {   QuestionareModel.SupportBudgetType('2'); }
                    else {   QuestionareModel.SupportBudgetType('1'); }
                   // if (rdo[0].checked) {
                    if (rdo[0].checked) {
                        pn2.style.display = 'none';
                        pn1.style.display = 'block';
                    console.log('showactivity');
                  document.getElementById('divactivity').style.display = 'block';
                    }
                    else if (rdo[1].checked) {
                        pn2.style.display = 'block';
                        pn1.style.display = 'none';            
                    }
                   SelectSupportBudgetPanel();
                    SelectMealCountRadio();
    }
                 
                function SelectSupportBudgetPanel() { console.log('support');
                rsb = $('input[type=radio][name=SupportBudgetType]');
                B21 = $('td.B21');
                B22 = $('td.B22');
                B21.hide();
                B22.hide();
                if (rsb[0].checked) {

                        B21.show();
                        B22.hide();
                    } else if (rsb[1].checked) {
                            B21.hide();
                            B22.show();
                }
                }

                function SelectMealCountRadio() { console.log('meal');
                rsb = $('input[type=radio][name=SelectMealCount]');
                rbGov = $('input[type=radio][name=Food3Meals]'); 
                var spanmeal = $('#span_meal_select')[0];
                     
                      $('.trshow2_1').hide();
                      $('.trshow2_2').hide();
                      $('.trshow3_1').hide();
                      $('.trshow3_2').hide();
                      $('.trmealbreakfast').hide();
                //var spanmealnongov = $('#meal_nongoverment')[0];
                if (rbGov[0].checked) {
                    if (rsb[0].checked) {spanmeal.textContent = 'จัดในสถานที่ราชการหรือเอกชนที่ไม่ใช่มืออาชีพ (ไม่เกิน 600 บาท/วัน/คน)'; $('.trmealbreakfast').show();}
                    else {spanmeal.textContent = 'จัดในสถานที่ราชการหรือเอกชนที่ไม่ใช่มืออาชีพ (ไม่เกิน 400 บาท/วัน/คน)';}    
                  
                      $('.trshow2_1').show();
                      $('.trshow2_2').hide();
                      $('.trshow3_1').hide();
                      $('.trshow3_2').show();
                    } else if (rbGov[1].checked) {
                    if (rsb[0].checked) {spanmeal.textContent = 'จัดในโรงแรม (ไม่เกิน 950 บาท/วัน/คน)'; $('.trmealbreakfast').show();}
                    else {spanmeal.textContent = 'จัดในโรงแรม (ไม่เกิน 700 บาท/วัน/คน)';}
                     
                      $('.trshow2_1').hide();
                      $('.trshow2_2').show();
                      $('.trshow3_1').show();
                      $('.trshow3_2').hide();
                }

               }



                ";
           // ScriptManager.RegisterClientScriptBlock(
           //UpdatePanelProjectBudget,
           //this.GetType(),
           //"DocumentStart",
           //doc,
           //true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DocumentStart",
                             doc, true);
            //String scriptUrl = ResolveUrl("~/Scripts/projectbudget.js?v=" + DateTime.Now.Ticks.ToString());
            ////String scriptUrl = ResolveUrl("~/Scripts/manage.processingplan.js?v=" + DateTime.Now.Ticks.ToString());
            //var refScript = "<script type='text/javascript' src='" + scriptUrl + "'></script>";
            //ScriptManager.RegisterClientScriptBlock(
            //           UpdatePanelProjectBudget,
            //           this.GetType(),
            //           "ValidateProjectBudget",
            //           refScript,
            //           false);

        }

        private void RegisterScriptRefreshQN()
        {
            var act = (from acts in _service.GetDB().PROJECTBUDGETACTIVITies where acts.ACTIVITYID == (NewCopyActivityID>0 ? NewCopyActivityID:ActivityID) select acts).FirstOrDefault();
            bool isManage = (act.ACTIVITYNAME == Constants.ACTIVITY_BUDGET_MANAGE_EXPENSE_CODE);
            var tabNone = isManage ? "tabNormalBudget" : "tabManageBudget"  ;
            string after = @"InitialOtherExpense();
                            CreateVueGrids();";
            if (isManage)
            {
                var bud = from buds in _service.GetDB().PROJECTBUDGETACTIVITies
                          where buds.PROJECTID == act.PROJECTID && buds.ACTIVITYNAME != Constants.ACTIVITY_BUDGET_MANAGE_EXPENSE_CODE
                          select buds;
                var totbud = bud.Sum(s => s.TOTALAMOUNT);
                totbud = totbud.HasValue ? totbud.Value : 0;
                var tot10p = totbud / 10;

                after += @"$('#divRDBudgetType2')[0].style.display = 'none';
                          QuestionareModel.BudgetType('1') ;
                          $('input[type=radio][name=BudgetType]')[0].checked = true;";
                after += string.Format("QuestionareModel.sum1to13('{0:##,##.#0}') ;", totbud);
                after += string.Format("QuestionareModel.cal10per('{0:##,##.#0}') ;", tot10p);
                after += "appVueOther.items = QuestionareModel.other_expenses();";
            }

            after += @"  SelectBudgetPanel(); 
                           $('#" + tabNone + "')[0].style.display = 'none';";
            string script = Business.QuestionareHelper.QuestionareJS(NewCopyActivityID > 0 ? NewCopyActivityID : ActivityID, QuestionareGroup, UpdatePanelProjectBudget.ClientID, hdfIsDisable.ClientID, "", after);
            RegisterScriptToDoc();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), QuestionareGroup + this.ClientID,
            //   script, true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), QuestionareGroup + this.ClientID,
                             script, true);
            string scriptVue = "";
            scriptVue = @"<script type=""text/javascript"" src=""../../Scripts/Vue/VueBudgetDetailGrid.js?v=" + DateTime.Now.Ticks.ToString() + @"""></script>";

        
            var setVueParam = @"";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "BudjectDetailGridJSFile", scriptVue, false);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "BudjectDetailGridInitialData" + this.ClientID,
                   setVueParam, true);
        }

        protected void BindDisplay()
        {
            try
            {
                RegisterScriptRefreshQN();

                
                decimal projectId = ProjectID;
                if (projectId > 0)
                {
                    HiddenFieldProjectID.Value = projectId.ToString();

                    ServiceModels.ProjectInfo.ProjectBudget model = new ServiceModels.ProjectInfo.ProjectBudget();
                    var result = _service.GetProjectBudgetInfoByProjectID(projectId);
                    if (result.IsCompleted && result.Data != null)
                    {
                        model = result.Data;

                        bool canSendProjectInfo = (model.RequiredSubmitData == null);
                        RequiredSubmitData = model.RequiredSubmitData;

                        List<Common.ProjectFunction> functions = _service.GetProjectFunction(model.ProjectID).Data;
                        //kenghot
                        //bool isEditable = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData));
                        var master = (this.Page.Master as MasterPages.SiteMaster);
                        //bool isEditable = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData)
                        //    || master.UserInfo.IsAdministrator);
                        bool isEditable = true;
                        // IsEditableProjectBudget = isEditable;
                        hdfIsDisable.Value = (!isEditable).ToString().ToLower();
                        IsEditableProjectBudget = false;
                        IsRequestCenterBudget = model.IsRequestCenter;

                        //ButtonSave.Visible = isEditable;
                        //ButtonAddBudgetDetail.Visible = isEditable;
                        ButtonAddBudgetDetail.Visible = false ;
                        //ButtonSendProjectInfo.Visible = (functions.Contains(Common.ProjectFunction.SaveDarft) && canSendProjectInfo);
                        //ButtonDelete.Visible = functions.Contains(Common.ProjectFunction.Delete);
                        //HyperLinkPrint.Visible = (functions.Contains(Common.ProjectFunction.PrintDataForm) && canSendProjectInfo);
                        //ButtonReject.Visible = functions.Contains(Common.ProjectFunction.Reject);

                        var act = model.BudgetActivities.Where(w => w.ActivityID == (NewCopyActivityID > 0 ? NewCopyActivityID :ActivityID)).FirstOrDefault();
                        TextBoxTotalAmount.Text = string.Format("{0:##,#0.#0}",  act.TotalAmount);
                        //TextBoxReviseAmount.Text = model.TotalReviseAmount.ToString();
                        LabelTotalBudgetAmount.Text = TextBoxTotalAmount.Text;

                    
                         GridViewActivityData = model.BudgetActivities;


                        //lab
                        // TextBoxTotalAmount.Enabled = false;
                        //TextBoxTotalAmount.Attributes.Add("disabled", "true");
                        //if (model.IsBudgetGotSupport != null)
                        //{
                        //    if (model.IsBudgetGotSupport == true)
                        //    {
                        //        RadioButtonGotSupportYes.Checked = true;
                        //        TextBoxGotSupportName.Text = model.BudgetGotSupportName;
                        //        TextBoxGotSupportAmount.Text = model.BudgetGotSupportAmount.ToString();
                        //    }
                        //    else
                        //    {
                        //        RadioButtonGotSupportNo.Checked = true;
                        //    }
                        //}

                        if (model.ApprovalStatus == "1")
                        {
                            IsApproved = true;
                           // DivBudgetDetailRequestAmount.Visible = true;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Budget", ex);
                ShowErrorMessageNew(ex.Message);
            }
        }
        private string SaveQN()
        {
            string ret = "";
            try
            {
                if (true)
                {
                   

                    var result = _service.SaveProjectQuestionareResult(NewCopyActivityID>0? NewCopyActivityID :ActivityID, QuestionareGroup, hdfQViewModel.Value, true, false,Request.UserHostAddress);
                    if (result.IsCompleted)
                    {
                        //Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        //page.RebindData("TabPanelProjectBudget");
                        //ShowResultMessage(result.Message);
                    }
                    else
                    {
                        ret = result.Message[0];
                    }
                }else
                {
                    ret = "ไม่สามารถบันทึกข้อมูลได้";
                }

            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
                ret = ex.Message ;
            }
            return ret;
        }
 
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            ServiceModels.ProjectInfo.ProjectBudget model = new ServiceModels.ProjectInfo.ProjectBudget();
            try
            {
                ShowErrorMessageNew("");
                if (!Page.IsValid)
                {
                    SaveQN();
                    RegisterScriptRefreshQN();
                   // this.BindData();
                    return;
                }
                    

                var qn = AddQNToBudget();
                if (qn != "")
                {
                    ShowErrorMessageNew(qn);
                    SaveQN();
                    RegisterScriptRefreshQN();
                    return;
                }
                var saveqn = SaveQN();
                if (saveqn != "")
                {
                    ShowErrorMessageNew(saveqn);
                    return;
                }

                // return;
                //if (Page.IsValid)
             
                 if (true){
                    decimal projectId = Convert.ToDecimal(HiddenFieldProjectID.Value);
                    decimal? totalAmount = Convert.ToDecimal(TextBoxTotalAmount.Text.Trim());
                    bool? supportFlag = null;
                    string supportName = string.Empty;
                    string tmpSupportAmount;
                    decimal? supportAmount = (decimal?)null;
                    //if (RadioButtonGotSupportYes.Checked)
                    //{
                    //    supportFlag = true;
                    //    supportName = TextBoxGotSupportName.Text.Trim();
                    //    tmpSupportAmount = TextBoxGotSupportAmount.Text;
                    //    supportAmount = Convert.ToDecimal(tmpSupportAmount);
                    //}

                    //if (RadioButtonGotSupportNo.Checked)
                    //{
                    //    supportFlag = false;
                    //}
                    

                    var viewStateDetail = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
                    List<ServiceModels.ProjectInfo.BudgetDetail> list = (viewStateDetail != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)viewStateDetail : new List<ServiceModels.ProjectInfo.BudgetDetail>();
                    model.ActivityID = ActivityID;
                    model.ProjectID = projectId;
                    model.TotalRequestAmount = totalAmount;                  
                    model.IsBudgetGotSupport = supportFlag;
                    model.BudgetGotSupportName = supportName;
                    model.BudgetGotSupportAmount = supportAmount;
                    model.BudgetDetails = list;
                    model.BudgetActivities = GridViewActivityData;

                    var result = _service.SaveProjectBudgetActivity(model);
                    if (result.IsCompleted)
                    {
                        RequiredSubmitData = result.Data.RequiredSubmitData;
                        // Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        //  page.RebindData("TabPanelProjectBudget");      

                        //ShowResultMessage(result.Message);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "savecomplete",
                             "c2x.closeFormDialog();", true);
                    }
                    else
                    {
                        ShowErrorMessageNew(result.Message);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
                ShowErrorMessageNew(ex.Message);
            }
        }
            protected void ButtonCopyBudget_Click(object sender, EventArgs e)
        {
            ServiceModels.ProjectInfo.ProjectBudget model = new ServiceModels.ProjectInfo.ProjectBudget();
            try
            {
                //Beer03092021
                if (TextBoxAddActivityName.Text.Trim() == Constants.ACTIVITY_BUDGET_MANAGE_EXPENSE_CODE)
                {
                    ShowErrorMessage("ไม่สามารถตั้งชื่อ '" + Constants.ACTIVITY_BUDGET_MANAGE_EXPENSE_CODE + "' ซ้ำได้");
                    return;
                }
                var list = GridViewActivityData;
                var newAct = new ServiceModels.ProjectInfo.BudgetActivity
                {
                    ActivityDESC = TextBoxAddActivityDESC.Text.Trim(),
                    ActivityName = TextBoxAddActivityName.Text.Trim(),
                    TotalAmount = 0

                };

                list.Add(newAct);
                GridViewActivityData = list;
                UpdateActivityRunno();
                //GridViewActivity.DataSource = GridViewActivityData;
                // GridViewActivity.DataBind();
                TextBoxAddActivityDESC.Text = "";
                TextBoxAddActivityName.Text = "";
                SaveData(false);

                var qn = AddQNToBudget();
                if (qn != "")
                {
                    ShowErrorMessageNew(qn);
                    SaveQN();
                    RegisterScriptRefreshQN();
                    return;
                }
                var saveqn = SaveQN();
                if (saveqn != "")
                {
                    ShowErrorMessageNew(saveqn);
                    return;
                }

                // return;
                //if (Page.IsValid)

                if (true)
                {
                    decimal projectId = Convert.ToDecimal(HiddenFieldProjectID.Value);
                    decimal? totalAmount = Convert.ToDecimal(TextBoxTotalAmount.Text.Trim());
                    bool? supportFlag = null;
                    string supportName = string.Empty;
                    string tmpSupportAmount;
                    decimal? supportAmount = (decimal?)null;


                    var viewStateDetail = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
                    List<ServiceModels.ProjectInfo.BudgetDetail> list2 = (viewStateDetail != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)viewStateDetail : new List<ServiceModels.ProjectInfo.BudgetDetail>();
                    model.ActivityID = NewCopyActivityID>0 ? NewCopyActivityID :ActivityID;
                    model.ProjectID = projectId;
                    model.TotalRequestAmount = totalAmount;
                    model.IsBudgetGotSupport = supportFlag;
                    model.BudgetGotSupportName = supportName;
                    model.BudgetGotSupportAmount = supportAmount;
                    model.BudgetDetails = list2;
                    model.BudgetActivities = GridViewActivityData;

                    var result = _service.SaveProjectBudgetActivity(model);
                    if (result.IsCompleted)
                    {
                        RequiredSubmitData = result.Data.RequiredSubmitData;
                        // Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        //  page.RebindData("TabPanelProjectBudget");      

                        //ShowResultMessage(result.Message);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "savecomplete",
                             "c2x.closeFormDialog();", true);
                    }
                    else
                    {
                        ShowErrorMessageNew(result.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
                ShowErrorMessageNew(ex.Message);
            }
        }
        private void SaveData(bool CheckValid)
        {
            ServiceModels.ProjectInfo.ProjectBudget model = new ServiceModels.ProjectInfo.ProjectBudget();
            try
            {
                if (Page.IsValid || !CheckValid)
                {
                    decimal projectId = Convert.ToDecimal(HiddenFieldProjectID.Value);
                    if (string.IsNullOrEmpty(TextBoxTotalAmount.Text))
                    {
                        TextBoxTotalAmount.Text = "0.00";
                    }
                    decimal? totalAmount = Convert.ToDecimal(TextBoxTotalAmount.Text.Trim());
                    bool? supportFlag = null;
                    string supportName = string.Empty;
                    string tmpSupportAmount;
                    decimal? supportAmount = (decimal?)null;

                    var viewStateDetail = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
                    List<ServiceModels.ProjectInfo.BudgetDetail> list = (viewStateDetail != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)viewStateDetail : new List<ServiceModels.ProjectInfo.BudgetDetail>();

                    model.ProjectID = projectId;
                    model.TotalRequestAmount = totalAmount;
                    model.IsBudgetGotSupport = supportFlag;
                    model.BudgetGotSupportName = supportName;
                    model.BudgetGotSupportAmount = supportAmount;
                    model.BudgetDetails = new List<ServiceModels.ProjectInfo.BudgetDetail>();   // list;
                    model.BudgetActivities = GridViewActivityData;
                    model.BudgetDetails = list;

                    var result = _service.SaveProjectBudget(model);
                    if (result.IsCompleted)
                    {
                        NewCopyActivityID = (Decimal)result.Data.BudgetActivities[result.Data.BudgetActivities.Count - 1].ActivityID;
                        RequiredSubmitData = result.Data.RequiredSubmitData;
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelProjectBudget");
                        ShowResultMessage(result.Message);
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
        private void UpdateActivityRunno()
        {
            long i = 1;
            var data = GridViewActivityData;
            foreach (ServiceModels.ProjectInfo.BudgetActivity a in data)
            {
                a.RunNo = i;
                i++;
            }
            GridViewActivityData = data;
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Decimal projectId = 0;
                decimal.TryParse(HiddenFieldProjectID.Value, out projectId);
                if (projectId > 0)
                {
                    var result = _service.DeleteProject(projectId);
                    if (result.IsCompleted)
                        Response.Redirect(Page.ResolveClientUrl("~/ProjectInfo/ProjectInfoList.aspx?isDeleteSuccess=true"));
                    else
                        ShowErrorMessageNew(result.Message);
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
                ShowErrorMessageNew(ex.Message);
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
                        page.RebindData("TabPanelProjectBudget");
                        string message = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Message.SubmitDataToReviewSuccess : Message.SendDataToReviewSuccess;
                        ShowResultMessage(message);
                    }
                    else
                    {
                        ShowErrorMessageNew(sendDataToReviewResult.Message[0]);
                    }
                //}
                //else
                //{
                //    ShowErrorMessageNew(result.Message);
                //}
            }
        }

        protected void ProjectBudgetDetailValidate(object source, ServerValidateEventArgs args)
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = null;
            if (obj != null)
            {
                list = (List<ServiceModels.ProjectInfo.BudgetDetail>)obj;
            }
            //kenghot18
            // args.IsValid = ((list != null) && (list.Count > 0));
            args.IsValid = true;
        }


        //protected void ProjectBudgetDetailValidateTotalAmount(object source, ServerValidateEventArgs args)
        //{
        //    var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
        //    List<ServiceModels.ProjectInfo.BudgetDetail> list = null;
        //    bool isValid = true;

        //    Label labelTotalBudgetAmount = (Label)GridViewBudgetDetail.FooterRow.FindControl("LabelTotalBudgetAmount");
        //    if (labelTotalBudgetAmount != null)
        //    {
        //        String totalBudgetDetailAmountText = labelTotalBudgetAmount.Text.Replace(",", "");
        //        Decimal totalBudgetDetailAmount = 0;
        //        Decimal.TryParse(totalBudgetDetailAmountText, out totalBudgetDetailAmount);

        //        string textBoxTotalAmountValue = TextBoxTotalAmount.Text;
        //        Decimal totalBudgetAmount = 0;
        //        Decimal.TryParse(textBoxTotalAmountValue, out totalBudgetAmount);

        //        if (totalBudgetDetailAmount != totalBudgetAmount)
        //        {
        //            isValid = false;
        //        }
        //    }
           
        //    args.IsValid = isValid;
        //}

        #region Grid
        //method for binding GridView
        protected void BindGridViewBudgetDetail()
        {
            List<ServiceModels.ProjectInfo.BudgetDetail> list = new List<ServiceModels.ProjectInfo.BudgetDetail>();

            decimal projectId = Convert.ToDecimal(HiddenFieldProjectID.Value);
            ServiceModels.ProjectInfo.ProjectBudget model = new ServiceModels.ProjectInfo.ProjectBudget();
            var result = _service.GetProjectBudgetInfoByProjectID(projectId);
            if (result.IsCompleted)
            {
                foreach (var item in result.Data.BudgetDetails.Where(w => w.ActivityID == (NewCopyActivityID > 0 ? NewCopyActivityID : ActivityID)).ToList() )
                {
                    ServiceModels.ProjectInfo.BudgetDetail d = new ServiceModels.ProjectInfo.BudgetDetail();
                    d.UID = Guid.NewGuid().ToString();
                    d.ProjectBudgetID = item.ProjectBudgetID;
                    d.No = item.No;
                    d.Detail = item.Detail;
                    d.Amount = item.Amount;
                    d.ReviseAmount = item.ReviseAmount;
                    d.Revise1Amount = item.Revise1Amount;
                    d.Revise2Amount = item.Revise2Amount;
                    d.ReviseDetail = item.ReviseDetail;
                    d.BudgetCode = item.BudgetCode;
                    list.Add(d);
                }

                ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = list;
            }

            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            if (obj != null)
            {
                list = (List<ServiceModels.ProjectInfo.BudgetDetail>)obj;
                GridViewBudgetDetail.DataSource = list;
                GridViewBudgetDetail.DataBind();
            }
            else
            {
                list = new List<ServiceModels.ProjectInfo.BudgetDetail>();
                list.Add(new ServiceModels.ProjectInfo.BudgetDetail()
                {
                    UID = Guid.NewGuid().ToString(),
                    Detail = "",
                    Amount = null
                });
                GridViewBudgetDetail.DataSource = list;
                GridViewBudgetDetail.EditIndex = 0;
                GridViewBudgetDetail.DataBind();
            }
        }

        protected void GridViewBudgetDetail_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
           
            if (!IsEditableProjectBudget)
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
                
            }

            if(IsApproved && IsRequestCenterBudget){
                e.Row.Cells[3].Visible = false;               
            }
            else if (IsApproved)
            {
                e.Row.Cells[4].Visible = false;      
            }
            else
            {
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
            }
            
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[1].Visible = false;

                Dictionary<string, decimal> budget = GetTotalBudget();

                Label labelRequest =  (Label)e.Row.Cells[3].FindControl("LabelTotalBudgetAmount");
                if (labelRequest != null)
                {
                    labelRequest.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(budget["Rquest"], "N2", "");
                }


                Label labelRevise1 = (Label)e.Row.Cells[4].FindControl("LabelTotalRevise1Amount");
                if (labelRevise1 != null)
                {                   
                    labelRevise1.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(budget["Revise1"], "N2", "");                                     
                }

                Label labelRevise2 = (Label)e.Row.Cells[5].FindControl("LabelTotalRevise2Amount");
                if (labelRevise2 != null)
                {
                    labelRevise2.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(budget["Revise2"], "N2", "");
                }

            }
        }

        protected void GridViewBudgetDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewBudgetDetail.EditIndex = e.NewEditIndex;
            RebindBudgetDetailDataSource();
        }

        protected void GridViewBudgetDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();
            if (list.Count > 0)
            {
                GridViewBudgetDetail.EditIndex = -1;
                RebindBudgetDetailDataSource();
            }
            
        }
        public void GetBudgetType2(List<ServiceModels.ProjectInfo.BudgetDetail> list, Newtonsoft.Json.Linq.JObject jo)
        {
            //1.1
            CreateBudgetDetail(list, "B21_1_1", "สนับสนุนการจัดตั้งศูนย์บริการคนพิการทั่วไป  ค่าจัดสิ่งอำนวยความสะดวกสำหรับคนพิการ", jo, new string[] { "B21_1_1" }, new string[] { "บาท" }, 0);
            //local
            CreateBudgetDetail(list, "B22_1_1", "สนับสนุนการจัดตั้งศูนย์บริการคนพิการทั่วไป ค่าจัดสิ่งอำนวยความสะดวกสำหรับคนพิการ", jo, new string[] { "B22_1_1" }, new string[] { "บาท" }, 0);
            //1.2
            CreateBudgetDetail(list, "B21_1_2", "สนับสนุนการจัดตั้งศูนย์บริการคนพิการทั่วไป ค่าจัดทำป้ายชื่อศูนย์บริการคนพิการทั่วไป", jo, new string[] { "B21_1_2" }, new string[] { "บาท" }, 0);
            //local
            CreateBudgetDetail(list, "B22_1_2", "สนับสนุนการจัดตั้งศูนย์บริการคนพิการทั่วไป ค่าจัดทำป้ายชื่อศูนย์บริการคนพิการทั่วไป", jo, new string[] { "B22_1_2" }, new string[] { "บาท" }, 0);

            //2.1
            CreateBudgetDetail(list, "B21_2_1", "การบริหารจัดการศูนย์บริการคนพิการทั่วไป ค่าสาธารณูปโภค", jo, new string[] { "B21_2_1_1", "B21_2_1_2" }, new string[] { "บาท", "วัน" }, 0);
            //2.2
            CreateBudgetDetail(list, "B21_2_2", "การบริหารจัดการศูนย์บริการคนพิการทั่วไป ค่าตอบแทนผู้ปฏิบัติงาน", jo, new string[] { "B21_2_2_1", "B21_2_2_2", "B21_2_2_3" }, new string[] { "บาท", "คน", "วัน" }, 0);

            //2.3.1
            CreateBudgetDetail(list, "B21_2_3_1", "ค่าใช้จ่ายในการจัดประชุม คณะกรรมการบริหาร ศูนย์บริการคนพิการทั่วไป (ค่าอาหารกลางวัน)", jo, new string[] { "B21_2_3_1_1", "B21_2_3_1_2", "B21_2_3_1_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //2.3.2
            CreateBudgetDetail(list, "B21_2_3_2", "ค่าใช้จ่ายในการจัดประชุม คณะกรรมการบริหาร ศูนย์บริการคนพิการทั่วไป (ค่าอาหารว่างและเครื่องดื่ม)", jo, new string[] { "B21_2_3_2_1", "B21_2_3_2_2", "B21_2_3_2_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //2.3.2
            CreateBudgetDetail(list, "B21_2_3_3", "ค่าใช้จ่ายในการจัดประชุม คณะกรรมการบริหาร ศูนย์บริการคนพิการทั่วไป (ค่าพาหนะเดินทาง)", jo, new string[] { "B21_2_3_3_1", "B21_2_3_3_2", "B21_2_3_3_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //local
            //2.3.1
            CreateBudgetDetail(list, "B22_2_3_1", "ค่าใช้จ่ายในการจัดประชุม คณะกรรมการบริหาร ศูนย์บริการคนพิการทั่วไป (ค่าอาหารกลางวัน)", jo, new string[] { "B22_2_3_1_1", "B22_2_3_1_2", "B22_2_3_1_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //2.3.2
            CreateBudgetDetail(list, "B22_2_3_2", "ค่าใช้จ่ายในการจัดประชุม คณะกรรมการบริหาร ศูนย์บริการคนพิการทั่วไป (ค่าอาหารว่างและเครื่องดื่ม)", jo, new string[] { "B22_2_3_2_1", "B22_2_3_2_2", "B22_2_3_2_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //2.3.2
            CreateBudgetDetail(list, "B22_2_3_3", "ค่าใช้จ่ายในการจัดประชุม คณะกรรมการบริหาร ศูนย์บริการคนพิการทั่วไป (ค่าพาหนะเดินทาง)", jo, new string[] { "B22_2_3_3_1", "B22_2_3_3_2", "B22_2_3_3_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);

            //2.4
       
            CreateBudgetDetail(list, "B21_2_4", "ค่าวัสดุอุปกรณ์สำนักงาน", jo, new string[] { "B21_2_4_1", "B21_2_4_2" }, new string[] { "บาท" ,"เดือน" }, 0);
            //local
            CreateBudgetDetail(list, "B22_2_4", "ค่าวัสดุอุปกรณ์สำนักงาน", jo, new string[] { "B22_2_4_1", "B22_2_4_2" }, new string[] { "บาท", "เดือน" }, 0);

            //3.1
            CreateBudgetDetail(list, "B21_3_1", "การประเมินศักยภาพคนพิการ และทำแผนพัฒนาศักยภาพ คนพิการรายบุคคล ก่อนการจัดบริการ", jo, new string[] { "B21_3_1_1", "B21_3_1_2", "B21_3_1_3" }, new string[] { "บาท", "คน" ,"ครั้ง"}, 0);
            //3.2
            CreateBudgetDetail(list, "B21_3_2", "การฝึกทักษะด้านการทำ ความคุ้นเคยกับสภาพแวดล้อม และการเคลื่อนไหว (Orientation & Mobility : O&M) สำหรับ คนพิการทางการเห็น", jo, new string[] { "B21_3_2_1", "B21_3_2_2" }, new string[] { "บาท", "คน" }, 0);
            //3.3
            CreateBudgetDetail(list, "B21_3_3", "การบริการผู้ช่วยคนพิการ", jo, new string[] { "B21_3_3_1", "B21_3_3_2", "B21_3_3_3" }, new string[] { "บาท", "คน", "วัน"}, 0);
            //local
            CreateBudgetDetail(list, "B22_3_3", "การบริการผู้ช่วยคนพิการ", jo, new string[] { "B22_3_3_1", "B22_3_3_2", "B22_3_3_3" }, new string[] { "บาท", "คน", "วัน" }, 0);
            //3.4
            CreateBudgetDetail(list, "B21_3_4", "การบริการล่ามภาษามือ", jo, new string[] { "B21_3_4_1", "B21_3_4_2", "B21_3_4_3", "B21_3_4_4" }, new string[] { "บาท", "คน", "วัน", "ครั้ง" }, 0);
            //local
            CreateBudgetDetail(list, "B22_4_3", "การบริการล่ามภาษามือ", jo, new string[] { "B22_3_4_1", "B22_3_4_2", "B22_3_4_3", "B22_3_4_4" }, new string[] { "บาท", "คน", "วัน","ครั้ง" }, 0);

            //3.5
          
            var jv = jo["B21_3_5_text"].ToString();
            jv = (string.IsNullOrEmpty(jv)) ? "" : jv;
            string b1Other = "การปรับสภาพแวดล้อม ที่อยู่อาศัยให้แก่คนพิการ ";
            if (!string.IsNullOrEmpty(jv)) b1Other += "\n" + jv;
         
            CreateBudgetDetail(list, "B21_3_5",b1Other, jo, new string[] { "B21_3_5" }, new string[] { "บาท" }, 0);
            //local
            jv = jo["B22_3_5_text"].ToString();
            jv = (string.IsNullOrEmpty(jv)) ? "" : jv;
            b1Other = "การปรับสภาพแวดล้อม ที่อยู่อาศัยให้แก่คนพิการ ";
            if (!string.IsNullOrEmpty(jv)) b1Other += "\n" + jv;
            CreateBudgetDetail(list, "B22_3_5", b1Other, jo, new string[] { "B22_3_5" }, new string[] { "บาท" }, 0);

            //3.6
            CreateBudgetDetail(list, "B21_3_6", "การฟื้นฟูสมรรถภาพ ทางด้านร่างกาย", jo, new string[] { "B21_3_6_1", "B21_3_6_2", "B21_3_6_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //3.7.1
            CreateBudgetDetail(list, "B21_3_7_1", "การพัฒนาทักษะการช่วยเหลือตนเอง รายบุคคล", jo, new string[] { "B21_3_7_1_1", "B21_3_7_1_2", "B21_3_7_1_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //3.7.2
            CreateBudgetDetail(list, "B21_3_7_2", "การพัฒนาทักษะการช่วยเหลือตนเอง รายกลุ่ม", jo, new string[] { "B21_3_7_2_1", "B21_3_7_2_2", "B21_3_7_2_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //3.8.1
            CreateBudgetDetail(list, "B21_3_8_1", "การพัฒนาทักษะทางการพูด รายบุคคล", jo, new string[] { "B21_3_8_1_1", "B21_3_8_1_2", "B21_3_8_1_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //3.8.2
            CreateBudgetDetail(list, "B21_3_8_2", "การพัฒนาทักษะทางการพูด รายกลุ่ม", jo, new string[] { "B21_3_8_2_1", "B21_3_8_2_2", "B21_3_8_2_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //3.9.1
            CreateBudgetDetail(list, "B21_3_9_1", "การพัฒนาสู่สุขภาวะ รายบุคคล", jo, new string[] { "B21_3_9_1_1", "B21_3_9_1_2", "B21_3_9_1_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //3.9.2
            CreateBudgetDetail(list, "B21_3_9_2", "การพัฒนาสู่สุขภาวะ รายกลุ่ม", jo, new string[] { "B21_3_9_2_1", "B21_3_9_2_2", "B21_3_9_2_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //3.10.1
            CreateBudgetDetail(list, "B21_3_10_1", "การพัฒนาสู่สุขภาวะ รายบุคคล", jo, new string[] { "B21_3_10_1_1", "B21_3_10_1_2", "B21_3_10_1_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //3.10.2
            CreateBudgetDetail(list, "B21_3_10_2", "การปรับพฤติกรรม รายกลุ่ม", jo, new string[] { "B21_3_10_2_1", "B21_3_10_2_2", "B21_3_10_2_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);

            //3.11
            CreateBudgetDetail(list, "B21_3_11", "การพัฒนาทักษะทางการได้ยิน รายบุคคล", jo, new string[] { "B21_3_11_1", "B21_3_11_2", "B21_3_11_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
           
            //3.12.1
            CreateBudgetDetail(list, "B21_3_12_1", "การพัฒนาทักษะทางการเห็น รายบุคคล", jo, new string[] { "B21_3_12_1_1", "B21_3_12_1_2", "B21_3_12_1_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //3.12.2
            CreateBudgetDetail(list, "B21_3_12_2", "การพัฒนาทักษะทางการเห็น รายกลุ่ม", jo, new string[] { "B21_3_12_2_1", "B21_3_12_2_2", "B21_3_12_2_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //3.13.1
            CreateBudgetDetail(list, "B21_3_13_1", "การเสริมสร้างพัฒนาการ รายบุคคล", jo, new string[] { "B21_3_13_1_1", "B21_3_13_1_2", "B21_3_13_1_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);
            //3.13.2
            CreateBudgetDetail(list, "B21_3_13_2", "การเสริมสร้างพัฒนาการ รายกลุ่ม", jo, new string[] { "B21_3_13_2_1", "B21_3_13_2_2", "B21_3_13_2_3" }, new string[] { "บาท", "คน", "ครั้ง" }, 0);

            //3.14
            CreateBudgetDetail(list, "B21_3_14", "บริการกายอุปกรณ์ รถโยกคนพิการ", jo, new string[] { "B21_3_14_1", "B21_3_14_2" }, new string[] { "บาท", "อัน" }, 0);
            CreateBudgetDetail(list, "B21_3_14_1", "บริการกายอุปกรณ์ ไม้เท้าขาว", jo, new string[] { "B21_3_14_1_1", "B21_3_14_2_1" }, new string[] { "บาท", "อัน" }, 0);
            //local
            CreateBudgetDetail(list, "B22_3_14", "บริการกายอุปกรณ์ รถโยกคนพิการ", jo, new string[] { "B22_3_14_1", "B22_3_14_2" }, new string[] { "บาท", "อัน" }, 0);
            CreateBudgetDetail(list, "B22_3_14_1", "บริการกายอุปกรณ์ ไม้เท้าขาว", jo, new string[] { "B22_3_14_1_1", "B22_3_14_2_1" }, new string[] { "บาท", "อัน" }, 0);
            //3.15
            CreateBudgetDetail(list, "B21_3_15", "ค่าบริการประสานส่งต่อ", jo, new string[] { "B21_3_15_1", "B21_3_15_2" }, new string[] { "บาท", "คน" }, 0);
            
            //3.16.1
            CreateBudgetDetail(list, "B21_3_16_1", "ค่าพาหนะนำพาคนพิการ รถจักรยานยนต์", jo, new string[] { "B21_3_16_1_1", "B21_3_16_1_2"  }, new string[] { "บาท", "กม."  }, 0);
            //3.16.2
            CreateBudgetDetail(list, "B21_3_16_2", "ค่าพาหนะนำพาคนพิการ รถยนต์", jo, new string[] { "B21_3_16_2_1", "B21_3_16_2_2"  }, new string[] { "บาท", "กม."  }, 0);
            //local
            //3.16.1
            CreateBudgetDetail(list, "B22_3_16_1", "ค่าพาหนะนำพาคนพิการ รถจักรยานยนต์", jo, new string[] { "B22_3_16_1_1", "B22_3_16_1_2" }, new string[] { "บาท", "กม." }, 0);
            //3.16.2
            CreateBudgetDetail(list, "B22_3_16_2", "ค่าพาหนะนำพาคนพิการ รถยนต์", jo, new string[] { "B22_3_16_2_1", "B22_3_16_2_2" }, new string[] { "บาท", "กม." }, 0);
        }
        public void GetBudgetType1(List<ServiceModels.ProjectInfo.BudgetDetail> list, Newtonsoft.Json.Linq.JObject jo)
        {   //1.1
            //CreateBudgetDetail(list, "B1_1_1_1", "ค่าอาหาร (จัดอาหารครบ 3 มื้อ) จัดในสถานที่ราชการหรือเอกชนที่ไม่ใช่มืออาชีพ ", jo, new string[] { "B1_1_1_1" }, new string[] { "บาท" }, 0);
            string meal1_1 = string.Format("ค่าอาหารจัดในสถานที่{0} ({1})"
                , (jo["Food3Meals"].ToString() == "1") ? "ราชการ" : "เอกชน" 
                , jo["SelectMealCount"].ToString() == "1" ? "ครบ 3 มื้อ" : "ไม่ครบ 3 มื้อ");
          
            CreateBudgetDetail(list, "B1_1_1_2_M", meal1_1 + " อาหารเช้า", jo, new string[] { "B1_1_1_2_M_1", "B1_1_1_2_M_2", "B1_1_1_2_M_3" }, new string[] { "คน" ,"มื้อ","บาท" }, 0);
            CreateBudgetDetail(list, "B1_1_1_2_L", meal1_1 + " อาหารกลางวัน", jo, new string[] { "B1_1_1_2_L_1", "B1_1_1_2_L_2", "B1_1_1_2_L_3" }, new string[] { "คน", "มื้อ", "บาท" }, 0);
            CreateBudgetDetail(list, "B1_1_1_2_D", meal1_1 + " อาหารเย็น", jo, new string[] { "B1_1_1_2_D_1", "B1_1_1_2_D_2", "B1_1_1_2_D_3" }, new string[] { "คน", "มื้อ", "บาท" }, 0);
            //1.2
           // CreateBudgetDetail(list, "B1_1_2_1", "ค่าอาหาร (ไม่ครบ 3 มื้อ) จัดในสถานที่ราชการหรือเอกชนที่ไม่ใช่มืออาชีพ ", jo, new string[] { "B1_1_2_1" }, new string[] { "บาท" }, 0);
           // CreateBudgetDetail(list, "B1_1_2_2_L", "ค่าอาหารจัดในโรงแรม อาหารกลางวัน", jo, new string[] { "B1_1_2_2_L_1", "B1_1_2_2_L_2", "B1_1_2_2_L_3" }, new string[] { "คน", "มื้อ", "บาท" }, 0);
            //CreateBudgetDetail(list, "B1_1_2_2_D", "ค่าอาหารจัดในโรงแรม อาหารเย็น", jo, new string[] { "B1_1_2_2_D_1", "B1_1_2_2_D_2", "B1_1_2_2_D_3" }, new string[] { "คน", "มื้อ", "บาท" }, 0);
            //2.1.1
            CreateBudgetDetail(list, "B1_2_1", "ค่าอาหารว่างและเครื่องดื่ม จัดในสถานที่ราชการหรือเอกชนที่ไม่ใช่มืออาชีพ", jo, new string[] { "B1_2_1_1", "B1_2_1_2", "B1_2_1_3" }, new string[] { "คน", "มื้อ", "บาท" }, 35);
            CreateBudgetDetail(list, "B1_2_2", "ค่าอาหารว่างและเครื่องดื่ม จัดในสถานที่เอกชนหรือโรงแรม", jo, new string[] { "B1_2_2_1", "B1_2_2_2", "B1_2_2_3" }, new string[] { "คน", "มื้อ", "บาท" }, 75);
            //3.1.1
            CreateBudgetDetail(list, "B1_3_1_1", "ค่าที่พัก ค่าเช่าห้องพักคนเดียว", jo, new string[] { "B1_3_1_1_1", "B1_3_1_1_2", "B1_3_1_1_3" }, new string[] { "คน", "คืน", "บาท" }, 75);
            //3.1.2
            CreateBudgetDetail(list, "B1_3_1_2", "ค่าที่พัก ค่าเช่าห้องพักคู่", jo, new string[] { "B1_3_1_2_1", "B1_3_1_2_2", "B1_3_1_2_3" }, new string[] { "คน", "คืน", "บาท" }, 75);
            //3.1.3
            CreateBudgetDetail(list, "B1_3_1_3", "ค่าที่พัก กรณีผู้จัดไม่จัดที่พัก", jo, new string[] { "B1_3_1_3_1", "B1_3_1_3_2", "B1_3_1_3_3" }, new string[] { "คน", "คืน", "บาท" }, 75);
            //3.2.1
            CreateBudgetDetail(list, "B1_3_2_1", "ค่าที่พัก เหมาจ่ายเป็นค่าสาธารณูปโภค", jo, new string[] { "B1_3_2_1_1", "B1_3_2_1_2", "B1_3_2_1_3" }, new string[] { "คน", "คืน", "บาท" }, 75);
            //4.1
            CreateBudgetDetail(list, "B1_4_1", "ค่าพาหนะ เดินทางภายในจังหวัดหน่วยจัด", jo, new string[] { "B1_4_1_1", "B1_4_1_3", "B1_4_1_2"  }, new string[] { "คน", "วัน", "บาท" }, 1000);
            //4.2
            CreateBudgetDetail(list, "B1_4_2", "ค่าพาหนะ เดินทางจากจังหวัดที่มีพื้นที่ติดต่อกับหน่วยจัด", jo, new string[] { "B1_4_2_1", "B1_4_2_3", "B1_4_2_2" }, new string[] { "คน", "วัน", "บาท" }, 1200);
            //4.3
            CreateBudgetDetail(list, "B1_4_3", "ค่าพาหนะ เดินทางจากทั่วประเทศมายังหน่วยจัด", jo, new string[] { "B1_4_3_1", "B1_4_3_3", "B1_4_3_2" }, new string[] { "คน", "วัน", "บาท" }, 0);
            //4.4.1
            CreateBudgetDetail(list, "B1_4_4_1", "เดินทางโดยเครื่องบิน วิทยากร เดินทางโดยเครื่องบินชั้นประหยัด", jo, new string[] { "B1_4_4_1_1", "B1_4_4_1_3", "B1_4_4_1_2" }, new string[] { "คน", "วัน", "บาท" }, 0);
            //4.4.2
            CreateBudgetDetail(list, "B1_4_4_2", "เดินทางโดยเครื่องบิน เจ้าหน้าที่โครงการ ไม่เกิน 3 คน เดินทางโดยสายการบินต้นทุนต่ำ", jo, new string[] { "B1_4_4_2_1", "B1_4_4_2_3", "B1_4_4_2_2" }, new string[] { "คน", "วัน", "บาท" }, 0);
            //4.5.1
            CreateBudgetDetail(list, "B1_4_5_1", "ค่าชดเชยกรณีใช้ยานพาหนะส่วนตัว (รถยนต์)", jo, new string[] { "B1_4_5_1_1", "B1_4_5_1_3", "B1_4_5_1_2" }, new string[] { "กิโลเมตร", "วัน", "บาท" }, 0);
            //4.5.2
            CreateBudgetDetail(list, "B1_4_5_2", "ค่าชดเชยกรณีใช้ยานพาหนะส่วนตัว (รถจักรยานยนต์)", jo, new string[] { "B1_4_5_2_1", "B1_4_5_2_3", "B1_4_5_2_2" }, new string[] { "กิโลเมตร", "วัน", "บาท" }, 0);
            //5
            CreateBudgetDetail(list, "B1_5", "ค่าเบี้ยเลี้ยงสำหรับเจ้าหน้าที่โครงการในวันเดินทางไปและกลับ", jo, new string[] { "B1_5_1", "B1_5_2", "B1_5_3" }, new string[] { "คน", "วัน", "บาท" }, 0);
            //6.1.1
            // CreateBudgetDetail(list, "B1_6_1_1", "ค่าตอบแทน วิทยากรภาครัฐ", jo, new string[] { "B1_6_1_1_1", "B1_6_1_1_2", "B1_6_1_1_3" }, new string[] { "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_6_1_1", "ค่าตอบแทน วิทยากรภาครัฐ", jo, "vue_6_1_1", new string[] { "คน", "ชม.","วัน", "บาท" } );
            //6.1.2
            //CreateBudgetDetail(list, "B1_6_1_2", "ค่าตอบแทน วิทยากรเอกชน", jo, new string[] { "B1_6_1_2_1", "B1_6_1_2_2", "B1_6_1_2_3" }, new string[] { "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_6_1_2", "ค่าตอบแทน วิทยากรเอกชน", jo, "vue_6_1_2", new string[] { "คน", "ชม.", "วัน", "บาท" });
            //6.1.3
            //CreateBudgetDetail(list, "B1_6_1_3", "ค่าตอบแทน วิทยากรผู้ทรงคุณวุฒิ ", jo, new string[] { "B1_6_1_3_1", "B1_6_1_3_2", "B1_6_1_3_3" }, new string[] { "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_6_1_3", "ค่าตอบแทน วิทยากรผู้ทรงคุณวุฒิ", jo, "vue_6_1_3", new string[] { "คน", "ชม.", "วัน", "บาท" });
            //6.2.1
            //CreateBudgetDetail(list, "B1_6_2_1", "ค่าตอบแทน วิทยากรอภิปรายภาครัฐ", jo, new string[] { "B1_6_2_1_1", "B1_6_2_1_2", "B1_6_2_1_3" }, new string[] { "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_6_2_1", "ค่าตอบแทน วิทยากรอภิปรายภาครัฐ", jo, "vue_6_2_1", new string[] { "คน", "ชม.", "วัน", "บาท" });
            //6.2.2
           // CreateBudgetDetail(list, "B1_6_2_2", "ค่าตอบแทน วิทยากรอภิปรายเอกชน", jo, new string[] { "B1_6_2_2_1", "B1_6_2_2_2", "B1_6_2_2_3" }, new string[] { "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_6_2_2", "ค่าตอบแทน วิทยากรอภิปรายเอกชน", jo, "vue_6_2_2", new string[] { "คน", "ชม.", "วัน", "บาท" });
            //6.2.3
            //CreateBudgetDetail(list, "B1_6_2_3", "ค่าตอบแทน วิทยากรอภิปรายผู้ทรงคุณวุฒิ ", jo, new string[] { "B1_6_2_3_1", "B1_6_2_3_2", "B1_6_2_3_3" }, new string[] { "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_6_2_3", "ค่าตอบแทน วิทยากรอภิปรายผู้ทรงคุณวุฒิ", jo, "vue_6_2_3", new string[] { "คน", "ชม.", "วัน", "บาท" });
            //6.3.1
           // CreateBudgetDetail(list, "B1_6_3_1", "ค่าตอบแทน วิทยากรกลุ่มภาครัฐ", jo, new string[] { "B1_6_3_1_4", "B1_6_3_1_1", "B1_6_3_1_2", "B1_6_3_1_3" }, new string[] {"กลุ่ม", "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_6_3_1", "ค่าตอบแทน วิทยากรกลุ่มภาครัฐ", jo, "vue_6_3_1", new string[] {"กลุ่ม", "คน", "ชม.", "วัน", "บาท" });
            //6.3.2
            //CreateBudgetDetail(list, "B1_6_3_2", "ค่าตอบแทน วิทยากรกลุ่มเอกชน", jo, new string[] { "B1_6_3_2_4", "B1_6_3_2_1", "B1_6_3_2_2", "B1_6_3_2_3" }, new string[] { "กลุ่ม", "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_6_3_2", "ค่าตอบแทน วิทยากรกลุ่มเอกชน", jo, "vue_6_3_2", new string[] { "กลุ่ม", "คน", "ชม.", "วัน", "บาท" });
            //6.3.3
            //CreateBudgetDetail(list, "B1_6_3_3", "ค่าตอบแทน วิทยากรกลุ่มผู้ทรงคุณวุฒิ ", jo, new string[] { "B1_6_3_3_4", "B1_6_3_3_1", "B1_6_3_3_2", "B1_6_3_3_3" }, new string[] { "กลุ่ม", "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_6_3_3", "ค่าตอบแทน วิทยากรกลุ่มผู้ทรงคุณวุฒิ", jo, "vue_6_3_3", new string[] { "กลุ่ม", "คน", "ชม.", "วัน", "บาท" });
            //6.4.1
            //CreateBudgetDetail(list, "B1_6_4_1", "ค่าตอบแทน วิทยากรฝึกอาชีพทั่วไป", jo, new string[] { "B1_6_4_1_1", "B1_6_4_1_2", "B1_6_4_1_3" }, new string[] { "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_6_4_1", "ค่าตอบแทน วิทยากรฝึกอาชีพทั่วไป", jo, "vue_6_4_1", new string[] { "คน", "ชม.", "วัน", "บาท" });
            //6.4.2
            // CreateBudgetDetail(list, "B1_6_4_2", "ค่าตอบแทน วิทยากรภาคฝึกอาชีพเชี่ยวชาญ", jo, new string[] { "B1_6_4_2_1", "B1_6_4_2_2", "B1_6_4_2_3" }, new string[] { "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_6_4_2", "ค่าตอบแทน วิทยากรภาคฝึกอาชีพเชี่ยวชาญ", jo, "vue_6_4_2", new string[] { "คน", "ชม.", "วัน", "บาท" });
            //7
            //CreateBudgetDetail(list, "B1_7", "ค่าตอบแทนล่ามภาษามือ", jo, new string[] { "B1_7_1", "B1_7_2", "B1_7_3" }, new string[] { "คน", "ชม.", "บาท" }, 0);
            CreateBudgetDetailGrid(list, "B1_7", "ค่าตอบแทนล่ามภาษามือ", jo, "vue1_7", new string[] { "คน", "ชม.", "วัน", "บาท" });
            //8.1
            CreateBudgetDetail(list, "B1_8_1", "ค่าเช่าสถานที่ (จัดในโรงแรม)", jo, new string[] { "B1_8_1_1", "B1_8_1_2"  }, new string[] {   "วัน", "บาท" }, 0);
            //8.2
            CreateBudgetDetail(list, "B1_8_2", "ค่าเช่าสถานที่ (จัดในสถานที่ราชการ)", jo, new string[] { "B1_8_2_1", "B1_8_2_2" }, new string[] { "วัน", "บาท" }, 0);
            //8.3.1
            CreateBudgetDetail(list, "B1_8_3_1", "ค่าเช่าสถานที่ (จัดในสถานที่เอกชน ไม่เกิน 5 วัน)", jo, new string[] { "B1_8_3_1_1", "B1_8_3_1_2" }, new string[] { "วัน", "บาท" }, 0);
            //8.3.2
            CreateBudgetDetail(list, "B1_8_3_2", "ค่าเช่าสถานที่ (จัดในสถานที่เอกชน มากกว่า 5 วัน)", jo, new string[] { "B1_8_3_2"}, new string[] {  "บาท" }, 0);
            //9.1
            CreateBudgetDetail(list, "B1_9_1", "ค่าเช่ารถตู้ปรับอากาศ", jo, new string[] { "B1_9_1_1", "B1_9_1_2", "B1_9_1_3" }, new string[] { "คัน", "วัน", "บาท" }, 0);
            //9.2.1
            CreateBudgetDetail(list, "B1_9_2_1", "ค่าเช่ารถบัสแบบพัดลม", jo, new string[] { "B1_9_2_1_1", "B1_9_2_1_2", "B1_9_2_1_3" }, new string[] { "คัน", "วัน", "บาท" }, 0);
            //9.2.2
            CreateBudgetDetail(list, "B1_9_2_2", "ค่าเช่ารถบัสปรับอากาศ 30 – 32 ที่นั่ง", jo, new string[] { "B1_9_2_2_1", "B1_9_2_2_2", "B1_9_2_2_3" }, new string[] { "คัน", "วัน", "บาท" }, 0);
            //9.2.3
            CreateBudgetDetail(list, "B1_9_2_3", "ค่าเช่ารถบัสปรับอากาศ VIP 2 ชั้น 40 – 45 ที่นั่ง", jo, new string[] { "B1_9_2_3_1", "B1_9_2_3_2", "B1_9_2_3_3" }, new string[] { "คัน", "วัน", "บาท" }, 0);
            //9.2.4
            CreateBudgetDetail(list, "B1_9_2_4", "ค่าเช่ารถบัสปรับอากาศ VIP 2 ชั้น 40 – 50 ที่นั่ง", jo, new string[] { "B1_9_2_4_1", "B1_9_2_4_2", "B1_9_2_4_3" }, new string[] { "คัน", "วัน", "บาท" }, 0);
            //10
            CreateBudgetDetail(list, "B1_10", "ค่าน้ำมันเชื้อเพลิง", jo, new string[] { "B1_10_1", "B1_10_2" }, new string[] { "คัน", "บาท" }, 0);
            //11.1
            CreateBudgetDetail(list, "B1_11_1", "ค่าเอกสารประกอบการอบรม", jo, new string[] { "B1_11_1_1", "B1_11_1_2" }, new string[] { "ชุด", "บาท" }, 0);
            //11.2
            CreateBudgetDetail(list, "B1_11_2", "ค่าเอกสารอักษรเบรลล์ประกอบการอบรม", jo, new string[] { "B1_11_2_1", "B1_11_2_2" }, new string[] { "ชุด", "บาท" }, 0);
            //11.3
            CreateBudgetDetail(list, "B1_11_3", "ค่าเอกสารเสียงหรือซีดีประกอบการอบรม", jo, new string[] { "B1_11_3_1", "B1_11_3_2" }, new string[] { "ชุด", "บาท" }, 0);
            //12
            //CreateBudgetDetail(list, "B1_12", "ค่ากระเป๋าผ้า", jo, new string[] { "B1_12_1", "B1_12_2" }, new string[] { "คน", "บาท" }, 0);
            //13
            CreateBudgetDetail(list, "B1_13", "ค่าวัสดุฝึกอบรมหรือฝึกอาชีพ", jo, new string[] { "B1_13_1", "B1_13_2" }, new string[] { "คน", "บาท" }, 0);
            //13 other
            var jv = jo["B1_13_text"].ToString();
            jv = (string.IsNullOrEmpty(jv)) ? "" : jv;
            string b1Other = "ค่าวัสดุฝึกอบรมหรือฝึกอาชีพ(อื่นๆ) ";
            if (!string.IsNullOrEmpty(jv)) b1Other += "\n" + jv;

            //13. รายการค่าใช้จ่ายอื่นๆ
            CreateBudgetDetailGrid(list, "B1_B1OtherExpense", "รายการค่าใช้จ่ายอื่นๆ", jo, "vue1_B1OtherExpense", new string[] { "", "บาท" });
            CreateBudgetDetail(list, "B1_13_other", b1Other, jo, new string[] { "B1_13_other"  }, new string[] {  "บาท" }, 0);
            //14.1
            CreateBudgetDetail(list, "B1_14_1", "ค่าใช้จ่ายในการติดตามหรือประเมินผลโครงการหรือถอดบทเรียน", jo, new string[] { "B1_14_1_1", "B1_14_1_2" }, new string[] { "ครั้ง", "บาท" }, 0);
            //14.2
            CreateBudgetDetail(list, "B1_14_2", "ตอบแทนผู้ช่วยเหลือคนพิการเฉพาะกิจ", jo, new string[] { "B1_14_2_1", "B1_14_2_2", "B1_14_2_3" }, new string[] { "คน", "วัน", "บาท" }, 0);
            //14.3
            CreateBudgetDetail(list, "B1_14_3", "ตอบแทนอาสาสมัครที่ทำหน้าที่ประสานงานโครงการ", jo, new string[] { "B1_14_3_1", "B1_14_3_2", "B1_14_3_3" }, new string[] { "คน", "วัน", "บาท" }, 0);
            //14.4
            CreateBudgetDetail(list, "B1_14_4", "ค่าจ้างเจ้าหน้าที่บันทึกวีดีโอภาษามือ", jo, new string[] { "B1_14_4_1", "B1_14_4_2", "B1_14_4_3" }, new string[] { "คน", "วัน", "บาท" }, 0);
            //14.5
            CreateBudgetDetail(list, "B1_14_5", "ค่าจัดทำและบันทึกวีดีโอหรือซีดี", jo, new string[] { "B1_14_5_1", "B1_14_5_2" }, new string[] { "ชุด", "บาท" }, 0);
            //14.6
            CreateBudgetDetail(list, "B1_14_6", "ค่าถ่ายภาพและล้างอัดขยายภาพ", jo, new string[] { "B1_14_6_1", "B1_14_6_2" }, new string[] { "ชุด", "บาท" }, 0);
            //14.7
            CreateBudgetDetail(list, "B1_14_7", "ค่าจัดทำเอกสารรายงานผลโครงการ", jo, new string[] { "B1_14_7_1", "B1_14_7_2" }, new string[] { "ชุด", "บาท" }, 0);
            //14.8
            CreateBudgetDetail(list, "B1_14_8", "ค่าจดบันทึกการประชุม", jo, new string[] { "B1_14_8_1", "B1_14_8_2" }, new string[] { "คน", "บาท" }, 0);
            //14.9
            //B1_14_9_text
           // jv = jo["B1_14_9_text"].ToString();
           // jv = (string.IsNullOrEmpty(jv)) ? "" : jv;
            b1Other = "ค่าใช้จ่ายอื่นๆ ตามความจำเป็น ";
            decimal amt = 0;
            //if (!string.IsNullOrEmpty(jv)) b1Other += "\n" + jv;
            // CreateBudgetDetail(list, "B1_14_9_other", b1Other, jo, new string[] { "B1_14_9_other"  }, new string[] {   "บาท" }, 0);
            if (jo["other_expenses"] != null)
            {
                var jos = jo["other_expenses"].ToList();
        
                b1Other = "ค่าใช้จ่ายอื่นๆ ตามความจำเป็น \n";
                foreach (Newtonsoft.Json.Linq.JObject j in jos)
                {
                    decimal amttmp = 0;
                    amttmp = decimal.Parse(j["amount"].ToString());
                    if (amttmp > 0)
                    {
                        amt += amttmp;
                        b1Other += string.Format("{0} ({1:##,##.#0} บาท) \n", j["detail"].ToString(), amttmp);
                    }
                }
                if (amt > 0)
                {
                    //CreateBudgetDetail(list, "other_expenses", b1Other, jo, new string[] { "B1_14_9_other" }, new string[] { "บาท" }, 0);
                    ServiceModels.ProjectInfo.BudgetDetail bg;
                    bg = list.Where(s => s.BudgetCode == "other_expenses").FirstOrDefault();
                    if (bg == null)
                    {
                        bg = new ServiceModels.ProjectInfo.BudgetDetail();
                        list.Add(bg);
                        bg.ReviseAmount = amt;
                        bg.ReviseDetail = b1Other;
                    }
                    bg.BudgetCode = "other_expenses";
                    bg.Detail = b1Other;
                    bg.Amount = amt;
                    if (!bg.ReviseAmount.HasValue) bg.ReviseAmount = 0;
                }
            }


        }


        private string AddQNToBudget()
        {
            string msg = "";

            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? new List<ServiceModels.ProjectInfo.BudgetDetail>(((List<ServiceModels.ProjectInfo.BudgetDetail>)obj) )
                : new List<ServiceModels.ProjectInfo.BudgetDetail>();
            Dictionary<string, string> jv = new Dictionary<string, string>();
            Newtonsoft.Json.Linq.JObject jo = new Newtonsoft.Json.Linq.JObject();
            jo = Newtonsoft.Json.Linq.JObject.Parse(hdfQViewModel.Value);

            if (jo["BudgetType"] == null) return "กรุณาระบุประเภทโคงการ";

            var BudgetType = jo["BudgetType"].ToString();
            if (BudgetType == "1")
            {
                GetBudgetType1(list,jo);
            }
            else
            {
                GetBudgetType2(list,jo);
            }

            var err = list.Where(w => w.BudgetCode == "error").FirstOrDefault();
            if (err != null)
            {
                return err.Detail;
            }

            var zero = (list.Where(w => w.Amount == 0)).ToList();
            if (BudgetType == "1")
            {
                zero.AddRange(list.Where(w => w.BudgetCode.Substring(0, 2) == "B2").ToList());
            }
            if (BudgetType == "2")
            {
                zero.AddRange(list.Where(w => w.BudgetCode.Substring(0, 2) == "B1").ToList());
            }
            foreach (ServiceModels.ProjectInfo.BudgetDetail z in zero )
            {
                list.Remove(z);
            }
            var i = 1;
            foreach (ServiceModels.ProjectInfo.BudgetDetail l in list)
            {
                l.No = i;
                i++;
                 
            }

            ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = list;
            Dictionary<string, decimal> budget = GetTotalBudget();
            TextBoxTotalAmount.Text = budget["Rquest"].ToString();
            //foreach (KeyValuePair<string, Newtonsoft.Json.Linq.JToken> j in jo)
            //{
            //    var tmp = q.Where(w => w.QFIELD == j.Key).FirstOrDefault();

            //    if (tmp == null)
            //    {
            //        tmp = new PROJECTQUESTION();
            //        tmp.QFIELD = j.Key;
            //        tmp.QTYPE = "X";
            //        qn.PROJECTQUESTIONs.Add(tmp);


            //    }
            //    tmp.QVALUE = j.Value.ToString();
            //}
            return msg;
        }
        private void CreateBudgetDetailGrid(List<ServiceModels.ProjectInfo.BudgetDetail> obj, string budgetCode, string budjetDetail, Newtonsoft.Json.Linq.JObject jobj,string fname,  string[] bgUnit)

        {
            ServiceModels.ProjectInfo.BudgetDetail ret;
            try
            {
                string detail = budjetDetail;
                string sAmt = "";
                string sAmtItem = "";
                decimal Amount = 0;
                var j1 = jobj[fname].ToList();
                foreach (var j1tmp in j1)
                {
                    var j2 = j1tmp.ToList();
                    var jtot = j2[bgUnit.Count()].ToString();
                    sAmtItem = "";
                    if (!string.IsNullOrEmpty(jtot))
                    {
                        Decimal decTot;
                        if (decimal.TryParse(jtot, out decTot))
                        {
                            if (decTot > 0)
                            {
                                Amount += decTot;
                                for (int i = 0; i < bgUnit.Count(); i++)
                                {
                                    string jv = j2[i].ToString();
                                    if (bgUnit[i] == "")
                                    {
                                        sAmtItem += jv + " " ;
                                    }
                                    else
                                    {
                                        jv = (string.IsNullOrEmpty(jv)) ? "0" : jv;
                                        Decimal dec;
                                        decimal.TryParse(jv, out dec);
                                        //  Amount *= dec;
                                        sAmtItem += string.Format("{0:##,##.#0} {1} ", dec, bgUnit[i]) + " X ";
                                    }

                                }

                                sAmtItem = sAmtItem.Trim();
                                if (sAmtItem.Length > 0)
                                {
                                    if (sAmtItem[sAmtItem.Length - 1] == 'X')
                                    {
                                        sAmtItem = sAmtItem.Substring(0, sAmtItem.Length - 1);
                                    }
                                    sAmt += string.Format("({0} = {1:##,##.#0} บาท) \n", sAmtItem, decTot);
                                }
                            }
                        }

                    }

                }

                var sdetail = detail + "\n  " + sAmt ;
                ret = obj.Where(s => s.BudgetCode == budgetCode).FirstOrDefault();
                if (ret == null)
                {
                    ret = new ServiceModels.ProjectInfo.BudgetDetail();
                    obj.Add(ret);
                    ret.ReviseAmount = Amount;
                    ret.ReviseDetail = sdetail;
                }
                ret.BudgetCode = budgetCode;
                ret.Detail = sdetail;
                ret.Amount = Amount;
                if (!ret.ReviseAmount.HasValue) ret.ReviseAmount = 0;

            }
            catch (Exception ex)
            {
                ret = new ServiceModels.ProjectInfo.BudgetDetail();
                ret.BudgetCode = "error";
                ret.Detail = "Error get budget from scrren : " + budgetCode + " : " + ex.Message;
                obj.Add(ret);
            }


        }
        private void CreateBudgetDetail(List<ServiceModels.ProjectInfo.BudgetDetail> obj,string budgetCode, string budjetDetail , Newtonsoft.Json.Linq.JObject jo,string[] bgField, string[] bgUnit, decimal maxBudget)

        {
            ServiceModels.ProjectInfo.BudgetDetail ret;
            try
            {
                string detail = budjetDetail;
                string sAmt = "";
                decimal Amount = 1;
                for (int i = 0; i < bgField.Count(); i++)
                {
                    var jv = jo[bgField[i]].ToString();
                    jv = (string.IsNullOrEmpty(jv)) ? "0" : jv;
                    Decimal dec;
                    decimal.TryParse(jv,out  dec);
                    Amount *= dec;
                    sAmt += string.Format("{0:##,#0} {1} ", dec, bgUnit[i]) + " X " ;
                }
                sAmt = sAmt.Trim();
                if (sAmt.Length > 0)
                {
                    if (sAmt[sAmt.Length - 1] == 'X')
                    {
                        sAmt = sAmt.Substring(0,sAmt.Length - 1);
                    }
                }
                var sdetail = detail + " ( " + sAmt + " )";
                ret = obj.Where(s => s.BudgetCode == budgetCode).FirstOrDefault();
                    if (ret == null)
                    {
                        ret = new ServiceModels.ProjectInfo.BudgetDetail();
                        obj.Add(ret);
                       ret.ReviseAmount = Amount;
                       ret.ReviseDetail = sdetail;
                    }
                ret.BudgetCode = budgetCode;
                ret.Detail = sdetail;
                ret.Amount = Amount;
                if (!ret.ReviseAmount.HasValue) ret.ReviseAmount = 0;
              
            } catch(Exception ex){
                ret = new ServiceModels.ProjectInfo.BudgetDetail();
                ret.BudgetCode = "error";
                ret.Detail = "Error get budget from scrren : " + budgetCode + " : " + ex.Message;
                obj.Add(ret);
            }

             
        }
        protected void GridViewBudgetDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null)? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();
            ServiceModels.ProjectInfo.BudgetDetail tempItem;
            String uid = e.CommandArgument.ToString();

            Page.Validate("SaveBudgetDetail");
            
            if ((e.CommandName == "save") && (Page.IsValid))
            {              
                var editRow = GridViewBudgetDetail.Rows[GridViewBudgetDetail.EditIndex];
                ServiceModels.ProjectInfo.BudgetDetail oldItem = list.Where(x => x.UID == uid).FirstOrDefault();
                ServiceModels.ProjectInfo.BudgetDetail editItem = (oldItem != null) ? oldItem : new ServiceModels.ProjectInfo.BudgetDetail();
                TextBox textBoxBudgetDetail = (TextBox)editRow.FindControl("TextBoxBudgetDetail");
                TextBox textBoxBudgetAmount = (TextBox)editRow.FindControl("TextBoxBudgetAmount");

                String detail = textBoxBudgetDetail.Text.TrimEnd();
                decimal amount = Decimal.Parse(textBoxBudgetAmount.Text);

                editItem.Detail = detail;
                editItem.ReviseDetail = detail;
                editItem.Amount = amount;
                editItem.ReviseAmount = amount;

                

                if (oldItem != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        tempItem = list[i];
                        if (tempItem.UID == uid)
                        {
                            list[i] = tempItem;
                            break;
                        }
                    }
                }
                else
                {
                    editItem.UID = uid; 
                    editItem.No = list.Count + 1;
                    list.Add(editItem);
                }

                ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = list;

                Dictionary<string, decimal> budget = GetTotalBudget();
                TextBoxTotalAmount.Text = budget["Rquest"].ToString();
                GridViewBudgetDetail.EditIndex = -1;                
                GridViewBudgetDetail.DataSource = list;
                GridViewBudgetDetail.DataBind();
            }
            else if (e.CommandName == "del")
            {
                List<ServiceModels.ProjectInfo.BudgetDetail> newList = new List<ServiceModels.ProjectInfo.BudgetDetail>();
                int no = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    tempItem = list[i];
                    if (tempItem.UID != uid)
                    {
                        no++;
                        tempItem.No = no;
                        newList.Add(tempItem);
                    }
                }
                
                ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = newList;
                Dictionary<string, decimal> budget = GetTotalBudget();
                TextBoxTotalAmount.Text = budget["Rquest"].ToString();
                GridViewBudgetDetail.DataSource = newList;
                GridViewBudgetDetail.DataBind();
            }

            RegisterRequiredData();
        }

        protected void ButtonAddBudgetDetail_Click(object sender, EventArgs e)
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null)? ((List<ServiceModels.ProjectInfo.BudgetDetail>)obj).ToList() : new List<ServiceModels.ProjectInfo.BudgetDetail>();
            
            list.Insert(0, new ServiceModels.ProjectInfo.BudgetDetail() { 
                UID = Guid.NewGuid().ToString(),
                Detail = "",
                Amount = null
            });

            GridViewBudgetDetail.DataSource = list;
            GridViewBudgetDetail.EditIndex = 0;
            GridViewBudgetDetail.DataBind();
        }

        private Dictionary<string, decimal> GetTotalBudget()
        {
            decimal totalRquestAmount = 0;
            decimal totalRevise = 0;
            decimal totalRevise1 = 0;
            decimal totalRevise2 = 0;

            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();
            ServiceModels.ProjectInfo.BudgetDetail data;
            for (int i = 0; i < list.Count; i++)
            {
                data = list[i];
                totalRquestAmount += (data.Amount.HasValue) ? (decimal)data.Amount : 0;
                totalRevise += (data.ReviseAmount.HasValue) ? (decimal)data.ReviseAmount : 0;
                totalRevise1 += (data.Revise1Amount.HasValue) ? (decimal)data.Revise1Amount : 0;
                totalRevise2 += (data.Revise2Amount.HasValue) ? (decimal)data.Revise2Amount : 0;
            }
            Dictionary<string, decimal> total = new Dictionary<string, decimal>();
            total.Add("Rquest", totalRquestAmount);
            total.Add("Revise", totalRevise);
            total.Add("Revise1", totalRevise1);
            total.Add("Revise2", totalRevise2);

            return total;
        }
        private void RebindBudgetActivityDataSource()
        {
          
          
        }
        private void RebindBudgetDetailDataSource()
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();

            ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = list;
            GridViewBudgetDetail.DataSource = list;
            GridViewBudgetDetail.DataBind();
        }

        #endregion



        private void CheckAuthorized()
        {
            var user = Session["CurrentUser"];
            bool isCenter = false;
            bool isProvince = false;
            bool isShow = false;
            bool isReadOnly = false;

            if (user != null)
            {
                ServiceModels.Security.SecurityInfo userInfo = (ServiceModels.Security.SecurityInfo)user;
                if (userInfo.Roles.Contains("center"))
                {
                    isCenter = true;
                    isShow = true;
                }
                else if (userInfo.Roles.Contains("province"))
                {
                    isProvince = true;
                    isShow = true;
                }
                else if (userInfo.Roles.Contains("company"))
                {
                    isReadOnly = true;
                }

                ViewState["IsCenter"] = isCenter;
                ViewState["IsProvince"] = isProvince;
                ViewState["IsShow"] = isShow;
                ViewState["IsReadOnly"] = isReadOnly;
            }
            else
            {
                Response.Redirect(Page.ResolveClientUrl("~/Account/Login.aspx"));
            }
        }

        //protected void CustomValidatorGotSupportInfo_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    bool isChecked = RadioButtonGotSupportYes.Checked;
        //    string gotSupportName = TextBoxGotSupportName.Text;
        //    string gotSupportAmont = TextBoxGotSupportAmount.Text;
        //    string fieldName = String.Format("{0} และ {1}", Nep.Project.Resources.UI.LabelGotSupportName, Nep.Project.Resources.UI.LabelGotSupportAmount);
        //    string error = String.Format(Nep.Project.Resources.Error.RequiredField, fieldName);
        //    if (isChecked && (String.IsNullOrEmpty(gotSupportName) || String.IsNullOrEmpty(gotSupportAmont)))
        //    {
        //        args.IsValid = false;
        //        CustomValidator validator = (CustomValidator)source;
        //        validator.Text = error;
        //        validator.ErrorMessage = error;
        //    }
        //}

        private void RegisterClientScript()
        {
            String validateScript = @"
                function validateProjectBudgetDetail(oSrc, args) {
                    var colNo = $('.product-budget-no');
                    var col, text;
                    var isValid = false;
                    for (var i = 0; i < colNo.length; i++) {
                        col = colNo[i];
                        text = $.trim($(col).text());
                        if (text.length > 0) {
                            isValid = true;
                            break;
                        }
                    }                
                    args.IsValid = isValid;
                }
                $('#" + TextBoxTotalAmount.ClientID + @"').prop('disabled', true); 
            ";
            //ScriptManager.RegisterClientScriptBlock(
            //           UpdatePanelProjectBudget,
            //           this.GetType(),
            //           "ValidateProjectBudgetDetailScript",
            //           validateScript,
            //           true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidateProjectBudgetDetailScript",
                             validateScript, true);
            string disableRadio = "";
            var info = (from i in _service.GetDB().ProjectGeneralInfoes where i.ProjectID == ProjectID select i).FirstOrDefault();
            if (info != null)
            {
                if (info.OrganizationTypeID.HasValue)
                {

                    var otype = _service.GetDB().MT_OrganizationType.Where(w => w.OrganizationTypeID == info.OrganizationTypeID).FirstOrDefault();
                    if (otype != null)
                    {
                        if(otype.ToBeUnder == "1") //Goverment
                        {  //$('#rdBudgetType1').attr('disabled','true');
                            //$('#rdSupportBudgetType1').prop('checked', true)
                            disableRadio = @"  
                                             $('#rdSupportBudgetType1').attr('disabled','true');
                                             $('div.divSupportBudgetType1')[0].style.display = 'none';
                                             $('div.divSupportBudgetType2')[0].style.display = 'block';";
                        }
                        else
                        {
                            disableRadio = @"$('#rdSupportBudgetType2').attr('disabled','true');
                                            $('div.divSupportBudgetType2')[0].style.display = 'none';
                                            $('div.divSupportBudgetType1')[0].style.display = 'block';";
                                          
                        }
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "DisableRadiobutton",
                        disableRadio, true);
                    }
                }
            }


            //            String validateBudgetTotalAmountScript = @"
            //                function projectBudgetDetailValidateTotalAmount(oSrc, args) {
            //                    var isValid = true;
            //                    var totalBudgetAmountLabel = $('.total-budget-amount');
            //                    if (totalBudgetAmountLabel != null) {                    
            //                        var totalBudgetAmount = (totalBudgetAmountLabel != null)? $(totalBudgetAmountLabel).text() : '0';
            //                        totalBudgetAmount = parseFloat(totalBudgetAmount.replace(/[,]+/, ''));
            //
            //                        var textBoxTotalAmount = $('#" + TextBoxTotalAmount.ClientID + @"').val();
            //                        textBoxTotalAmount = parseFloat(textBoxTotalAmount.replace(/[,]+/, ''));                        
            //                        if (totalBudgetAmount != textBoxTotalAmount) {
            //                            isValid = false;
            //                        }
            //                    }
            //                    args.IsValid = isValid;
            //                }
            //            ";
            //            ScriptManager.RegisterClientScriptBlock(
            //                       UpdatePanelProjectBudget,
            //                       this.GetType(),
            //                       "ValidateBudgetTotalAmountScript",
            //                       validateBudgetTotalAmountScript,
            //                       true);

            RegisterRequiredData();
            
        }

        private void RegisterRequiredData()
        {

            //String onloadScript = @"
            //     $(function () {      
            //        SetTabHeader(" + Common.Web.WebUtility.ToJSON(RequiredSubmitData) + @");
            //     });

            //";
            String onloadScript = @"
                 $(function () {      
                
                 });
            
            ";
            //ScriptManager.RegisterStartupScript(
            //          UpdatePanelProjectBudget,
            //          this.GetType(),
            //          "UpdatePanelProjectBudgetPlancript",
            //          onloadScript,
            //          true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdatePanelProjectBudgetPlancript",
                             onloadScript, true);

        }

        protected void CustomValidatorMaxAmount_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = null;
            bool isValid = true;
            decimal maxValue = new Decimal(999999999.99);
            String msg = String.Format(Nep.Project.Resources.Error.ValidationRangeTypeNumber, Nep.Project.Resources.Model.ProjectBudgetDetail_TotalAmount, "1", "999,999,999.99");
            CustomValidator validator = (CustomValidator)source;
            if (obj != null)
            {
                list = (List<ServiceModels.ProjectInfo.BudgetDetail>)obj;
                if (list.Count > 0)
                {
                    String strTotalBudget = TextBoxTotalAmount.Text;
                    strTotalBudget = strTotalBudget.Replace(",", "");
                    Decimal totalBudget = 0;
                    if (Decimal.TryParse(strTotalBudget, out totalBudget))
                    {
                        isValid = (totalBudget <= maxValue);
                    }
                }
            }
            validator.Text = msg;
            validator.ErrorMessage = msg;
            args.IsValid = isValid;
           
        }

        protected void GridViewActivity_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }




        private void ShowErrorMessageNew(string s)
        {
            ErrorLabel.Text = s;
        }
        private void ShowErrorMessageNew(List<string> s)
        {
            string err = "";
            foreach (string t in s)
            {
                err += t + "<br>";
            }
            ErrorLabel.Text = err;
        }
  
    }
}