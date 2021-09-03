using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project;
namespace Nep.Project.Web.ProjectInfo
{
    public partial class BudgetCopyDetailPopUp : System.Web.UI.Page
    {
        public IServices.IProjectInfoService _projectService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string strID = Request.QueryString["actid"];
                if (!string.IsNullOrEmpty(strID))
                {
                    decimal actID = decimal.Parse(strID);
                    if (actID > 0)
                    {
                        var ba = (from b in _projectService.GetDB().PROJECTBUDGETACTIVITies where b.ACTIVITYID == actID select b).FirstOrDefault();
                        if (ba != null)
                        {
                            //LabelActivity.Text = string.Format("กิจกรรมที่ {0} : {1}", ba.RUNNO.ToString(), ba.ACTIVITYNAME);
                            var pj = (from p in _projectService.GetDB().ProjectInformations where p.ProjectID == ba.PROJECTID select p).FirstOrDefault();
                            if (pj != null)
                            {
                                LabelProjectName.Text = string.Format("โครงการ : {0}", pj.ProjectNameTH);
                                 ProjectBudgetDetail1.ProjectID = ba.PROJECTID;
                                ProjectBudgetDetail1.Visible = true;
                                ProjectBudgetDetail1.BindData();
                                return;
                            }
                        }

                    }
                }
                LabelProjectName.Text = "เกิดข้อผิดพลาด กรุณาลองอีกครั้ง";
                LabelProjectName.ForeColor = System.Drawing.Color.Red;
                ButtonClose.Visible = true;

            }

        }
    }
}