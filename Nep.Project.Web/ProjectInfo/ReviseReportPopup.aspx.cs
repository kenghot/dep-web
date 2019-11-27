using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.ProjectInfo
{
    public partial class ReviseReportPopup : Infra.BasePage
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
                //var result = _projectService.GetRejectComment((decimal)id);
                //if (result.IsCompleted)
                //{
                    
                //    TextBoxRejectComment.Text =  result.Data[0];
                //    var chk = new Business.ProjectInfoService.RejectTopic(result.Data[1]);
                

                //}
                //else
                //{
                //    ShowErrorMessage(result.Message);
                //}
            }
        }

        protected void ButtonSaveComment_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                ServiceModels.ReturnMessage result = _projectService.SendReportToRevise((decimal)ProjectID,TextBoxRejectComment.Text );
                if (result.IsCompleted)
                {
                    String scriptTag = @"
                                      
                        window.parent.redirectoProjectList('sendreporttoreview=true');
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