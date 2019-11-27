using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.ServiceModels;
using Nep.Project.Common;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class OrgAssistanceControl : Nep.Project.Web.Infra.BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RepeaterOrgAssistance_DataBinding(object sender, EventArgs e)
        {
            List<ServiceModels.ProjectInfo.OrganizationAssistance> listAssistance = new List<ServiceModels.ProjectInfo.OrganizationAssistance>();
            Repeater OrganizationAssistanceRepeater = (Repeater)sender;
            for (int i = 1; i < 5; i++)
            {
                ServiceModels.ProjectInfo.OrganizationAssistance item = new ServiceModels.ProjectInfo.OrganizationAssistance();
                item.No = i.ToString() + ".";
                listAssistance.Add(item);
            }

            OrganizationAssistanceRepeater.DataSource = listAssistance;
        }

        public void BindRepeaterOrgAssistance(List<ServiceModels.ProjectInfo.OrganizationAssistance> list)
        {
            List<ServiceModels.ProjectInfo.OrganizationAssistance> listAssistance = new List<ServiceModels.ProjectInfo.OrganizationAssistance>();
            listAssistance = list;

            for (int i = 0; i < 4; i++)
            {
                TextBox txtOrganizationName = (TextBox)RepeaterOrgAssistance.Items[i].FindControl("TextBoxOrganizationName");
                TextBox txtAmount = (TextBox)RepeaterOrgAssistance.Items[i].FindControl("TextBoxAmount");

                txtOrganizationName.Text = listAssistance[i].OrganizationName;
                txtAmount.Text = listAssistance[i].Amount.ToString();
            }
        }

        public List<ServiceModels.ProjectInfo.OrganizationAssistance> GetDataEditingOrgAssistance()
        {
            List<ServiceModels.ProjectInfo.OrganizationAssistance> listAssistance = new List<ServiceModels.ProjectInfo.OrganizationAssistance>();
            try
            {
                for (int i = 0; i < RepeaterOrgAssistance.Items.Count; i++)
                {
                    TextBox txtOrganizationName = (TextBox)RepeaterOrgAssistance.Items[i].FindControl("TextBoxOrganizationName");
                    TextBox txtAmount = (TextBox)RepeaterOrgAssistance.Items[i].FindControl("TextBoxAmount");

                    ServiceModels.ProjectInfo.OrganizationAssistance dataItem = new ServiceModels.ProjectInfo.OrganizationAssistance();
                    dataItem.No = (i +1).ToString();
                    dataItem.OrganizationName = txtOrganizationName.Text.Trim();
                    if (!string.IsNullOrEmpty(txtAmount.Text.Trim()))
                        dataItem.Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                    else
                        dataItem.Amount = null;

                    listAssistance.Add(dataItem);
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
            }

            return listAssistance;
        }
    }
}