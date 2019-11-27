using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Account
{
    public partial class ChangePassword : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IAuthenticationService service { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.IsAllowAnonymous = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TextBoxRegisterName.Text = UserInfo.FirstName + " " + UserInfo.LastName;
                TextBoxOrganizationName.Text = UserInfo.OrganizationName;
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            var oldPassword = TextBoxOldPassword.Text.Trim();
            var newPassword = TextBoxPassword.Text.Trim();

            var result = service.ChangePassword(oldPassword, newPassword);
            if (result.IsCompleted)
            {
                ShowResultMessage(Resources.Message.ChangePasswordSuccess);
            }
            else
            {
                ShowErrorMessage(result.Message);
            }
        }
    }
}