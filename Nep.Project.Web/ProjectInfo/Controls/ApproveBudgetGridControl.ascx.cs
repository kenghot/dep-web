using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project;
namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class ApproveBudgetGridControl : System.Web.UI.UserControl
    {

        private string BUDGET_DETAIL_LIST_VIEWSTATE_KEY = "ViewStatateBudgetDetailData";
        private string BUDGET_DETAIL_TOTAL_VIEWSTATE_KEY = "ViewStatateBudgetDetailData";
      
        public Decimal TotalAmountRevise1
        {
            get
            {
                decimal total = 0;
                if (ViewState["TotalAmountRevise1"] != null)
                {
                    total = Convert.ToDecimal(ViewState["TotalAmountRevise1"]);
                }
                return total;
            }
            set
            {
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
        public decimal[] ReviseTotalAmount
        {
            get
            {
                decimal[] ret = ((decimal[])ViewState[BUDGET_DETAIL_TOTAL_VIEWSTATE_KEY]);
                if (ret != null || ret.Length == 2)
                {
                    return ret;
                } else
                {
                    return new decimal[]{0,0};
                }
            }
            set
            {
                ViewState[BUDGET_DETAIL_TOTAL_VIEWSTATE_KEY] = value;
            }
        }
        public List<ServiceModels.ProjectInfo.BudgetDetail> BudgetDetailData
        {
            get
            {
                if (ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] == null)
                {
                    ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = new List<ServiceModels.ProjectInfo.BudgetDetail>();
                }
                return (List<ServiceModels.ProjectInfo.BudgetDetail>)ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            }
            set
            {
                ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY ] = value;
            }
        }
        public Boolean? IsCenterReviseProject
        {
            get
            {
                bool? isCenter = (bool?)null;
                if (ViewState["IsCenterReviseProject"] != null)
                {
                    isCenter = Convert.ToBoolean(ViewState["IsCenterReviseProject"]);
                }
                return isCenter;
            }
            set
            {
                ViewState["IsCenterReviseProject"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void RefreshData(List<ServiceModels.ProjectInfo.BudgetDetail> data  , decimal? actid,bool? isCenterRevise)
        {
            
            IsCenterReviseProject = isCenterRevise;
            BudgetDetailData = data.Where(w => w.ActivityID == actid.Value).ToList();
            GridViewApprovalBudgetDetail.DataSource = BudgetDetailData;
            GridViewApprovalBudgetDetail.DataBind();

         
            
           //var p = this.Parent.Parent

        }
        #region Manage GridView
        protected void GridViewBudgetDetail_DataBound(Object sender, EventArgs e)
        {
            var p = this.Page;
            GridView grid = (GridView)this.Parent.Parent.Parent.Parent.Parent.FindControl("GridViewApprovalBudgetDetail");
           // TabApprovalControl ap = (TabApprovalControl)this.Parent.Parent.Parent.Parent.Parent;
            // grid.DataBind();
            //ap.BindingBudgetDetailGridView();
        }
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
            List<ServiceModels.ProjectInfo.BudgetDetail> data = (List<ServiceModels.ProjectInfo.BudgetDetail>)ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
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
                        return data.OrderBy(o => o.ReviseDetail).ToList();
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
            GridView g = (GridView)sender;
            SortDirection sd = Infra.GridViewSort.GetSortDirection(g.ClientID, e.SortExpression, null);
            GridViewApprovalBudgetDetail.DataSource = SortBudget(sd, e.SortExpression);
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

        protected void GridViewApprovalBudgetDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();
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
                ServiceModels.ProjectInfo.BudgetDetail editItem = (oldItem != null) ? oldItem : new ServiceModels.ProjectInfo.BudgetDetail();
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


                if (oldItem != null)
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
                // string scriptTag = "document.getElementById('" + RefreshButtonName + "').click();";
                Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                page.RebindData("TabPanelProjectApprovalRefreshTotal");
               
                
            }

        }
        private Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
            {
                return root;
            }
            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
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

        protected void UpdateTotalProjectBudget(List<ServiceModels.ProjectInfo.BudgetDetail> list)
        {
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

            //TextBoxRevise1Amount.Text = (RadioButtonRevise1_2.Checked) ? Common.Web.WebUtility.DisplayInForm(total1, "N2", "") : "";
            //TextBoxRevise2Amount.Text = (RadioButtonRevise2_2.Checked) ? Common.Web.WebUtility.DisplayInForm(total2, "N2", "") : "";
        }

        private void RebindBudgetDetailDataSource()
        {
            var obj = ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY];
            List<ServiceModels.ProjectInfo.BudgetDetail> list = (obj != null) ? (List<ServiceModels.ProjectInfo.BudgetDetail>)obj : new List<ServiceModels.ProjectInfo.BudgetDetail>();

            ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = list;
            GridViewApprovalBudgetDetail.DataSource = list;
            GridViewApprovalBudgetDetail.DataBind();
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

    }
}