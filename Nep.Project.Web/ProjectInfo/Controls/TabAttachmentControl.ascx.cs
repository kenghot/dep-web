using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using Nep.Project.Common;
using System.Net;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabAttachmentControl : Nep.Project.Web.Infra.BaseUserControl
    {
        public IServices.IProjectInfoService _service { get; set; }

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

        public void BindData()
        {
            bindProjectAttachment();
            ButtonSendProjectInfo.Text = (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) ? Nep.Project.Resources.UI.ButtonSubmit : Nep.Project.Resources.UI.ButtonSendProjectInfo;


            String onloadScript = @"
                $(function () {      
                SetTabHeader(" + Common.Web.WebUtility.ToJSON(RequiredSubmitData) + @");
                });
            
            ";
            ScriptManager.RegisterStartupScript(
                        UpdatePanelAttachment,
                        this.GetType(),
                        "UpdatePanelAttachmentPlancript",
                        onloadScript,
                        true);

            var inf = _service.GetDB().ProjectInformations.Where(w => w.ProjectID == ProjectID).FirstOrDefault();
            var id = ProjectID.ToString();
            if (inf != null)
            {
                if (!string.IsNullOrEmpty(inf.ProjectNo))
                {
                    id = inf.ProjectNo.Trim();
                }
            }
         
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                var url = $"http://203.154.94.105/ajax_get_edoc_link.php?the_id=%22{id}%22&mode=project";
                string s = client.DownloadString(url);
   
                ltrEdoc.Text = s;
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if(Page.IsValid){
                    ServiceModels.ProjectInfo.ProjectDocument newDoc = AttachmentProvideControl.GetProjectDocument();
                    newDoc.ProjectID = this.ProjectID;

                    var result = _service.SaveProjectDocument(newDoc);
                    if (result.IsCompleted)
                    {
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelAttachment");
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

        protected void ButtonSendProjectInfo_Click(object sender, EventArgs e)
        {
            var id = Request.QueryString["id"];
            decimal projectID = 0;
            if (Decimal.TryParse(id, out projectID))
            {
                //var result = _service.ValidateSubmitData(projectID);
                //if (result.IsCompleted)
                //{
                    var sendDataToReviewResult = _service.SendDataToReview(projectID,Request.UserHostAddress);
                    if (sendDataToReviewResult.IsCompleted)
                    {                        
                        Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                        page.RebindData("TabPanelAttachment");

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

        private void bindProjectAttachment()
        {
            var result = _service.GetProjectAttachment(ProjectID);
            if (result.IsCompleted)
            {
                ServiceModels.ProjectInfo.TabAttachment attach = result.Data;
                bool canSendProjectInfo = (attach.RequiredSubmitData == null);
                RequiredSubmitData = attach.RequiredSubmitData;
                //Manage Function
                List<Common.ProjectFunction> functions = _service.GetProjectFunction(attach.ProjectID).Data;
                //kenghot
                //bool isEditable = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData) || (functions.Contains(Common.ProjectFunction.ReviseAttachment)));
                var master = (this.Page.Master as MasterPages.SiteMaster);
                bool isEditable = (functions.Contains(Common.ProjectFunction.SaveDarft) || functions.Contains(Common.ProjectFunction.ReviseData) || (functions.Contains(Common.ProjectFunction.ReviseAttachment))
                    || master.UserInfo.IsAdministrator);
                ButtonSave.Visible = isEditable;
                ButtonDraft.Visible = ButtonSave.Visible;
                ButtonSendProjectInfo.Visible = (functions.Contains(Common.ProjectFunction.SaveDarft) && canSendProjectInfo);
                ButtonDelete.Visible = functions.Contains(Common.ProjectFunction.Delete);
                HyperLinkPrint.Visible = (functions.Contains(Common.ProjectFunction.PrintDataForm) && canSendProjectInfo);
                ButtonReject.Visible = functions.Contains(Common.ProjectFunction.Reject);


                AttachmentProvideControl.IsEditabled = isEditable;
                AttachmentProvideControl.AttachmentProvides = attach.Attachments;
                AttachmentProvideControl.DataBind();

                TextBoxResponsibleProject.Text = attach.ResponsibleProject;
                TextBoxProposerProject.Text = attach.ProposerProject;           

            }
            else
            {
                ShowErrorMessage(result.Message);
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Decimal projectId = ProjectID;
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
    }
}