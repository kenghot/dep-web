using System;
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
    public partial class ProjectBudgetControlBackup : Nep.Project.Web.Infra.BaseUserControl
    {
        private string BUDGET_DETAIL_LIST_VIEWSTATE_KEY = "BUDGET_DETAIL_LIST";     
        public IServices.IProjectInfoService _service { get; set; }

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
            ButtonSendProjectInfo.Text = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Nep.Project.Resources.UI.ButtonSubmit : Nep.Project.Resources.UI.ButtonSendProjectInfo;

            BindDisplay();
            BindGridViewBudgetDetail();
            RegisterClientScript();
        }

        protected void BindDisplay()
        {
            try
            {
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
                        bool isEditable = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData)
                            || master.UserInfo.IsAdministrator);
                        IsEditableProjectBudget = isEditable;
                        IsRequestCenterBudget = model.IsRequestCenter;

                        ButtonSave.Visible = isEditable;
                        ButtonAddBudgetDetail.Visible = isEditable;
                        ButtonSendProjectInfo.Visible = (functions.Contains(Common.ProjectFunction.SaveDarft) && canSendProjectInfo);
                        ButtonDelete.Visible = functions.Contains(Common.ProjectFunction.Delete);
                        HyperLinkPrint.Visible = (functions.Contains(Common.ProjectFunction.PrintDataForm) && canSendProjectInfo);
                        ButtonReject.Visible = functions.Contains(Common.ProjectFunction.Reject);
                                              

                        TextBoxTotalAmount.Text = model.TotalRequestAmount.ToString();
                        TextBoxReviseAmount.Text = model.TotalReviseAmount.ToString();

                        if (model.IsBudgetGotSupport != null)
                        {
                            if (model.IsBudgetGotSupport == true)
                            {
                                RadioButtonGotSupportYes.Checked = true;
                                TextBoxGotSupportName.Text = model.BudgetGotSupportName;
                                TextBoxGotSupportAmount.Text = model.BudgetGotSupportAmount.ToString();
                            }
                            else
                            {
                                RadioButtonGotSupportNo.Checked = true;
                            }
                        }

                        if (model.ApprovalStatus == "1")
                        {
                            IsApproved = true;
                            DivBudgetDetailRequestAmount.Visible = true;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Budget", ex);
                ShowErrorMessage(ex.Message);
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            ServiceModels.ProjectInfo.ProjectBudget model = new ServiceModels.ProjectInfo.ProjectBudget();
            try
            {
                if(Page.IsValid){
                    decimal projectId = Convert.ToDecimal(HiddenFieldProjectID.Value);
                    decimal? totalAmount = Convert.ToDecimal(TextBoxTotalAmount.Text.Trim());
                    bool? supportFlag = null;
                    string supportName = string.Empty;
                    string tmpSupportAmount;
                    decimal? supportAmount = (decimal?)null;
                    if (RadioButtonGotSupportYes.Checked)
                    {
                        supportFlag = true;
                        supportName = TextBoxGotSupportName.Text.Trim();
                        tmpSupportAmount = TextBoxGotSupportAmount.Text;
                        supportAmount = Convert.ToDecimal(tmpSupportAmount);
                    }

                    if (RadioButtonGotSupportNo.Checked)
                    {
                        supportFlag = false;
                    }
                    

                    var viewStateDetail = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
                    List<ServiceModels.ProjectInfo.BudgetDetail> list = (viewStateDetail != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)viewStateDetail : new List<ServiceModels.ProjectInfo.BudgetDetail>();

                    model.ProjectID = projectId;
                    model.TotalRequestAmount = totalAmount;                  
                    model.IsBudgetGotSupport = supportFlag;
                    model.BudgetGotSupportName = supportName;
                    model.BudgetGotSupportAmount = supportAmount;
                    model.BudgetDetails = list;

                    var result = _service.SaveProjectBudget(model);
                    if (result.IsCompleted)
                    {
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
                    var sendDataToReviewResult = _service.SendDataToReview(projectID);
                    if (sendDataToReviewResult.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelProjectBudget");
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

        protected void ProjectBudgetDetailValidate(object source, ServerValidateEventArgs args)
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = null;
            if (obj != null)
            {
                list = (List<ServiceModels.ProjectInfo.BudgetDetail>)obj;
            }
            args.IsValid = ((list != null) && (list.Count > 0));
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
                foreach (var item in result.Data.BudgetDetails)
                {
                    ServiceModels.ProjectInfo.BudgetDetail d = new ServiceModels.ProjectInfo.BudgetDetail();
                    d.UID = Guid.NewGuid().ToString();
                    d.ProjectBudgetID = item.ProjectBudgetID;
                    d.No = item.No;
                    d.Detail = item.Detail;
                    d.Amount = item.Amount;
                    d.Revise1Amount = item.Revise1Amount;
                    d.Revise2Amount = item.Revise2Amount;
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

        protected void CustomValidatorGotSupportInfo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isChecked = RadioButtonGotSupportYes.Checked;
            string gotSupportName = TextBoxGotSupportName.Text;
            string gotSupportAmont = TextBoxGotSupportAmount.Text;
            string fieldName = String.Format("{0} และ {1}", Nep.Project.Resources.UI.LabelGotSupportName, Nep.Project.Resources.UI.LabelGotSupportAmount);
            string error = String.Format(Nep.Project.Resources.Error.RequiredField, fieldName);
            if (isChecked && (String.IsNullOrEmpty(gotSupportName) || String.IsNullOrEmpty(gotSupportAmont)))
            {
                args.IsValid = false;
                CustomValidator validator = (CustomValidator)source;
                validator.Text = error;
                validator.ErrorMessage = error;
            }
        }

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
            ";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelProjectBudget,
                       this.GetType(),
                       "ValidateProjectBudgetDetailScript",
                       validateScript,
                       true);

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
            
                String onloadScript = @"
                 $(function () {      
                    SetTabHeader(" + Common.Web.WebUtility.ToJSON(RequiredSubmitData) + @");
                 });
            
            ";
                ScriptManager.RegisterStartupScript(
                          UpdatePanelProjectBudget,
                          this.GetType(),
                          "UpdatePanelProjectBudgetPlancript",
                          onloadScript,
                          true);
            
           
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
    }
}