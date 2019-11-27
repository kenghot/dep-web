using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.ProjectInfo
{
    public partial class CancelProjectRequestPopup : Infra.BasePage
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

        }

        protected Decimal? CancelledAttachmentID
        {
            get
            {
                decimal? attachmentID = (decimal?)null;
                if (ViewState["CancelledAttachmentID"] == null)
                {
                    string tmpProjectID = Request.QueryString["attachmentid"];
                    decimal id = 0;
                    if (Decimal.TryParse(tmpProjectID, out id))
                    {
                        ViewState["CancelledAttachmentID"] = id;
                        
                    }

                }

                attachmentID = Convert.ToDecimal(ViewState["CancelledAttachmentID"]);
                
                return attachmentID;
            }

            set
            {
                ViewState["CancelledAttachmentID"] = value ;
            }
        }

        public String CancelledProjectRequestPrefix
        {
            get
            {
                if (ViewState["CancelledProjectRequestPrefix"] == null)
                {
                    string prefix = "Project/" + ProjectID;
                    ViewState["CancelledProjectRequestPrefix"] = prefix;
                }
                return ViewState["CancelledProjectRequestPrefix"].ToString();
            }

            set
            {
                ViewState["CancelledProjectRequestPrefix"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CancelledAttachmentID.HasValue && (CancelledAttachmentID > 0))
            {
                var result = _projectService.GetCancelledProjectRequest((decimal)CancelledAttachmentID);
                if (result.IsCompleted)
                {
                    List<ServiceModels.KendoAttachment> attachList = new List<ServiceModels.KendoAttachment>();
                    attachList.Add(result.Data);
                    C2XFileUploadCancelledProjectRequest.ExistingFiles = attachList;
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
                
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                var attachments = C2XFileUploadCancelledProjectRequest.AddedFiles;
                ServiceModels.KendoAttachment addedFile = (attachments != null) ? attachments.FirstOrDefault() : null;
               
                var saveResult = _projectService.SaveCancelledProjectRequest((decimal)ProjectID, addedFile);
                if (saveResult.IsCompleted)
                {
                    addedFile = saveResult.Data;
                    if (addedFile != null)
                    {
                        List<ServiceModels.KendoAttachment> attachList = new List<ServiceModels.KendoAttachment>();
                        attachList.Add(addedFile);
                        C2XFileUploadCancelledProjectRequest.ClearChanges();
                        C2XFileUploadCancelledProjectRequest.ExistingFiles = attachList;
                    }
                    
                    ShowResultMessage(saveResult.Message);

                    String scriptTag = @"
                    $(function () {                           
                        window.parent.reloadPanelProjectInfo();
                    });";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "reloadPanelProjectInfo", scriptTag, true);
                }
                else
                {
                    ShowErrorMessage(saveResult.Message);
                }                
            }
        }
    }
}