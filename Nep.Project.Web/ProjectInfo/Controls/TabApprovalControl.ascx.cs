using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Common;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabApprovalControl : Nep.Project.Web.Infra.BaseUserControl
    {
        private string BUDGET_DETAIL_LIST_VIEWSTATE_KEY = "APPROVAL_BUDGET_DETAIL_LIST";
        private string BUDGET_ACTIVITY_DETAIL_LIST_VIEWSTATE_KEY = "APPROVAL_BUDGET_ACTIVITY_DETAIL_LIST";
        public IServices.IProjectInfoService _projectService { get; set; }
        public IServices.IListOfValueService _listOfValueService { get; set; }

        public List<String> StatusCodeEditableList  {
            get
            {
                List<String> list = new List<String>();
                list.Add(Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_2_เจ้าหน้าที่พิจารณาเกณฑ์ประเมิน);
                list.Add(Common.LOVCode.Projectapprovalstatus.ขั้นตอนที่_4_อนุมัติโดยคณะกรรมการกลั่นกรอง);
                list.Add(Common.LOVCode.Projectapprovalstatus.ไม่อนุมัติโดยคณะกรรมการกลั่นกรอง);
                return list;
            }            
        }
        
        public String ProjectApprovalStatusCode
        {
            get
            {                
                return ViewState["ProjectApprovalStatusCode"].ToString();
            }
            set
            {
                ViewState["ProjectApprovalStatusCode"] = value;
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

        public Boolean? IsCenterReviseProject
        {
            get {
                bool? isCenter = (bool?)null;
                if (ViewState["IsCenterReviseProject"] != null)
                {
                    isCenter = Convert.ToBoolean(ViewState["IsCenterReviseProject"]);
                }
                return isCenter;
            }
            set {
                ViewState["IsCenterReviseProject"] = value;
            }
        }

        public Decimal TotalAmountRequest
        {
            get
            {
                decimal total = 0;
                if (ViewState["TotalAmountRequest"] != null)
                {
                    total = Convert.ToDecimal(ViewState["TotalAmountRequest"]);
                }
                return total;
            }
            set
            {
                ViewState["TotalAmountRequest"] = value;
            }
        }

        public Decimal TotalAmountRevise1
        {
            get {
                decimal total = 0;
                if (ViewState["TotalAmountRevise1"] != null)
                {
                    total = Convert.ToDecimal(ViewState["TotalAmountRevise1"]);
                }
                return total;
            }
            set {
                ViewState["TotalAmountRevise1"] = value;
            }
        }

        public Decimal TotalAmountRevise2
        {
            get
            {
                decimal total = 0;
                if (ViewState["TotalAmountRevise2"] != null)
                {
                    total = Convert.ToDecimal(ViewState["TotalAmountRevise2"]);
                }
                return total;
            }
            set
            {
                ViewState["TotalAmountRevise2"] = value;
            }
        }

        public Boolean IsApprovalDataEditable
        {
            get
            {
                bool isEdit = false;
                if (ViewState["IsApprovalDataEditable"] != null)
                {
                    isEdit = Convert.ToBoolean(ViewState["IsApprovalDataEditable"]);
                }

                return isEdit;
            }

            set
            {
                ViewState["IsApprovalDataEditable"] = value;
            }
        }

        protected void ButtonSaveApprovalProjectBudget_Click(object sender, EventArgs e)
        {
            try
            {
               
                
                if (Page.IsValid){
                    AddActivityDataToMainGrid();

                    ServiceModels.ProjectInfo.ProjectApprovalResult obj = GetData();
                    var result = _projectService.SaveProjectApprovalResult(obj);
                    if (result.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelProjectApproval");
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
                ShowErrorMessage(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
            }            

        }

        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    if (Page.IsPostBack)
        //    {
        //        string button = Global. GetPostBackControl(this).ID.ToString();
        //    }
        //}
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            #region for old data
            DateTime? approvalYearDate = DatePickerApprovalYear.SelectedDate;
            int approvalYear = (approvalYearDate != null) ? ((DateTime)approvalYearDate).Year : 0;
            if (IsGenerateProjectNo(approvalYear.ToString()))
            {
                TextBoxProjectNo.Enabled = false;
                TextBoxProjectNo.Attributes.Add("placeholder", Nep.Project.Resources.UI.LabelGeneratebySystem);
                CustomValidatorProjectNo.Enabled = false;
            }
            else
            {
                TextBoxProjectNo.Enabled = IsApprovalDataEditable;
                TextBoxProjectNo.Attributes.Remove("placeholder");
                CustomValidatorProjectNo.Enabled = true;
            }
            #endregion for old data


            #region for revise data
            
            bool isEditable = (StatusCodeEditableList.Contains(ProjectApprovalStatusCode));
            TextBoxApprovalNo.Enabled = isEditable;
            CustomValidatorApprovalNo.Enabled = isEditable;
            DatePickerApprovalYear.Enabled = isEditable;
            CustomValidatorDatePickerApprovalYear.Enabled = isEditable;
            DatePickerApprovalDate.Enabled = isEditable;
            CustomValidatorDatePickerApprovalDate.Enabled = isEditable;
            #endregion for revise data
        }

        public void BindData()
        {            
            var projectApprovalResult = _projectService.GetProjectApprovalResult(ProjectID);
            decimal? budgetTypeID = (decimal?)null;
            decimal provinceID = 0;
            if (projectApprovalResult.IsCompleted)
            {
                ServiceModels.ProjectInfo.ProjectApprovalResult obj = projectApprovalResult.Data;
                var act = _projectService.GetProjectBudgetInfoByProjectID(ProjectID);
                List<Common.ProjectFunction> functions = _projectService.GetProjectFunction(obj.ProjectID).Data;
                //var json =   Nep.Project.Common.Web.WebUtility.ToJSON(act); 
                bool isEditable = (functions.Contains(Common.ProjectFunction.SaveApproval) && IsEvaluationPass(obj.EvaluationIsPassAss4, obj.EvaluationIsPassAss5, obj.EvaluationScoreCode ));
                ButtonSaveApprovalProjectBudget.Visible = isEditable;
                IsApprovalDataEditable = isEditable;
                TotalAmountRequest = (decimal)obj.TotalBudgetRequest;
                ProjectApprovalStatusCode = obj.ProjectApprovalStatusCode;


                budgetTypeID = obj.BudgetTypeID;
                provinceID = obj.ProvinceID;

                if(!isEditable){
                    string gridViewCss = GridViewApprovalBudgetDetail.CssClass;
                    gridViewCss += " hide-command-column";
                    GridViewApprovalBudgetDetail.CssClass = gridViewCss;
                }               

                IsCenterReviseProject = obj.IsCenterReviseProject;
                //budgetTypeID = obj.bu

                //งบประมาณรวมทั้งโครงการ
                //LabelProjectNo.Text = Common.Web.WebUtility.DisplayInHtml(obj.ProjectNo , null,"-");
                //DisplayProjectNo(obj.ProjectNo, obj.ApprovalYear);

                TextBoxProjectNo.Text = obj.ProjectNo;

                LabelOrganizationName.Text = obj.OrganizationName;
                LabelProjectName.Text = obj.ProjectName;
                LabelBudgetRequest.Text = Common.Web.WebUtility.DisplayInHtml(obj.TotalBudgetRequest, "N2", "-");
            
                ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = obj.BudgetDetails;
                GridViewApprovalBudgetDetail.DataSource = obj.BudgetDetails;
                GridViewApprovalBudgetDetail.DataBind();

                ViewState[BUDGET_ACTIVITY_DETAIL_LIST_VIEWSTATE_KEY] = act.Data.BudgetActivities;
                GridViewActivity.DataSource = ViewState[BUDGET_ACTIVITY_DETAIL_LIST_VIEWSTATE_KEY];
                GridViewActivity.DataBind();
                //Infra.GridViewSort.GetSortDirection(GridViewApprovalBudgetDetail.ClientID, "Amount",SortDirection.Ascending);
                //GridViewApprovalBudgetDetail.Sort("Amount", SortDirection.Descending);
                //ผลการพิจารณา
                LabelAssessmentPropertyResult.Text = Common.Web.WebUtility.DisplayInHtml(obj.EvaluationStatusName, null, "-");
                LabelAssessmentScoreDesc.Text = String.Format(Nep.Project.Resources.Message.AssessmentScoreDesc, Common.Web.WebUtility.DisplayInHtml(obj.EvaluationScore, "N0", "-"));
                LabelAssessmentScoreResult.Text = Common.Web.WebUtility.DisplayInHtml(obj.EvaluationScoreDesc, null, "-");

                #region Approval Result
                if (obj.IsCenterReviseProject)
                {
                    //ผลการพิจารณาจากส่วนกลาง
                    //มติคณะทำงานกลั่นกรอง/ คณะอนุกรรมการตามประเภทความพิการ
                    PanelRevise1.Visible = true;
                    PanelRevise2.Visible = true;

                    TitleRevise1.InnerText = Nep.Project.Resources.UI.TitleDiscriminationTeamApprovalResult;

                    BindRevise1Result(obj.ApprovalStatusID1);
                    TextBoxRevise1Amount.Text = Common.Web.WebUtility.DisplayInForm(obj.ApprovalBudget1, "N2", "");
                    TextBoxRevise1Desc.Text = obj.ApprovalDesc1;
                    //TextBoxRevise1FirstName.Text = obj.ApprovalName1;
                    //TextBoxRevise1LastName.Text = obj.ApprovalLastName1;
                    //TextBoxRevise1Position.Text = obj.ApproverPosition1;
                    //DatePickerRevise1ApprovalDate.SelectedDate = obj.ApprovalDate1;

                    BindRevise2Result(obj.ApprovalStatusID2);
                    TextBoxRevise2Amount.Text = Common.Web.WebUtility.DisplayInForm(obj.ApprovalBudget2, "N2", "");
                    TextBoxRevise2Desc.Text = obj.ApprovalDesc2;
                    //TextBoxRevise2FirstName.Text = obj.ApprovalName2;
                    //TextBoxRevise2LastName.Text = obj.ApprovalLastName2;
                    //TextBoxRevise2Position.Text = obj.ApproverPosition2;
                    //DatePickerRevise2ApprovalDate.SelectedDate = obj.ApprovalDate2;
                   
                }
                else
                {
                    //ผลการพิจารณาคณะกรรมการจังหวัด          
                    PanelRevise1.Visible = true;
                    TitleRevise1.InnerText = Nep.Project.Resources.UI.TitleProvinceApprovalResult;
                    BindRevise1Result(obj.ApprovalStatusID1);

                    TextBoxRevise1Amount.Text = Common.Web.WebUtility.DisplayInForm(obj.ApprovalBudget1, "N2", "");
                    TextBoxRevise1Desc.Text = obj.ApprovalDesc1;
                    //TextBoxRevise1FirstName.Text = obj.ApprovalName1;
                    //TextBoxRevise1LastName.Text = obj.ApprovalLastName1;
                    //DatePickerRevise1ApprovalDate.SelectedDate = obj.ApprovalDate1;
                }

                //ครั้งที่
                TextBoxApprovalNo.Text = obj.ApprovalNo;
                DatePickerApprovalYear.SelectedDate = (!String.IsNullOrEmpty(obj.ApprovalYear)) ? new DateTime(Int32.Parse(obj.ApprovalYear), 1, 1) : (DateTime?)null;
                DatePickerApprovalDate.SelectedDate = obj.ApprovalDate;

                #endregion Approval Result
                            
            }
            else
            {                
                ShowErrorMessage(projectApprovalResult.Message);
            }

            BindDropDownListBudgetType(provinceID, budgetTypeID);
            RegisterClientScript();
        }

        private bool IsEvaluationPass(String evaluationIsPassAss4, String evaluationIsPassAss5, String evaluationScoreCode)
        {
            bool isPass = ((evaluationIsPassAss4 == Common.Constants.BOOLEAN_TRUE) && 
                (evaluationIsPassAss5 == Common.Constants.BOOLEAN_TRUE) &&
                (evaluationScoreCode == Common.LOVCode.Evaluationstatus.ผ่าน));
            return isPass;
        }

        private void BindDropDownListBudgetType(decimal provinceID, decimal? budgetTypeID)
        {
            var budgetTypeResult = _listOfValueService.ListBudgetType(provinceID, budgetTypeID);
            if (budgetTypeResult.IsCompleted)
            {
                List<ServiceModels.ListOfValue> list = budgetTypeResult.Data;
                list.Insert(0, new ServiceModels.ListOfValue { 
                    LovID = 0,
                    LovName = Project.Resources.UI.DropdownPleaseSelect
                });

                DropDownListBudgetType.DataSource = list;
                DropDownListBudgetType.DataBind();

                if(budgetTypeID.HasValue){
                    DropDownListBudgetType.SelectedValue = budgetTypeID.ToString();
                }
            }else{
                ShowErrorMessage(budgetTypeResult.Message);
            }
        }

        private void RebindGridviewData()
        {
            var budgetDetails = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            if (budgetDetails != null)
            {
                GridViewApprovalBudgetDetail.DataSource = (List<ServiceModels.ProjectInfo.BudgetDetail>)budgetDetails;
                GridViewApprovalBudgetDetail.DataBind();
            }
        }

        private ServiceModels.ProjectInfo.ProjectApprovalResult GetData()
        {
            ServiceModels.ProjectInfo.ProjectApprovalResult obj = new ServiceModels.ProjectInfo.ProjectApprovalResult();
            obj.ProjectID = ProjectID;
            String txtReviseAmount;
            obj.IsCenterReviseProject = (IsCenterReviseProject.HasValue)? (bool)IsCenterReviseProject : false;
            var revise1AprrovalResult = PanelRevise1.Controls.OfType<RadioButton>().Where(x => x.Checked).FirstOrDefault();
            if (revise1AprrovalResult != null)
            {                
                obj.ApprovalStatusID1 = Convert.ToDecimal(revise1AprrovalResult.Attributes["Value"]);
                obj.ApprovalBudget1 = (!String.IsNullOrEmpty(TextBoxRevise1Amount.Text))? Convert.ToDecimal(TextBoxRevise1Amount.Text): (decimal?)null;
            }
            txtReviseAmount = TextBoxRevise1Amount.Text.Trim();
            obj.BudgetTypeID = Convert.ToDecimal(DropDownListBudgetType.SelectedValue);
            obj.ApprovalBudget1 = (!String.IsNullOrEmpty(txtReviseAmount)) ? Decimal.Parse(txtReviseAmount) : (decimal?)null;

            obj.ApprovalDesc1 = TextBoxRevise1Desc.Text.TrimEnd();
            //obj.ApprovalName1 = TextBoxRevise1FirstName.Text.Trim();
            //obj.ApprovalLastName1 = TextBoxRevise1LastName.Text.Trim();
            //obj.ApproverPosition1 = TextBoxRevise1Position.Text.Trim();
            //obj.ApprovalDate1 = DatePickerRevise1ApprovalDate.SelectedDate;

            if(IsCenterReviseProject.Value == true){
                var revise2AprrovalResult = PanelRevise2.Controls.OfType<RadioButton>().Where(x => x.Checked).FirstOrDefault();
                if (revise2AprrovalResult != null)
                {
                    obj.ApprovalStatusID2 = Convert.ToDecimal(revise2AprrovalResult.Attributes["Value"]);
                    obj.ApprovalBudget2 = (!String.IsNullOrEmpty(TextBoxRevise2Amount.Text)) ? Convert.ToDecimal(TextBoxRevise2Amount.Text) : (decimal?)null;
                }

                obj.ApprovalDesc2 = TextBoxRevise2Desc.Text.TrimEnd();
                //obj.ApprovalName2 = TextBoxRevise2FirstName.Text.Trim();
                //obj.ApprovalLastName2 = TextBoxRevise2LastName.Text.Trim();
                //obj.ApproverPosition2 = TextBoxRevise2Position.Text.Trim();
                //obj.ApprovalDate2 = DatePickerRevise2ApprovalDate.SelectedDate;
            }
            

            obj.ApprovalNo = TextBoxApprovalNo.Text.Trim();
            obj.ApprovalYear = (DatePickerApprovalYear.SelectedDate.HasValue) ? (((DateTime)DatePickerApprovalYear.SelectedDate).Year.ToString()) : null;
            obj.ApprovalDate = DatePickerApprovalDate.SelectedDate;
            
            var budgetDetails = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            if(budgetDetails != null){
                obj.BudgetDetails = (List<ServiceModels.ProjectInfo.BudgetDetail>)budgetDetails;
            }

            obj.ipAddress = Request.UserHostAddress;
            obj.ProjectNo = TextBoxProjectNo.Text.Trim();           

            return obj;
        }

        #region BindData to RadioBox
        private void BindRevise1Result(decimal? selectedValue)
        {
            var statusResult = _listOfValueService.ListActive(Common.LOVGroup.ApprovalStatus1, selectedValue);
            decimal selectedID = (selectedValue.HasValue) ? (decimal)selectedValue : -1;
            CustomValidatorRevise1Amount.Enabled = true;
            CustomValidatorRevise1Desc.Enabled = true;
            if (statusResult.IsCompleted)
            {
                List<ServiceModels.ListOfValue> list = statusResult.Data;
                //list = list.Where(x => x.LovCode != Common.LOVCode.Approvalstatus1.ชะลอการพิจารณา).ToList();

                ServiceModels.ListOfValue status;
                for (int i = 0; i < list.Count; i++)
                {
                    status = list[i];
                    if (i == 0)
                    {
                        RadioButtonRevise1_1.Text = status.LovName;
                        RadioButtonRevise1_1.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise1_1.Checked = true;
                        }
                    }
                    else if (i == 1)
                    {
                        RadioButtonRevise1_2.Text = status.LovName;
                        RadioButtonRevise1_2.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise1_2.Checked = true;
                        }
                    }
                    else if (i==2)
                    {
                        RadioButtonRevise1_3.Text = status.LovName;
                        RadioButtonRevise1_3.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise1_3.Checked = true;
                        }
                    }
                    else if (i == 3)
                    {
                        RadioButtonRevise1_4.Text = status.LovName;
                        RadioButtonRevise1_4.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise1_4.Checked = true;
                        }
                    }
                    else if (i == 4)
                    {
                        RadioButtonRevise1_5.Text = status.LovName;
                        RadioButtonRevise1_5.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise1_5.Checked = true;
                        }
                    }
                    else if (i == 5)
                    {
                        RadioButtonRevise1_6.Text = status.LovName;
                        RadioButtonRevise1_6.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise1_6.Checked = true;
                        }
                    }
                }
            }
            else
            {
                ShowErrorMessage(statusResult.Message);
            }
        }

        private void BindRevise2Result(decimal? selectedValue)
        {
            var statusResult = _listOfValueService.ListActive(Common.LOVGroup.ApprovalStatus2, selectedValue);
            decimal selectedID = (selectedValue.HasValue) ? (decimal)selectedValue : -1;
            CustomValidatorRevise2Amount.Enabled = true;
            CustomValidatorRevise2Desc.Enabled = true;
            if (statusResult.IsCompleted)
            {
                List<ServiceModels.ListOfValue> list = statusResult.Data;
               // list = list.Where(x => x.LovCode != Common.LOVCode.Approvalstatus2.ชะลอการพิจารณา).ToList();
                ServiceModels.ListOfValue status;
                for (int i = 0; i < list.Count; i++)
                {
                    status = list[i];
                    if (i == 0)
                    {
                        RadioButtonRevise2_1.Text = status.LovName;
                        RadioButtonRevise2_1.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise2_1.Checked = true;
                        }
                    }
                    else if (i == 1)
                    {
                        RadioButtonRevise2_2.Text = status.LovName;
                        RadioButtonRevise2_2.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise2_2.Checked = true;
                        }
                    }
                    else if (i == 2)
                    {
                        RadioButtonRevise2_3.Text = status.LovName;
                        RadioButtonRevise2_3.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise2_3.Checked = true;
                        }
                    }

                    else if (i == 3)
                    {
                        RadioButtonRevise2_4.Text = status.LovName;
                        RadioButtonRevise2_4.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise2_4.Checked = true;
                        }
                    }
                    else if (i == 4)
                    {
                        RadioButtonRevise2_5.Text = status.LovName;
                        RadioButtonRevise2_5.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise2_5.Checked = true;
                        }
                    }
                    else if (i == 5)
                    {
                        RadioButtonRevise2_6.Text = status.LovName;
                        RadioButtonRevise2_6.Attributes.Add("Value", status.LovID.ToString());
                        if (selectedID == status.LovID)
                        {
                            RadioButtonRevise2_6.Checked = true;
                        }
                    }
                }
            }
            else
            {
                ShowErrorMessage(statusResult.Message);
            }
        }
        #endregion BindData to RadioBox

        #region Manage GridView
        protected void GridViewBudgetDetail_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            bool isCenter = (IsCenterReviseProject.HasValue) ? (bool)IsCenterReviseProject : false;    
          

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label labelRequestAmount = (Label)e.Row.Cells[0].FindControl("LabelRequestAmount");
                Label labelReviseAmount = (Label)e.Row.Cells[0].FindControl("LabelReviseAmount");

                Dictionary<string, decimal> totalAmount = GetTotalBudget();

                TotalAmountRevise1 = totalAmount["Revise1"];
                TotalAmountRevise2 = totalAmount["Revise2"];

                if ((labelRequestAmount != null) && (labelReviseAmount != null))
                {
                    labelRequestAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(totalAmount["Rquest"], "N2", "0.00");
                    labelReviseAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(totalAmount["Revise"], "N2", "0.00");
                    
                }

                if (isCenter)
                {
                    Label labelRevise1CenterAmount = (Label)e.Row.Cells[0].FindControl("LabelRevise1CenterAmount");
                    Label labelRevise2CenterAmount = (Label)e.Row.Cells[0].FindControl("LabelRevise2CenterAmount");



                    if ((labelRevise1CenterAmount != null) && (labelRevise2CenterAmount != null))
                    {
                        
                        labelRevise1CenterAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(totalAmount["Revise1"], "N2", "0.00");
                        labelRevise2CenterAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(totalAmount["Revise2"], "N2", "0.00");
                    }
                }
                else
                {
                    Label labelRevise1ProvinceAmount = (Label)e.Row.Cells[0].FindControl("LabelRevise1ProvinceAmount");
                    if (labelRevise1ProvinceAmount != null)
                    {
                        labelRevise1ProvinceAmount.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(totalAmount["Revise1"], "N2", "0.00");
                    }
                    
                }  
            }            
                       
        }
        //kenghot
        protected List<ServiceModels.ProjectInfo.BudgetDetail> SortBudget(SortDirection sd, string se)
        {
            // Have we generated data before?
            List<ServiceModels.ProjectInfo.BudgetDetail> data = (List < ServiceModels.ProjectInfo.BudgetDetail > )ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            if (data != null && data.Count() > 0)
            {
                //// Create a sample DB
                //SimulatedDB = new List<MyObject>();
                //var rnd = new Random();

                //for (int i = 0; i < 20; i++)
                //{
                //    var node = new MyObject();
                //    node.Id = i;
                //    node.Name = String.Format("Name {0}", i);
                //    node.CreationDate = DateTime.Now.AddDays(rnd.Next(100));
                //    node.Amount = (rnd.Next(1000) * rnd.NextDouble());

                //    SimulatedDB.Add(node);
                //}
                // Return sorted list
                if (sd == SortDirection.Ascending)
                    // return data.AsQueryable<ServiceModels.ProjectInfo.BudgetDetail>().OrderBy<ServiceModels.ProjectInfo.BudgetDetail>(se).ToList();
                    if (se == "Amount")
                    return data.OrderBy(o => o.Amount).ToList();
                    else
                     return data.OrderBy(o => o.ReviseDetail ).ToList();
                else
                    if (se == "Amount")
                       return data.OrderByDescending(o => o.Amount).ToList();
                    else
                       return data.OrderByDescending(o => o.ReviseDetail).ToList();
                //  return data.AsQueryable<ServiceModels.ProjectInfo.BudgetDetail>().OrderByDescending<MyObject>(se).ToList();
            }
            return null; 
         
        }
        //kenghot
        protected void GridViewApprovalBudgetDetail_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView g = (GridView) sender;
            SortDirection sd = Infra.GridViewSort.GetSortDirection(g.ClientID, e.SortExpression, null);
            GridViewApprovalBudgetDetail.DataSource = SortBudget( sd, e.SortExpression);   
            GridViewApprovalBudgetDetail.DataBind();
        }

        protected void GridViewApprovalBudgetDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {           
            GridViewApprovalBudgetDetail.EditIndex = e.NewEditIndex;         
            RebindBudgetDetailDataSource();

            var editRow = GridViewApprovalBudgetDetail.Rows[GridViewApprovalBudgetDetail.EditIndex];
            if (IsCenterReviseProject.HasValue && (IsCenterReviseProject == true))
            {
                CustomValidator validatorRevise1Amount = (CustomValidator)editRow.FindControl("CustomValidatorRevise1Amount");
                validatorRevise1Amount.Enabled = true;

                CustomValidator validatorRevise2Amount = (CustomValidator)editRow.FindControl("CustomValidatorRevise2Amount");
                validatorRevise2Amount.Enabled = true;
            }
            else
            {
                RequiredFieldValidator requiredAmount = (RequiredFieldValidator)editRow.FindControl("RequiredFieldValidatorAmount");
                requiredAmount.Enabled = true;

                CustomValidator validatorReviseAmount = (CustomValidator)editRow.FindControl("CustomValidatorProvinceReviseAmount");
                validatorReviseAmount.Enabled = true;

            }
        }

        protected void GridViewApprovalBudgetDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();
            if (list.Count > 0)
            {
                GridViewApprovalBudgetDetail.EditIndex = -1;
                RebindBudgetDetailDataSource();
            }
            
        }

        protected void GridViewApprovalBudgetDetail_RowCommand(object sender, GridViewCommandEventArgs e){
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null)? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();
            ServiceModels.ProjectInfo.BudgetDetail tempItem;

            Page.Validate("SaveBudgetApprovalDetail");
            if ((e.CommandName == "save") && Page.IsValid)
            {
                String txtProjectBudgetID = GridViewApprovalBudgetDetail.DataKeys[GridViewApprovalBudgetDetail.EditIndex].Values["ProjectBudgetID"].ToString();
                decimal projectBudgetID = Decimal.Parse(txtProjectBudgetID);
                var editRow = GridViewApprovalBudgetDetail.Rows[GridViewApprovalBudgetDetail.EditIndex];

                TextBox textBoxBudgetProvinceCommitteeAmount = (TextBox)editRow.FindControl("TextBoxBudgetProvinceCommitteeAmount");

                TextBox textBoxBudgetDiscriminationTeamAmount = (TextBox)editRow.FindControl("TextBoxBudgetDiscriminationTeamAmount");
                TextBox textBoxBudgetSubcommitteeAmount = (TextBox)editRow.FindControl("TextBoxBudgetSubcommitteeAmount");
                TextBox textBoxRemark = (TextBox)editRow.FindControl("TextBoxApprovalRemark");

                ServiceModels.ProjectInfo.BudgetDetail oldItem = list.Where(x => x.ProjectBudgetID == projectBudgetID).FirstOrDefault();
                ServiceModels.ProjectInfo.BudgetDetail editItem = (oldItem != null)? oldItem :new ServiceModels.ProjectInfo.BudgetDetail();
                editItem.ApprovalRemark = textBoxRemark.Text;

                if (IsCenterReviseProject.HasValue && (IsCenterReviseProject == true))
                {
                    editItem.Revise1Amount = (!String.IsNullOrEmpty(textBoxBudgetDiscriminationTeamAmount.Text)) ? (Decimal.Parse(textBoxBudgetDiscriminationTeamAmount.Text)) : (decimal?)null;
                    editItem.Revise2Amount = (!String.IsNullOrEmpty(textBoxBudgetSubcommitteeAmount.Text)) ? (Decimal.Parse(textBoxBudgetSubcommitteeAmount.Text)) : (decimal?)null;
                }
                else
                {
                    editItem.Revise1Amount = (!String.IsNullOrEmpty(textBoxBudgetProvinceCommitteeAmount.Text)) ? (Decimal.Parse(textBoxBudgetProvinceCommitteeAmount.Text)) : (decimal?)null;
                }
                              

                if ( oldItem != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        tempItem = list[i];
                        if (tempItem.ProjectBudgetID == projectBudgetID)
                        {
                            list[i] = editItem;
                            break;
                        }
                    }
                }


                UpdateTotalProjectBudget(list);

                GridViewApprovalBudgetDetail.EditIndex = -1;
                ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = list;
                GridViewApprovalBudgetDetail.DataSource = list;
                GridViewApprovalBudgetDetail.DataBind();

            }           

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

        #endregion Manage GridView
        public void UpdateTotalProjectBudget()
        {
            BindingBudgetDetailGridView();
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (List<ServiceModels.ProjectInfo.BudgetDetail>)ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            decimal total1 = 0, total2 = 0;
            decimal budget1 = 0, budget2 = 0;
            if (list != null)
            {
                ServiceModels.ProjectInfo.BudgetDetail item;

                for (int i = 0; i < list.Count; i++)
                {
                    item = list[i];
                    budget1 = (item.Revise1Amount.HasValue) ? (decimal)item.Revise1Amount : 0;
                    budget2 = (item.Revise2Amount.HasValue) ? (decimal)item.Revise2Amount : 0;

                    total1 += budget1;
                    total2 += budget2;
                }
            }

            TextBoxRevise1Amount.Text = (RadioButtonRevise1_2.Checked) ? Common.Web.WebUtility.DisplayInForm(total1, "N2", "") : "";
            TextBoxRevise2Amount.Text = (RadioButtonRevise2_2.Checked) ? Common.Web.WebUtility.DisplayInForm(total2, "N2", "") : "";
        }

        public void UpdateTotalProjectBudget(List<ServiceModels.ProjectInfo.BudgetDetail> list)
        {
            AddActivityDataToMainGrid();
            decimal total1 = 0, total2 = 0;
            decimal budget1 = 0, budget2 = 0;
            if (list != null)
            {
                ServiceModels.ProjectInfo.BudgetDetail item;
                
                for (int i = 0; i < list.Count; i++)
                {
                    item = list[i];
                    budget1 = (item.Revise1Amount.HasValue) ? (decimal)item.Revise1Amount : 0;
                    budget2 = (item.Revise2Amount.HasValue) ? (decimal)item.Revise2Amount : 0;

                    total1 += budget1;
                    total2 += budget2;
                }
            }

            TextBoxRevise1Amount.Text = (RadioButtonRevise1_2.Checked)? Common.Web.WebUtility.DisplayInForm(total1, "N2", "") : "";
            TextBoxRevise2Amount.Text = (RadioButtonRevise2_2.Checked)? Common.Web.WebUtility.DisplayInForm(total2, "N2", "") : "";
        }

        private void RebindBudgetDetailDataSource()
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();
            
            ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = list;
            GridViewApprovalBudgetDetail.DataSource = list;
            GridViewApprovalBudgetDetail.DataBind();
        }
            
        protected void BudgetDetailButtonCancel_Click(object sender, EventArgs e)
        {
            //BindGridViewApprovalBudgetDetail();
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

            GridViewApprovalBudgetDetail.DataSource = list;
            GridViewApprovalBudgetDetail.EditIndex = 0;
            GridViewApprovalBudgetDetail.DataBind();
            
        }
        
        #region ValidateApprovedWithCondition
        protected void ValidateRevise1Amount(object source, ServerValidateEventArgs args)
        {
            string error = ValidateReviseAmount("Revise1ApprovalResult");
            if (!String.IsNullOrEmpty(error))
            {
                CustomValidator validator = source as CustomValidator;
                args.IsValid = false;                
                validator.Text = error;
                validator.ErrorMessage = error;
            }
        }

        protected void ValidateRevise2Amount(object source, ServerValidateEventArgs args)
        {
            string error = ValidateReviseAmount("Revise2ApprovalResult");
            if (!String.IsNullOrEmpty(error))
            {
                CustomValidator validator = source as CustomValidator;
                args.IsValid = false;
                validator.Text = error;
                validator.ErrorMessage = error;
            }
        }

        private string ValidateReviseAmount(string groupName)
        {           
            
            RadioButton radio;
            string text;
            string error = "";
            decimal amount = 0;
            bool isParseAmount;
            string fieldName = "";
            decimal totalRequestBudget = TotalAmountRequest;

           
            if (groupName == "Revise1ApprovalResult")
            {
                radio = RadioButtonRevise1_2;
                text = TextBoxRevise1Amount.Text.Trim();
                isParseAmount = Decimal.TryParse(text, out amount);

                
                fieldName = (IsCenterReviseProject == true) ? Nep.Project.Resources.Model.ProjectBudgetDetail_Revise1CenterTotalAmount : Nep.Project.Resources.Model.ProjectBudgetDetail_Revise1ProvinceTotalAmount;
                if ((radio.Checked) && ((String.IsNullOrEmpty(text)) || (!isParseAmount)))
                {
                    error = String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectApproval_Budget);
                }
                else if ((radio.Checked) && (amount >= totalRequestBudget))
                {
                    error = String.Format(Nep.Project.Resources.Error.LessThan, fieldName, Nep.Project.Resources.Model.ProjectInfo_BudgetRequest);
                }
            }
            else if (groupName == "Revise2ApprovalResult")
            {
                radio = RadioButtonRevise2_2;
                text = TextBoxRevise2Amount.Text.Trim();
                isParseAmount = Decimal.TryParse(text, out amount);
                if ((radio.Checked) && ((String.IsNullOrEmpty(text)) || (!isParseAmount)))
                {
                    error = String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectApproval_Budget);
                }
                else if ((radio.Checked) && (amount >= totalRequestBudget))
                {
                    error = String.Format(Nep.Project.Resources.Error.LessThan, Nep.Project.Resources.Model.ProjectBudgetDetail_Revise2TotalAmount, Nep.Project.Resources.Model.ProjectInfo_BudgetRequest);
                }
            }


            return error;
        }

        #endregion ValidateApprovedWithCondition

        #region ValidateApprovalResult
        protected void ValidateRvise1Approved(object source, ServerValidateEventArgs args)
        {
            bool isValid = ValidateRadioButtonApproved("Revise1ApprovalResult");
            if ((!String.IsNullOrEmpty(TextBoxApprovalNo.Text.Trim())) &&
                (DatePickerApprovalYear.SelectedDate.HasValue) &&
                (DatePickerApprovalDate.SelectedDate.HasValue) && (!isValid))
            {
                isValid = false;
            }

            String error = "";
            if ((!isValid) && IsCenterReviseProject.HasValue && (IsCenterReviseProject == true))
            {
                error = String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.TitleDiscriminationTeamApprovalResult);
            }
            else if ((!isValid) && IsCenterReviseProject.HasValue && (IsCenterReviseProject == false))
            {
                error = String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.UI.TitleProvinceApprovalResult);
            }
            CustomValidator validator = source as CustomValidator;
            validator.Text = error;
            validator.ErrorMessage = error;

            args.IsValid = (String.IsNullOrEmpty(error));
        }

        protected void ValidateRvise2Approved(object source, ServerValidateEventArgs args)
        {

            bool isValid = ValidateRadioButtonApproved("Revise2ApprovalResult");
            if ((!String.IsNullOrEmpty(TextBoxApprovalNo.Text.Trim())) &&
                (DatePickerApprovalYear.SelectedDate.HasValue) &&
                (DatePickerApprovalDate.SelectedDate.HasValue) && (!isValid))
            {
                isValid = false;
            }
            args.IsValid = isValid;
        }

        private bool ValidateRadioButtonApproved(string groupName)
        {
            bool isValid = false;
            if ((groupName == "Revise1ApprovalResult") && 
                (RadioButtonRevise1_1.Checked || RadioButtonRevise1_2.Checked || RadioButtonRevise1_3.Checked ||
                RadioButtonRevise1_4.Checked || RadioButtonRevise1_5.Checked || RadioButtonRevise1_6.Checked))
            {
                isValid = true;
            }
            else if ((groupName == "Revise2ApprovalResult") && 
                (RadioButtonRevise2_1.Checked || RadioButtonRevise2_2.Checked || RadioButtonRevise2_3.Checked || 
                RadioButtonRevise2_4.Checked || RadioButtonRevise2_5.Checked || RadioButtonRevise2_6.Checked))
            {
                isValid = true;
            }            

            return isValid;     
        }
        #endregion ValidateApprovalResult

        #region Validate Approval No Detail
        protected void CustomValidatorApprovalNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isValid = true;
            if(IsRequiredApprovalNoDetail()){
                isValid = (!String.IsNullOrEmpty(TextBoxApprovalNo.Text.Trim()));
            }
            args.IsValid = isValid;
        }

        protected void CustomValidatorDatePickerApprovalYear_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isValid = true;
            if (IsRequiredApprovalNoDetail())
            {
                isValid = (DatePickerApprovalYear.SelectedDate.HasValue);
            }
            args.IsValid = isValid;
        }

        protected void CustomValidatorDatePickerApprovalDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isValid = true;
            if (IsRequiredApprovalNoDetail())
            {
                isValid = (DatePickerApprovalDate.SelectedDate.HasValue);
            }
            args.IsValid = isValid;
        }
        private void AddActivityDataToMainGrid()
        {
            List<ServiceModels.ProjectInfo.BudgetDetail> budgetDetails = (List<ServiceModels.ProjectInfo.BudgetDetail>)ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            if (budgetDetails != null)
            {
                budgetDetails.Clear();

                foreach (GridViewRow row in GridViewActivity.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        ApproveBudgetGridControl grid = (ApproveBudgetGridControl)row.FindControl("ApproveBudgetGridControl");
                        if (grid != null)
                        {
                            budgetDetails.AddRange(grid.BudgetDetailData);
                        }
                    }
                }
                ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = budgetDetails;
            }
        }
        public void BindingBudgetDetailGridView()
        {
            AddActivityDataToMainGrid();
            GridViewApprovalBudgetDetail.DataSource = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            GridViewApprovalBudgetDetail.DataBind();
        }
        protected void CustomValidatorApprovalBudgetDetail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            
            String errorText = "";
            string fileName;
            AddActivityDataToMainGrid();
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            var isApproved = (RadioButtonRevise1_3.Checked || RadioButtonRevise2_3.Checked)? false : true;
            if(isApproved){
                if (obj != null)
                {
                    bool isCheckRevise1 = (RadioButtonRevise1_1.Checked || RadioButtonRevise1_2.Checked);
                    bool isCheckRevise2 = (RadioButtonRevise2_1.Checked || RadioButtonRevise2_2.Checked);
                    List<ServiceModels.ProjectInfo.BudgetDetail> list = (List<ServiceModels.ProjectInfo.BudgetDetail>)obj;
                    ServiceModels.ProjectInfo.BudgetDetail budget;
                    for (int i = 0; i < list.Count; i++)
                    {
                        budget = list[i];
                        fileName = (IsCenterReviseProject == true) ? Nep.Project.Resources.Model.ProjectBudgetDetail_Revise1CenterAmount :
                                Nep.Project.Resources.Model.ProjectBudgetDetail_Revise1ProvinceAmount;

                        if (isCheckRevise1 && (!RadioButtonRevise1_2.Checked) && ((!budget.Revise1Amount.HasValue) || (budget.Revise1Amount.HasValue && budget.Revise1Amount <= 0)))
                        {

                            errorText = String.Format(Nep.Project.Resources.Error.RequiredField, fileName);
                            break;
                        }
                        else if (isCheckRevise2 && (!RadioButtonRevise2_2.Checked) && ((!budget.Revise2Amount.HasValue) || (budget.Revise2Amount.HasValue && budget.Revise2Amount <= 0)))
                        {
                            errorText = String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectBudgetDetail_Revise2Amount);
                            break;
                        }

                        // อนุมัติ ตามวงเงินที่โครงการขอสนับสนุน
                        if (isCheckRevise1 && RadioButtonRevise1_1.Checked && (budget.Revise1Amount.Value != budget.Amount))
                        {
                            errorText = String.Format(Nep.Project.Resources.Error.ValidateReviseBudgetAmount, fileName);
                            break;
                        }
                        else if (isCheckRevise2 && RadioButtonRevise2_1.Checked && (budget.Revise2Amount.Value != budget.Amount))
                        {
                            errorText = String.Format(Nep.Project.Resources.Error.ValidateReviseBudgetAmount, Nep.Project.Resources.Model.ProjectBudgetDetail_Revise2Amount);
                            break;
                        }

                    }
                }
                else
                {
                    errorText = Nep.Project.Resources.Error.ValidateApprovalBudgetDetail;
                }
                CustomValidator valiator = source as CustomValidator;
                valiator.Text = errorText;
                valiator.ErrorMessage = errorText;
            }
            
            
            args.IsValid = (String.IsNullOrEmpty(errorText));
        }
                

        private bool IsRequiredApprovalNoDetail()
        {
            bool isRequired = false;

            if (IsCenterReviseProject.HasValue && (IsCenterReviseProject == true))
            {
                if( (RadioButtonRevise1_1.Checked ||
                     RadioButtonRevise1_2.Checked ||
                     RadioButtonRevise1_3.Checked) &&
                    (RadioButtonRevise2_1.Checked ||
                    RadioButtonRevise2_2.Checked ||
                    RadioButtonRevise2_3.Checked))
                {
                    isRequired = true;
                }
            }
            else if (IsCenterReviseProject.HasValue && (IsCenterReviseProject == false)) 
            {
                if (RadioButtonRevise1_1.Checked ||
                     RadioButtonRevise1_2.Checked ||
                     RadioButtonRevise1_3.Checked)
                {
                     isRequired = true;
                }
            }
            return isRequired;
        }
        #endregion Validate Approval No Detail


        private void RegisterClientScript()
        {
            String searchSmountInput = "input[class*=\"form-control\"]";
            String script = @"
                function validateReviseAmount(oSrc, args) {
                    var requiredError = '" + String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.ProjectApproval_Budget) +@"';
                    var requiredChecked = '"+ String.Format(Nep.Project.Resources.Error.ValidateReviseBudgetApproval) + @"';
                    var errorText = '';

                    var isValid = true;
                    var parent = $(oSrc).closest('div');
                    var radioChecked = $(parent).find('input:checked');
                    var amountInput = $(parent).find('"+ searchSmountInput + @"');
                    var amountInputValue = $(amountInput).val();
                    amountInputValue = amountInputValue.replace(/,/g, '');                
                
                    if (radioChecked.prop('checked') && ((amountInputValue =='' ) || (isNaN(amountInputValue)))) {
                        isValid = false;
                        errorText = requiredError;
                    } else if ((typeof (radioChecked.prop('checked')) == 'undefined') && (amountInputValue != '')) {
                        isValid = false;
                        errorText = requiredChecked;
                    }

                    $(oSrc).text(errorText);               
                    args.IsValid = isValid;
                }  

                function onApprovalYearChanged(sender, args) {
                   
                    var approvalYearDatePicker = $find('DatePickerApprovalYear');

                    if(approvalYearDatePicker != null){
                        var approvalYearDate = approvalYearDatePicker.get_selectedDate();
     
                        var isGen = false;    
                        var approvalYear = (approvalYearDate != null)? approvalYearDate.getFullYear() : 0;
                    
                        var txtId = '" + TextBoxProjectNo.ClientID + @"';                    
                        if(approvalYear > 2016){
                            $('#'+ txtId).val('');     
                            $('#'+ txtId).attr('disabled', 'disabled');
                            $('#'+ txtId).attr('placeholder', '" + Nep.Project.Resources.UI.LabelGeneratebySystem + @"');                      
                        }else{
                            $('#'+ txtId).removeAttr('disabled');
                            $('#'+ txtId).removeAttr('placeholder');                       
                        } 
                    }                   
                    
                }

                function validateProjectNo(oSrc, args){

                    var isValid = false;
                    var approvalYearDatePicker = $find('DatePickerApprovalYear');  
                    var approvalYearDate = approvalYearDatePicker.get_selectedDate();
                    var approvalYear = (approvalYearDate != null)? approvalYearDate.getFullYear() : 0;
                    var isGen = (approvalYear > 2016);
                   
                    isValid = ((isGen && (args.Value == '')) || (!isGen && args.Value != '')) ;
                    args.IsValid = isValid;  
                }
            ";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelApprovalProjectBudget,
                       this.GetType(),
                       "ValidateReviseAmountScript",
                       script,
                       true);
             
        }       

        protected void CustomValidatorRevise1Desc_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isValid = true;
            if (RadioButtonRevise1_3.Checked || RadioButtonRevise1_4.Checked || 
                RadioButtonRevise1_5.Checked || RadioButtonRevise1_6.Checked)
            {
                string tmp = TextBoxRevise1Desc.Text.TrimEnd();
                isValid = (tmp.Length > 0);
            }

            args.IsValid = isValid;
        }

        protected void CustomValidatorRevise2Desc_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isValid = true;
            if (RadioButtonRevise2_3.Checked || RadioButtonRevise2_4.Checked ||
                RadioButtonRevise2_5.Checked || RadioButtonRevise2_6.Checked)
            {
                string tmp = TextBoxRevise2Desc.Text.TrimEnd();
                isValid = (tmp.Length > 0);
            }

            args.IsValid = isValid;
        }

        protected void RadioButtonRevise1CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonRevise1_2.Checked)
            {
                Dictionary<string, decimal> budget = GetTotalBudget();
                TextBoxRevise1Amount.Text = budget["Revise1"].ToString();                
            }
            else
            {
                TextBoxRevise1Amount.Text = "";
            }
        }

        protected void RadioButtonRevise2CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonRevise2_2.Checked)
            {
                Dictionary<string, decimal> budget = GetTotalBudget();
                TextBoxRevise2Amount.Text = budget["Revise2"].ToString();
            }
            else
            {
                TextBoxRevise2Amount.Text = "";
            }
        }

        protected void CustomValidatorProvinceReviseAmount_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var editRow = GridViewApprovalBudgetDetail.Rows[GridViewApprovalBudgetDetail.EditIndex];
            HiddenField textBoxRquestAmount = (HiddenField)editRow.FindControl("HiddenFieldRequestAmount");
            TextBox textBoxReviseAmount = (TextBox)editRow.FindControl("TextBoxBudgetProvinceCommitteeAmount");
            decimal requestAmount = 0;
            decimal reviseAmount = 0;
            if (!String.IsNullOrEmpty(textBoxReviseAmount.Text))
            {
                Decimal.TryParse(textBoxRquestAmount.Value, out requestAmount);
                Decimal.TryParse(textBoxReviseAmount.Text, out reviseAmount);

                args.IsValid = (reviseAmount <= requestAmount);
            }
        }

        protected void CustomValidatorRevise1Amount_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var editRow = GridViewApprovalBudgetDetail.Rows[GridViewApprovalBudgetDetail.EditIndex];
            HiddenField textBoxRquestAmount = (HiddenField)editRow.FindControl("HiddenFieldRequestAmount");
            TextBox textBoxReviseAmount = (TextBox)editRow.FindControl("TextBoxBudgetDiscriminationTeamAmount");
            decimal requestAmount = 0;
            decimal reviseAmount = 0;
            if (!String.IsNullOrEmpty(textBoxReviseAmount.Text))
            {
                Decimal.TryParse(textBoxRquestAmount.Value, out requestAmount);
                Decimal.TryParse(textBoxReviseAmount.Text, out reviseAmount);

                args.IsValid = (reviseAmount <= requestAmount);
            }
        }

        protected void CustomValidatorRevise2Amount_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var editRow = GridViewApprovalBudgetDetail.Rows[GridViewApprovalBudgetDetail.EditIndex];
            HiddenField textBoxRquestAmount = (HiddenField)editRow.FindControl("HiddenFieldRequestAmount");
            TextBox textBoxReviseAmount = (TextBox)editRow.FindControl("TextBoxBudgetSubcommitteeAmount");
            decimal requestAmount = 0;
            decimal reviseAmount = 0;
            if (!String.IsNullOrEmpty(textBoxReviseAmount.Text))
            {
                Decimal.TryParse(textBoxRquestAmount.Value, out requestAmount);
                Decimal.TryParse(textBoxReviseAmount.Text, out reviseAmount);

                args.IsValid = (reviseAmount <= requestAmount);
            }
        }

        //private void DisplayProjectNo(String projectNo, String approvalYear)
        //{
        //    bool isGen = IsGenerateProjectNo(approvalYear);            

        //    if (isGen)
        //    {
        //        TextBoxProjectNo.Enabled = false;
        //    }else
        //    {
        //        TextBoxProjectNo.Enabled = true;
        //    }

        //    TextBoxProjectNo.Text = projectNo;

        //}
          
        
        private bool IsGenerateProjectNo(String approvalYear)
        {
            bool isGen = false;
            if (!String.IsNullOrEmpty(approvalYear))
            {
                int year = 0;
                Int32.TryParse(approvalYear, out year);
                isGen = (year > 2016);
            }
            return isGen;
        }     

        protected void CustomValidatorProjectNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isGen = false;
            DateTime? tmpApprovedYear = DatePickerApprovalYear.SelectedDate;

            if (tmpApprovedYear != null)
            {
                int approvedYear = ((DateTime)tmpApprovedYear).Year;                

                isGen = (approvedYear > 2016);
            }

            args.IsValid = (isGen && args.Value == "") || (!isGen && args.Value != "");

        }
 

        protected void GridViewActivity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelActivityName = (Label)e.Row.FindControl("LabelActivityName");
                HiddenField HiddenActivitID = (HiddenField)e.Row.FindControl("HiddenActivitID");
                ApproveBudgetGridControl g = (ApproveBudgetGridControl)e.Row.FindControl("ApproveBudgetGridControl");
                List<ServiceModels.ProjectInfo.BudgetDetail> obj = (List < ServiceModels.ProjectInfo.BudgetDetail > )ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
                if (obj != null)
                {  string id = (string.IsNullOrEmpty(HiddenActivitID.Value)) ? "0" : HiddenActivitID.Value;
                    
                    g.RefreshData(obj, decimal.Parse(id), IsCenterReviseProject);
                }

            }
        }
        public void UpdateApproveBudgetTotal()
        {
            BindingBudgetDetailGridView();
            UpdateTotalProjectBudget();
        }

        protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
        {
            BindingBudgetDetailGridView();
            UpdateTotalProjectBudget();
        }
    }
}