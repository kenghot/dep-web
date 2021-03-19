using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using Nep.Project.Common.Web;

namespace Nep.Project.Web.ProjectInfo
{
    public partial class ProjectInfoForm : Nep.Project.Web.Infra.BasePage
    {
        
        public IServices.IProjectInfoService _projectInfoService { get; set; }
        public IServices.IProviceService _provinceService { get; set; }
        public IServices.IOrganizationParameterService _orgParamService { get; set; }
               
        public ServiceModels.Security.SecurityInfo userProfile { get; set; }
        public String ProjectApprovalStatusCode
        {
            get {
                string code = "";
                if (ViewState["ProjectApprovalStatusCode"] != null)
                {
                    code = ViewState["ProjectApprovalStatusCode"].ToString();
                }

                return code;                     
            }

            set
            {
                ViewState["ProjectApprovalStatusCode"] = value;
            }
        }
        public String ProjectResponseEmail
        {
            get
            {
                string code = "";
                if (ViewState["ProjectResponseEmail"] != null)
                {
                    code = ViewState["ProjectResponseEmail"].ToString();
                }

                return code;
            }

            set
            {
                ViewState["ProjectResponseEmail"] = value;
            }
        }
        public String FollowupStatusCode
        {
            get
            {
                string code = "";
                if (ViewState["FollowupStatusCode"] != null)
                {
                    code = ViewState["FollowupStatusCode"].ToString();
                }

                return code;
            }

            set
            {
                ViewState["FollowupStatusCode"] = value;
            }
        }


        /// <summary>
        /// 1 = Approvaed
        /// 0 = Not Approved
        /// null = Nothing
        /// </summary>
        public String ApprovalStatus
        {
            get
            {
                string code = "";
                if (ViewState["ApprovalStatus"] != null)
                {
                    code = ViewState["ApprovalStatus"].ToString();
                }

                return code;
            }

            set
            {
                ViewState["ApprovalStatus"] = value;
            }
        }

        public Boolean HasEvaluationInfo
        {
            get
            {
                bool hasInfo = false;
                if (ViewState["HasEvaluationInfo"] != null)
                {
                    hasInfo = Convert.ToBoolean(ViewState["HasEvaluationInfo"].ToString());
                }

                return hasInfo;
            }

            set
            {
                ViewState["HasEvaluationInfo"] = value;
            }
        }

