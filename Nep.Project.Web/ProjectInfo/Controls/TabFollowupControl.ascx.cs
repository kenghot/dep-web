using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    
    public partial class TabFollowupControl : Nep.Project.Web.Infra.BaseUserControl
    {
       
        public IServices.IProjectInfoService _service { get; set; }
        public IServices.IListOfValueService _lovService { get; set; }
        public IServices.IProviceService _provService { get; set; }
        public IServices.IOrganizationParameterService _paramService { get; set; }

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
                ViewState["FollowupViewAttachmentPrefix"] = value;
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

        public Boolean IsEditableFolloup
        {
            get
            {
                bool isEdit = true;
                if (ViewState["IsEditableFolloup"] != null)
                {
                    isEdit = Convert.ToBoolean(ViewState["IsEditableFolloup"]);
                }

                return isEdit;
            }

            set
            {
                ViewState["IsEditableFolloup"] = value;
            }
        }
               
        public DateTime? FirstDeadlineDate
        {
            get
            {
                DateTime? date = (DateTime?)null;
                if (ViewState["FirstDeadlineDate"] != null)
                {
                    date = Convert.ToDateTime(ViewState["FirstDeadlineDate"]);
                }
                return date;
            }

            set
            {
                ViewState["FirstDeadlineDate"] = value;
            }
        }

        public Boolean IsShowPrintingOpt
        {
            get
            {
                bool isShow = true;
                if (ViewState["IsShowPrintingOpt"] != null)
                {
                    isShow = Convert.ToBoolean(ViewState["IsShowPrintingOpt"]);
                }

                return isShow;
            }

            set
            {
                ViewState["IsShowPrintingOpt"] = value;
            }
        }

        public Boolean IsPrintTracking {
            get
            {
                bool canPrint = true;
                if (ViewState["IsPrintTracking"] != null)
                {
                    canPrint = Convert.ToBoolean(ViewState["IsPrintTracking"]);
                }

                return canPrint;
            }

            set
            {
                ViewState["IsPrintTracking"] = value;
            }
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            string parameter = Request["__EVENTARGUMENT"];
            if (parameter != null && (parameter == "updated"))
            {
                ShowResultMessage(Nep.Project.Resources.Message.SaveSuccess);
            } else if (parameter != null && (parameter == "deleted")) {
                ShowResultMessage(Nep.Project.Resources.Message.DeleteSuccess);
            }

            Dictionary<String, String> followupScoreDesc = new Dictionary<string, string>();
            followupScoreDesc.Add("S96", Nep.Project.Resources.UI.FollowupScoreDesc_96_100);
            followupScoreDesc.Add("S91", Nep.Project.Resources.UI.FollowupScoreDesc_91_95);
            followupScoreDesc.Add("S81", Nep.Project.Resources.UI.FollowupScoreDesc_81_90);
            followupScoreDesc.Add("S70", Nep.Project.Resources.UI.FollowupScoreDesc_70_80);
            followupScoreDesc.Add("S0", Nep.Project.Resources.UI.FollowupScoreDesc_0_69);

            String printingFormUrl = ResolveUrl("~/ProjectInfo/FollowupPrintingPopup");
            printingFormUrl = String.Format("{0}?projectid={1}&isshowopt={2}", printingFormUrl, ProjectID, IsShowPrintingOpt);

            String attachUrl = ResolveUrl("~/AttachmentHandler/View/Project/" + ProjectID + "/");

            String scriptTag = @"


                    function openPrintingForm(reportTrackingID, canPrinting) {
                        var pageUrl = '" + printingFormUrl + @"';
                        pageUrl = (typeof(reportTrackingID) != 'undefined')? (pageUrl + '&trackingid=' + reportTrackingID): pageUrl;
                        pageUrl = (typeof(canPrinting) != 'undefined')? (pageUrl + '&canprinting=' + canPrinting) : pageUrl;
                        
                        c2x.openFormDialog(pageUrl, '" + Nep.Project.Resources.UI.TitleLetterTrackingForm + @"', { width: 790, height: 420 }, null);    
                        return false;
                    }

                    function openPrintingAttachment(attachmentID, attachmentName) {
                        var attachUrl = '" + attachUrl + @"';
                        attachUrl += attachmentID + '/' + attachmentName;
                        
                        window.open(attachUrl, '_blank');                        
                        return false;
                    }

                    function reloadGridViewPrintedDocument(param) {    
                        var p = (typeof(param) != 'undefined')? param : '';                        
                                         
                       __doPostBack('" + UpdatePanelPrintedDocument.UniqueID + @"', p);
               
                    } 

                    function ConfirmDeleteTrackingDoc() {
                            var message = " + Nep.Project.Common.Web.WebUtility.ToJSON(Nep.Project.Resources.Message.DeleteConfirmation) + @";
                            var isConfirm = window.confirm(message);
                            return isConfirm;
                        }    

                    function handleDdlTracking() {
                        var ddl = $('.tracking-dropdownlist');
                        if ((ddl != null) && (ddl.length > 0)) {
                            for (var i = 0; i < ddl.length; i++) {
                                $(ddl[i]).change(function () { calTrackingScore() });
                            }
                        }
                    } 

                    function calTrackingScore() {
                        var trackingScore = " + Nep.Project.Common.Web.WebUtility.ToJSON(followupScoreDesc) + @";
                        var total = 0, score = 0, selectedValue = '';
                        var ddl = $('.tracking-dropdownlist');
                        var scoreDescLabel = $('.followup-score-desc').get(0);
                        var scoreDesc = '';

                        if ((ddl != null) && (ddl.length > 0)) {
                            for (var i = 0; i < ddl.length; i++) {
                                selectedValue = $(ddl[i]).val();
                                selectedValue = (selectedValue == '')? 0 : selectedValue;
                                score = parseInt(selectedValue, 10);
                                total += score
                            }
                            var scoreLabel = $('.followup-score').get(0);
                            $(scoreLabel).text(total);

                            if(total >= 96){
                                scoreDesc = trackingScore.S96;
                            }else if(total >= 91){
                                scoreDesc = trackingScore.S91;
                            }else if(total >= 91){
                                scoreDesc = trackingScore.S91;
                            }else if(total >= 81){
                                scoreDesc = trackingScore.S81;
                            }else if(total >= 70){
                                scoreDesc = trackingScore.S70;
                            }else{
                                scoreDesc = trackingScore.S0;
                            }
                           
                            $(scoreDescLabel).text(scoreDesc);
                        }
                    }              
                    ";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openPrintingForm", scriptTag, true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HandleFollowupTrackingScore" + this.ClientID, @"
                $(function () {
                        handleDdlTracking();                 
                });", true);

            //String script = @"
            //    function onProcessingPlanDateSelectionChanged(sender, args) {
            //        var planStartDatePicker = $find('ProcessingPlanStartDate');
            //        var planEndDatePicker = $find('ProcessingPlanEndDate');

            //        var planStartDate = planStartDatePicker.get_selectedDate();
                    

            //        var planEndDate = planEndDatePicker.get_selectedDate();  
                    

                   
            //        var diff = 0;                   
            //        if ((planStartDate != null) && (planEndDate != null)) {
            //            planStartDate = new Date(planStartDate.getFullYear(), planStartDate.getMonth(), planStartDate.getDate(),0,0,0,0);
            //            planEndDate = new Date(planEndDate.getFullYear(), planEndDate.getMonth(), planEndDate.getDate(),0,0,0,0);

            //            diff = Math.floor((planEndDate - planStartDate) / 86400000);
            //            diff = diff + 1;
            //        }

            //        $('.total-processing-period').val((diff > 0) ? diff : '');
                
            //    }

                
            //";
            //ScriptManager.RegisterClientScriptBlock(
            //           this,
            //           this.GetType(),
            //           "ProcessingPlanDateSelectionChangedScript",
            //           script,
            //           true);

            string cal = @"  
             var nActivityScore, nParticipantScore, nPeriodScore, nActivity1, nActivity2;
            function CalculateScore()
            { var o,r,t;
              o = $( ""input[name$= 'Objective2']:checked"" ).val();
              r = $( ""input[name$= 'Result2']:checked"" ).val();
              t = $( ""input[name$= 'Target2']:checked"" ).val();
              var nObjective2 = (o==null ? 0 : Number( o ));
              var nResult2 = (r==null ? 0 : Number( r ));
              var nTarget2 =(t==null ? 0 : Number( t ));
              var nTotalScore2 = nObjective2 + nResult2 + nTarget2;
              var nTotalPercent2 = parseFloat( (nTotalScore2 / 50) * 100 ).toFixed(2);
              var nTotalScore1 = Number(nParticipantScore.val()) + Number(nActivityScore.val()) + Number(nPeriodScore.val());
              var nTotalPercent1 = parseFloat( (nTotalScore1 / 50) * 100).toFixed(2);
              $( ""span[id$= 'lblObjectiveScore']"" ).text(nObjective2);
              $( ""span[id$= 'lblResultScore']"" ).text(nResult2);
              $( ""span[id$= 'lblTargetScore']"" ).text(nTarget2);
              $( ""span[id$= 'lblTotalScore2']"" ).text(nTotalScore2);
              $( ""span[id$= 'lblTotalPercent2']"" ).text(nTotalPercent2);
              $( ""span[id$= 'lblToatalScorePart2']"" ).text(nTotalScore2);

              $( ""span[id$= 'lblTotalScore1']"" ).text(nTotalScore1);
              $( ""span[id$= 'lblTotalPercent1']"" ).text(nTotalPercent1);
              $( ""span[id$= 'lblToatalScorePart1']"" ).text(nTotalScore1);

              var nTotalScore = nTotalScore1 + nTotalScore2;
              var nTotalPercent  = parseFloat( (nTotalScore  / 100) * 100).toFixed(2);
              $( ""span[id$= 'lblTotalScore']"" ).text(nTotalScore);
               $( ""span[id$= 'lblTotalPercent']"" ).text(nTotalPercent);
              var h = $( ""input[name$= 'HiddenValue']"");
              var hValue = '{OBJECTIVESCORE:'  + nObjective2 + ', RESULTSCORE:'+ nResult2 + ',TARGETSCORE:' + nTarget2 +
                           ',TOTALPERCENT:' + nTotalPercent +',TOTALPERCENT1:' +nTotalPercent1 + ',TOTALPERCENT2:'+nTotalPercent2 +
                           ',TOTALTALSCORE:' + nTotalScore + ',TOTALSCORE1:'+nTotalScore1+',TOTALSCORE2:'+nTotalScore2+'}';
              h.val(hValue);



            } ";
            string kendoNum = @"$(document).ready(function() {" +
                string.Format("nActivityScore = $('#{0}').kendoNumericTextBox({{format:'n0',decimals:0,min:0,max:20,change: CalculateScore}});", txbActivityScore.ClientID) +
                string.Format("nParticipantScore = $('#{0}').kendoNumericTextBox({{format:'n0',decimals:0,min:0,max:20,change: CalculateScore}});", txbParticipantScore.ClientID) +
                string.Format("nPeriodScore = $('#{0}').kendoNumericTextBox({{format:'n0',decimals:0,min:0,max:10,change: CalculateScore}});", txbPeriodScore.ClientID) +
                 string.Format("nActivity1 = $('#{0}').kendoNumericTextBox({{format:'n0',decimals:0,min:0,max:10,change: CalculateScore}});", txbActivity1.ClientID) +
                 string.Format("nActivity2 = $('#{0}').kendoNumericTextBox({{format:'n0',decimals:0,min:0,max:10,change: CalculateScore}});", txbActivity2.ClientID)
                + "$('input[type=radio]').change(function(){ CalculateScore(); })" +
                @" 
                CalculateScore();})
                "

                ;
   
               
           
          ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "KendoNumeric" + this.ClientID, cal+ kendoNum, true);

        }
     
        protected void ButtonSaveFollowup_Click(object sender, EventArgs e)
        {           

            if (Page.IsValid)
            {
                ServiceModels.ProjectInfo.ProjectFollowup model = GetData();
              

                var result = _service.SaveProjectFollowup(model);
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

        protected void GridViewPrintedDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String key = e.CommandArgument.ToString();
            decimal trackingID = 0;
            Decimal.TryParse(key, out trackingID);

            if ((e.CommandName == "del") && (trackingID > 0))
            {
                var result = _service.DeleteFollowupTrackingDocumentForm(trackingID);
                if (result.IsCompleted)
                {
                    GridViewPrintedDocument.DataBind();
                    ShowResultMessage(result.Message);
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }
        }

        public List<ServiceModels.ProjectInfo.FollowupTrackingDocumentList> GridViewPrintedDocument_GetData(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {
            var result = _service.GetListFollowupTrackingDocumentList(GridViewPrintedDocument.QueryParameter);
            List<ServiceModels.ProjectInfo.FollowupTrackingDocumentList> data = new List<ServiceModels.ProjectInfo.FollowupTrackingDocumentList>();
            totalRowCount = 0;
            if (result.IsCompleted)
            {
                data = Reorder(startRowIndex, result.Data);
                totalRowCount = result.TotalRow;
                GridViewPrintedDocument.TotalRows = result.TotalRow;                
            }
            else
            {
                ShowErrorMessage(result.Message[0]);
            }

            return data;
        }

        private List<ServiceModels.ProjectInfo.FollowupTrackingDocumentList> Reorder(int startRowIndex, List<ServiceModels.ProjectInfo.FollowupTrackingDocumentList> list)
        {
            if(list != null){
                int row = startRowIndex;
                for (int i = 0; i < list.Count; i++)
                {
                    row++;
                    list[i].No = row;
                }
            }
            return list;
        }

        public  void BindData()
        {
            PrepareDropdownList();
            if (ProjectID > 0)
            {
                var result = _service.GetProjectFollowup(ProjectID);
                var praram = _paramService.GetOrganizationParameter();
                if (result.IsCompleted)
                {
                    ServiceModels.ProjectInfo.ProjectFollowup followup = result.Data;
                    decimal summaryAssessmentScore = 0;
                    decimal summaryFollowupScore = 0;

                    if (followup != null)
                    {
                        decimal centerProvinceID = _provService.GetCenterProvinceID().Data;
                        decimal? projectProvinceID = followup.ProvinceID;
                        bool isReported = (followup.FollowupStatusCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว);
                        List<Common.ProjectFunction> functions = _service.GetProjectFunction(followup.ProjectID).Data;
                        bool isEditable = functions.Contains(Common.ProjectFunction.SaveTrackingResult);
                        bool isPrintTracking = functions.Contains(Common.ProjectFunction.PrintTrackingDocument);
                        IsPrintTracking = isPrintTracking;
                        ButtonSaveFollowup.Visible = isEditable;// || UserInfo.IsAdministrator;
                        ButtonFollowupPrint.Visible = isPrintTracking;
                        C2XFileUploadProjectFollowupAttachment.Enabled = isEditable;
                                             
                                               
                        #region Popup Printing
                        
                        decimal? userProviceID = UserInfo.ProvinceID;
                        if (isPrintTracking && ((userProviceID.HasValue) && (projectProvinceID.HasValue) && (userProviceID == projectProvinceID))
                            || (projectProvinceID == centerProvinceID))
                        {                            
                            IsShowPrintingOpt = false;
                        }
                        else if (isPrintTracking && UserInfo.IsCenterOfficer && (followup.ProvinceAbbr != praram.Data.CenterProvinceAbbr))
                        {                            
                            IsShowPrintingOpt = true;
                        }
                        #endregion Popup Printing
                     


                        LabelApprovalBudget.Text = String.Format(Nep.Project.Resources.UI.LabelFollwupBudget,
                            Common.Web.WebUtility.DisplayInHtml(followup.ApprovalBudget, "N2", "0.00"));
                        //1.ชื่อโครงการ
                        summaryAssessmentScore += (followup.Assessment61.HasValue) ? (decimal)followup.Assessment61 : 0;
                        summaryFollowupScore += (followup.ProjectFollowupValue.HasValue) ? (decimal)followup.ProjectFollowupValue : 0;
                        TextBoxAssessmentProjectName.Text = followup.ProjectName;
                        LabelAssessmentProjectNameScore.Text = Common.Web.WebUtility.DisplayInHtml(followup.Assessment61, "N0", "-");
                       // DropDownListAssessmentProjectNameScore.SelectedValue = followup.ProjectFollowupValue.ToString();

                        //2.หลักการและเหตุผล
                        summaryAssessmentScore += (followup.Assessment62.HasValue) ? (decimal)followup.Assessment62 : 0;
                        summaryFollowupScore += (followup.ReasonFollowupValue.HasValue) ? (decimal)followup.ReasonFollowupValue : 0;
                        TextBoxAssessmentPrinciples.Text = followup.Reason;
                        LabelAssessmentPrinciplesScore.Text = Common.Web.WebUtility.DisplayInHtml(followup.Assessment62, "N0", "-");
                       // DropDownListAssessmentPrinciplesScore.SelectedValue = followup.ReasonFollowupValue.ToString();

                        //3.วัตถุประสงค์ของโครงการ
                        summaryAssessmentScore += (followup.Assessment63.HasValue) ? (decimal)followup.Assessment63 : 0;
                        summaryFollowupScore += (followup.ObjectiveFollowupValue.HasValue) ? (decimal)followup.ObjectiveFollowupValue : 0;
                        TextBoxAssessmentObjective.Text = followup.Objective;
                        LabelAssessmentObjectiveScore.Text = Common.Web.WebUtility.DisplayInHtml(followup.Assessment63, "N0", "-");
                       // DropDownListAssessmentObjectiveScore.SelectedValue = followup.ObjectiveFollowupValue.ToString();

                        //4.กลุ่มเป้าหมาย 
                        summaryAssessmentScore += (followup.Assessment64.HasValue) ? (decimal)followup.Assessment64 : 0;
                        summaryFollowupScore += (followup.TargetGroupFollowupValue.HasValue) ? (decimal)followup.TargetGroupFollowupValue : 0;
                        TextBoxAssementTargetName.Text = followup.TargetGroup;
                        LabelAssementTargetNameScore.Text = Common.Web.WebUtility.DisplayInHtml(followup.Assessment64, "N0", "-");
                       // DropDownListAssementTargetNameScore.SelectedValue = followup.TargetGroupFollowupValue.ToString();

                        //5.สถานที่ 
                        summaryAssessmentScore += (followup.Assessment65.HasValue) ? (decimal)followup.Assessment65 : 0;
                        summaryFollowupScore += (followup.LocationFollowupValue.HasValue) ? (decimal)followup.LocationFollowupValue : 0;
                        TextBoxAssementSupportPlace.Text = followup.Location;
                        LabelAssementSupportPlaceScore.Text = Common.Web.WebUtility.DisplayInHtml(followup.Assessment65, "N0", "-");
                       // DropDownListAssementSupportPlaceScore.SelectedValue = followup.LocationFollowupValue.ToString();

                        //6.ระยะเวลา  
                        summaryAssessmentScore += (followup.Assessment66.HasValue) ? (decimal)followup.Assessment66 : 0;
                        summaryFollowupScore += (followup.TimingFollowupValue.HasValue) ? (decimal)followup.TimingFollowupValue : 0;
                        TextBoxAssementPeriod.Text = followup.Timing;
                        LabelAssementPeriodScore.Text = Common.Web.WebUtility.DisplayInHtml(followup.Assessment66, "N0", "-");
                       // DropDownListAssementPeriodScore.SelectedValue = followup.TimingFollowupValue.ToString();

                        //7.วิธีการดำเนินงาน  
                        summaryAssessmentScore += (followup.Assessment67.HasValue) ? (decimal)followup.Assessment67 : 0;
                        summaryFollowupScore += (followup.OperationMethodFollowupValue.HasValue) ? (decimal)followup.OperationMethodFollowupValue : 0;
                        TextBoxAssementProcessing.Text = followup.OperationMethod;
                        LabelAssementProcessingScore.Text = Common.Web.WebUtility.DisplayInHtml(followup.Assessment67, "N0", "-");
                      //  DropDownListAssementProcessingScore.SelectedValue = followup.OperationMethodFollowupValue.ToString();

                        //8.ข้อบ่งชี้ด้านงบประมาณ   
                        summaryAssessmentScore += (followup.Assessment68.HasValue) ? (decimal)followup.Assessment68 : 0;
                        summaryFollowupScore += (followup.BudgetFollowupValue.HasValue) ? (decimal)followup.BudgetFollowupValue : 0;
                        TextBoxAssemetBubget.Text = followup.Budget;
                        LabelAssemetBubgetScore.Text = Common.Web.WebUtility.DisplayInHtml(followup.Assessment68, "N0", "-");
                       // DropDownListAssemetBubgetScore.SelectedValue = followup.BudgetFollowupValue.ToString();

                        //9.ผลที่คาดว่าจะได้รับ   
                        summaryAssessmentScore += (followup.Assessment69.HasValue) ? (decimal)followup.Assessment69 : 0;
                        summaryFollowupScore += (followup.ExpectionFollowupValue.HasValue) ? (decimal)followup.ExpectionFollowupValue : 0;
                        TextBoxAssementAnticipation.Text = followup.Expection;
                        //LabelAssementAnticipationScore.Text = Common.Web.WebUtility.DisplayInHtml(followup.Assessment69, "N0", "-");
                        //DropDownListAssementAnticipationScore.SelectedValue = followup.ExpectionFollowupValue.ToString();

                        //LabelSummayAssementScore.InnerText = Common.Web.WebUtility.DisplayInHtml(summaryAssessmentScore, "N0", "-");
                        //LabelSummayFollowupScore.InnerText = Common.Web.WebUtility.DisplayInHtml(summaryFollowupScore, "N0", "-");
                        //LabelSummayFollowupScoreDesc.InnerText = GetSummaryFolloupDescription(summaryFollowupScore);


                        //Attachment                        
                        C2XFileUploadProjectFollowupAttachment.ClearChanges();
                        C2XFileUploadProjectFollowupAttachment.ExistingFiles = followup.Attachments;
                        C2XFileUploadProjectFollowupAttachment.DataBind();

                        #region new follow up
                        lblParticipant1.Text = followup.TotalTargetGroup.ToString() ;
                        lblParticipant2.Text = followup.TotalParticipant.ToString();
                        lblPeriod1.Text = followup.Period1;
                        //lblPeriod2.Text = followup.Period2;
                      
                        if (followup.ProjectFollowup2 != null)
                        {
                      

                            var f = followup.ProjectFollowup2;
                            ProjectInfoStartDate.SelectedDate = f.PERIODFROM2;
                            ProjectInfoEndDate.SelectedDate = f.PERIODTO2;
                            SetRadioValue("Objective1", (int)f.OBJECTIVE1);
                            SetRadioValue("Objective2",(int)f.OBJECTIVE2);
                            SetRadioValue("Target1",(int)f.RESULT1);
                            SetRadioValue("Target2",(int)f.RESULT2);
                            SetRadioValue("Result1",(int)f.TARGET1);
                            SetRadioValue("Result2",(int)f.TARGET2);
                            txbActivity1.Text = f.ACTIVITY1.ToString()  ;
                            txbActivity2.Text = f.ACTIVITY2.ToString()    ;
                            txbActivityScore.Text = f.ACTIVITYSCORE1.ToString() ;
                            lblObjectiveScore.Text = f.OBJECTIVESCORE.ToString()  ;
                            txbParticipantScore.Text= f.PARTICIPANTSCORE1 .ToString()  ;
                            txbPeriodScore.Text = f.PERIODSCORE1.ToString()  ;
                            lblResultScore.Text = f.RESULTSCORE.ToString()   ;
                            lblTargetScore.Text = f.TARGETSCORE.ToString()  ;
                            lblTotalPercent.Text =f.TOTALPERCENT.ToString()   ;
                            lblTotalPercent1.Text = f.TOTALPERCENT1.ToString() ;
                            lblTotalPercent2.Text=f.TOTALPERCENT2.ToString() ;
                            lblTotalScore.Text= f.TOTALTALSCORE.ToString() ;
                            lblTotalScore1.Text=f.TOTALSCORE1.ToString()  ;
                            lblTotalScore2.Text = f.TOTALSCORE2.ToString()   ;
                            lblToatalScorePart1.Text = f.TOTALSCORE1.ToString();
                            lblToatalScorePart2.Text = f.TOTALSCORE2.ToString();
                            
                        }
                        #endregion new follow up
                    }
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }
        }
        private void SetRadioValue(string g, int i)
        {
            switch (g)
            {
                case "Objective1":
                    if (i == 1) rdbObjective1_1.Checked = true; else if (i == 2) rdbObjective1_2.Checked = true; else if (i == 3) rdbObjective1_3.Checked = true;
                    break;
                case "Objective2":
                    if (i == 1) rdbObjective2_1.Checked = true; else if (i == 2) rdbObjective2_2.Checked = true; else if (i == 3) rdbObjective2_3.Checked = true;
                    break;
                case "Target1":
                    if (i == 1) rdbTarget1_1.Checked = true; else if (i == 2) rdbTarget1_2.Checked = true; else if (i == 3) rdbTarget1_3.Checked = true;
                    break;
                case "Target2":
                    if (i == 1) rdbTarget2_1.Checked = true; else if (i == 2) rdbTarget2_2.Checked = true; else if (i == 3) rdbTarget2_3.Checked = true;
                    break;
                case "Result1":
                    if (i == 1) rdbResult1_1.Checked = true; else if (i == 2) rdbResult1_2.Checked = true; else if (i == 3) rdbResult1_3.Checked = true;
                    break;
                case "Result2":
                    if (i == 1) rdbResult2_1.Checked = true; else if (i == 2) rdbResult2_2.Checked = true; else if (i == 3) rdbResult2_3.Checked = true;
                    break;
            }
        }
        private void PrepareDropdownList()
        {
            List<ServiceModels.GenericDropDownListData> list;
            int score;

            // 1.ชื่อโครงการ
            list = new List<ServiceModels.GenericDropDownListData>();
            list.Add(new ServiceModels.GenericDropDownListData() { Text = Project.Resources.UI.DropdownPleaseSelect, Value = "" });
            score = Convert.ToInt32(Common.AssessmentProjectNameScore.Score5); 
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProjectNameScore.Score2); 
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentProjectNameScore.DataSource = list;
            DropDownListAssessmentProjectNameScore.DataBind();

            // 2.หลักการและเหตุผล
            list = new List<ServiceModels.GenericDropDownListData>();
            list.Add(new ServiceModels.GenericDropDownListData() { Text = Project.Resources.UI.DropdownPleaseSelect, Value = "" });
            score = Convert.ToInt32(Common.AssessmentPrinciplesScore.Score15);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentPrinciplesScore.Score2);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentPrinciplesScore.DataSource = list;
            DropDownListAssessmentPrinciplesScore.DataBind();

            // 3.วัตถุประสงค์ของโครงการ
            list = new List<ServiceModels.GenericDropDownListData>();
            list.Add(new ServiceModels.GenericDropDownListData() { Text = Project.Resources.UI.DropdownPleaseSelect, Value = "" });
            score = Convert.ToInt32(Common.AssessmentObjectiveScore.Score15);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score =Convert.ToInt32( Common.AssessmentObjectiveScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentObjectiveScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentObjectiveScore.DataSource = list;
            DropDownListAssessmentObjectiveScore.DataBind();

            // 4.กลุ่มเป้าหมาย
            list = new List<ServiceModels.GenericDropDownListData>();
            list.Add(new ServiceModels.GenericDropDownListData() { Text = Project.Resources.UI.DropdownPleaseSelect, Value = "" });
            score = Convert.ToInt32(Common.AssessmentProjectTargetScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProjectTargetScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProjectTargetScore.Score3);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProjectTargetScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssementTargetNameScore.DataSource = list;
            DropDownListAssementTargetNameScore.DataBind();

            // 5.สถานที่
            list = new List<ServiceModels.GenericDropDownListData>();
            list.Add(new ServiceModels.GenericDropDownListData() { Text = Project.Resources.UI.DropdownPleaseSelect, Value = "" });
            score = Convert.ToInt32(Common.AssessmentSupportPlaceScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentSupportPlaceScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentSupportPlaceScore.Score3);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentSupportPlaceScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssementSupportPlaceScore.DataSource = list;
            DropDownListAssementSupportPlaceScore.DataBind();

            // 6.ระยะเวลา
            list = new List<ServiceModels.GenericDropDownListData>();
            list.Add(new ServiceModels.GenericDropDownListData() { Text = Project.Resources.UI.DropdownPleaseSelect, Value = "" });
            score = Convert.ToInt32(Common.AssessmentPeriodScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentPeriodScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentPeriodScore.Score3);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentPeriodScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssementPeriodScore.DataSource = list;
            DropDownListAssementPeriodScore.DataBind();

            // 7.วิธีการดำเนินงาน
            list = new List<ServiceModels.GenericDropDownListData>();
            list.Add(new ServiceModels.GenericDropDownListData() { Text = Project.Resources.UI.DropdownPleaseSelect, Value = "" });
            score = Convert.ToInt32(Common.AssessmentProcessingScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProcessingScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProcessingScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssementProcessingScore.DataSource = list;
            DropDownListAssementProcessingScore.DataBind();

            //8.ข้อบ่งชี้ด้านงบประมาณ
            list = new List<ServiceModels.GenericDropDownListData>();
            list.Add(new ServiceModels.GenericDropDownListData() { Text = Project.Resources.UI.DropdownPleaseSelect, Value = "" });
            score = Convert.ToInt32(Common.AssessmentBudgetScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentBudgetScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentBudgetScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssemetBubgetScore.DataSource = list;
            DropDownListAssemetBubgetScore.DataBind();

            //9.ผลที่คาดว่าจะได้รับ
            list = new List<ServiceModels.GenericDropDownListData>();
            list.Add(new ServiceModels.GenericDropDownListData() { Text = Project.Resources.UI.DropdownPleaseSelect, Value = "" });
            score = Convert.ToInt32(Common.AssessmentAnticipationScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentAnticipationScore.Score3);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentAnticipationScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssementAnticipationScore.DataSource = list;
            DropDownListAssementAnticipationScore.DataBind();
            
        }


        private String GetSummaryFolloupDescription(decimal summaryScore)
        {

            String[] score = new String[] { };

            


            String text = "";
            if(summaryScore >= 96 ){
                text = Nep.Project.Resources.UI.FollowupScoreDesc_96_100;
            }
            else if (summaryScore >= 91)
            {
                text = Nep.Project.Resources.UI.FollowupScoreDesc_91_95;
            }
            else if (summaryScore >= 81)
            {
                text = Nep.Project.Resources.UI.FollowupScoreDesc_81_90;
            }
            else if (summaryScore >= 70)
            {
                text = Nep.Project.Resources.UI.FollowupScoreDesc_70_80;
            }
            else
            {
                text = Nep.Project.Resources.UI.FollowupScoreDesc_0_69;
            }
            return text;
        }

        private ServiceModels.ProjectInfo.ProjectFollowup GetData()
        {
            ServiceModels.ProjectInfo.ProjectFollowup model = new ServiceModels.ProjectInfo.ProjectFollowup();

            model.ProjectID = ProjectID;
            
            model.ProjectName = TextBoxAssessmentProjectName.Text.TrimEnd();
            //model.ProjectFollowupValue = Decimal.Parse(DropDownListAssessmentProjectNameScore.SelectedValue);
            model.ProjectFollowupValue = 0;

            model.Reason = TextBoxAssessmentPrinciples.Text.TrimEnd();
            //model.ReasonFollowupValue = Decimal.Parse(DropDownListAssessmentPrinciplesScore.SelectedValue);
            model.ReasonFollowupValue = 0;

            model.Objective = TextBoxAssessmentObjective.Text.TrimEnd();
            //model.ObjectiveFollowupValue = Decimal.Parse(DropDownListAssessmentObjectiveScore.SelectedValue);
            model.ObjectiveFollowupValue =0;

            model.TargetGroup = TextBoxAssementTargetName.Text.TrimEnd();
            //model.TargetGroupFollowupValue = Decimal.Parse(DropDownListAssementTargetNameScore.SelectedValue);
            model.TargetGroupFollowupValue = 0;

            model.Location = TextBoxAssementSupportPlace.Text.TrimEnd();
            //model.LocationFollowupValue = Decimal.Parse(DropDownListAssementSupportPlaceScore.SelectedValue);
            model.LocationFollowupValue = 0;

            model.Timing = TextBoxAssementPeriod.Text.TrimEnd();
            //model.TimingFollowupValue = Decimal.Parse(DropDownListAssementPeriodScore.SelectedValue);
            model.TimingFollowupValue = 0;

            model.OperationMethod = TextBoxAssementProcessing.Text.TrimEnd();
            //model.OperationMethodFollowupValue = Decimal.Parse(DropDownListAssementProcessingScore.SelectedValue);
            model.OperationMethodFollowupValue = 0;

            model.Budget = TextBoxAssemetBubget.Text.TrimEnd();
            //model.BudgetFollowupValue = Decimal.Parse(DropDownListAssemetBubgetScore.SelectedValue);
            model.BudgetFollowupValue = 0;

            model.Expection = TextBoxAssementAnticipation.Text.TrimEnd();
            //model.ExpectionFollowupValue = Decimal.Parse(DropDownListAssementAnticipationScore.SelectedValue);
            model.ExpectionFollowupValue = 0;

            IEnumerable<ServiceModels.KendoAttachment> addedFiles = C2XFileUploadProjectFollowupAttachment.AddedFiles;
            IEnumerable<ServiceModels.KendoAttachment> removedFiles = C2XFileUploadProjectFollowupAttachment.RemovedFiles;
            model.AddedAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
            model.RemovedAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;

            //var f = new DBModels.Model.PROJECTFOLLOWUP2();
            
         
            var f = Newtonsoft.Json.JsonConvert.DeserializeObject<DBModels.Model.PROJECTFOLLOWUP2>(HiddenValue.Value);
            model.ProjectFollowup2 = f;
            f.ACTIVITY1 = Convert.ToInt16 ( txbActivity1.Text) ;
            f.ACTIVITY2 = Convert.ToInt16(txbActivity2.Text);
            f.ACTIVITYSCORE1 = Convert.ToInt16(txbActivityScore.Text);
            f.OBJECTIVE1 = GetRadioValue("Objective1");
            f.OBJECTIVE2 = GetRadioValue("Objective2");
            //f.OBJECTIVESCORE = Convert.ToInt16(lblObjectiveScore.Text);
            f.PARTICIPANTSCORE1 = Convert.ToInt16(txbParticipantScore .Text);
            f.PERIODSCORE1 = Convert.ToInt16(txbPeriodScore.Text);
            f.PROJECTID = model.ProjectID;
            f.RESULT1 = GetRadioValue("Result1");
            f.RESULT2 = GetRadioValue("Result2");
            //f.RESULTSCORE = Convert.ToInt16(lblResultScore.Text);
            f.TARGET1 = GetRadioValue("Target1");
            f.TARGET2 = GetRadioValue("Target2");
            f.PERIODFROM2 = Convert.ToDateTime(ProjectInfoStartDate.SelectedDate);
            f.PERIODTO2 = Convert.ToDateTime(ProjectInfoEndDate.SelectedDate);
            //f.TARGETSCORE = Convert.ToInt16(lblTargetScore.Text);
            //f.TOTALPERCENT = Convert.ToDecimal(lblTotalPercent.Text);
            //f.TOTALPERCENT1 = Convert.ToDecimal(lblTotalPercent1.Text);
            //f.TOTALPERCENT2 = Convert.ToDecimal(lblTotalPercent2.Text);
            //f.TOTALTALSCORE  = Convert.ToDecimal(lblTotalScore.Text);
            //f.TOTALSCORE1 = Convert.ToDecimal(lblTotalScore1.Text);
            //f.TOTALSCORE2 = Convert.ToDecimal(lblTotalScore2.Text);

            return model;
        }
        protected string CalScore(DBModels.Model.PROJECTFOLLOWUP2 f)
        {
            string msg;
            //DateTime startDate = Convert.ToDateTime(ProcessingPlanStartDate.SelectedDate);
            //DateTime endDate = Convert.ToDateTime(ProcessingPlanEndDate.SelectedDate);
            if (string.IsNullOrEmpty(txbActivity1.Text.Trim()) || string.IsNullOrEmpty(txbActivity2.Text.Trim()))
                {
                msg = "โปรดระบุจำนวนกิจกรรม";
                   return msg;
            }
            f.ACTIVITY1 = Convert.ToDecimal(txbActivity1.Text.Trim());
            f.ACTIVITY2 = Convert.ToDecimal(txbActivity2.Text.Trim());
            if (f.ACTIVITY2 == 0 || f.ACTIVITY1==0)
            {
                msg = "โปรดระบุจำนวนกิจกรรม";
                return msg;
            }
            if (f.ACTIVITY2 > f.ACTIVITY1 )
            {
                msg = "จำนวนกิจกรรมของผลดำเนินงานต้องไม่มากกว่าที่ระบุในโครงการ";
                return msg;
            }
    
            //f.PERIODFROM2 = startDate;
            //f.PERIODTO2 = endDate;
            //f.ACTIVITY1 =  txbActivity1.

            return "";
        }
        protected int GetRadioValue(string groupName)
        {
            switch (groupName)
            {
                case "Objective1":
                    if (rdbObjective1_1.Checked) return 1; else if (rdbObjective1_2.Checked) return 2; else if (rdbObjective1_3.Checked) return 3; else return -1;
                    break;
                case "Objective2":
                    if (rdbObjective2_1.Checked) return 1; else if (rdbObjective2_2.Checked) return 2; else if (rdbObjective2_3.Checked) return 3; else return -1;
                    break;
                case "Target1":
                    if (rdbTarget1_1.Checked) return 1; else if (rdbTarget1_2.Checked) return 2; else if (rdbTarget1_3.Checked) return 3; else return -1;
                    break;
                case "Target2":
                    if (rdbTarget2_1.Checked) return 1; else if (rdbTarget2_2.Checked) return 2; else if (rdbTarget2_3.Checked) return 3; else return -1;
                    break;
                case "Result1":
                    if (rdbResult1_1.Checked) return 1; else if (rdbResult1_2.Checked) return 2; else if (rdbResult1_3.Checked) return 3; else return -1;
                    break;
                case "Result2":
                    if (rdbResult2_1.Checked) return 1; else if (rdbResult2_2.Checked) return 2; else if (rdbResult2_3.Checked) return 3; else return -1;
                    break;
            }
           return -1;
        }

        //private ServiceModels.ProjectInfo.ProjectFollowup GetData()
        //{
        //    ServiceModels.ProjectInfo.ProjectFollowup model = new ServiceModels.ProjectInfo.ProjectFollowup();

        //    model.ProjectID = ProjectID;

        //    model.ProjectName = TextBoxAssessmentProjectName.Text.TrimEnd();
        //    model.ProjectFollowupValue = Decimal.Parse(DropDownListAssessmentProjectNameScore.SelectedValue);

        //    model.Reason = TextBoxAssessmentPrinciples.Text.TrimEnd();
        //    model.ReasonFollowupValue = Decimal.Parse(DropDownListAssessmentPrinciplesScore.SelectedValue);

        //    model.Objective = TextBoxAssessmentObjective.Text.TrimEnd();
        //    model.ObjectiveFollowupValue = Decimal.Parse(DropDownListAssessmentObjectiveScore.SelectedValue);

        //    model.TargetGroup = TextBoxAssementTargetName.Text.TrimEnd();
        //    model.TargetGroupFollowupValue = Decimal.Parse(DropDownListAssementTargetNameScore.SelectedValue);

        //    model.Location = TextBoxAssementSupportPlace.Text.TrimEnd();
        //    model.LocationFollowupValue = Decimal.Parse(DropDownListAssementSupportPlaceScore.SelectedValue);

        //    model.Timing = TextBoxAssementPeriod.Text.TrimEnd();
        //    model.TimingFollowupValue = Decimal.Parse(DropDownListAssementPeriodScore.SelectedValue);

        //    model.OperationMethod = TextBoxAssementProcessing.Text.TrimEnd();
        //    model.OperationMethodFollowupValue = Decimal.Parse(DropDownListAssementProcessingScore.SelectedValue);

        //    model.Budget = TextBoxAssemetBubget.Text.TrimEnd();
        //    model.BudgetFollowupValue = Decimal.Parse(DropDownListAssemetBubgetScore.SelectedValue);

        //    model.Expection = TextBoxAssementAnticipation.Text.TrimEnd();
        //    model.ExpectionFollowupValue = Decimal.Parse(DropDownListAssementAnticipationScore.SelectedValue);

        //    IEnumerable<ServiceModels.KendoAttachment> addedFiles = C2XFileUploadProjectFollowupAttachment.AddedFiles;
        //    IEnumerable<ServiceModels.KendoAttachment> removedFiles = C2XFileUploadProjectFollowupAttachment.RemovedFiles;
        //    model.AddedAttachments = (addedFiles.Count() > 0) ? addedFiles.ToList() : null;
        //    model.RemovedAttachments = (removedFiles.Count() > 0) ? removedFiles.ToList() : null;

        //    // followup 2
        //    var f = new DBModels.Model.PROJECTFOLLOWUP2();

        //    return model;
        //}
        protected void UpdatePanelPrintedDocument_Load(object sender, EventArgs e)
        {
            string parameter = Request["__EVENTARGUMENT"];
            bool isReload = ((parameter != null) && ((parameter == "deleted") || (parameter == "updated")));
            
            if ((!IsPostBack) || isReload)
            {
                //GridViewPrintedDocument
                List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();
                fields.Add(new ServiceModels.FilterDescriptor()
                {
                    Field = "ProjectID",
                    Operator = ServiceModels.FilterOperator.IsEqualTo,
                    Value = ProjectID
                });

                this.GridViewPrintedDocument.FilterDescriptors = fields;
                this.GridViewPrintedDocument.DataBind();

                //GridViewPrintedDocument.DataBind();
            }            
        }

        protected void CustomValidatorPlanDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //if (ProcessingPlanStartDate.SelectedDate.HasValue && ProcessingPlanEndDate.SelectedDate.HasValue)
            //{
            //    int diff = GetDiffStartAndEndProcessingPlanate();
            //    args.IsValid = (diff > 0);
            //}
        }

        //private int GetDiffStartAndEndProcessingPlanate()
        //{
        //    int diff = 0;
        //    DateTime? startDate = ProcessingPlanStartDate.SelectedDate;
        //    DateTime? endDate = ProcessingPlanEndDate.SelectedDate;

        //    if (startDate.HasValue && endDate.HasValue)
        //    {
        //        diff = ((DateTime)endDate).Subtract((DateTime)startDate).Days + 1;
        //    }

        //    return diff;
        //}
    }
}