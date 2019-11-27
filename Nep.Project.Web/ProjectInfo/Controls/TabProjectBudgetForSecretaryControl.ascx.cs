using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using AjaxControlToolkit;
using Nep.Project.Common;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabProjectBudgetForSecretaryControl : Nep.Project.Web.Infra.BaseUserControl
    {
        private string BUDGET_DETAIL_LIST_VIEWSTATE_KEY = "BUDGET_DETAIL_LIST";
        
        public IServices.IProjectInfoService _service { get; set; }

        public Boolean IsEditableProjectReviseBudget
        {
            get
            {
                bool isEdit = false;
                if (ViewState["IsEditableProjectReviseBudget"] != null)
                {
                    isEdit = Convert.ToBoolean(ViewState["IsEditableProjectReviseBudget"]);
                }

                return isEdit;
            }
            set
            {
                ViewState["IsEditableProjectReviseBudget"] = value;
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

        public void BindData()
        {
            BindDisplay();
            BindGridViewBudgetDetail();
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
                        List<Common.ProjectFunction> functions = _service.GetProjectFunction(model.ProjectID).Data;
                        //kenghot
                        //bool isEditable = functions.Contains(Common.ProjectFunction.ReviseBudget);
                        var master = (this.Page.Master as MasterPages.SiteMaster);
                        bool isEditable = (functions.Contains(Common.ProjectFunction.ReviseBudget) || master.UserInfo.IsAdministrator);                           
                        ButtonSave.Visible = isEditable;
                        //HyperLinkPrint.Visible = (functions.Contains(Common.ProjectFunction.PrintBudget) && model.CanSendProjectInfo);
                        IsEditableProjectReviseBudget = isEditable;

                        TextBoxTotalAmount.Text = model.TotalRequestAmount.ToString();
                        HiddenFieldReviseAmount.Value = model.TotalReviseAmount.ToString();
                        TextBoxTotalReviseAmount.Text = model.TotalReviseAmount.ToString();

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
                    decimal? totalRequestAmount = Convert.ToDecimal(TextBoxTotalAmount.Text);
                    decimal? totalReviseAmount = Convert.ToDecimal(HiddenFieldReviseAmount.Value);
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

                    model.TotalRequestAmount = totalRequestAmount;
                    model.TotalReviseAmount = totalReviseAmount;
                    model.IsBudgetGotSupport = supportFlag;
                    model.BudgetGotSupportName = supportName;
                    model.BudgetGotSupportAmount = supportAmount;
                    model.BudgetDetails = list;

                    var result = _service.SaveProjectBudget(model);
                    if (result.IsCompleted)
                    {
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
                    var result = _service.DeleteProjectBudgetByID(projectId);
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
                    d.ReviseDetail = item.ReviseDetail;
                    d.ReviseAmount = item.ReviseAmount;
                    d.ReviseRemark = item.ReviseRemark;
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
            if (!IsEditableProjectReviseBudget)
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //if (IsEditableProjectReviseBudget)
                //{
                //    e.Row.Cells[0].ColumnSpan = 2;
                //    e.Row.Cells[1].Visible = false;
                //}
                //else
                //{
                //    //e.Row.Cells[0].ColumnSpan = 2;
                //}

                e.Row.Cells[0].ColumnSpan = 3;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;


                //e.Row.Cells[1].ColumnSpan = 2;
                //e.Row.Cells[2].Visible = false;

                Label labelTotal = (Label)e.Row.Cells[3].FindControl("LabelTotalBudgetAmount");
                if (labelTotal != null)
                {
                    labelTotal.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(GetTotalBudget(), "N2", "");
                }

                Label labelTotalRevise = (Label)e.Row.Cells[4].FindControl("LabelTotalBudgetAmountSecretary");
                if (labelTotalRevise != null)
                {
                    decimal totalBudget = GetTotalRevise();
                    HiddenFieldReviseAmount.Value = totalBudget.ToString();
                    labelTotalRevise.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(totalBudget, "N2", "");
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
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();
            ServiceModels.ProjectInfo.BudgetDetail tempItem;
            String uid = e.CommandArgument.ToString();

            Page.Validate("SaveEditBudgetDetail");

            if ((e.CommandName == "save") && Page.IsValid)
            {
                var editRow = GridViewBudgetDetail.Rows[GridViewBudgetDetail.EditIndex];
                ServiceModels.ProjectInfo.BudgetDetail oldItem = list.Where(x => x.UID == uid).FirstOrDefault();
                ServiceModels.ProjectInfo.BudgetDetail editItem = (oldItem != null) ? oldItem : new ServiceModels.ProjectInfo.BudgetDetail();

                TextBox textBoxReviseDetail = (TextBox)editRow.FindControl("TextBoxReviseDetail");
                TextBox textBoxReviseAmount = (TextBox)editRow.FindControl("TextBoxReviseAmount");
                TextBox textBoxReviseRemark = (TextBox)editRow.FindControl("TextBoxReviseRemark");

                editItem.ReviseDetail = textBoxReviseDetail.Text.TrimEnd();
                editItem.ReviseRemark = textBoxReviseRemark.Text.TrimEnd();

                if (!String.IsNullOrEmpty(textBoxReviseAmount.Text))
                {
                    editItem.ReviseAmount = Decimal.Parse(textBoxReviseAmount.Text);
                }
                else
                {
                    editItem.ReviseAmount = null;
                }

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
                decimal reviseAmount = GetTotalRevise();
                HiddenFieldReviseAmount.Value = reviseAmount.ToString();
                //TextBoxTotalAmount.Text = reviseAmount.ToString();

                GridViewBudgetDetail.EditIndex = -1;
               
                GridViewBudgetDetail.DataSource = list;
                GridViewBudgetDetail.DataBind();
            }
        }

        private void RebindBudgetDetailDataSource()
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();

            ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = list;
            GridViewBudgetDetail.DataSource = list;
            GridViewBudgetDetail.DataBind();
        }
        
        private decimal GetTotalBudget()
        {
            decimal? total = 0;
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();
            ServiceModels.ProjectInfo.BudgetDetail data;
            for (int i = 0; i < list.Count; i++)
            {
                data = list[i];
                total += (data.Amount.HasValue) ? data.Amount : (decimal?)0;
            }
            return (total.HasValue ? (decimal)total : 0);
        }

        private decimal GetTotalRevise()
        {
            decimal? total = 0;
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();
            ServiceModels.ProjectInfo.BudgetDetail data;
            for (int i = 0; i < list.Count; i++)
            {
                data = list[i];
                total += (data.ReviseAmount.HasValue) ? data.ReviseAmount : (decimal?)0;
            }
            return (total.HasValue ? (decimal)total : 0);
        }
        #endregion


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

        protected void CustomValidatorReviseAmount_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var editRow = GridViewBudgetDetail.Rows[GridViewBudgetDetail.EditIndex];
            HiddenField textBoxRquestAmount = (HiddenField)editRow.FindControl("HiddenFieldRequestAmount");
            TextBox textBoxReviseAmount = (TextBox)editRow.FindControl("TextBoxReviseAmount");
            decimal requestAmount = 0;
            decimal reviseAmount = 0;
            if(!String.IsNullOrEmpty(textBoxReviseAmount.Text)){
                Decimal.TryParse(textBoxRquestAmount.Value, out requestAmount);
                Decimal.TryParse(textBoxReviseAmount.Text, out reviseAmount);

                args.IsValid = (reviseAmount <= requestAmount);
            }
        }

       
        
    }
}