        public Boolean HasApprovalInfo
        {
            get
            {
                bool hasInfo = false;
                if (ViewState["HasApprovalInfo"] != null)
                {
                    hasInfo = Convert.ToBoolean(ViewState["HasApprovalInfo"].ToString());
                }

                return hasInfo;
            }

            set
            {
                ViewState["HasApprovalInfo"] = value;
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
        //kenghot18
        public Decimal? BudgetAmount
        {
            get
            {
                decimal budget = 0;
                if (ViewState["BudgetAmount"] == null)
                {
                    ViewState["BudgetAmount"] = 0;
                }


                return (decimal)ViewState["BudgetAmount"];
            }
            set
            {
                ViewState["BudgetAmount"] = value;

            }
        }
        public List<Common.ProjectFunction> ProjectRoles {
            get
            {
                List<Common.ProjectFunction> roles = new List<Common.ProjectFunction>() ;
                if (ViewState["ProjectRoles"] != null)
                {
                    roles = (List<Common.ProjectFunction>)ViewState["ProjectRoles"];
                }
                return roles;
            }

            set
            {
                ViewState["ProjectRoles"] = value;
            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.MANAGE_PROJECT, Common.FunctionCode.REQUEST_PROJECT, Common.FunctionCode.TRACK_PROJECT, Common.FunctionCode.VIEW_PROJECT };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {        
           
            if (!IsPostBack)
            {
                _projectInfoService.SaveLogAccess((userProfile == null)? null: userProfile.UserID, Common.LOVCode.Logaccess.รายละเอียดโครงการ, "I", Request.UserHostAddress);
                bool isSaveSuccess;               
                if (Boolean.TryParse(Request.QueryString["isSaveSuccess"], out isSaveSuccess) && (isSaveSuccess == true))
                {
                    ShowResultMessage(Message.SaveSuccess);
                }  
                BindData();
                DisplayGeneralInfoControl.BindData();
                //int act;
                //if (int.TryParse(Request.QueryString["ActiveTabIndex"], out act))
                //{
                //    TabContainerProjectInfoForm.ActiveTabIndex = act;
                //    TabContainerProjectInfoForm_ActiveTabChanged(TabContainerProjectInfoForm, new EventArgs());
                //}
            }
 
            
        }



        protected void Page_PreRender(object sender, EventArgs e)
        {            
            String projectApprovalStatusCode = this.ProjectApprovalStatusCode;
            String followupStatusCode = this.FollowupStatusCode;
            bool isOfficer = (UserInfo.IsProvinceOfficer || UserInfo.IsCenterOfficer );
            bool isApproved = ((!String.IsNullOrEmpty(ApprovalStatus)) && (ApprovalStatus == "1"));

            TabPanelAssessment.Visible = false;
            TabPanelProjectApproval.Visible = false;
            TabPanelProcessed.Visible = false;
            TabPanelContract.Visible = false;
            TabPanelReportResult.Visible = false;
            TabPanelSatisfy.Visible = false;
            TabPanelSelfEvaluate.Visible = false;
            TabSurveyParticipant.Visible = false;
            TabPanelFollowup.Visible = false;
            TabProsecute.Visible = false;
            TabPanelFollowUpProcess.Visible = false;
            //kenghot18
            TabPanelResponse.Visible = true;
            if (UserInfo.UserGroupCode  == Common.UserGroupCode.องค์กรภายนอก)
            {
                var email = (from pg in _projectInfoService.GetDB().ProjectGeneralInfoes where pg.ProjectID == this.ProjectID select pg.RESPONSEEMAIL).FirstOrDefault();
                if (string.IsNullOrEmpty(email))
                {
                    TabPanelResponse.Visible = false;
                }

            }


            //ProjectBudgetForSecretaryControl.Visible = false;
            //ProjectBudgetControl.Visible = false;            
                       
            if(isOfficer)
            {
                if (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร)
                {
                    //ProjectBudgetControl.Visible = true; 
                }
                else
                {
                    //ProjectBudgetForSecretaryControl.Visible = true;
                }


                if (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_1_เจ้าหน้าที่ประสานงานส่งแบบเสนอโครงการ)
                {
                    TabPanelAssessment.Visible = true;

                }
                //kenghot
                //else if ((projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_2_เจ้าหน้าที่พิจารณาเกณฑ์ประเมิน) ||
                //    (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_4_อนุมัติโดยคณะกรรมการกลั่นกรอง) || 
                //    (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยคณะกรรมการกลั่นกรอง) ||
                //    (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยอนุกรรมการกองทุนหรือจังหวัด))
                else if ((projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_2_เจ้าหน้าที่พิจารณาเกณฑ์ประเมิน) ||
                     (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_4_อนุมัติโดยคณะกรรมการกลั่นกรอง) ||
                     (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยคณะกรรมการกลั่นกรอง) ||
                     (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยอนุกรรมการกองทุนหรือจังหวัด) ||
                     (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_3_1_ชะลอการพิจารณา) ||
                     (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_4_1_ชะลอการพิจารณา) ||
                     (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกโดยคณะกรรมการกลั่นกรอง) ||
                     (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกโดยอนุกรรมการกองทุนหรือจังหวัด) ||
                     (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.อื่นๆ_โดยคณะกรรมการกลั่นกรอง) ||
                     (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.อื่นๆ_โดยอนุกรรมการกองทุนหรือจังหวัด)
                     )
                {
                    TabPanelAssessment.Visible = true;
                    TabPanelProjectApproval.Visible = true;
                    // TabPanelProcessed.Visible = true;

                }
                else if ((projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_3_อนุมัติโดยอนุกรรมการจังหวัด) ||
                    (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที_5_อนุมัติโดยอนุกรรมการกองทุน))
                {
                    TabPanelAssessment.Visible = true;
                    TabPanelProjectApproval.Visible = true;
                    // TabPanelProcessed.Visible = true;
                    TabPanelContract.Visible = (isApproved);
                }
                else if (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอน_6_1_รอโอนเงิน)
                {
                    TabPanelAssessment.Visible = true;
                    TabPanelProjectApproval.Visible = true;
                    // TabPanelProcessed.Visible = true;
                    TabPanelContract.Visible = true;
                }
                else if (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว)
                {
                    TabPanelAssessment.Visible = true;
                    TabPanelProjectApproval.Visible = true;
                    TabPanelProcessed.Visible = true;
                    TabPanelContract.Visible = true;
                    TabPanelFollowup.Visible = true;

                    TabProsecute.Visible = true;
                    if (BudgetAmount.Value > 5000000)
                    {
                        TabPanelFollowUpProcess.Visible = true;
                    }
                }
                else if (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกสัญญา)
                {
                    TabPanelAssessment.Visible = true;
                    TabPanelProjectApproval.Visible = true;
                    TabPanelProcessed.Visible = true;
                    TabPanelContract.Visible = true;
                }
                else if (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ยกเลิกคำร้อง)
                {
                    TabPanelAssessment.Visible = HasEvaluationInfo;
                    TabPanelProjectApproval.Visible = HasApprovalInfo;

                }
            }
            else
            {                
                
                //Organization View
                if (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว)
                {
                    TabPanelContract.Visible = true;
                    TabPanelProcessed.Visible = true;
                }                
            }
           
            
            List<Common.ProjectFunction> roles = ProjectRoles;
            //NepHelper.WriteLog(UserInfo.UserGroupCode);
            if((followupStatusCode != Common.LOVCode.Followupstatus.รายงานผลแล้ว) && (projectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_6_ทำสัญญาเรียบร้อยแล้ว) && 
                (roles.Contains(Common.ProjectFunction.SaveDraftReportResult) || (roles.Contains(Common.ProjectFunction.SaveReportResult) || UserInfo.UserGroupCode == Common.UserGroupCode.องค์กรภายนอก))){
                    TabPanelReportResult.Visible = true;
                    TabPanelSatisfy.Visible = true;
                TabPanelSelfEvaluate.Visible = true;
                TabSurveyParticipant.Visible = true;
                }
            else if ((followupStatusCode == Common.LOVCode.Followupstatus.รายงานผลแล้ว) && (roles.Contains(Common.ProjectFunction.CanViewProjectInfo) || UserInfo.UserGroupCode == Common.UserGroupCode.องค์กรภายนอก) )
            {
                TabPanelReportResult.Visible = true;
                TabPanelSatisfy.Visible = true;
                TabPanelSelfEvaluate.Visible = true;
                TabSurveyParticipant.Visible = true;
            }
            string tabRequired = "";
            tabRequired += IsTabRequired("SATISFY", TabPanelSatisfy);
             
            tabRequired += IsTabRequired("SELF", TabPanelSelfEvaluate);
            tabRequired += IsTabRequired("SELF", TabSurveyParticipant);
            // การดำเนินการ
            var proc = (from pc in _projectInfoService.GetDB().PROJECTPROCESSEDs where pc.PROJECTID == ProjectID select pc ).FirstOrDefault();
            if (proc == null)
            {
                tabRequired +=  "RequiredTabByName('" + TabPanelProcessed.HeaderText + "');";
            }
            // แบบรายงานผลการปฎิบัติการ
            var rep = (from pc in _projectInfoService.GetDB().ProjectReports where pc.ProjectID == ProjectID select pc).FirstOrDefault();
            if ((rep != null && followupStatusCode != Common.LOVCode.Followupstatus.รายงานผลแล้ว) || (rep == null))
            {
                tabRequired += "RequiredTabByName('" + TabPanelReportResult.HeaderText + "');";
            }
            RegisterSetRequiredTab(tabRequired);
           // TabContainerProjectInfoForm.ActiveTabIndex = 3;// TabPanelProjectBudget.TabIndex;

        }
        private void RegisterSetRequiredTab(string s)
        {

            String script = @"
                 $(function () {      
                   " + s  + @" 
                    
                 });
            
            ";
            ScriptManager.RegisterStartupScript(
                      UpdatePanelProjectInfoForm,
                      this.GetType(),
                      "UpdatePanelGeneralInfoControl",
                      script,
                      true);
        }
        private string IsTabRequired(string QNGroup, AjaxControlToolkit.TabPanel tab)
        {
            string ret = "";
            if (tab.Visible)
            {
                var db = _projectInfoService.GetDB();
                var qn = (from q in db.PROJECTQUESTIONHDs where q.PROJECTID == ProjectID && q.QUESTGROUP == QNGroup && q.ISREPORTED == "1" select q).FirstOrDefault();
                if (qn == null)
                {
                    ret = "RequiredTabByName('" + tab.HeaderText + "');";
                }
             
            }
            return ret;

        }              

        protected void TabContainerProjectInfoForm_ActiveTabChanged(object sender, EventArgs e)
        {
            string tabName = TabContainerProjectInfoForm.ActiveTab.ID;
            bool isOfficer = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer);
            
            DisplayGeneralInfoControl.Visible = false;
            DisplayProjectInfoControl.Visible = false;
            PersonalControl.Visible = false;
            ProsecuteControl.Visible = false;
            ResponseControl.Visible = false;
            ProjectBudgetControl.Visible = false;
            ProjectBudgetForSecretaryControl.Visible = false;
            ProcessingPlanControl.Visible = false;
            DisplayAttachmentControl.Visible = false;
            
            DisplayAssessmentControl.Visible = false;
            ApprovalControl.Visible = false;
            ProcessedControl.Visible = false;
            ContractControl.Visible = false;
            ReportResultControl.Visible = false;
            SatisfyControl.Visible = false;
            SelfEvaluateControl.Visible = false;
            SurveyParticipant.Visible = false;
            FollowupControl.Visible = false;
            Follow5MControl.Visible = false;
            FollowUnder5MControl.Visible = false;
            switch (tabName)
            {
                case "TabPanelGeneralInfo":
                    {
                        DisplayGeneralInfoControl.Visible = true;
                        DisplayGeneralInfoControl.BindData();
                        break;
                    }
                case "TabPanelProjectInfo":
                    {
                        DisplayProjectInfoControl.Visible = true;
                        DisplayProjectInfoControl.BindData();                      
                        break;
                    }
                case "TabPersonal":
                    {                        
                        PersonalControl.Visible = true;
                        PersonalControl.BindData();
                        break;
                    }
                case "TabProcessingPlan":
                    {
                        ProcessingPlanControl.Visible = true;
                        ProcessingPlanControl.BindData();
                        break;
                    }
                case "TabPanelProjectBudget":
                    {
                        if ((ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร) ||
                            (!isOfficer) || UserInfo.IsAdministrator)
                        {
                            ProjectBudgetControl.Visible = true;
                            ProjectBudgetControl.BindData();
                        }
                        else
                        {
                            ProjectBudgetForSecretaryControl.Visible = true;
                            ProjectBudgetForSecretaryControl.BindData();
                        }

                        break;
                    }
                case "TabPanelAttachment":
                    {
                        DisplayAttachmentControl.Visible = true;
                        DisplayAttachmentControl.BindData();
                        break;
                    }
                case "TabPanelResponse":
                    {
                        ResponseControl.Visible = true;
                        ResponseControl.BindData();
                        break;
                    }
                case "TabPanelAssessment":
                    {
                        DisplayAssessmentControl.Visible = true;
                        DisplayAssessmentControl.BindData();
                        break;
                    }
                case "TabPanelProjectApproval":
                    {
                        ApprovalControl.Visible = true;
                        ApprovalControl.BindData();
                        break;
                    }
                case "TabPanelProcessed":
                    {
                        ProcessedControl.Visible = true;
                        ProcessedControl.BindData();
                        break;
                    }
                case "TabPanelContract":
                    {
                        ContractControl.Visible = true;
                        ContractControl.BindData();
                        break;
                    }
                case "TabPanelFollowUpProcess":
                    {
                        
                        FollowProcessingControl.Visible = true;

                        FollowProcessingControl.BindData();
                        break;
                    }
                case "TabPanelReportResult":
                    {
                        ReportResultControl.Visible = true;
                        ReportResultControl.BindData();
                        break;
                    }
                case "TabPanelSatisfy":
                    {
                        SatisfyControl.Visible = true;
                      
                        SatisfyControl.BindData();
                        break;
                    }
                case "TabPanelSelfEvaluate":
                    {
                        SelfEvaluateControl.Visible = true;

                        SelfEvaluateControl.BindData();
                        break;
                    }
                case "TabSurveyParticipant":
                    {
                        SurveyParticipant.Visible = true;

                        SurveyParticipant.BindData();
                        break;
                    }
                case "TabPanelFollowup":
                    {
                        //kenghot18
                        //FollowupControl.Visible = false;
                        //FollowupControl.BindData();
                        //if (BudgetAmount <= 5000000)
                        if (true)
                        {
                            FollowUnder5MControl.Visible = true;
                            FollowUnder5MControl.BindData();
                        }
                        else
                        {
                            Follow5MControl.Visible = true;
                            Follow5MControl.BindData();
                        }
                        break;
                    }
                case "TabProsecute":
                    {
                        ProsecuteControl.Visible = true;
                        ProsecuteControl.BindData();
                        break;
                    }
            }
        }

        public void RebindData(String tabFocusName)
        {
            bool isOfficer = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer);
            BindData();

            switch (tabFocusName)
            {
                case "TabPanelGeneralInfo":
                    {
                        DisplayGeneralInfoControl.BindData();
                        break;
                    }
                case "TabPanelProjectInfo":
                    {                        
                        DisplayProjectInfoControl.BindData();
                        break;
                    }
                case "TabPersonal":
                    {                        
                        PersonalControl.BindData();
                        break;
                    }
                case "TabProcessingPlan":
                    {                       
                        ProcessingPlanControl.BindData();
                        break;
                    }
                case "TabPanelProjectBudget":
                    {
                        if ((ProjectApprovalStatusCode == Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร) ||
                            (!isOfficer) || UserInfo.IsAdministrator)
                        {
                            ProjectBudgetControl.BindData();
                        }
                        else
                        {
                            ProjectBudgetForSecretaryControl.BindData();
                        }

                        break;
                    }
                case "TabPanelAttachment":
                    {
                        DisplayAttachmentControl.BindData();
                        break;
                    }
                case "TabResponse":
                    {
                        ResponseControl.BindData();
                        break;
                    }
                case "TabPanelAssessment":
                    {
                        DisplayAssessmentControl.BindData();
                        break;
                    }
                case "TabPanelProjectApproval":
                    {
                        ApprovalControl.BindData();
                        break;
                    }
                case "TabPanelProcessed":

                    {
                        ProcessedControl.BindData();
                        break;
                    }              
                case "TabPanelContract":
                    {
                        ContractControl.BindData();
                        break;
                    }
                case "TabPanelFollowUpProcess":
                    {
                        
                        FollowProcessingControl.BindData();
                        break;
                    }
                case "TabPanelReportResult":
                    {
                        ReportResultControl.BindData();
                        break;
                    }
                case "TabPanelSatisfy":
                    {
                       
                        SatisfyControl.BindData();
                        break;
                    }
                case "TabPanelSelfEvaluate":
                    {

                        SelfEvaluateControl.BindData();
                        break;
                    }
                case "TabSurveyParticipant":
                    {

                        SurveyParticipant.BindData();
                        break;
                    }
                case "TabPanelFollowup":
                    {
                        //kenghot18
                        //FollowupControl.BindData();
                        if (BudgetAmount <= 5000000)
                        {
                            FollowUnder5MControl.BindData();
                        }
                        else
                        {
                            Follow5MControl.BindData();
                        }

                        break;
                    }
                case "TabProsecute":
                    {
                        ProsecuteControl.BindData();
                        break;
                    }
                case "TabPanelProjectApprovalRefreshTotal":
                    {
                        ApprovalControl.UpdateTotalProjectBudget();
                        break;
                    }

            }
        }

        

        private Boolean CanViewData(decimal? creatorOrganizationID,  decimal? projectProvinceID, decimal projectOrgID, string projectApprovalStatusCode)
        {
            bool isCan = false;
            decimal? userProvinceId = UserInfo.ProvinceID;
            decimal? userOrganizationID = UserInfo.OrganizationID;

            if(UserInfo.IsCenterOfficer){
                isCan = true;
            }
            else if (UserInfo.IsProvinceOfficer && (userProvinceId.HasValue && projectProvinceID.HasValue) && (userProvinceId == projectProvinceID))
            {
                isCan = true;
            }
            else if ((!UserInfo.IsCenterOfficer && !UserInfo.IsProvinceOfficer) && 
                (((userOrganizationID.HasValue && creatorOrganizationID.HasValue) && (userOrganizationID == creatorOrganizationID)) ||
                 ((userOrganizationID == projectOrgID) && (projectApprovalStatusCode != Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร))))
            {               
                isCan = true;
            }          
          
            return isCan;
        }
        private void SetRejectedTab(ServiceModels.ProjectInfo.TabProjectInfo d)
        {
            btnShowSuggestion.Visible = false;
            //var isRejected = _projectInfoService.getproje
            if (d.ProjectApprovalStatusCode == "0" && !string.IsNullOrEmpty(d.RejectComment))
            {
                var rej = new Business.ProjectInfoService.RejectTopic(d.RejectTopic);
                this.TabPanelProjectInfo.HeaderText = (rej.IsProjInfo) ? string.Format("<span style='color:red'>{0} *แก้ไข</span>", Nep.Project.Resources.UI.TabTitleProjectInfo) : Nep.Project.Resources.UI.TabTitleProjectInfo ;
                this.TabPersonal.HeaderText = (rej.IsPerson) ? string.Format("<span style='color:red'>{0} *แก้ไข</span>",   Nep.Project.Resources.UI.TabTitlePersonal) : Nep.Project.Resources.UI.TabTitlePersonal;
                this.TabProcessingPlan.HeaderText = (rej.IsProcess) ? string.Format("<span style='color:red'>{0} *แก้ไข</span>", Nep.Project.Resources.UI.TabTitleProcessingPlan) : Nep.Project.Resources.UI.TabTitleProcessingPlan;
                this.TabPanelProjectBudget.HeaderText = (rej.IsBudget) ? string.Format("<span style='color:red'>{0} *แก้ไข</span>", Nep.Project.Resources.UI.TabTitleProjectBudget) : Nep.Project.Resources.UI.TabTitleProjectBudget;
                this.TabPanelAttachment.HeaderText = (rej.IsAttach) ? string.Format("<span style='color:red'>{0} *แก้ไข</span>", Nep.Project.Resources.UI.TabTitleAttachment) : Nep.Project.Resources.UI.TabTitleAttachment;
                LableSuggestion.Text = d.RejectComment;
                btnShowSuggestion.Visible = true;
            }
        }
        private void SetProjectHistory()
        {
            PanelHistory.Visible = false;
            var hist = _projectInfoService.GetProjectHistoryList(ProjectID);
            if (hist.IsCompleted)
            {
                if (hist.Data.Count() > 0)
                {
                    PanelHistory.Visible = true;
                    LabelProjectHistory.Text = "";
                    foreach (ServiceModels.ProjectInfo.ProjectHistory ph in hist.Data)
                    {
                        LabelProjectHistory.Text += string.Format("{0}โดย {1} ({2}) วันที่ {3:dd/MM/yyyy hh:mm:ss}<br />",
                            ph.HistoryName , ph.userName , ph.History.IPADDRESS, ph.History.ENTRYDT);

                    }
                }
            }

        }
        private void BindData()
        {
            decimal projectID = ProjectID;

            if (projectID > 0)
            {
                HiddenFieldProjectID.Value = ProjectID.ToString();

                ServiceModels.ProjectInfo.TabProjectInfo modelProjectInfo = new ServiceModels.ProjectInfo.TabProjectInfo();
                var resultProjectInfo = _projectInfoService.GetProjectInformationByProjecctID(projectID);
                

                if (resultProjectInfo.IsCompleted)
                {
                    modelProjectInfo = resultProjectInfo.Data;
                    
                    if (modelProjectInfo != null)
                    {
                        ProjectRoles = modelProjectInfo.ProjectRole;
                        SetProjectHistory();
                        bool canViewData = ProjectRoles.Contains(Common.ProjectFunction.CanViewProjectInfo);
                        DataContainer.Visible = canViewData;
                        ApprovalStatus = modelProjectInfo.ApprovalStatus;
                        HasEvaluationInfo = modelProjectInfo.HasEvaluationInfo;
                        HasApprovalInfo = modelProjectInfo.HasApprovalInfo;
                        FollowupStatusCode = modelProjectInfo.FollowupStatusCode;
                        ProjectApprovalStatusCode = modelProjectInfo.ProjectApprovalStatusCode;
                        //kenghot18
                        BudgetAmount = modelProjectInfo.BudgetValue;
                        SetRejectedTab(modelProjectInfo);
                        if (canViewData)
                        {
                            string projectName = modelProjectInfo.ProjectInfoNameTH;
                            ShowProjectName(projectName);

                            LabelApprovalStatus.Text = modelProjectInfo.ProjectApprovalStatusName;                           
                        }
                        else
                        {
                            ShowErrorMessage(Nep.Project.Resources.Error.CanotViewProjectData);
                        }
                    }
                    else
                    {
                        ShowErrorMessage(Nep.Project.Resources.Message.NoRecord);
                    }
                }
            }

        }

        private void AddRequiredTabCss(List<string> dataNames)
        {
            if(dataNames != null){
                string dataName;
                for (int i = 0; i < dataNames.Count; i++ )
                {
                    dataName = dataNames[i];
                    if (dataName == "ProjectInformation")
                    {
                        TabContainerProjectInfoForm.Tabs[1].CssClass = "required";
                        //TabPanelAttachment
                        //TabPanelProjectInfo.HeaderTemplate(
                        //TabPanelProjectInfo.Attributes.Add("class", "required");
                    }
                    else if (dataName == "ProjectPersonel")
                    {
                        TabContainerProjectInfoForm.Tabs[2].CssClass = "required";
                        //TabPersonal.Attributes.Add("class", "required");
                    }                    
                    else if (dataName == "ProjectOperation")
                    {
                        TabContainerProjectInfoForm.Tabs[3].CssClass = "required";
                        //TabProcessingPlan.Attributes.Add("class", "required");
                    }
                    else if (dataName == "ProjectBudget")
                    {
                        TabContainerProjectInfoForm.Tabs[4].CssClass = "required";
                        //TabPanelProjectBudget.Attributes.Add("class", "required");
                 
                    }
                    else if (dataName == "ProjectDocument")
                    {
                        TabContainerProjectInfoForm.Tabs[5].CssClass = "required";
                        //TabPanelAttachment.Attributes.Add("class", "required");
                    }
                    
                }
            }
        }

    }
}