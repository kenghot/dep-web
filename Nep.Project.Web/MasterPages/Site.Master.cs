using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace Nep.Project.Web.MasterPages
{
    public partial class SiteMaster : MasterPage
    {
        public ServiceModels.Security.SecurityInfo UserInfo { get; set; }

        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        
        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            MenuAddORGUser.Visible = false;

            var roles = UserInfo.Roles;
            if (UserInfo.UserGroupCode == Common.UserGroupCode.องค์กรภายนอก)
            {
                MenuAddORGUser.Visible = true;
            }           

            if(roles.Contains(Common.FunctionCode.MANAGE_ORGANIZATION)){
                MenuOrganization.Visible = true;
            }

            if (roles.Contains(Common.FunctionCode.MANAGE_USER))
            {               
                MenuManageUser.Visible = true;
            }


            if (roles.Contains(Common.FunctionCode.REQUEST_PROJECT))
            {
                //MenuReport4.Visible = false; /*รายงานรายละเอียดโครงการที่ได้รับการสนับสนุน*/
                //MenuReportReportReceiverFunding.Visible = false; /*รายงานผู้ขอรับเงินสนับสนุนโครงการ*/
                MenuReport.Visible = false;
                MenuReportOverlap.Visible = false;
            }
            

            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        public void ShowProjectName(string projectName)
        {
            LabelProjectName.Visible = true;
            LabelProjectName.Text = projectName;
        }

        #region "Result Message"
        //public void ShowResultMessage(string message)
        //{
        //    ValidationSummaryMaster.DisplayMode = ValidationSummaryDisplayMode.List;
        //    ValidationSummaryMaster.CssClass = Common.Constants.CSS_RESULT_TEXT;
        //    ValidationSummaryMaster.AddMessage(message, true);
        //    //ShowMessage(message, Common.Constraints.CssResultText);
        //}

        //public void ShowAllResultMessage(string message)
        //{
        //    ValidationSummaryMaster.DisplayMode = ValidationSummaryDisplayMode.List;
        //    ValidationSummaryMaster.CssClass = Common.Constants.CSS_RESULT_TEXT;            
        //    ValidationSummaryMaster.AddMessage(message, false);
        //}

        //public void ShowResultMessage(List<string> messages)
        //{
        //    ValidationSummaryMaster.DisplayMode = ValidationSummaryDisplayMode.List;
        //    ValidationSummaryMaster.CssClass = Common.Constants.CSS_RESULT_TEXT;
        //    ValidationSummaryMaster.AddMessage(messages);
        //    //ShowMessage(messages, Common.Constraints.CssResultText);
        //}
        #endregion

        #region "Error Message"
        //public void ShowErrorMessage(string message)
        //{
        //    ValidationSummaryMaster.DisplayMode = ValidationSummaryDisplayMode.List;
        //    ValidationSummaryMaster.CssClass = Common.Constants.CSS_ERROR_TEXT;
        //    ValidationSummaryMaster.AddMessage(message);
        //    //ShowMessage(message, Common.Constraints.CssErrorText);
        //}

        //public void ShowErrorMessage(List<string> messages)
        //{
        //    ValidationSummaryMaster.DisplayMode = ValidationSummaryDisplayMode.List;
        //    ValidationSummaryMaster.CssClass = Common.Constants.CSS_ERROR_TEXT;
        //    ValidationSummaryMaster.AddMessage(messages);
        //    //ShowMessage(messages, Common.Constraints.CssErrorText);
        //}
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string  DisplayUserInfo()
        {
            string info;
            string time = "";
            if (string.IsNullOrEmpty(Request.Cookies[Common.Constants.SESSION_LOGIN_DATETIME].Value))
            {
                time = string.Format("{0:dd/MM/yyyy HH:mm:ss", DateTime.Now);
                Response.Cookies[Common.Constants.SESSION_LOGIN_DATETIME].Value = time;
            }else
            {
                time = Request.Cookies[Common.Constants.SESSION_LOGIN_DATETIME].Value;
            }
            info = string.Format("{0}<br>{1}<br>{2}", UserInfo.FullName,time, Request.UserHostAddress);
            return info;
        }
    }
}