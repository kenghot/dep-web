﻿using AjaxControlToolkit;
using Nep.Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabFollowUnder5MControl : Nep.Project.Web.Infra.BaseUserControl
    {
        private string PROJECTPARTICIPANT_LIST_VIEWSTATE_KEY = "PROJECTPARTICIPANT_LIST";
        private const String GENDER_MALE = "M";
        private const String GENDER_FEMALE = "F";
        private string QuestionareGroup = "FLUN5M";
        private string TableAttach = "FOLLOW";
        private string FieldAttach = "FOLLOW_UNDER5M";
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

            string scriptTag = Business.QuestionareHelper.CommonScript(UpdatePanelReportResult.ClientID, hdfQViewModel.ClientID, "");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "QuestionareCommon" + this.ClientID, scriptTag, true);
            //#region Project Participant
            //List<ServiceModels.GenericDropDownListData> genderList = new List<ServiceModels.GenericDropDownListData>();
            //genderList.Add(new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.LabelMale, Value = GENDER_MALE });
            //genderList.Add(new ServiceModels.GenericDropDownListData { Text = Nep.Project.Resources.UI.LabelFemale, Value = GENDER_FEMALE });



            //bool isRequiredData = ((ContractYear != null) && (ContractYear > 2016));
            //var md = new ServiceModels.ProjectInfo.ProjectParticipant();

            ////String scriptUrl = ResolveUrl("~/Scripts/manage.projectreport.js?v=" + DateTime.Now.Ticks.ToString());
            //String scriptUrl = ResolveUrl("~/Scripts/manage.projectreport.js?v=5");
            //var refScript = "<script type='text/javascript' src='" + scriptUrl + "'></script>";
            //ScriptManager.RegisterClientScriptBlock(
            //           UpdatePanelReportResult,
            //           this.GetType(),
            //           "RefUpdatePanelReportResultScript",
            //           refScript,
            //           false);


            //ScriptManager.RegisterStartupScript(
            //          UpdatePanelReportResult,
            //          this.GetType(),
            //          "UpdatePanelProjectInfoScript",
            //          participantScript,
            //          true);
            //#endregion Project Participant
        }
        //public void RegistQNScript()
        //{
        //    string script = @"var QuestionareBindingControls = [];
        //        $('[data-bind]').attr('data-bind',function(a,b) {QuestionareBindingControls.push(b.split(':')[1].trim());});" +
        //      string.Format("$('#{0}').val(QuestionareBindingControls);", hdfQViewModel.ClientID);
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Questionare" + this.ClientID,
        //       script, true);
        //}
        public void BindData()
        {
            bool isEditable = false;
            bool isReadOnly = false;

           // var result = _projectService.GetProjectReportResult(ProjectID);
            var db = _projectService.GetDB();
            //var qnh = (from q in db.PROJECTQUESTIONHDs where q.PROJECTID == ProjectID && q.QUESTGROUP == QuestionareGroup select q).FirstOrDefault();
            var oper = (from op in db.ProjectOperations where op.ProjectID == ProjectID select op).FirstOrDefault();
            var con = (from c in db.ProjectContracts where c.ProjectID == ProjectID select c).FirstOrDefault();
            var gen = (from i in db.ProjectGeneralInfoes where i.ProjectID == ProjectID select i).FirstOrDefault();
            
            //var attach = new Business.AttachmentService(db);
            //var f = attach.GetAttachmentOfTable("FOLLOWU5", "FOLLOWU5", ProjectID);
            //FileUploadAttachment.ClearChanges();
            //FileUploadAttachment.ExistingFiles = f;
            //FileUploadAttachment.DataBind();
            //var morejs = string.Format("var nT_1_2 = $('#{0}').kendoNumericTextBox({{format:'n0',decimals:0,min:0,max:120}});", T_1_2.ClientID);
            //string script = Business.QuestionareHelper.QuestionareJS(ProjectID, QuestionareGroup, UpdatePanelReportResult.ClientID, hdfIsDisable.ClientID, "","");

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Questionare" + this.ClientID,
            //   script, true);
            string script = "";
            script = @"<script type=""text/javascript"" src=""../../Scripts/Vue/FollowUpUnder5M.js?v=" + DateTime.Now.Ticks.ToString() + @"""></script>";
            script += @"<script type=""text/javascript"" src=""../../Scripts/Vue/VueQN.js?v=" + DateTime.Now.Ticks.ToString() + @"""></script>";
            script += @"<script type=""text/javascript"" src=""../../Scripts/file-upload-helper.js?v=" + DateTime.Now.Ticks.ToString() + @"""></script>";


            var qnh = (from q in db.PROJECTQUESTIONHDs where q.PROJECTID == ProjectID && q.QUESTGROUP == QuestionareGroup select q).FirstOrDefault();


                bool isReported = false; // (model.FollowupStatusCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว);
                if (qnh != null)
                {
                    isReported = (qnh.ISREPORTED == "1") ? true : false;
                }
            var setVueParam = @"//$( document ).ready(function() { 
                               // console.log('set');
                                appVueQN.param.projID = " + ProjectID.ToString() + @";
                                appVueQN.param.qnGroup = '" + QuestionareGroup + @"';
                                appVueQN.param.IsReported = '" + ((isReported) ? "1" : "0") + @"';
                                //console.log(appVueQN.param);
                                Vue.prototype.$CalulateFollowUnder5M = CalulateFollowUnder5M;
                                appVueQN.getData();
                                
                                //});  
                                ";
            if (gen != null)
            {
                setVueParam += string.Format("Vue.set(appVueQN.extend,'projectTHName','{0}');", gen.ProjectInformation.ProjectNameTH);
                setVueParam += string.Format("Vue.set(appVueQN.extend,'budgetRate','{0}');", (gen.BudgetReviseValue > 5000000) ? "1" : "2");
                setVueParam += string.Format("Vue.set(appVueQN.extend,'name1','{0}');", $"{gen.ProjectPersonel.Firstname1} {gen.ProjectPersonel.Lastname1}");
                setVueParam += string.Format("Vue.set(appVueQN.extend,'name2','{0}');", $"{gen.ProjectPersonel.Firstname2} {gen.ProjectPersonel.Lastname2}");
                setVueParam += string.Format("Vue.set(appVueQN.extend,'tel1','{0}');", gen.ProjectPersonel.Telephone1);
                setVueParam += string.Format("Vue.set(appVueQN.extend,'tel2','{0}');", gen.ProjectPersonel.Telephone2);
                setVueParam += string.Format("Vue.set(appVueQN.extend,'approvedBudget','{0:##,#0.#0}');", gen.BudgetReviseValue);
            }
          
            if (con != null)
            {
                setVueParam += string.Format("Vue.set(appVueQN.extend,'organization','{0}');", con.ProjectGeneralInfo.OrganizationNameTH);
                setVueParam += string.Format("Vue.set(appVueQN.extend,'contractEndDate','{0:dd/MM/yyyy}');", con.ContractEndDate);
                setVueParam += string.Format("Vue.set(appVueQN.extend,'contractStartDate','{0:dd/MM/yyyy}');", con.ContractStartDate);
            }
            if (oper != null)
            {
                setVueParam += string.Format("Vue.set(appVueQN.extend,'startDate','{0:dd/MM/yyyy}');", oper.StartDate);
            }
            setVueParam += @"          
                function SaveAttachmentFiles() {
                   SaveAttachmentToDB(" + ProjectID.ToString() + "," + FileUploadAttachment.Controls[1].ClientID + "," + FileUploadAttachment.Controls[0].ClientID + @", '" + TableAttach + @"', '" + FieldAttach + @"');
                   
                 };
              ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "QuestionareJSFile", script, false);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "QNInitialData" + this.ClientID,
                   setVueParam, true);
            List<Common.ProjectFunction> functions = _projectService.GetProjectFunction(ProjectID).Data;
                
                HasSaveDraftReportResultRole = functions.Contains(Common.ProjectFunction.SaveDraftReportResult);
                HasSaveReportResultRole = functions.Contains(Common.ProjectFunction.SaveReportResult);
                
                isEditable = (HasSaveDraftReportResultRole &&  !isReported) || UserInfo.IsAdministrator;
                IsEditable = isEditable;
                isReadOnly = (!isEditable);

                hdfIsDisable.Value = (isEditable) ? "false" : "true" ;
                //CreateParticipantForm.Visible = !isReported;

                ButtonSaveReportResult.Visible = isEditable;
                //ButtonAddParticipant.Visible = isEditable;
                ButtonSaveAndSendProjectReport.Visible = isEditable;
                //ButtonOfficerSave.Visible = (HasSaveReportResultRole && (!HasSaveDraftReportResultRole));
                //HyperLinkPrint.Visible = functions.Contains(Common.ProjectFunction.PrintReport);

                ButtonSaveAndSendProjectReport.Text = (UserInfo.OrganizationID.HasValue) ? Nep.Project.Resources.UI.ButtonSendProjectReport : Nep.Project.Resources.UI.ButtonConfirmReportResult;

                //attachfile
                var att = new Business.AttachmentService(_projectService.GetDB());

                FileUploadAttachment.ClearChanges();
                FileUploadAttachment.ExistingFiles = att.GetAttachmentOfTable(TableAttach, FieldAttach, ProjectID);
                FileUploadAttachment.DataBind();
                //งบประมาณโครงการ
                //decimal reviseBudgetAmount = (model.ReviseBudgetAmount.HasValue) ? model.ReviseBudgetAmount.Value : 0;
                //decimal actualExpense = (model.ActualExpense.HasValue) ? model.ActualExpense.Value : 0;
                //decimal balanceAmount = reviseBudgetAmount - actualExpense;
                //balanceAmount = (balanceAmount < 0) ? 0 : balanceAmount;


                //Check Is submit data
                decimal? userProvinceID = UserInfo.ProvinceID;
                decimal? userOrganizationID = UserInfo.OrganizationID;

                //if (String.IsNullOrEmpty(model.FollowupStatusCode) ||
                //    (!String.IsNullOrEmpty(model.FollowupStatusCode) && (model.FollowupStatusCode != Common.LOVCode.Followupstatus.รายงานผลแล้ว)))
                //{
                //    if (
                //        (!UserInfo.IsAdministrator) &&
                //        ((model.CreatorOrganizationID.HasValue && userOrganizationID.HasValue && (model.CreatorOrganizationID != userOrganizationID)) ||
                //        (model.CreatorOrganizationID.HasValue && userOrganizationID == null) ||
                //        (model.CreatorOrganizationID == null && userOrganizationID == null && model.ProvinceID.HasValue && userProvinceID.HasValue && (model.ProvinceID != userProvinceID)))
                //        )
                //    {
              
                //        return;   
                //    }
                //}

                //กิจกรรมของโครงการ
                //TextBoxActivityDescription.Text = model.ActivityDescription;
                //TextBoxActivityDescription.ReadOnly = isReadOnly;

                

                //ผลการดำเนินงาน/ประโยชน์ที่ได้รับจากการดำเนินงาน
  
              

                ////ลงชื่อผู้รายงาน
                //TextBoxReporter1FirstName.Text = model.ReporterName1;
                //TextBoxReporter1FirstName.Enabled = isEditable;

                //TextBoxReporter1LastName.Text = model.ReporterLastname1;
                //TextBoxReporter1LastName.Enabled = isEditable;

                //DatePickerReporter1.SelectedDate = model.RepotDate1;
                //DatePickerReporter1.Enabled = isEditable;

                //TextBoxReporter1Position.Text = model.Position1;
                //TextBoxReporter1Position.Enabled = isEditable;

                //TextBoxReporter1Telephone.Text = model.Telephone1;
                //TextBoxReporter1Telephone.Enabled = isEditable;

               



                //PanelSuggestionDesc.Visible = (functions.Contains(Common.ProjectFunction.PrintReport) && (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer));
                //PanelSuggestionDesc.Visible = ((HasSaveDraftReportResultRole && HasSaveReportResultRole) || (functions.Contains(Common.ProjectFunction.PrintReport) && (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer)) || (model.FollowupStatusCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว));
                bool canSaveReportResult = (functions.Contains(Common.ProjectFunction.SaveReportResult)); 
                //ลงชื่อเจ้าหน้าที่ผู้รายงาน
                //TextBoxSuggestionDesc.Text = model.SuggestionDesc;
                //TextBoxSuggestionDesc.ReadOnly = !canSaveReportResult;

                //TextBoxReporter2FirstName.Text = model.ReporterName2; 
                //TextBoxReporter2FirstName.Enabled = canSaveReportResult;

                //TextBoxReporter2LastName.Text = model.ReporterLastname2;
                //TextBoxReporter2LastName.Enabled = canSaveReportResult;

                //DatePickerReporter2.SelectedDate = model.RepotDate2;
                //DatePickerReporter2.Enabled = canSaveReportResult;

                //TextBoxReporter2Position.Text = model.Position2;
                //TextBoxReporter2Position.Enabled = canSaveReportResult;

                //TextBoxReporter2Telephone.Text = model.Telephone2;
                //TextBoxReporter2Telephone.Enabled = canSaveReportResult;     
           
               
                    
               
         
        }


        #region Main Button
        protected void ButtonSaveReportResult_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    bool isSaveReportResult = (HasSaveReportResultRole && HasSaveReportResultRole);
                    bool isSendReport = false;
                    if (sender == ButtonSaveAndSendProjectReport) isSendReport = true;
                    var result = _projectService.SaveProjectQuestionareResult(ProjectID, QuestionareGroup, hdfQViewModel.Value, isSaveReportResult, isSendReport,Request.UserHostAddress);
                    var resultAttach = _projectService.SaveAttachFile(ProjectID, Common.LOVCode.Attachmenttype.PROJECT_FOLLOWUP, FileUploadAttachment.RemovedFiles.ToList(),
                    FileUploadAttachment.AddedFiles.ToList(), TableAttach, FieldAttach);
                    if (result.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelFollowup");
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
       
        //protected void ButtonSaveAndSendProjectReport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Page.IsValid)
        //        {
        //            bool isSaveReportResult = (HasSaveReportResultRole && HasSaveReportResultRole);
        //            var result = _projectService.SaveProjectReportResult(GetData(), isSaveReportResult, true);
        //            if (result.IsCompleted)
        //            {
        //                Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
        //                page.RebindData("TabPanelReportResult");
        //                string message = (HasSaveDraftReportResultRole && HasSaveReportResultRole) ? Nep.Project.Resources.Message.SendProjectReportMessage : Nep.Project.Resources.Message.SubmitProjectReportMessage;
        //                ShowResultMessage(result.Message);    
        //            }
        //            else
        //            {
        //                ShowErrorMessage(result.Message);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
        //        ShowErrorMessage(ex.Message);
        //    }
        //}


        #endregion Main Button
        

        #region Manage GridView
   
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
       


        protected void CustomValidatorParticipant_ServerValidate(object source, ServerValidateEventArgs args)

        { 

            //String participantText = HiddenFieldParticipant.Value;
            //args.IsValid = (participantText != "");

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

 

        #endregion Manage GridView

        #region CustomValidator
        
     
        //protected void CustomValidatorOperationLevelDesc_ServerValidate(object source, ServerValidateEventArgs args)
        //{            
        //    String selectedCode;
        //    String error = "";
           
        //    var operationLevels = PanelOperationLevel.Controls.OfType<RadioButton>().Where(x => x.Checked).FirstOrDefault();
        //    if (operationLevels != null)
        //    {
        //        selectedCode = operationLevels.Attributes["Data-Code"];
        //        String textDesc = TextBoxOperationLevelDesc.Text;
        //        if ((selectedCode == Common.LOVCode.Operationlevel.สูงกว่าเป้าหมาย_เพราะ) && (String.IsNullOrEmpty(textDesc)))
        //        {
        //            error = String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectReportResult_OperationLevelDesc);
        //        }
        //        else if ((selectedCode != Common.LOVCode.Operationlevel.สูงกว่าเป้าหมาย_เพราะ) && (!String.IsNullOrEmpty(textDesc)))
        //        {
        //            String fieldDesc = RadioButtonOperationLevel_1.Text;
        //            error = String.Format(Nep.Project.Resources.Error.ValidateOperationLevelDesc, fieldDesc);
        //        }
                
        //    }

        //    CustomValidator validator = source as CustomValidator;
        //    validator.Text = error;
        //    validator.ErrorMessage = error;

        //    args.IsValid =String.IsNullOrEmpty(error);
        //}

        //protected void CustomValidatorReportAttachment_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    String textReviseBudgetAmount = LabelReviseBudgetAmount.Text;
        //    String textActualExpense = TextBoxActualExpense.Text;
        //    decimal reviseBudgetAmount = 0;
        //    decimal actualExpense = 0;

        //    Decimal.TryParse(textReviseBudgetAmount.Replace(",", ""), out reviseBudgetAmount);
        //    Decimal.TryParse(textActualExpense, out actualExpense);
        //    decimal totalBalance =  reviseBudgetAmount - actualExpense;

        //    IEnumerable<ServiceModels.KendoAttachment> files = FileUploadReportAttachment.AllFiles;
        //    if ((totalBalance > 0) && ((files == null) || (files.Count() == 0)))
        //    {
        //        args.IsValid = false;
        //    }
        //}

        #endregion CustomValidator

        #region Private Method
        



        private ServiceModels.ProjectInfo.ProjectReportResult GetData()
        {
            return null;
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


        //private void RegisterClientScript()
        //{
            


        //    String script = @"
        //        function validateParticipant(oSrc, args) {
        //            var participantText = $('#"+ HiddenFieldParticipant.ClientID + @"').val();                 
        //            args.IsValid = (participantText != '');
        //        }

        //        function validateOperationResult(sender, args) {
        //            var checkBoxList = document.getElementById('" + RadioButtonListOperationResult.ClientID + @"');
        //            var checkboxes = checkBoxList.getElementsByTagName('input');
        //            var isValid = false;
        //            for (var i = 0; i < checkboxes.length; i++) {
        //                if (checkboxes[i].checked) {
        //                    isValid = true;
        //                    break;
        //                }
        //            }
        //            args.IsValid = isValid;
        //        }

        //        function validateOperationLevel(oSrc, args) {
        //            var checkBoxList = document.getElementById('RadioButtonOperationLevelContainer');              
        //            var checkboxes = checkBoxList.getElementsByTagName('input');
        //            var isValid = false;
        //            for (var i = 0; i < checkboxes.length; i++) {
        //                if (checkboxes[i].checked) {
        //                    isValid = true;
        //                    break;
        //                }
        //            }
        //            args.IsValid = isValid;
        //        }

        //        function handleActualExpense() {
        //            var textbox = $('.textbox-actual-expense').get(0);
        //            var labelBalance = $('.project-report-balance-amount').get(0);
        //            $(textbox).bind('change', function () {
        //                var actualAmount = parseFloat($(this).val());
        //                var budget = $('.revise-budget-amount').text();
        //                var balanceAmont = 0;
        //                actualAmount = parseFloat(actualAmount);
        //                budget = parseFloat(budget.replace(/,/g, ''));
        //                balanceAmont = budget - actualAmount;
        //                balanceAmont = (balanceAmont > 0) ? balanceAmont : 0;
        //                balanceAmont = balanceAmont.format('n2');
        //                $('.project-report-balance-amount').text(balanceAmont);                    
        //            });
        //        }

        //        function onActualExpenseChage(el) {
        //            var actualAmount = parseFloat($(el).val());
        //            var budget = $('.revise-budget-amount').text();
        //            var balanceAmont = 0;
        //            actualAmount = parseFloat(actualAmount);
        //            budget = parseFloat(budget.replace(/,/g, ''));
        //            balanceAmont = budget - actualAmount;
        //            balanceAmont = (balanceAmont > 0) ? balanceAmont : 0;                    
        //            balanceAmont = balanceAmont.format('n2');
        //            $('.project-report-balance-amount').val(balanceAmont);
        //        }
        //    ";
        //    ScriptManager.RegisterClientScriptBlock(
        //               UpdatePanelReportResult,
        //               this.GetType(),
        //               "ManageReportResultScript",
        //               script,
        //               true);
        //}

        #endregion Private Method


    }
}