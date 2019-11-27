using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.ProjectInfo
{
    public partial class RejectCommentPopup : Infra.BasePage
    {
        public IServices.IProjectInfoService _projectService { get; set; }

        protected Decimal? ProjectID
        {
            get
            {
                decimal? projectid = (decimal?)null;
                if (ViewState["ProjectID"] == null)
                {
                    string tmpProjectID = Request.QueryString["projectid"];
                    decimal id = 0;
                    if (Decimal.TryParse(tmpProjectID, out id))
                    {
                        ViewState["ProjectID"] = id;
                    }
                }

                projectid = Convert.ToDecimal(ViewState["ProjectID"]);
                return projectid;
            }

            set
            {
                ViewState["ProjectID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            decimal? id = ProjectID;
            if((!IsPostBack) && (id.HasValue)){
                var result = _projectService.GetRejectComment((decimal)id);
                if (result.IsCompleted)
                {
                    
                    TextBoxRejectComment.Text =  result.Data[0];
                    var chk = new Business.ProjectInfoService.RejectTopic(result.Data[1]);
                    cb1.Checked = chk.IsProjInfo;
                    cb2.Checked = chk.IsBudget;
                    cb3.Checked = chk.IsPerson;
                    cb4.Checked = chk.IsAttach;
                    cb5.Checked = chk.IsProcess;

                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }
        }

        protected void ButtonSaveComment_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string comment = TextBoxRejectComment.Text.Trim();
                string chkbox = "";
                List<string> ls = new List<string>();
                if (cb1.Checked) ls.Add("1");
                if (cb2.Checked) ls.Add("2");
                if (cb3.Checked) ls.Add("3");
                if (cb4.Checked) ls.Add("4");
                if (cb5.Checked) ls.Add("5");
                if (ls.Count > 0)
                {
                    chkbox = string.Join(",", ls.ToArray());
                }
                ServiceModels.ReturnMessage result = _projectService.RejectToOrganization((decimal)ProjectID, comment,chkbox);
                if (result.IsCompleted)
                {
                    String scriptTag = @"
                                      
                        window.parent.redirectoProjectList('rejectsuccess=true');
                   ";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RedirectoProjectList", scriptTag, true);
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }  
        }
    }
}