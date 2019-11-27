using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Account
{
    public partial class ForgetPasswordConfirm : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IAuthenticationService service { get; set; }

        private static string AcitvatationCode_QueryString = "code";
        private static string Username_QueryString = "username";

        protected void Page_Init(object sender, EventArgs e)
        {
            this.IsAllowAnonymous = true;
        }

        String username = null;
        String code = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Username_QueryString]) && !string.IsNullOrEmpty(Request.QueryString[AcitvatationCode_QueryString]))
            {
                username = Request.QueryString[Username_QueryString];
                code = Request.QueryString[AcitvatationCode_QueryString];
            }

            if (!IsPostBack)
            {
                var result = service.GetForgetPasswordInfo(username, code);
                if (result.IsCompleted)
                {
                    TextBoxEmail.Text = result.Data.Email;
                    TextBoxRegisterName.Text = result.Data.FirstName + " " + result.Data.LastName;

                    if (!String.IsNullOrEmpty(result.Data.OrganizationName))
                    {
                        TextBoxOrganizationName.Text = result.Data.OrganizationName;
                    }
                    else
                    {
                        OrganizationNameBlock.Visible = false;
                    }

                    
                }
                else
                {
                    EnableForm(false);
                    ShowErrorMessage(result.Message);
                }
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            ServiceModels.Security.ConfirmForgetPassword data = new ServiceModels.Security.ConfirmForgetPassword();
            data.Token = code;
            data.UserName = username;
            data.Password = TextBoxPassword.Text.Trim();
            data.ConfirmPassword = TextBoxConfirmPassword.Text.Trim();


            var result = service.ConfirmForgetPassword(data);
            if (result.IsCompleted)
            {
                ShowResultMessage("รหัสผ่านได้ถูกกำหนด คุณสามารถเข้าสู่ระบบโดยใช้รหัสผ่านใหม่ได้ทันที");
                EnableForm(false);
            }
            else
            {
                ShowErrorMessage(result.Message);
            }
        }

        private void EnableForm(Boolean isEnable)
        {
            ButtonSubmit.Enabled = isEnable;
            ButtonSubmit.Visible = isEnable;

            PanelForgetPassword.Visible = isEnable;
        }
    }
}