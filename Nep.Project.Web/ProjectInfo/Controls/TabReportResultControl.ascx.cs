using AjaxControlToolkit;
using Nep.Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabReportResultControl : Nep.Project.Web.Infra.BaseUserControl
    {
        private string PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY = "PROJECTPARTICIPANT_LIST";
        private const String GENDER_MALE = "M";
        private const String GENDER_FEMALE = "F";

        public IServices.IProjectInfoService _projectService { get; set; }
        public IServices.IListOfValueService _listOfValueService { get; set; }

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
        public Decimal EtcTargetGroupID
        {
            get
            {
                decimal id = 0;
                if (ViewState["EtcTargetGroupID"] != null)
                {   
                    Decimal.TryParse(ViewState["EtcTargetGroupID"].ToString(), out id);                   
                }
                return id;
            }

            set
            {
                ViewState["EtcTargetGroupID"] = value;
            }
        }

        public Boolean IsEditable
        {
            get
            {
                bool  isEdit = false;
                if (ViewState["IsEditableProjectReport"] != null)
                {
                    isEdit = Convert.ToBoolean(ViewState["IsEditableProjectReport"]);
                }
                return isEdit;
            }

            set { ViewState["IsEditableProjectReport"] = value; }
        }

        public Boolean HasSaveDraftReportResultRole
        {
            get { return Convert.ToBoolean(ViewState["HasSaveDraftReportResultRole"]); }
            set
            {
                ViewState["HasSaveDraftReportResultRole"] = value;
            }
        }

        public Boolean HasSaveReportResultRole
        {
            get { return Convert.ToBoolean(ViewState["HasSaveReportResultRole"]); }
            set
            {
                ViewState["HasSaveReportResultRole"] = value;
            }
        }


        public String FollowupViewAttachmentPrefix
        {
            get
            {
                if (ViewState["FollowupViewAttachmentPrefix"] == null)
                {
                    string prefix = "Project/" + ProjectID;
                    ViewState["FollowupViewAttachmentPrefix"] = prefix;
                }
                return ViewState["FollowupViewAttachmentPrefix"].ToString();
            }

            set
            {
                ViewState["ViewAttachmentPrefix"] = value;
            }
        }

        public Decimal? ContractYear
        {
            get
            {
                decimal contractYear = 0;
                if (ViewState["ContractYear"] != null)
                {
                    contractYear = Convert.ToDecimal(ViewState["ContractYear"]);
                }
                return contractYear;
            }

            set
            {
                ViewState["ContractYear"] = value;
            }
        }
        public List<ServiceModels.ProjectInfo.ProjectTargetNameList> TargetGroupList
        {
            get
            {
                List<ServiceModels.ProjectInfo.ProjectTargetNameList> list = new List<ServiceModels.ProjectInfo.ProjectTargetNameList>();
                if (ViewState["TargetGroupList"] != null)
                {
                    list = (List<ServiceModels.ProjectInfo.ProjectTargetNameList>)ViewState["TargetGroupList"];
                }
                return list;
            }

            set
            {
                ViewState["TargetGroupList"] = value;
            }
        }
        //private List<ServiceModels.GenericDropDownListData> isCrippleList  {
        //    get
        //    {
        //        if (Session["isCrippleList"] == null)
        //        {
        //            List<ServiceModels.GenericDropDownListData> isCrippleListTmp = new List<ServiceModels.GenericDropDownListData>();
        //            isCrippleListTmp.Add(new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.LabelCripple, Value = "1" });
        //            isCrippleListTmp.Add(new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.LabelCrippleSupporter, Value = "0" });
        //            //kenghot
        //            isCrippleListTmp.Add(new ServiceModels.GenericDropDownListData { Text = "วิทยากร", Value = "2" });
        //            isCrippleListTmp.Add(new ServiceModels.GenericDropDownListData { Text = "อาสาสมัครประชุมหน้าที่ประสานงาน", Value = "3" });
        //            isCrippleListTmp.Add(new ServiceModels.GenericDropDownListData { Text = "เจ้าหน้าที่โครงการ", Value = "4" });
        //            isCrippleListTmp.Add(new ServiceModels.GenericDropDownListData { Text = "กลุ่มเป้าหมายอื่นๆ ", Value = "5" });
        //            Session["isCrippleList"] = isCrippleListTmp;
        //        }
        //        return (List<ServiceModels.GenericDropDownListData>)Session["isCrippleList"];
        //    }
        //    set { Session["isCrippleList"] = value; }  }
        protected override void OnPreRender(EventArgs e)
        {
           
         

            base.OnPreRender(e);
 
            #region Project Participant
            List<ServiceModels.GenericDropDownListData> genderList = new List<ServiceModels.GenericDropDownListData>();
            genderList.Add(new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.LabelMale, Value = GENDER_MALE });
            genderList.Add(new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.LabelFemale, Value = GENDER_FEMALE });

            //List<ServiceModels.GenericDropDownListData> isCrippleList = new List<ServiceModels.GenericDropDownListData>();
            //isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.LabelCripple, Value = "1" });
            //isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.LabelCrippleSupporter, Value = "0" });
            ////kenghot
            //isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = "วิทยากร", Value = "2" });
            //isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = "อาสาสมัครประชุมหน้าที่ประสานงาน", Value = "3" });
            //isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = "เจ้าหน้าที่โครงการ", Value = "4" });
            //isCrippleList.Add(new ServiceModels.GenericDropDownListData { Text = "กลุ่มเป้าหมายอื่นๆ ", Value = "5" });
           

            bool isRequiredData = ((ContractYear != null) && (ContractYear > 2016));
            var md = new ServiceModels.ProjectInfo.ProjectParticipant();

            //String scriptUrl = ResolveUrl("~/Scripts/manage.projectreport.js?v=" + DateTime.Now.Ticks.ToString());
            String scriptUrl = ResolveUrl("~/Scripts/manage.projectreport.js?v=" + DateTime.Now.Ticks.ToString());
            var refScript = "<script type='text/javascript' src='" + scriptUrl + "'></script>";
            refScript += "<script type='text/javascript' src='../../Scripts/import-participants.js?v=" + DateTime.Now.Ticks.ToString() + "'></script>"; 
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelReportResult,
                       this.GetType(),
                       "RefUpdatePanelReportResultScript",
                       refScript,
                       false);

            String participantScript = @" 
            $(function () {                   
                    
                    //c2xProjectReport.config({
                    var cfg = {
                        IsRequiredData : " + Newtonsoft.Json.JsonConvert.SerializeObject(isRequiredData) + @",
                            
                        HiddenTotalMaleParticipantID : '"+ HiddTotalMaleParticipant.ClientID +@"',
                        HiddenTotalFemaleParticipantID : '" + HiddTotalFemaleParticipant.ClientID + @"',

                        HiddenParticipantID: '" + HiddenFieldParticipant.ClientID + @"',
                        TextBoxParticipantTargetGroupID: '" + TextBoxParticipantTargetGroup.ClientID + @"',
                        ParticipantGridID: '" + ParticipantGrid.ClientID + @"',
                        ParticipantTargetGroupEtcBlockID: 'ParticipantTargetGroupEtcBlock',
                        TextBoxParticipantTargetGroupEtcCreate: '" + TextBoxParticipantTargetGroupEtc.ClientID + @"',

                        ParticipantGenderCreate: '" + TextBoxParticipantGender.ClientID + @"',
                        ParticipantFirstNameCreate: '" + TextBoxParticipantFirstName.ClientID + @"',
                        ParticipantLastNameCreate: '" + TextBoxParticipantLastName.ClientID + @"',
                        ParticipantIdCardCreate: '" + TextBoxParticipantIDCardNo.ClientID + @"',
                        ParticipantIsCrippleCreate: '" + TextBoxIsCripple.ClientID+ @"',

                        BtnAddProjectTarget:'" + ImageButtonSaveParticipant.ClientID + @"',

                        TargetGroupEtcValueID: " + EtcTargetGroupID + @",
                        TargetGroupList: " + Newtonsoft.Json.JsonConvert.SerializeObject(TargetGroupList) + @",                        
                        GenderList: " + Newtonsoft.Json.JsonConvert.SerializeObject(genderList) + @",

                        IsCrippleList : " + Newtonsoft.Json.JsonConvert.SerializeObject(md.isCrippleList) + @",

                        RequiredTargetGroupMsg: '" + String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectTarget_TargetName) + @"',
                        RequiredTargetGroupEtcMsg: '" + String.Format(Nep.Project.Resources.Error.RequiredField, "ชื่อกลุ่มเป้าหมายอื่นๆ") + @"',
                        RequiredTargetGroupDupMsg: '" + String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.ProjectTarget_TargetName) + @"',
                        RequiredFirstNameMsg:  '" + String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectParticipant_FirstName) + @"', 
                        RequiredLastNameMsg:   '" + String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectParticipant_LastName) + @"', 
                        RequiredIdCardMsg: '" + String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectParticipant_IDCardNo) + @"', 
                        IdCardInvalidMsg: '" + Nep.Project.Resources.Error.InvalidIDCardNo + @"', 
                        IdCardDupMsg: '" + String.Format(Nep.Project.Resources.Error.ClientDuplicateValue, Nep.Project.Resources.Model.ProjectParticipant_IDCardNo) + @"', 

                        ColumnTitle:{
                                     No: '" + Nep.Project.Resources.Model.ProjectParticipant_No + @"', FirstName:'" + Nep.Project.Resources.Model.ProjectParticipant_FirstName + @"',
                                     LastName: '" + Nep.Project.Resources.Model.ProjectParticipant_LastName + @"', IDCardNo:'" + Nep.Project.Resources.Model.ProjectParticipant_IDCardNo + @"',
                                     Gender: '" + Nep.Project.Resources.Model.ProjectParticipant_Gender + @"', TargetGroupID: '" + Nep.Project.Resources.Model.ProjectParticipant_TargetGroup + @"',
                                     IsCripple: '" + Nep.Project.Resources.Model.ProjectParticipant_IsCripple + @"'
                                    },
                        IsView: " + Newtonsoft.Json.JsonConvert.SerializeObject(!IsEditable) + @",
                        ProjectID: " + ProjectID + @"
                        };
                    c2xProjectReport.config(cfg);
                    var cfg2 = jQuery.extend(true, {}, cfg);
                    objIMP.config(cfg2);
                    c2xProjectReport.createDdlProjectTargetGroup();
                    c2xProjectReport.createDdlGender();
                    c2xProjectReport.createGridParticipant();
                    c2xProjectReport.createDdlIsCripple();
                   

                });";
            
            ScriptManager.RegisterStartupScript(
                      UpdatePanelReportResult,
                      this.GetType(),
                      "UpdatePanelProjectInfoScript",
                      participantScript,
                      true);
            #endregion Project Participant
        }

        public void BindData()
        {
            bool isEditable = false;
            bool isReadOnly = false;

            var result = _projectService.GetProjectReportResult(ProjectID);
            var act = _projectService.GetProjectBudgetInfoByProjectID(ProjectID);

            TargetGroupList = ComboBoxParticipantTarget_GetData();
            if (result.IsCompleted)
            {
          
                ServiceModels.ProjectInfo.ProjectReportResult model = result.Data;
                ContractYear = model.ContractYear;
          
                bool isReported = (model.FollowupStatusCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว);
                List<Common.ProjectFunction> functions = _projectService.GetProjectFunction(model.ProjectID).Data;
                
                HasSaveDraftReportResultRole = functions.Contains(Common.ProjectFunction.SaveDraftReportResult);
                HasSaveReportResultRole = functions.Contains(Common.ProjectFunction.SaveReportResult);
                
                isEditable = HasSaveDraftReportResultRole;
                IsEditable = isEditable;
                isReadOnly = (!isEditable);


                CreateParticipantForm.Visible = !isReported;

                ButtonSaveReportResult.Visible = isEditable;
                //ButtonAddParticipant.Visible = isEditable;
                ButtonSaveAndSendProjectReport.Visible = isEditable;
                LabelNeedQN.Visible = false;
                if (ButtonSaveAndSendProjectReport.Visible)
                {
                    ButtonSaveAndSendProjectReport.Enabled = true;
                    var db = _projectService.GetDB();
                    var qn = from q in db.PROJECTQUESTIONHDs
                             where q.PROJECTID == ProjectID && (q.QUESTGROUP == "SATISFY" ||
                             q.QUESTGROUP == "SELF") && q.ISREPORTED == "1"
                             select q;
                    if (qn.Count() < 2)
                    {
                        ButtonSaveAndSendProjectReport.Enabled = false;
                        LabelNeedQN.Visible = true;
                    }
                    else
                    {
                        var proc = (from pc in db.PROJECTPROCESSEDs where pc.PROJECTID == ProjectID select pc).FirstOrDefault();
                        if (proc == null)
                        {
                            ButtonSaveAndSendProjectReport.Enabled = false;
                            LabelNeedQN.Visible = true;
                        }
                    }
                }
                ButtonOfficerSave.Visible = (HasSaveReportResultRole && (!HasSaveDraftReportResultRole));
                ButtonRevise.Visible = ButtonOfficerSave.Visible;
                HyperLinkPrint.Visible = functions.Contains(Common.ProjectFunction.PrintReport);

                ButtonSaveAndSendProjectReport.Text = (UserInfo.OrganizationID.HasValue) ? Nep.Project.Resources.UI.ButtonSendProjectReport : Nep.Project.Resources.UI.ButtonConfirmReportResult;

                act.Data.ReviseBudgetAmount = model.ReviseBudgetAmount;
                act.Data.Interest = model.Interest;
                //งบประมาณโครงการ
                string script = "";
                script = @"<script type=""text/javascript"" src=""../../Scripts/Vue/VueActivityBudject.js?v=" + DateTime.Now.Ticks.ToString() + @"""></script>";
                script += @"<script type=""text/javascript"" src=""../../Scripts/JsBarcode.all.min.js""></script>";
                script += @"<script type=""text/javascript"" src=""../../Scripts/xlsx.core.min.js""></script>";
               // script += @"<script type=""text/javascript"" src=""../../Scripts/import-participants.js?v=" + DateTime.Now.Ticks.ToString() + @"""></script>";
                String actTxt = Nep.Project.Common.Web.WebUtility.ToJSON(act);
                var setVueParam = @"appVueAB.param.projID = " + ProjectID.ToString() + @";
                                appVueAB.data = " + actTxt + @";
                                appVueAB.sumData();
                                appVueAB.$mount('#divActivityBudget');$('#excelFile').change(objIMP.importFile);
                                function GetActOBJJSON() { " + hdfActOBJ.ClientID + @".value = JSON.stringify(appVueAB.$data); //console.log(appVueAB.$data);
                                }";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "QuestionareJSFile", script, false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "QNInitialData" + this.ClientID,
                       setVueParam, true);
                decimal reviseBudgetAmount = (model.ReviseBudgetAmount.HasValue) ? model.ReviseBudgetAmount.Value : 0;
                decimal actualExpense = (model.ActualExpense.HasValue) ? model.ActualExpense.Value : 0;
                decimal balanceAmount = reviseBudgetAmount - actualExpense;
                balanceAmount = (balanceAmount < 0) ? 0 : balanceAmount;
                LabelBudgetAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(model.BudgetAmount, "N2", "0.00");
                LabelReviseBudgetAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(model.ReviseBudgetAmount, "N2", "0.00");
                //TextBoxActualExpense.Text = Nep.Project.Common.Web.WebUtility.DisplayInForm(model.ActualExpense, "N2", "");
                //TextBoxActualExpense.Enabled = isEditable;
                //TextBoxBalanceAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInForm(balanceAmount, "N2", "0.00");
                List<ServiceModels.KendoAttachment> attachList = new List<ServiceModels.KendoAttachment>();
                attachList = model.ReportAttachments;
                if (model.ReportAttachment != null)
                {
                    
                    attachList.Add(model.ReportAttachment);
              
                }
                FileUploadReportAttachment.ClearChanges();
                FileUploadReportAttachment.ExistingFiles = attachList;
                FileUploadReportAttachment.DataBind();

                FileUploadReportAttachment.Enabled = isEditable;

                // attach Activity
                attachList = model.ActivityAttachments;
 
                FileUploadActivityAttachment.ClearChanges();
                FileUploadActivityAttachment.ExistingFiles = attachList;
                FileUploadActivityAttachment.DataBind();

                FileUploadActivityAttachment.Enabled = isEditable;

                // attach Participant
                attachList = model.ParticipantAttachments;

                FileUploadParticipantAttachment.ClearChanges();
                FileUploadParticipantAttachment.ExistingFiles = attachList;
                FileUploadParticipantAttachment.DataBind();

                FileUploadParticipantAttachment.Enabled = isEditable;

                // attach Result
                attachList = model.ResultAttachments;

                FileUploadResultAttachment.ClearChanges();
                FileUploadResultAttachment.ExistingFiles = attachList;
                FileUploadResultAttachment.DataBind();

                FileUploadResultAttachment.Enabled = isEditable;

                //Check Is submit data
                decimal? userProvinceID = UserInfo.ProvinceID;
                decimal? userOrganizationID = UserInfo.OrganizationID;

                if (String.IsNullOrEmpty(model.FollowupStatusCode) ||
                    (!String.IsNullOrEmpty(model.FollowupStatusCode) && (model.FollowupStatusCode != Common.LOVCode.Followupstatus.รายงานผลแล้ว)))
                {
                    if (
                        (!UserInfo.IsAdministrator) &&
                        ((model.CreatorOrganizationID.HasValue && userOrganizationID.HasValue && (model.CreatorOrganizationID != userOrganizationID)) ||
                        (model.CreatorOrganizationID.HasValue && userOrganizationID == null) ||
                        (model.CreatorOrganizationID == null && userOrganizationID == null && model.ProvinceID.HasValue && userProvinceID.HasValue && (model.ProvinceID != userProvinceID)))
                        )
                    {
                        //กรณีข้อมูลยังไม่ส่งถึงเจ้าหน้าที่ คนที่ไม่ได้สร้างจะไม่เห็นข้อมูล
                        //สรุปผลการดำเนินงาน
                        BindRadioButtonListOperationResult(null, isEditable);

                        //เปรียบเทียบกับวัตถุประสงค์
                        BindRadioButtonOperationLevel(null, null, isEditable);

                        RegisterClientScript();
                        return;   
                    }
                }

                //กิจกรรมของโครงการ
                TextBoxActivityDescription.Text = model.ActivityDescription;
                TextBoxActivityDescription.ReadOnly = isReadOnly;

                //ผู้เข้าร่วมโครงการ/กิจกรรม
                HiddenFieldParticipant.Value = ((model.Participants != null) && (model.Participants.Count > 0))? Newtonsoft.Json.JsonConvert.SerializeObject(model.Participants) : "";
                HiddTotalMaleParticipant.Value = model.MaleParticipant.ToString();
                HiddTotalFemaleParticipant.Value = model.FemaleParticipant.ToString();
                LabelTotalMaleParticipant.Text = model.MaleParticipant.ToString("N0");
                LabelTotalFemaleParticipant.Text = model.FemaleParticipant.ToString("N0");
                LabelTotalParticipant.Text = (model.MaleParticipant + model.FemaleParticipant).ToString("N0");
                //BindGridViewParticipant(model.Participants);
                //updateSummaryParticipantAmount(model.Participants);

                

                //ผลการดำเนินงาน/ประโยชน์ที่ได้รับจากการดำเนินงาน
                TextBoxBenefit.Text = model.Benefit;
                TextBoxBenefit.ReadOnly = isReadOnly;

                //ปัญหาอุปสรรค์และวิธีการแก้ปัญหาจากการดำเนินการ
                TextBoxProblemsAndObstacle.Text = model.ProblemsAndObstacle;
                TextBoxProblemsAndObstacle.ReadOnly = isReadOnly;

                //ข้อคิดเห็นและข้อเสนอแนะ
                TextBoxSuggestion.Text = model.Suggestion;
                TextBoxSuggestion.ReadOnly = isReadOnly;

                //สรุปผลการดำเนินงาน
                BindRadioButtonListOperationResult(model.OperationResult, isEditable);

                //เปรียบเทียบกับวัตถุประสงค์
                BindRadioButtonOperationLevel(model.OperationLevel, model.OperationLevelDesc, isEditable);

                //ลงชื่อผู้รายงาน
                TextBoxReporter1FirstName.Text = model.ReporterName1;
                TextBoxReporter1FirstName.Enabled = isEditable;

                TextBoxReporter1LastName.Text = model.ReporterLastname1;
                TextBoxReporter1LastName.Enabled = isEditable;

                DatePickerReporter1.SelectedDate = model.RepotDate1;
                DatePickerReporter1.Enabled = isEditable;

                TextBoxReporter1Position.Text = model.Position1;
                TextBoxReporter1Position.Enabled = isEditable;

                TextBoxReporter1Telephone.Text = model.Telephone1;
                TextBoxReporter1Telephone.Enabled = isEditable;

               


                //PanelSuggestionDesc.Visible = (functions.Contains(Common.ProjectFunction.PrintReport) && (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer));
                PanelSuggestionDesc.Visible = ((HasSaveDraftReportResultRole && HasSaveReportResultRole) || (functions.Contains(Common.ProjectFunction.PrintReport) && (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer)) || (model.FollowupStatusCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว));
                bool canSaveReportResult = (functions.Contains(Common.ProjectFunction.SaveReportResult)); 
                //ลงชื่อเจ้าหน้าที่ผู้รายงาน
                TextBoxSuggestionDesc.Text = model.SuggestionDesc;
                TextBoxSuggestionDesc.ReadOnly = !canSaveReportResult;

                TextBoxReporter2FirstName.Text = model.ReporterName2; 
                TextBoxReporter2FirstName.Enabled = canSaveReportResult;

                TextBoxReporter2LastName.Text = model.ReporterLastname2;
                TextBoxReporter2LastName.Enabled = canSaveReportResult;

                DatePickerReporter2.SelectedDate = model.RepotDate2;
                DatePickerReporter2.Enabled = canSaveReportResult;

                TextBoxReporter2Position.Text = model.Position2;
                TextBoxReporter2Position.Enabled = canSaveReportResult;

                TextBoxReporter2Telephone.Text = model.Telephone2;
                TextBoxReporter2Telephone.Enabled = canSaveReportResult;     
           
               
                    
               
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            var etcTargetGroupResult = _listOfValueService.GetListOfValueByCode(Common.LOVGroup.TargetGroup, Common.LOVCode.Targetgroup.อื่นๆ);
            if (etcTargetGroupResult.IsCompleted)
            {
                EtcTargetGroupID = etcTargetGroupResult.Data.LovID;
            }
            else
            {
                ShowErrorMessage(etcTargetGroupResult.Message);
            }


            //TextBoxParticipantFirstName.Attributes.Add("keydown", "c2xProjectReport.onTextBoxKeyUp(event, true)");
            //TextBoxParticipantLastName.Attributes.Add("keydown", "c2xProjectReport.onTextBoxKeyUp(event, true)");
            //TextBoxParticipantIDCardNo.Attributes.Add("keydown", "c2xProjectReport.onTextBoxKeyUp(event, true)");
            //TextBoxParticipantTargetGroupEtc.Attributes.Add("keydown", "c2xProjectReport.onTextBoxKeyUp(event, true)");

            RegisterClientScript();
        }


        #region Main Button
        protected void ButtonSaveReportResult_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    
                    bool isSaveReportResult = (HasSaveReportResultRole && HasSaveReportResultRole);
                    var result = _projectService.SaveProjectReportResult(GetData(), isSaveReportResult, false);
                    if (result.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelReportResult");
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
       
        protected void ButtonSaveAndSendProjectReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    bool isSaveReportResult = (HasSaveReportResultRole && HasSaveReportResultRole);
                    var result = _projectService.SaveProjectReportResult(GetData(), isSaveReportResult, true);
                    if (result.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelReportResult");
                        string message = (HasSaveDraftReportResultRole && HasSaveReportResultRole) ? Nep.Project.Resources.Message.SendProjectReportMessage : Nep.Project.Resources.Message.SubmitProjectReportMessage;
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

        protected void ButtonOfficerSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    var result = _projectService.SaveOfficerProjectReport(GetOfficerData());
                    if (result.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelReportResult");
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
        #endregion Main Button
        

        #region Manage GridView
        //protected void BindGridViewParticipant(List<ServiceModels.ProjectInfo.ProjectParticipant> participants)
        //{
        //    List<ServiceModels.ProjectInfo.ProjectParticipant> list;
        //    ServiceModels.ProjectInfo.ProjectParticipant temp;
        //    if ((participants != null) && (participants.Count > 0))
        //    {
        //        list = participants;
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            temp = list[i];
        //            temp.No = (i + 1);
        //            temp.UID = Guid.NewGuid().ToString();                    
        //            list[i] = temp;

        //        }
        //        ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY] = list;
        //        //GridViewParticipant.DataSource = list;
        //        //GridViewParticipant.DataBind();
        //    }
        //    else
        //    {
        //        list = new List<ServiceModels.ProjectInfo.ProjectParticipant>();
        //        list.Add(new ServiceModels.ProjectInfo.ProjectParticipant()
        //        {
        //            UID = Guid.NewGuid().ToString(),
        //            FirstName = "",
        //            LastName = "",
        //            IDCardNo = "",
        //            Gender = GENDER_MALE,
        //            ProjectTargetGroupID = (decimal?)null,
        //            TargetGroupID = (decimal?)null,
        //            TargetGroupCode = "",
        //            TargetGroupName = "",                   
        //        });
        //        //GridViewParticipant.DataSource = list;
        //        //GridViewParticipant.EditIndex = 0;
        //        //GridViewParticipant.DataBind();
        //    }


        //}

        //protected void OnComboBoxParticipantTargetSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var editRow = GridViewParticipant.Rows[GridViewParticipant.EditIndex];

        //    ComboBox comboBoxProjectTarget = (ComboBox)editRow.FindControl("ComboBoxParticipantTarget");
        //    string value = comboBoxProjectTarget.SelectedItem.Value;
        //    decimal targetID = 0;
        //    Decimal.TryParse(value, out targetID);

        //    AjaxControlToolkit.ComboBox cbbEtcTargetGroup = (AjaxControlToolkit.ComboBox)editRow.FindControl("ComboBoxParticipantTargetEtc");
        //    CustomValidator validator = (CustomValidator)editRow.FindControl("CustomValidatorParticipantTargetEtc");

        //    if (targetID == EtcTargetGroupID)
        //    {
        //        cbbEtcTargetGroup.Visible = true;
        //        validator.Enabled = true;
        //    }
        //    else
        //    {
        //        cbbEtcTargetGroup.Visible = false;
        //        validator.Enabled = false;
        //    }

        //}

        public List<ServiceModels.GenericDropDownListData> ComboBoxGender_GetData()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            list.Add(new ServiceModels.GenericDropDownListData()
            {
                Text = Nep.Project.Resources.UI.LabelMale,
                Value = GENDER_MALE
            });

            list.Add(new ServiceModels.GenericDropDownListData()
            {
                Text = Nep.Project.Resources.UI.LabelFemale,
                Value = GENDER_FEMALE
            });
            return list;
        }

        public List<ServiceModels.ProjectInfo.ProjectTargetNameList> ComboBoxParticipantTarget_GetData()
        {
            List<ServiceModels.ProjectInfo.ProjectTargetNameList> list = new List<ServiceModels.ProjectInfo.ProjectTargetNameList>();
            var result = _projectService.GetProjectTargetForParticipant(ProjectID);
            if (result.IsCompleted)
            {
                list = result.Data;
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return list;
        }

        //public List<ServiceModels.GenericDropDownListData> ComboBoxParticipantTargetEtc_GetData()
        //{
        //    List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
        //    var result = _projectService.GetProjectParticipantTargetEtc(ProjectID);
        //    if (result.IsCompleted)
        //    {

        //        if ((result.Data != null) && (result.Data.Count > 0))
        //        {
        //            list = result.Data;
        //        }
        //        else
        //        {
        //            list.Add(new ServiceModels.GenericDropDownListData { Value = "", Text = ""});
        //        }
        //    }
        //    else
        //    {
        //        ShowErrorMessage(result.Message);
        //    }

        //    return list;
        //}

        protected void ButtonAddParticipant_Click(object sender, EventArgs e)
        {
            var obj = ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.ProjectParticipant> list = (obj != null) ?
                ((List<ServiceModels.ProjectInfo.ProjectParticipant>)obj).ToList() : new List<ServiceModels.ProjectInfo.ProjectParticipant>();

            list.Insert(0, new ServiceModels.ProjectInfo.ProjectParticipant()
            {
                UID = Guid.NewGuid().ToString(),
                FirstName = "",
                LastName = "",
                IDCardNo = "",
                Gender = GENDER_MALE,
                ProjectTargetGroupID = (decimal?)null,
                TargetGroupID = (decimal?)null,
                TargetGroupCode = "",
                TargetGroupName = ""
                
            });

            //GridViewParticipant.DataSource = list;
            //GridViewParticipant.EditIndex = 0;
            //GridViewParticipant.DataBind();
        }

        protected void GridViewParticipant_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //GridViewParticipant.EditIndex = e.NewEditIndex;
            //RebindParticipantDataSource();
            //var obj = ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY];
            //List<ServiceModels.ProjectInfo.ProjectParticipant> list = (obj != null) ? (List<ServiceModels.ProjectInfo.ProjectParticipant>)obj : new List<ServiceModels.ProjectInfo.ProjectParticipant>();
            //var editRow = GridViewParticipant.Rows[GridViewParticipant.EditIndex];
            //HiddenField hiddUid = (HiddenField)editRow.FindControl("HiddenFieldUid");
            //string uid = hiddUid.Value;

            //var editItem = list.Where(x => x.UID == uid).FirstOrDefault();
            //if (editItem != null)
            //{
            //    decimal? targetGroupID = editItem.TargetGroupID;
            //    decimal? projectTargetGroupID = editItem.ProjectTargetGroupID;
            //    string gender = editItem.Gender;
            //    string targetKey = "";
            //    if (targetGroupID.HasValue)
            //    {
            //        targetKey = String.Format("{0}:{1}", projectTargetGroupID, targetGroupID);
            //        AjaxControlToolkit.ComboBox cbbTargetGroup = (AjaxControlToolkit.ComboBox)editRow.FindControl("ComboBoxParticipantTarget");
            //        cbbTargetGroup.SelectedValue = targetKey;
            //    }

            //    DropDownList ddlGender = (DropDownList)editRow.FindControl("DropDownListGender");
            //    ddlGender.SelectedValue = editItem.Gender;

            //}  
        }

        protected void GridViewParticipant_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //var obj = ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY];
            //List<ServiceModels.ProjectInfo.ProjectParticipant> list = (obj != null) ?
            //    (List<ServiceModels.ProjectInfo.ProjectParticipant>)obj : new List<ServiceModels.ProjectInfo.ProjectParticipant>();
            //if (list.Count > 0)
            //{
            //    GridViewParticipant.EditIndex = -1;
            //    RebindParticipantDataSource();
            //}
        }

        protected void GridViewParticipant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //var obj = ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY];
            //List<ServiceModels.ProjectInfo.ProjectParticipant> list = (obj != null) ? (List<ServiceModels.ProjectInfo.ProjectParticipant>)obj : new List<ServiceModels.ProjectInfo.ProjectParticipant>();
            //ServiceModels.ProjectInfo.ProjectParticipant tempItem;
            //String uid = e.CommandArgument.ToString();

            
            //if (e.CommandName == "save" && (Page.IsValid))
            //{                
            //    var editRow = GridViewParticipant.Rows[GridViewParticipant.EditIndex];
            //    ServiceModels.ProjectInfo.ProjectParticipant oldItem = list.Where(x => x.UID == uid).FirstOrDefault();
            //    ServiceModels.ProjectInfo.ProjectParticipant editItem = (oldItem != null) ? oldItem : new ServiceModels.ProjectInfo.ProjectParticipant();
            //    TextBox textBoxFirstName = (TextBox)editRow.FindControl("TextBoxParticipantFirstName");
            //    TextBox textBoxLastName = (TextBox)editRow.FindControl("TextBoxParticipantLastName");
            //    TextBox textBoxIDCardNo = (TextBox)editRow.FindControl("TextBoxParticipantIDCardNo");
            //    DropDownList ddlGender = (DropDownList)editRow.FindControl("DropDownListGender");
            //    AjaxControlToolkit.ComboBox cbbTargetGroup = (AjaxControlToolkit.ComboBox)editRow.FindControl("ComboBoxParticipantTarget");

            //    String genderValue = ddlGender.SelectedValue;
            //    String targetKeyValue = cbbTargetGroup.SelectedValue;
            //    String[] tmpKeyValue = targetKeyValue.Split(new char[1] { ':' }); /* ProjectTargetID:TargetID */
            //    String projectTargetValue = tmpKeyValue[0];
            //    String targetGroupValue = tmpKeyValue[1];

            //    Decimal targetGroupID = 0;
            //    Decimal projectTargetID = 0;
            //    Decimal.TryParse(targetGroupValue, out targetGroupID);
            //    Decimal.TryParse(projectTargetValue, out projectTargetID);

            //    String selectText = cbbTargetGroup.SelectedItem.Text;
            //    String etc = (targetGroupID == EtcTargetGroupID) ? selectText : "";
            //    etc = etc.Trim();

            //    editItem.FirstName = textBoxFirstName.Text.Trim();
            //    editItem.LastName = textBoxLastName.Text.Trim();
            //    editItem.IDCardNo = textBoxIDCardNo.Text.Replace("-", "");
            //    editItem.Gender = genderValue;
            //    editItem.GenderName = (genderValue == GENDER_MALE) ? Nep.Project.Resources.UI.LabelMale : Nep.Project.Resources.UI.LabelFemale;
            //    editItem.TargetGroupID = targetGroupID;
            //    editItem.ProjectTargetGroupID = projectTargetID;
            //    editItem.TargetGroupName = selectText;
            //    editItem.TargetGroupEtc = etc;


            //    if (oldItem != null)
            //    {
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            tempItem = list[i];
            //            if (tempItem.UID == uid)
            //            {
            //                list[i] = editItem;
            //                break;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        editItem.No = (list.Count() + 1);
            //        editItem.UID = uid;
            //        list.Add(editItem);
            //    }

            //    GridViewParticipant.EditIndex = -1;
            //    ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY] = list;
            //    GridViewParticipant.DataSource = list;
            //    GridViewParticipant.DataBind();

            //    updateSummaryParticipantAmount(list);
                             
            //}           
            //else if ((e.CommandName == "del") && (list.Count > 1))
            //{
            //    int no = 0;
            //    List<ServiceModels.ProjectInfo.ProjectParticipant> newList = new List<ServiceModels.ProjectInfo.ProjectParticipant>();               
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        tempItem = list[i];
            //        if (tempItem.UID != uid)
            //        {
            //            no++;
            //            tempItem.No = no;
            //            newList.Add(tempItem);
            //        }
            //    }
            //    ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY] = newList;
            //    GridViewParticipant.DataSource = newList;
            //    GridViewParticipant.DataBind();

            //    updateSummaryParticipantAmount(newList);
            //}
        }

        protected void CustomValidatorParticipant_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //var obj = ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY];
            //List<ServiceModels.ProjectInfo.ProjectParticipant> list = (obj != null) ? (List<ServiceModels.ProjectInfo.ProjectParticipant>)obj : 
            //    new List<ServiceModels.ProjectInfo.ProjectParticipant>();
            
            //args.IsValid = (list.Count > 0);

            String participantText = HiddenFieldParticipant.Value;
            args.IsValid = (participantText != "");

        }


        protected void CustomValidatorParticpantName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //GridViewRow editRow = GridViewParticipant.Rows[GridViewParticipant.EditIndex];            
            //var obj = ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY];
            //List<ServiceModels.ProjectInfo.ProjectParticipant> list = (obj != null) ? (List<ServiceModels.ProjectInfo.ProjectParticipant>)obj : new List<ServiceModels.ProjectInfo.ProjectParticipant>();
            //TextBox textBoxFirstName = (TextBox)editRow.FindControl("TextBoxParticipantFirstName");
            //TextBox textBoxLastName = (TextBox)editRow.FindControl("TextBoxParticipantLastName");
            //HiddenField hiddUid = (HiddenField)editRow.FindControl("HiddenFieldUid");
            //string firstName = textBoxFirstName.Text.ToLower();
            //string lastName = textBoxLastName.Text.ToLower();
            //string editUid = hiddUid.Value;

            //if((!String.IsNullOrEmpty(firstName)) && (!String.IsNullOrEmpty(lastName))){
            //    var data = list.Where(x => (x.FirstName.ToLower() == firstName) && (x.LastName.ToLower() == lastName) && (x.UID != editUid)).FirstOrDefault();
            //    args.IsValid = (data == null);
            //}
        }

        protected void CustomValidatorParticpantIDCardNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //GridViewRow editRow = GridViewParticipant.Rows[GridViewParticipant.EditIndex];            
            //var obj = ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY];
            //List<ServiceModels.ProjectInfo.ProjectParticipant> list = (obj != null) ? (List<ServiceModels.ProjectInfo.ProjectParticipant>)obj : new List<ServiceModels.ProjectInfo.ProjectParticipant>();
            //TextBox textBoxIDCardNo = (TextBox)editRow.FindControl("TextBoxParticipantIDCardNo");
            //HiddenField hiddUid = (HiddenField)editRow.FindControl("HiddenFieldUid");
            //string idCardNo = textBoxIDCardNo.Text.Replace("-", "");
            //string editUid = hiddUid.Value;

            //if (!String.IsNullOrEmpty(idCardNo))
            //{
            //    var data = list.Where(x => (x.IDCardNo == idCardNo) && (x.UID != editUid)).FirstOrDefault();
            //    args.IsValid = (data == null);
            //}
        }
        
        protected void CustomValidatorParticipantTargetEtc_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //GridViewRow editRow = GridViewParticipant.Rows[GridViewParticipant.EditIndex];
            //AjaxControlToolkit.ComboBox cbbEtcTargetGroup = (AjaxControlToolkit.ComboBox)editRow.FindControl("ComboBoxParticipantTargetEtc");
            //int selectedIndex = cbbEtcTargetGroup.SelectedIndex;
            //args.IsValid = (selectedIndex < 0) ? false : true;
        }

        private void RebindParticipantDataSource()
        {
            var obj = ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.ProjectParticipant> list = (obj != null) ?
                (List<ServiceModels.ProjectInfo.ProjectParticipant>)obj : new List<ServiceModels.ProjectInfo.ProjectParticipant>();

            ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY] = list;
            //GridViewParticipant.DataSource = list;
            //GridViewParticipant.DataBind();
        }

        //private void updateSummaryParticipantAmount(List<ServiceModels.ProjectInfo.ProjectParticipant> list)
        //{
        //    int countMale = 0;
        //    int countFemale = 0;
        //    if((list != null) && (list.Count > 0)){
        //        countMale = list.Where(x => x.Gender == GENDER_MALE).Count();
        //        countFemale = list.Where(x => x.Gender == GENDER_FEMALE).Count();
        //    }

        //    //LabelTotalMaleParticipant.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(countMale, "N0", "0");
        //    //LabelTotalFemaleParticipant.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(countFemale, "N0", "0");
        //    //LabelTotalParticipant.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml((countMale + countFemale), "N0", "0");
        //}
        #endregion Manage GridView

        #region CustomValidator
        
        protected void CustomValidatorOperationResult_ServerValidate(object source, ServerValidateEventArgs args)
        {            
            args.IsValid = (!String.IsNullOrEmpty(RadioButtonListOperationResult.SelectedValue));
        }

        protected void CustomValidatorOperationLevel_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isValid = false;
            var operationLevels = PanelOperationLevel.Controls.OfType<RadioButton>().Where(x => x.Checked).FirstOrDefault();
            if (operationLevels != null)
            {
                isValid = true;               
            }
            args.IsValid = isValid;
        }

        protected void CustomValidatorOperationLevelDesc_ServerValidate(object source, ServerValidateEventArgs args)
        {            
            String selectedCode;
            String error = "";
           
            var operationLevels = PanelOperationLevel.Controls.OfType<RadioButton>().Where(x => x.Checked).FirstOrDefault();
            if (operationLevels != null)
            {
                selectedCode = operationLevels.Attributes["Data-Code"];
                String textDesc = TextBoxOperationLevelDesc.Text;
                if ((selectedCode == Common.LOVCode.Operationlevel.สูงกว่าเป้าหมาย_เพราะ) && (String.IsNullOrEmpty(textDesc)))
                {
                    error = String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectReportResult_OperationLevelDesc);
                }
                else if ((selectedCode != Common.LOVCode.Operationlevel.สูงกว่าเป้าหมาย_เพราะ) && (!String.IsNullOrEmpty(textDesc)))
                {
                    String fieldDesc = RadioButtonOperationLevel_1.Text;
                    error = String.Format(Nep.Project.Resources.Error.ValidateOperationLevelDesc, fieldDesc);
                }
                
            }

            CustomValidator validator = source as CustomValidator;
            validator.Text = error;
            validator.ErrorMessage = error;

            args.IsValid =String.IsNullOrEmpty(error);
        }

        protected void CustomValidatorReportAttachment_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //String textReviseBudgetAmount = LabelReviseBudgetAmount.Text;
            //String textActualExpense = TextBoxActualExpense.Text;
            //decimal reviseBudgetAmount = 0;
            //decimal actualExpense = 0;

            //Decimal.TryParse(textReviseBudgetAmount.Replace(",", ""), out reviseBudgetAmount);
            //Decimal.TryParse(textActualExpense, out actualExpense);
            //decimal totalBalance =  reviseBudgetAmount - actualExpense;

            //IEnumerable<ServiceModels.KendoAttachment> files = FileUploadReportAttachment.AllFiles;
            //if ((totalBalance > 0) && ((files == null) || (files.Count() == 0)))
            //{
            //    args.IsValid = false;
            //}
        }

        #endregion CustomValidator

        #region Private Method
        

        private void BindRadioButtonListOperationResult(Decimal? selectedID, bool isEditable)
        {
            var result = _listOfValueService.ListActive(Nep.Project.Common.LOVGroup.OperationResult, selectedID);
            if (result.IsCompleted)
            {
                RadioButtonListOperationResult.DataSource = result.Data;
                RadioButtonListOperationResult.DataBind();

                if (selectedID.HasValue)
                {
                    RadioButtonListOperationResult.SelectedValue = selectedID.ToString();
                }
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            RadioButtonListOperationResult.Enabled = isEditable;

        }

        private void BindRadioButtonOperationLevel(Decimal? selectedID, String levelDesc, bool isEditable)
        {
            var result = _listOfValueService.ListActive(Nep.Project.Common.LOVGroup.OperationLevel, selectedID);
            TextBoxOperationLevelDesc.ReadOnly = (!isEditable);
            if (result.IsCompleted)
            {
                List<ServiceModels.ListOfValue> list = result.Data;                
                ServiceModels.ListOfValue level;
                for (int i = 0; i < list.Count; i++)
                {
                    level = list[i];
                    if (i == 0)
                    {
                        RadioButtonOperationLevel_1.Text = level.LovName;
                        RadioButtonOperationLevel_1.Attributes.Add("Value", level.LovID.ToString());
                        RadioButtonOperationLevel_1.Attributes.Add("Data-Code", level.LovCode);
                        if (selectedID.HasValue && (selectedID.Value == level.LovID))
                        {
                            RadioButtonOperationLevel_1.Checked = true;
                            TextBoxOperationLevelDesc.Text = levelDesc;                            
                        }
                    }
                    else if (i == 1)
                    {
                        RadioButtonOperationLevel_2.Text = level.LovName;
                        RadioButtonOperationLevel_2.Attributes.Add("Value", level.LovID.ToString());
                        RadioButtonOperationLevel_2.Attributes.Add("Data-Code", level.LovCode);                        

                        if (selectedID.HasValue && (selectedID.Value == level.LovID))
                        {
                            RadioButtonOperationLevel_2.Checked = true;                            
                        }
                    }
                    else if (i == 2)
                    {
                        RadioButtonOperationLevel_3.Text = level.LovName;
                        RadioButtonOperationLevel_3.Attributes.Add("Value", level.LovID.ToString());
                        RadioButtonOperationLevel_3.Attributes.Add("Data-Code", level.LovCode);
                        if (selectedID.HasValue && (selectedID.Value == level.LovID))
                        {
                            RadioButtonOperationLevel_3.Checked = true;
                        }
                    }
                    else if (i == 3)
                    {
                        RadioButtonOperationLevel_4.Text = level.LovName;
                        RadioButtonOperationLevel_4.Attributes.Add("Value", level.LovID.ToString());
                        RadioButtonOperationLevel_4.Attributes.Add("Data-Code", level.LovCode);
                        if (selectedID.HasValue && (selectedID.Value == level.LovID))
                        {
                            RadioButtonOperationLevel_4.Checked = true;
                        }
                    }
                }
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            RadioButtonOperationLevel_1.Enabled = isEditable;
            RadioButtonOperationLevel_2.Enabled = isEditable;
            RadioButtonOperationLevel_3.Enabled = isEditable;
            RadioButtonOperationLevel_4.Enabled = isEditable;
        }
        private ServiceModels.ProjectInfo.ProjectReportResult GetData()
        {

            ServiceModels.ProjectInfo.ProjectReportResult model = new ServiceModels.ProjectInfo.ProjectReportResult();
            model.ProjectID = ProjectID;
            model.ActivityDescription = TextBoxActivityDescription.Text.TrimEnd();
            model.IPAddress = Request.UserHostAddress;
            var json = Newtonsoft.Json.Linq.JObject.Parse(hdfActOBJ.Value);

            //ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget> act  = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget>>(hdfActOBJ.Value);
            var act = json["data"].ToObject< ServiceModels.ReturnObject<ServiceModels.ProjectInfo.ProjectBudget>>();

            var rep = json.ToObject<ServiceModels.ProjectInfo.ProjectReportScreen>();

            //ServiceModels.ProjectInfo.ProjectReportScreen rep = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceModels.ProjectInfo.ProjectReportScreen>(r);
            //งบประมาณที่ใช้จ่ายจริง
            //if (!String.IsNullOrEmpty(TextBoxActualExpense.Text))
            //{
            //    model.ActualExpense = Convert.ToDecimal(TextBoxActualExpense.Text);
            //}
            model.ActualExpense = rep.GrandTotal.ActualExpense;
            model.Interest = act.Data.Interest;
            var other = _listOfValueService.GetListOfValueByCode(Common.LOVGroup.TargetGroup, Common.LOVCode.Targetgroup.อื่นๆ);
            //ผู้เข้าร่วมโครงการ/กิจกรรม
            int totalMale = 0;
            int totalFemale = 0;
            Int32.TryParse(HiddTotalMaleParticipant.Value, out totalMale);
            Int32.TryParse(HiddTotalFemaleParticipant.Value, out totalFemale);

            var participant = HiddenFieldParticipant.Value;
            if(!String.IsNullOrEmpty(participant)){
                List<ServiceModels.ProjectInfo.ProjectParticipant> list =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServiceModels.ProjectInfo.ProjectParticipant>>(HiddenFieldParticipant.Value);
                foreach(ServiceModels.ProjectInfo.ProjectParticipant p in list)
                {
                    if (int.Parse(p.IsCripple) > 1)
                    {
                        p.TargetGroupID = other.Data.LovID ;
                    }
                }
                model.Participants = list;
                model.MaleParticipant = totalMale;
                model.FemaleParticipant = totalFemale;
                //model = SetParticipantGenderAmount(model, list);
            }       
                        
            //var participants = ViewState[PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY];
            //if (participants != null)
            //{
            //    List<ServiceModels.ProjectInfo.ProjectParticipant> participantList = (List<ServiceModels.ProjectInfo.ProjectParticipant>)participants;
            //    model.Participants = participantList;
            //    model = SetParticipantGenderAmount(model, participantList);
            //}

            //แนบไฟล์
            IEnumerable<ServiceModels.KendoAttachment> addedFiles = FileUploadReportAttachment.AddedFiles;
            IEnumerable<ServiceModels.KendoAttachment> removedFiles = FileUploadReportAttachment.RemovedFiles;
            model.AddedReportAttachment = (addedFiles.Count() > 0) ? addedFiles.First() : null;
            model.RemovedReportAttachment = (removedFiles.Count() > 0) ? removedFiles.First() : null;
            //kenghot18
            model.AddedReportAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            model.RemovedReportAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;

            addedFiles = FileUploadActivityAttachment.AddedFiles;
            removedFiles = FileUploadActivityAttachment.RemovedFiles;
            model.AddedActivityAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            model.RemovedActivityAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;

            addedFiles = FileUploadParticipantAttachment.AddedFiles;
            removedFiles = FileUploadParticipantAttachment.RemovedFiles;
            model.AddedParticipantAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            model.RemovedParticipantAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;


            addedFiles = FileUploadResultAttachment.AddedFiles;
            removedFiles = FileUploadResultAttachment.RemovedFiles;
            model.AddedResultAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            model.RemovedResultAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;
            //end kenghot
            //ผลการดำเนินงาน/ประโยชน์ที่ได้รับจากการดำเนินงาน
            model.Benefit = TextBoxBenefit.Text.TrimEnd();

            //ปัญหาอุปสรรค์และวิธีการแก้ปัญหาจากการดำเนินการ
            model.ProblemsAndObstacle = TextBoxProblemsAndObstacle.Text.TrimEnd();

            //ข้อคิดเห็นและข้อเสนอแนะ
            model.Suggestion = TextBoxSuggestion.Text.TrimEnd();

            //สรุปผลการดำเนินงาน
            if (!String.IsNullOrEmpty(RadioButtonListOperationResult.SelectedValue))
            {
                model.OperationResult = Convert.ToDecimal(RadioButtonListOperationResult.SelectedValue);
            }

            //เปรียบเทียบกับวัตถุประสงค์
            var operationLevels = PanelOperationLevel.Controls.OfType<RadioButton>().Where(x => x.Checked).FirstOrDefault();
            if (operationLevels != null)
            {
                model.OperationLevel = Convert.ToDecimal(operationLevels.Attributes["Value"]);
                model.OperationLevelDesc = TextBoxOperationLevelDesc.Text.TrimEnd();
            }

            //ลงชื่อผู้รายงาน
            model.ReporterName1 = TextBoxReporter1FirstName.Text.Trim();
            model.ReporterLastname1 = TextBoxReporter1LastName.Text.Trim();
            model.Position1 = TextBoxReporter1Position.Text.Trim();
            model.RepotDate1 = DatePickerReporter1.SelectedDate;
            model.Telephone1 = TextBoxReporter1Telephone.Text.Trim();


            if(HasSaveDraftReportResultRole && HasSaveReportResultRole){
                //ข้อคิดเห็นและข้อเสนอแนะ
                model.SuggestionDesc = TextBoxSuggestionDesc.Text.Trim();

                //ลงชื่อผู้รายงาน
                model.ReporterName2 = TextBoxReporter2FirstName.Text.Trim();
                model.ReporterLastname2 = TextBoxReporter2LastName.Text.Trim();
                model.Position2 = TextBoxReporter2Position.Text.Trim();
                model.RepotDate2 = DatePickerReporter2.SelectedDate;
                model.Telephone2 = TextBoxReporter2Telephone.Text.Trim();
            }

           
            model.BudgetDetails = act.Data.BudgetDetails.ToList();
            return model;
        }

        private ServiceModels.ProjectInfo.ProjectReportResult SetParticipantGenderAmount(ServiceModels.ProjectInfo.ProjectReportResult obj, List<ServiceModels.ProjectInfo.ProjectParticipant> participants)
        {
            if(participants != null){
                int mCount = 0;
                int fCount = 0;
                ServiceModels.ProjectInfo.ProjectParticipant participant;
                for (int i = 0; i < participants.Count; i++ )
                {
                    participant = participants[i];
                    if (participant.Gender == GENDER_MALE)
                    {
                        mCount++;
                    }
                    else
                    {
                        fCount++;
                    }
                }

                obj.MaleParticipant = mCount;
                obj.FemaleParticipant = fCount;
            }

            return obj;
        }

        private ServiceModels.ProjectInfo.ProjectReportResult GetOfficerData()
        {
            ServiceModels.ProjectInfo.ProjectReportResult model = new ServiceModels.ProjectInfo.ProjectReportResult();
            model.ProjectID = ProjectID;

            //ข้อคิดเห็นและข้อเสนอแนะ
            model.SuggestionDesc = TextBoxSuggestionDesc.Text.Trim();

            //ลงชื่อผู้รายงาน
            model.ReporterName2 = TextBoxReporter2FirstName.Text.Trim();
            model.ReporterLastname2 = TextBoxReporter2LastName.Text.Trim();
            model.Position2 = TextBoxReporter2Position.Text.Trim();
            model.RepotDate2 = DatePickerReporter2.SelectedDate;
            model.Telephone2 = TextBoxReporter2Telephone.Text.Trim();

            return model;
        }

        private void RegisterClientScript()
        {
            


            String script = @"
                function validateParticipant(oSrc, args) {
                    var participantText = $('#"+ HiddenFieldParticipant.ClientID + @"').val();                 
                    args.IsValid = (participantText != '');
                }

                function validateOperationResult(sender, args) {
                    var checkBoxList = document.getElementById('" + RadioButtonListOperationResult.ClientID + @"');
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

                function validateOperationLevel(oSrc, args) {
                    var checkBoxList = document.getElementById('RadioButtonOperationLevelContainer');              
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

                function handleActualExpense() {
                    var textbox = $('.textbox-actual-expense').get(0);
                    var labelBalance = $('.project-report-balance-amount').get(0);
                    $(textbox).bind('change', function () {
                        var actualAmount = parseFloat($(this).val());
                        var budget = $('.revise-budget-amount').text();
                        var balanceAmont = 0;
                        actualAmount = parseFloat(actualAmount);
                        budget = parseFloat(budget.replace(/,/g, ''));
                        balanceAmont = budget - actualAmount;
                        balanceAmont = (balanceAmont > 0) ? balanceAmont : 0;
                        balanceAmont = balanceAmont.format('n2');
                        $('.project-report-balance-amount').text(balanceAmont);                    
                    });
                }

                function onActualExpenseChage(el) {
                    var actualAmount = parseFloat($(el).val());
                    var budget = $('.revise-budget-amount').text();
                    var balanceAmont = 0;
                    actualAmount = parseFloat(actualAmount);
                    budget = parseFloat(budget.replace(/,/g, ''));
                    balanceAmont = budget - actualAmount;
                    balanceAmont = (balanceAmont > 0) ? balanceAmont : 0;                    
                    balanceAmont = balanceAmont.format('n2');
                    $('.project-report-balance-amount').val(balanceAmont);
                }
            ";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelReportResult,
                       this.GetType(),
                       "ManageReportResultScript",
                       script,
                       true);
        }

        #endregion Private Method

        protected void GridViewParticipant_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(!IsEditable){
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
            }
        }
    }
}