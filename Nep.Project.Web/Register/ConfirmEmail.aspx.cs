using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Register
{
    public partial class ConfirmEmail : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IRegisterService service { get; set; }

        private static string AcitvatationCode_QueryString = "code";
        private static string ID_QueryString = "entryId";

        protected void Page_Init(object sender, EventArgs e)
        {
            this.IsAllowAnonymous = true;
        }

        int entryid = 0;
        String code = null;

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[ID_QueryString]) && !string.IsNullOrEmpty(Request.QueryString[AcitvatationCode_QueryString]))
            {
                entryid = int.Parse(Request.QueryString[ID_QueryString].ToString());
                code = Request.QueryString[AcitvatationCode_QueryString];
            }

            if (!IsPostBack)
            {
                var result = service.GetRegistryUser(entryid, code);
                if (result.IsCompleted)
                {
                    txtEmail.Text = result.Data.Email;
                    txtRegisterName.Text = result.Data.RegisterName;
                    txtTelephoneNo.Text = result.Data.TelephoneNo;
                    HiddenFieldOrgID.Value = (result.Data.OrganizationID.HasValue) ? result.Data.OrganizationID.ToString() : "";
                    HiddenFieldUserID.Value = (result.Data.RegisteredUserID.HasValue) ? result.Data.RegisteredUserID.ToString() : "";

                    
                }
                else
                {
                    ShowErrorMessage(result.Message);
                    EnableForm(false);
                }
            }

            
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            decimal? orgID = (!String.IsNullOrEmpty(HiddenFieldOrgID.Value)) ? Convert.ToDecimal(HiddenFieldOrgID.Value) : (decimal?)null;
            decimal? registeredUserID = (!String.IsNullOrEmpty(HiddenFieldUserID.Value)) ? Convert.ToDecimal(HiddenFieldUserID.Value) : (decimal?)null;
            ServiceModels.ConfirmEmail data = new ServiceModels.ConfirmEmail();
            data.ActivationCode = code;
            data.RegisterEntryID = entryid;
            data.Password = txtPassword.Text.Trim();
            data.ConfirmPassword = txtConfirmPassword.Text.Trim();
            data.RegisteredUserID = registeredUserID;

            if (orgID.HasValue)
            {
                var result = service.CreateExternalUser(data);
                if (result.IsCompleted)
                {
                    ShowResultMessage(result.Message);
                    EnableForm(false);
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }
            else
            {
                var result = service.CreatePasswordInternalUser(data);
                if (result.IsCompleted)
                {
                    ShowResultMessage(result.Message);                   
                    EnableForm(false);
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }

           
        }

        private void EnableForm(Boolean isEnable)
        {
            txtPassword.Enabled = isEnable;
            txtConfirmPassword.Enabled = isEnable;
            ButtonSubmit.Enabled = isEnable;
            ButtonSubmit.Visible = isEnable;
        }

        protected void CustomValidatorPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string text = txtPassword.Text.Trim();
            bool lengthValid = (text.Length >= 8);    
            bool hasLeter =  Regex.IsMatch(text, @"[a-zA-Z]+");
            bool hasNumber = Regex.IsMatch(text, @"[0-9]+");
            args.IsValid = (lengthValid && hasLeter && hasNumber);
        }
    }
}