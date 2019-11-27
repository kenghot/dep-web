using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Account
{
    public partial class ForgetPassword : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IRegisterService service { get; set; }
        public IServices.IAuthenticationService authService { get; set; }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.IsAllowAnonymous = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable ListOrganization()
        {
            return service.ListOrganization();
        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            String username = TxtUsername.Text.Trim();
            var result = authService.SubmitForgetPasswordRequest(username);

            if (result.IsCompleted)
            {
                this.PanelForgetPassword.Visible = false;
                this.PanelMessage.Visible = true;
                this.ButtonSend.Visible = false;
            }
            else
            {
                this.ShowErrorMessage(result.Message);
            }
        }        
    }
}