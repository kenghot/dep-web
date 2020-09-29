using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using Nep.Project.ServiceModels;
using Nep.Project.Common;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabSurveyParticipant : Nep.Project.Web.Infra.BaseUserControl
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

        public String ApprovalYear
        {
            get
            {
                return ViewState["ApprovalYear"].ToString();
            }

            set
            {
                ViewState["ApprovalYear"] = value;
            }
        }

        public String FollowupViewAttachmentPrefix
        {
            get
            {
                if (ViewState["FollowupViewAttachmentPrefix"] == null)
                {
                    string prefix = "Project/" + ProjectID;
                    ViewState["FollowupViewAttachmentPrefix"] = prefix;
                }
                return ViewState["FollowupViewAttachmentPrefix"].ToString();
            }

            set
            {
                ViewState["ViewAttachmentPrefix"] = value;
            }
        }

        public bool CanSaveContract
        {
            get
            {
                bool isCan = false;
                if (ViewState["CanSaveContract"] != null)
                {
                    isCan = Convert.ToBoolean(ViewState["CanSaveContract"]);
                }
                return isCan;
            }
            set
            {
                ViewState["CanSaveContract"] = value;
            }
        }

     

        public void BindData()
        {


            RegisterClientScript();
        }

   

        private void RegisterClientScript()
        {
            String script = "";
            var vue = System.IO.File.ReadAllText($"{Server.MapPath("\\html\\participantsurvey\\form.js")}");
            script += vue;//+ $"\nVueContract.refreshButton = '{ImageButtonRefresh.ClientID}'";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelContract,
                       this.GetType(),
                       "TabContractScript",
                       script,
                       true);
            
        }
 
    }
}