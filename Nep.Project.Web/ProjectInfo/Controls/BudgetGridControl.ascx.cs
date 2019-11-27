using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project;
using Nep.Project.Resources;
using Nep.Project.Common;
namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class BudgetGridControl : System.Web.UI.UserControl
    {
        private string BUDGET_DETAIL_LIST_VIEWSTATE_KEY = "BUDGET_DETAIL_LIST_USERCONTROL";
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

        public void RefreshData(List<ServiceModels.ProjectInfo.BudgetDetail> lst , decimal actid ,bool isEditableProjectBudget, bool isApproved, bool isRequestCenterBudget)
        {
            IsEditableProjectBudget = IsEditableProjectBudget;
            IsApproved = isApproved;
            IsRequestCenterBudget = isRequestCenterBudget;
            var data = lst.Where(w => w.ActivityID == actid).ToList();
            //string sname = GridViewBudgetDetail.ClientID + "_" + actid.ToString();
            //Session[sname] = data;
            int i = 1; 
            foreach (var d in data)
            {
                d.No = i;
                i++;
            }
            ViewState[BUDGET_DETAIL_LIST_VIEWSTATE_KEY] = data;
           
            
            GridViewBudgetDetail.DataSource = data;
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
        protected void GridViewBudgetDetail_RowDataBound(Object sender, GridViewRowEventArgs e)
        {

            if (!IsEditableProjectBudget)
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;

            }

            if (IsApproved && IsRequestCenterBudget)
            {
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

                Label labelRequest = (Label)e.Row.Cells[3].FindControl("LabelTotalBudgetAmount");
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
    }
}