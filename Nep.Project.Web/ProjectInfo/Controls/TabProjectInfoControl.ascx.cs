using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using Nep.Project.ServiceModels;
using AjaxControlToolkit;
using Nep.Project.Common;


namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabProjectInfoControl : Nep.Project.Web.Infra.BaseUserControl
    {
        public IServices.IProjectInfoService _service { get; set; }
        public IServices.IListOfValueService _lovService { get; set; }

        private string PROJECT_TARGET_LIST_VIEWSTATE_KEY = "PROJECT_TARGET_LIST";

        public Boolean IsEditableProjectInfo
        {
            get
            {
                bool isEdit = false;
                if(ViewState["IsEditableProjectInfo"] != null){
                    isEdit = Convert.ToBoolean(ViewState["IsEditableProjectInfo"]);
                }
                return isEdit;
            }
            set
            {
                ViewState["IsEditableProjectInfo"] = value;
            }
        }
        public String CancelledProjectRequestPrefix
        {
            get
            {
                if (ViewState["CancelledProjectRequestPrefix"] == null)
                {
                    string prefix = "Project/" + ProjectID;
                    ViewState["CancelledProjectRequestPrefix"] = prefix;
                }
                return ViewState["CancelledProjectRequestPrefix"].ToString();
            }

            set
            {
                ViewState["CancelledProjectRequestPrefix"] = value;
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
        }
        public Decimal? CancelledAttachmentID
        {
            get
            {
                decimal? id = (decimal?)null;
                if (ViewState["CancelledAttachmentID"] != null)
                {
                    id = Convert.ToDecimal(ViewState["CancelledAttachmentID"]);
                }
                return id;
            }

            set
            {
                ViewState["CancelledAttachmentID"] = value;
            }
        }

        public Decimal TargetGroupEtcID
        {
            get
            {
                decimal id = 0;
                if (ViewState["TargetGroupEtcID"] != null)
                {
                    id = Convert.ToDecimal(ViewState["TargetGroupEtcID"].ToString());
                }
                return id;
            }

            set
            {
                ViewState["TargetGroupEtcID"] = value;

            }
        }

        public List<ServiceModels.ListOfValue> TargetGroupList
        {
            get
            {
                List<ServiceModels.ListOfValue> list = new List<ServiceModels.ListOfValue>();
                if (ViewState["TargetGroupList"] != null)
                {
                    list = (List<ServiceModels.ListOfValue>)ViewState["TargetGroupList"];
                }
                return list;
            }

            set
            {
                ViewState["TargetGroupList"] = value;
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
            string parameter = Request["__EVENTARGUMENT"];
            if (!String.IsNullOrEmpty(parameter) && (parameter == "reloadinfo"))
            {
                Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                page.RebindData("TabPanelProjectInfo");
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            String cancelledFormUrl = ResolveUrl("~/ProjectInfo/CancelProjectRequestPopup");
            cancelledFormUrl = String.Format("{0}?projectid={1}", cancelledFormUrl, ProjectID);

            String attachUrl = ResolveUrl("~/AttachmentHandler/View/Project/" + ProjectID + "/");

            String attachmetID = (CancelledAttachmentID.HasValue)? CancelledAttachmentID.ToString() : "null";

            String scriptTag = @"
                    function openCancelProjectRequestForm() {
                        var cancelledAttachmentID = " + attachmetID + @";
                        var pageUrl = '" + cancelledFormUrl + @"';
                        pageUrl = (cancelledAttachmentID != null)? (pageUrl + '&attachmentid=' + cancelledAttachmentID): pageUrl;
                        
                        c2x.openFormDialog(pageUrl, '" + Nep.Project.Resources.UI.TitleCancelledProjectRequest + @"', { width: 520, height: 215 }, null);    
                        return false;
                    }

                    function openCancelledAttachment(attachmentID, attachmentName) {
                        var attachUrl = '" + attachUrl + @"';
                        attachUrl += attachmentID + '/' + attachmentName;
                        
                        window.open(attachUrl, '_blank');                        
                        return false;
                    }

                    function reloadPanelProjectInfo() {                       
                       __doPostBack('" + UpdatePanelProjectInfo.UniqueID + @"', 'reloadinfo');
               
                    }

                    function projectTargetGroupValidate(source, args)
                   {
                        var targetGroup = $('#" + HiddenFieldProjectTargetGroup.ClientID + @"').val();
                        if (targetGroup.length > 0){
                            args.IsValid = true;
                        } else {
                            args.IsValid = false;
                        }
                   }
                    ";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openCancelProjectRequestForm", scriptTag, true);

            #region ProjectInfo
            String scriptUrl = ResolveUrl("~/Scripts/manage.projectinfo.js");
            var refScript = "<script type='text/javascript' src='" + scriptUrl + "'></script>";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelProjectInfo,
                       this.GetType(),
                       "RefUpdatePanelProjectInfoScript",
                       refScript,
                       false);


            String script = @" 
                $(function () {                   
                    
                    c2xProjectInfo.targetGroupConfig({
                        HiddenFieldProjectTargetGroupID: '" + HiddenFieldProjectTargetGroup.ClientID + @"',
                        TextBoxProjectTargetID: '" + TextBoxProjectTarget.ClientID + @"',
                        ProductTargetGroupGridID: '" + ProductTargetGroupGrid.ClientID + @"',
                        ProjectTargetEtcCreateBlockID: 'ProjectTargetEtcBlock',

                        TextBoxProjectTargetEtcCreateID: '" + TextBoxProjectTargetEtc.ClientID + @"',
                        
                        TextBoxProjectTargetAmountCreateID: '" + TextBoxProjectTargetAmount.ClientID + @"',
                         BtnAddProjectTarget:'" + ImageButtonSaveProductTargetGroup.ClientID + @"',

                        TargetGroupEtcValueID: " + TargetGroupEtcID + @",
                        TargetGroupList: " + Newtonsoft.Json.JsonConvert.SerializeObject(TargetGroupList) + @",

                        RequiredTargetGroupMsg: '" + String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectTarget_TargetName) + @"',
                        RequiredTargetGroupEtcMsg: '" + String.Format(Nep.Project.Resources.Error.RequiredField, "ชื่อกลุ่มเป้าหมายอื่นๆ") + @"',
                        RequiredTargetGroupAmountMsg: '" + String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectTarget_Amount) + @"',
                        RequiredTargetGroupDupMsg: '" + String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.ProjectTarget_TargetName) + @"',
                    
                        ColumnTitle:{Target: '" + Nep.Project.Resources.Model.ProjectTarget_TargetName + @"', MenAmount:'" + Nep.Project.Resources.UI.LabelMale + @"',
                                     WomenAmount:'" + Nep.Project.Resources.UI.LabelFemale + @"', Amount:'" + Nep.Project.Resources.UI.LabelTotal + @"'},
                        IsView: " + Newtonsoft.Json.JsonConvert.SerializeObject(!IsEditableProjectInfo)+ @",
                        ProjectID: " +ProjectID+ @"
                        });

                    c2xProjectInfo.createDdlProjectTargetGroup();
                    c2xProjectInfo.createGridProjectTargetGroup();
                    
                    SetTabHeader(" + Common.Web.WebUtility.ToJSON(RequiredSubmitData) + @");
                });";

            ScriptManager.RegisterStartupScript(
                      UpdatePanelProjectInfo,
                      this.GetType(),
                      "UpdatePanelProjectInfoScript",
                      script,
                      true);

            #endregion ProjectInfo
        }


        public void BindData()
        {
            BindDropDownListProjectInfoType();
            BindCheckBoxListTypeDisabilitys();
            ButtonSendProjectInfo.Text = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Nep.Project.Resources.UI.ButtonSubmit : Nep.Project.Resources.UI.ButtonSendProjectInfo;

            List<String> errors = new List<string>();
            ServiceModels.ProjectInfo.TabProjectInfo projectInfo = new ServiceModels.ProjectInfo.TabProjectInfo();
            List<ServiceModels.ProjectInfo.ProjectTarget> projectTargets = new List<ServiceModels.ProjectInfo.ProjectTarget>();
            decimal projectID = ProjectID;

            if (projectID > 0)
            {
                var projectInfoResult = _service.GetProjectInformationByProjecctID(projectID); 
                if (projectInfoResult.IsCompleted)
                {
                    projectInfo = projectInfoResult.Data;
                    List<Common.ProjectFunction> functions = _service.GetProjectFunction(projectInfo.ProjectID).Data;
                    BindDisplay(projectInfo, functions);                   
                }
                else
                {
                    errors.Add(projectInfoResult.Message[0]);    
                }

              
                //Combox
                List<ServiceModels.ListOfValue> list = new List<ListOfValue>();
                var targetResult = _lovService.ListAll(Common.LOVGroup.TargetGroup);
                if (targetResult.IsCompleted)
                {
                    list = targetResult.Data;
                    TargetGroupList = list;
                    var etc = list.Where(x => x.LovCode == Common.LOVCode.Targetgroup.อื่นๆ).FirstOrDefault();
                    if (etc != null)
                    {
                        TargetGroupEtcID = etc.LovID;
                    }
                }
                else
                {
                    errors.Add(targetResult.Message[0]);
                }


                var result = _service.GetProjectTargetByProjectID(ProjectID);
                if (result.IsCompleted)
                {
                    if ((result.Data != null) && (result.Data.Count > 0))
                    {
                        HiddenFieldProjectTargetGroup.Value = Newtonsoft.Json.JsonConvert.SerializeObject(result.Data);
                    }
                }
                else
                {
                    errors.Add(result.Message[0]);
                }

                if (errors.Count > 0)
                {
                    ShowErrorMessage(errors);
                }           
            }

            BindGridProjectTarget(projectTargets);
            
            if(errors.Count > 0){
                ShowErrorMessage(errors);
            }
            //base.DataBind();

            //TextBoxProjectTargetEtc.Attributes.Add("keydown", "c2xProjectInfo.onTextBoxKeyUp(event, true)");
            //TextBoxProjectTargetAmount.Attributes.Add("keydown", "c2xProjectInfo.onAmountNumberTextBoxKeyUp(event, true)");
            //TextBoxProjectTargetWomenAmount.Attributes.Add("onkeyup", "c2xProjectInfo.onAmountNumberTextBoxKeyUp(event, true)");

            RegisterClientScript();           
            
        }
               

        protected void ButtonSave_Click(object sender, EventArgs e)
        {            
            try
            {
                if(Page.IsValid){
                    ServiceModels.ProjectInfo.TabProjectInfo model = GetProjectInfoData();
                    var objTarget = ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY];
                    //List<ServiceModels.ProjectInfo.ProjectTarget> list = (objTarget != null) ? (List<ServiceModels.ProjectInfo.ProjectTarget>)objTarget : new List<ServiceModels.ProjectInfo.ProjectTarget>();

                    var projectTargetGroup = HiddenFieldProjectTargetGroup.Value;
                    List<ServiceModels.ProjectInfo.ProjectTarget> list =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServiceModels.ProjectInfo.ProjectTarget>>(HiddenFieldProjectTargetGroup.Value);

                    var result = _service.SaveProjectInformation(model, list);
                    if (result.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelProjectInfo");
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

        private ServiceModels.ProjectInfo.TabProjectInfo GetProjectInfoData()
        {
            ServiceModels.ProjectInfo.TabProjectInfo model = new ServiceModels.ProjectInfo.TabProjectInfo();
                         
            string projectNo = LabelProjectNo.Text;
            string projectNameTH = TextBoxProjectInfoNameTH.Text.Trim();
            string projectNameEN = TextBoxProjectInfoNameEN.Text.Trim();
            //for save draft
            if (string.IsNullOrEmpty(DropDownListProjectInfoType.SelectedValue))
            {
                DropDownListProjectInfoType.SelectedIndex = 1;
            }
            decimal? projectTypeID = Convert.ToDecimal(DropDownListProjectInfoType.SelectedValue);
            DateTime projectDate = Convert.ToDateTime(ProjectInfoStartDate.SelectedDate);
            // for save draft
            if (string.IsNullOrEmpty(RadioButtonListTypeDisabilitys.SelectedValue))
            {
                RadioButtonListTypeDisabilitys.SelectedIndex = 0;
            }
            decimal? disabilityTypeID = Convert.ToDecimal(RadioButtonListTypeDisabilitys.SelectedValue);
            string priciple = TextBoxPrinciples.Text.TrimEnd();
            string objective = TextBoxProjectInfoObjective.Text.TrimEnd();
            string indicator = TextBoxProjectInfoindicator.Text.TrimEnd();
            string anticipation = TextBoxProjectInfoAnticipation.Text.TrimEnd();

            model.ProjectID = ProjectID;
            model.ProjectNo = projectNo;
            model.ProjectInfoNameTH = projectNameTH;
            model.ProjectInfoNameEN = projectNameEN;
            model.ProjectInfoType = projectTypeID;
            model.ProjectInfoStartDate = projectDate;
            model.TypeDisabilitys = disabilityTypeID;
            model.Principles = priciple;
            model.ProjectInfoObjective = objective;
            model.ProjectInfoindicator = indicator;
            model.ProjectInfoAnticipation = anticipation;

            return model;
        }

        private List<ServiceModels.ProjectInfo.ProjectTarget> GetProjectTargets()
        {
            List<ServiceModels.ProjectInfo.ProjectTarget> list = new List<ServiceModels.ProjectInfo.ProjectTarget>();
            if(ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY] != null){
                list = (List<ServiceModels.ProjectInfo.ProjectTarget>)ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY];
            }

            return list;
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
                        page.RebindData("TabPanelProjectInfo");
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

        #region Grid
        public List<ServiceModels.GenericDropDownListData> ComboBoxProjectTarget_GetData()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            //var dataSource = GridProjectTarget.DataSource;
            //decimal skipId = 0;
            //if ((dataSource != null) && (GridProjectTarget.EditIndex >= 0))
            //{
            //    List<ServiceModels.ProjectInfo.ProjectTarget> dataSourceList = (List<ServiceModels.ProjectInfo.ProjectTarget>)dataSource;
            //    skipId = dataSourceList[GridProjectTarget.EditIndex].TargetID; 
            //}
            
            //try
            //{
            //    list = _service.ListProjectTarget();

            //    List<ServiceModels.ProjectInfo.ProjectTarget> targetList = (List<ServiceModels.ProjectInfo.ProjectTarget>)ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY];
            //    if ((targetList != null) && (targetList.Count > 0))
            //    {
            //        List<string> selectedIds = new List<string>();
            //        for (int i = 0; i < targetList.Count; i++)
            //        {
            //            if (targetList[i].TargetID == skipId)
            //            {
            //                continue;
            //            }
            //            selectedIds.Add(targetList[i].TargetID.ToString());
            //        }

            //        list = list.Where(x => !selectedIds.Any(sid => sid == x.Value)).ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
            //    ShowErrorMessage(ex.Message);
            //}

            return list;
        }

        protected void ButtonAddProjectTarget_Click(object sender, EventArgs e)
        {
            //var obj = ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY];
            //List<ServiceModels.ProjectInfo.ProjectTarget> list = (obj != null) ? ((List<ServiceModels.ProjectInfo.ProjectTarget>)obj).ToList() : new List<ServiceModels.ProjectInfo.ProjectTarget>();

            //list.Insert(0, new ServiceModels.ProjectInfo.ProjectTarget()
            //{
            //    UID = Guid.NewGuid().ToString(),
            //    TargetName = "",
            //    TargetOtherName = "",
            //    MenAmount = null,
            //    WomenAmount = null,
            //    Amount = null
            //});

            //GridProjectTarget.DataSource = list;
            //GridProjectTarget.EditIndex = 0;
            //GridProjectTarget.DataBind();
        }

        protected void GridProjectTarget_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //List<ServiceModels.ProjectInfo.ProjectTarget> list = (List<ServiceModels.ProjectInfo.ProjectTarget>)ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY];
            //GridProjectTarget.EditIndex = e.NewEditIndex;
            //RebindBudgetDetailDataSource();

            //if ((list != null) && (list.Count > 0))
            //{
            //    var editRow = GridProjectTarget.Rows[e.NewEditIndex];
            //    ServiceModels.ProjectInfo.ProjectTarget target = list[e.NewEditIndex];
            //    ComboBox comboBoxProjectTarget = (ComboBox)editRow.FindControl("ComboBoxProjectTarget");
            //    comboBoxProjectTarget.SelectedValue = target.TargetID.ToString();

            //    if (target.TargetName.IndexOf(Common.Constants.OTHER_PROJECT_TARGET_NAME) >= 0)
            //    {
            //        TextBox textBoxTargetOtherName = (TextBox)editRow.FindControl("TextBoxTargetOtherName");
            //        RequiredFieldValidator validator = (RequiredFieldValidator)editRow.FindControl("RequiredFieldValidatorTargetOtherName");
            //        textBoxTargetOtherName.Visible = true;
            //        validator.Enabled = true;
            //    }
            //}           
        }

        protected void TargetAmount_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //GridViewRow editRow = GridProjectTarget.Rows[GridProjectTarget.EditIndex];
            //TextBox textBoxProjectTargetMenAmount = (TextBox)editRow.FindControl("TextBoxProjectTargetMenAmount");
            //TextBox textBoxProjectTargetWomenAmount = (TextBox)editRow.FindControl("TextBoxProjectTargetWomenAmount");
            //int women = 0, men = 0;
            //if (!String.IsNullOrEmpty(textBoxProjectTargetMenAmount.Text))
            //{
            //    Int32.TryParse(textBoxProjectTargetMenAmount.Text, out men);
            //}

            //if (!String.IsNullOrEmpty(textBoxProjectTargetWomenAmount.Text))
            //{
            //    Int32.TryParse(textBoxProjectTargetWomenAmount.Text, out women);
            //}
            //args.IsValid = ((women + men) > 0);
        }

        protected void GridProjectTarget_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //var obj = ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY];
            //List<ServiceModels.ProjectInfo.ProjectTarget> list = (obj != null) ? (List<ServiceModels.ProjectInfo.ProjectTarget>)obj : new List<ServiceModels.ProjectInfo.ProjectTarget>();
            //if (list.Count > 0)
            //{
            //    GridProjectTarget.EditIndex = -1;
            //    RebindBudgetDetailDataSource();
            //}
        }

        protected void GridProjectTarget_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //var obj = ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY];
            //List<ServiceModels.ProjectInfo.ProjectTarget> list = (obj != null) ? (List<ServiceModels.ProjectInfo.ProjectTarget>)obj : new List<ServiceModels.ProjectInfo.ProjectTarget>();
            //ServiceModels.ProjectInfo.ProjectTarget tempItem;
            //String uid = e.CommandArgument.ToString();

            //if ((e.CommandName == "save") && (Page.IsValid))
            //{
            //    var editRow = GridProjectTarget.Rows[GridProjectTarget.EditIndex];
            //    ComboBox combProjectTarget = (ComboBox)editRow.FindControl("ComboBoxProjectTarget");
            //    TextBox textBoxProjectTargetMenAmount = (TextBox)editRow.FindControl("TextBoxProjectTargetMenAmount");
            //    TextBox textBoxProjectTargetWomenAmount = (TextBox)editRow.FindControl("TextBoxProjectTargetWomenAmount");
            //    TextBox textBoxTargetOtherName = (TextBox)editRow.FindControl("TextBoxTargetOtherName");
            //    HiddenField hiddenProjectTargetId = (HiddenField)editRow.FindControl("HiddenProjectTargetID");

            //    ServiceModels.ProjectInfo.ProjectTarget oldItem = oldItem = list.Where(x => x.UID == uid).FirstOrDefault();
            //    ServiceModels.ProjectInfo.ProjectTarget editItem = (oldItem != null) ? oldItem : new ServiceModels.ProjectInfo.ProjectTarget();

            //    editItem.TargetID = Convert.ToInt32(combProjectTarget.SelectedValue);
            //    editItem.TargetName = combProjectTarget.SelectedItem.Text;
            //    editItem.TargetOtherName = ((textBoxTargetOtherName != null) && (textBoxTargetOtherName.Visible == true)) ? textBoxTargetOtherName.Text.Trim() : "";

            //    int women = 0, men = 0;
            //    if (!String.IsNullOrEmpty(textBoxProjectTargetMenAmount.Text))
            //    {
            //        Int32.TryParse(textBoxProjectTargetMenAmount.Text, out men);
            //        editItem.MenAmount = men;
            //    }

            //    if (!String.IsNullOrEmpty(textBoxProjectTargetWomenAmount.Text))
            //    {
            //        Int32.TryParse(textBoxProjectTargetWomenAmount.Text, out women);
            //        editItem.WomenAmount = women;
            //    }

            //    editItem.Amount = women + men;

            //    if (oldItem != null)
            //    {
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            tempItem = list[i];
            //            if (tempItem.UID == uid)
            //            {
            //                list[i] = tempItem;
            //                break;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        editItem.UID = uid;
            //        list.Add(editItem);
            //    }

            //    GridProjectTarget.EditIndex = -1;
            //    ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY] = list;
            //    GridProjectTarget.DataSource = list;
            //    GridProjectTarget.DataBind();              

            //}
            //else if ((e.CommandName == "del") && (list.Count > 1))
            //{
            //    List<ServiceModels.ProjectInfo.ProjectTarget> newList = new List<ServiceModels.ProjectInfo.ProjectTarget>();
            //    int no = 0;
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        tempItem = list[i];
            //        if (tempItem.UID != uid)
            //        {
            //            no++;
            //            newList.Add(tempItem);
            //        }
            //    }

            //    ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY] = newList;
            //    GridProjectTarget.DataSource = newList;
            //    GridProjectTarget.DataBind();
            //}
        }

        protected void GridProjectTarget_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if(!IsEditableProjectInfo){
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {              
                Label labelTotalAmount = (Label)e.Row.Cells[3].FindControl("LabelTotalProjectTargetAmount");
                
                int? amount =0, totalAmount = 0;
                if (labelTotalAmount != null)
                {
                    var obj = ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY];
                    List<ServiceModels.ProjectInfo.ProjectTarget> list = (obj != null) ? (List<ServiceModels.ProjectInfo.ProjectTarget>)obj : new List<ServiceModels.ProjectInfo.ProjectTarget>();
                    ServiceModels.ProjectInfo.ProjectTarget data;
                    for (int i = 0; i < list.Count; i++)
                    {
                        data = list[i];                       
                        amount += (data.Amount.HasValue) ? (int?)data.Amount : (int?)0;
                        totalAmount += (data.Amount.HasValue) ? (int?)data.Amount : (int?)0;
                    }

                    labelTotalAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(totalAmount, "N0", "");                  
                }
            }
        }

        protected void BindGridProjectTarget(List<ServiceModels.ProjectInfo.ProjectTarget> projectTargets)
        {
            //List<ServiceModels.ProjectInfo.ProjectTarget> newList = new List<ServiceModels.ProjectInfo.ProjectTarget>();
            //if ((projectTargets != null) && (projectTargets.Count > 0))
            //{               
            //    foreach (var item in projectTargets)
            //    {
            //        ServiceModels.ProjectInfo.ProjectTarget t = new ServiceModels.ProjectInfo.ProjectTarget();
            //        t.UID = Guid.NewGuid().ToString();
            //        t.ProjectID = item.ProjectID;
            //        t.ProjectTargetID = item.ProjectTargetID;
            //        t.TargetID = item.TargetID;
            //        t.TargetName = item.TargetName;
            //        t.TargetOtherName = item.TargetOtherName;
            //        t.MenAmount = item.MenAmount;
            //        t.WomenAmount = item.WomenAmount;
            //        t.Amount = item.Amount;
            //        newList.Add(t);
            //    }

            //    GridProjectTarget.DataSource = newList;               
            //    GridProjectTarget.DataBind();                 
            //}
            //else
            //{
            //    newList.Add(new ServiceModels.ProjectInfo.ProjectTarget()
            //    {
            //        UID = Guid.NewGuid().ToString(),
            //        TargetName = "",
            //        TargetOtherName = "",
            //        MenAmount = null,
            //        WomenAmount = null,
            //        Amount = null
            //    });
            //    GridProjectTarget.DataSource = newList;
            //    GridProjectTarget.EditIndex = 0;
            //    GridProjectTarget.DataBind();                
            //}
            //ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY] = newList;            
        }

        protected void TextBoxTargetAmount_OnTextChanged(object sender, EventArgs e)
        {
            //int editIndex = GridProjectTarget.EditIndex;
            //GridViewRow editRow = GridProjectTarget.Rows[editIndex];
            //TextBox textboxMenAmount = (TextBox)editRow.FindControl("TextBoxProjectTargetMenAmount");
            //TextBox textboxWomenAmount = (TextBox)editRow.FindControl("TextBoxProjectTargetWomenAmount");
            //Label labelAmount = (Label)editRow.FindControl("LabelTargetAmount");
            //decimal? women, men, amount;

            //if ((labelAmount != null) && (textboxMenAmount != null) && (textboxWomenAmount != null))
            //{
            //    string txtMen = textboxMenAmount.Text.Trim();
            //    string txtWomen = textboxWomenAmount.Text.Trim();

            //    men = (!string.IsNullOrEmpty(txtMen))? Convert.ToDecimal(txtMen) : 0;
            //    women = (!string.IsNullOrEmpty(txtWomen)) ? Convert.ToDecimal(txtWomen) : 0;

            //    amount = women + men;
            //    labelAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(amount, "N0", "");
            //    textboxMenAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(men, "N0", "");
            //    textboxWomenAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(women, "N0", "");
            //}
        }

        protected void OnComboBoxProjectTargetSelectedIndexChanged(object sender, EventArgs e)
        {
            //var editRow = GridProjectTarget.Rows[GridProjectTarget.EditIndex];

            //ComboBox comboBoxProjectTarget = (ComboBox)editRow.FindControl("ComboBoxProjectTarget");
            //string text = comboBoxProjectTarget.SelectedItem.Text;
            //TextBox textBoxTargetOtherName = (TextBox)editRow.FindControl("TextBoxTargetOtherName");            
            //RequiredFieldValidator validator = (RequiredFieldValidator)editRow.FindControl("RequiredFieldValidatorTargetOtherName");
            //text = text.Trim();
            //if (text.IndexOf(Common.Constants.OTHER_PROJECT_TARGET_NAME) >= 0)
            //{
            //    textBoxTargetOtherName.Visible = true;
            //    textBoxTargetOtherName.Text = "";
            //    validator.Enabled = true;
            //}
            //else
            //{
            //    textBoxTargetOtherName.Visible = false;
            //    validator.Enabled = false;
            //}
        }



        private void RebindBudgetDetailDataSource()
        {
            //var obj = ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY];
            //List<ServiceModels.ProjectInfo.ProjectTarget> list = (obj != null) ? (List<ServiceModels.ProjectInfo.ProjectTarget>)obj : new List<ServiceModels.ProjectInfo.ProjectTarget>();

            //ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY] = list;
            //GridProjectTarget.DataSource = list;
            //GridProjectTarget.DataBind();
        }

        #endregion

        #region Private
        private void BindDisplay(ServiceModels.ProjectInfo.TabProjectInfo model, List<Common.ProjectFunction> functions)
        {           
            if (model != null)
            {
                bool canSendProjectInfo = (model.RequiredSubmitData == null);
                RequiredSubmitData = model.RequiredSubmitData;

                //Check Function 
                //kenghot
                //IsEditableProjectInfo = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData));
                var master = (this.Page.Master as MasterPages.SiteMaster);
                IsEditableProjectInfo = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData) 
                    || master.UserInfo.IsAdministrator);
                //end kenghot

                ButtonSave.Visible = IsEditableProjectInfo;
                ButtonDraft.Visible = ButtonSave.Visible;
                //ButtonAddProjectTarget.Visible = IsEditableProjectInfo;
                CreateProjectGroupForm.Visible = IsEditableProjectInfo;
                //CreateProjectGroupFormDesc.Visible = IsEditableProjectInfo;

                ButtonSendProjectInfo.Visible = (functions.Contains(Common.ProjectFunction.SaveDarft) && canSendProjectInfo);
                ButtonDelete.Visible = functions.Contains(Common.ProjectFunction.Delete);
                HyperLinkPrint.Visible = (functions.Contains(Common.ProjectFunction.PrintDataForm) && canSendProjectInfo);
                ButtonCancelProjectRequest.Visible = functions.Contains(Common.ProjectFunction.CancelledProjectRequest);
                ButtonReject.Visible = functions.Contains(Common.ProjectFunction.Reject);

                TextBoxProjectInfoNameTH.Text = model.ProjectInfoNameTH;
                TextBoxProjectInfoNameEN.Text = model.ProjectInfoNameEN;
                LabelProjectNo.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(model.ProjectNo, null, "-");
                DropDownListProjectInfoType.SelectedValue = (model.ProjectInfoType.HasValue) ? model.ProjectInfoType.ToString() : "";
                ProjectInfoStartDate.SelectedDate = model.ProjectInfoStartDate;

                    
                DateTime thDate = new DateTime((int)model.BudgetYear, 1, 1, 0, 0, 0, Common.Constants.CULTUREINFO.Calendar);
                LabelBudgetYear.Text = thDate.ToString("yyyy", Common.Constants.UI_CULTUREINFO);
                    
                RadioButtonListTypeDisabilitys.SelectedValue = (model.TypeDisabilitys.HasValue) ? model.TypeDisabilitys.ToString() : null;
                TextBoxPrinciples.Text = model.Principles;
                TextBoxProjectInfoObjective.Text = model.ProjectInfoObjective;
                TextBoxProjectInfoindicator.Text = model.ProjectInfoindicator;
                TextBoxProjectInfoAnticipation.Text = model.ProjectInfoAnticipation;

                if(!String.IsNullOrEmpty(model.RejectComment)){
                    TextBoxRejectComment.Text = model.RejectComment;
                    RejectCommentBlock.Visible = true;
                }

                if(model.CancelledAttachmentID.HasValue){
                    List<KendoAttachment> attachList = new List<KendoAttachment>();
                    attachList.Add(model.CancelledAttachment);
                    C2XFileUploadCancelledProjectRequest.ExistingFiles = attachList;
                    CancelledProjectRequestBlock.Visible = true;
                    CancelledAttachmentID = model.CancelledAttachmentID;
                }

                LabelSubmitedDate.Text = (model.SubmitedDate.HasValue) ? ((DateTime)model.SubmitedDate).ToString("d MMMM yyyy HH:mm:ss", Common.Constants.UI_CULTUREINFO) : "-";

            }
        }

        private void BindDropDownListProjectInfoType()
        {
            try
            {
                List<ServiceModels.GenericDropDownListData> list = new List<GenericDropDownListData>();
                list = _service.ListProjectInfoType();

                DropDownListProjectInfoType.DataSource = list;
                DropDownListProjectInfoType.DataTextField = "Text";
                DropDownListProjectInfoType.DataValueField = "Value";
                DropDownListProjectInfoType.DataBind();

                DropDownListProjectInfoType.Items.Insert(0, new ListItem(UI.DropdownPleaseSelect, ""));
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        private void BindCheckBoxListTypeDisabilitys()
        {
            try
            {
                List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
                list = _service.ListDisabilityType();

                RadioButtonListTypeDisabilitys.DataSource = list;
                RadioButtonListTypeDisabilitys.DataTextField = "Text";
                RadioButtonListTypeDisabilitys.DataValueField = "Value";
                RadioButtonListTypeDisabilitys.DataBind();
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        private void RegisterClientScript()
        {
            //String projectInfoTargetAmountChangeScript = @"
            //    function onProjectInfoTargetAmountChange() {
            //        var maleAmount = $('.projectinfo-male-targetgroup').val();
            //        var femaleAmount = $('.projectinfo-female-targetgroup').val();
            //        var totalAmount = 0;

            //        maleAmount = maleAmount.replace(/,/g, '');
            //        maleAmount = parseInt(((maleAmount == '') ? 0 : maleAmount), 10);

            //        femaleAmount = femaleAmount.replace(/,/g, '');
            //        femaleAmount = parseInt(((femaleAmount == '') ? 0 : femaleAmount), 10);

            //        totalAmount = maleAmount + femaleAmount;
            //        totalAmount = totalAmount.format('N0');

            //        $('.projectinfo-total-targetgroup').val(totalAmount);


            //        var label = $('.projectinfo-total-targetgroup').next();
            //        label.text(totalAmount);

            //        var hidden = $(label).next();
            //        hidden.val(totalAmount);
            //    }
            //";
            //ScriptManager.RegisterClientScriptBlock(
            //           GridProjectTarget,
            //           this.GetType(),
            //           "ProjectInfoTargetAmountChang",
            //           projectInfoTargetAmountChangeScript,
            //           true);

            String validateDisabilityTypeScript = @"
                function ValidateRadioButtonList(sender, args) {
                    var checkBoxList = document.getElementById('" + RadioButtonListTypeDisabilitys.ClientID + @"');
                    var checkboxes = checkBoxList.getElementsByTagName('input');
                    var isValid = false;
                    for (var i = 0; i < checkboxes.length; i++) {
                        if (checkboxes[i].checked) {
                            isValid = true;
                            break;
                        }
                    }
                    args.IsValid = isValid;
                }
            ";
            ScriptManager.RegisterClientScriptBlock(
                       this.Page,
                       this.GetType(),
                       "ValidateDisabilityTypeScript",
                       validateDisabilityTypeScript,
                       true);




            String budgetYearScript = @"
                function onProjectInfoStartDateChanged(sender, args) {
                    var startDatePicker = $find('ProjectInfoStartDate');  
                    var startDate = startDatePicker.get_selectedDate();
                             
                    var strYear = '-';    
                    var year, month;    
                    if(startDate != null){
                        month = startDate.getMonth() + 1; 
                        year = startDate.getFullYear();
                        strYear = (month > 9)? ((year + 1) + 543) : (year + 543);
                    }
                    var labelId = '" + LabelBudgetYear.ClientID + @"';
                    $('#'+ labelId).text(strYear);                    
                
                }";

            ScriptManager.RegisterClientScriptBlock(
                       this.Page,
                       this.GetType(),
                       "UpdateBudgetYearScript",
                       budgetYearScript,
                       true);
        }
        #endregion Private

        protected void CustomValidatorTargetGroup_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isValid = false;

            var text = HiddenFieldProjectTargetGroup.Value;
            isValid = (!String.IsNullOrEmpty(text));
            //var obj = ViewState[PROJECT_TARGET_LIST_VIEWSTATE_KEY];
            //if(obj != null){
            //    List<ServiceModels.ProjectInfo.ProjectTarget> list = (List<ServiceModels.ProjectInfo.ProjectTarget>)obj;
            //    ServiceModels.ProjectInfo.ProjectTarget tmp = list.FirstOrDefault();
            //    if((tmp != null) && (tmp.TargetID > 0)){
            //        isValid = true;
            //    }                
            //}

            args.IsValid = isValid;
        }
       
    }


}