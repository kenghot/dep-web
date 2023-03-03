using Autofac.Integration.Web.Forms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Net;

namespace Nep.Project.Web.Account
{
    [InjectProperties]
    public partial class Login : Infra.BasePage
    {
        public IServices.IAuthenticationService _authSerive { get; set; }
        private ServiceModels.Security.SecurityInfo info = null;
        public IServices.IProjectInfoService _projService { get; set; }
       
        protected void Page_Init(object sender, EventArgs e)
        {
          
            IsAllowAnonymous = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
             Page.Master.Page.Title = "เข้าสู่ระบบ";
            if (!Page.IsPostBack)
            {
                String userName = !String.IsNullOrEmpty(Request.QueryString["username"]) ? Request.QueryString["username"] : "";
                String password = !String.IsNullOrEmpty(Request.QueryString["password"]) ? Request.QueryString["password"] : "";
                //if (!string.IsNullOrEmpty( userName) && !string.IsNullOrEmpty(password)){
                TextBoxUsername.Text = userName;
                TextBoxPassword.Text = password;
                AutoLogin();
             
            }
            if (UserInfo.IsAuthenticated)
            {
                RedirectAuthenticatedUser(UserInfo);
            }
            
            if (!Page.IsPostBack)
            {
                String registerFlag = (!String.IsNullOrEmpty(Request.QueryString["registerflag"])) ? (Request.QueryString["registerflag"].Trim().ToLower()) : "";
                
                String forgetPasswrod = (!String.IsNullOrEmpty(Request.QueryString["forgetpassword"])) ? (Request.QueryString["forgetpassword"].Trim().ToLower()) : "";

                if (registerFlag == "1")
                {
                    //Register new user
                    //Master.ShowResultMessage("การลงทะเบียนสำเร็จแล้ว ระบบจะส่งชื่อผู้ใช้งานและรหัสผ่านกลับไปยังอีเมล์ของท่าน");
                }
                else if (registerFlag == "2")
                {
                    //Register new company
                    //Master.ShowResultMessage("การแจ้งขอลงทะเบียนชื่อหน่อยงานสำเร็จแล้ว เมื่อเจ้าหน้าที่ตรวรสอบความถูกต้องแล้ว ระบบจะส่งชื่อผู้ใช้งานและรหัสผ่านกลับไปยังอีเมล์ของท่าน");
                }
                else if (forgetPasswrod == "true")
                {
                    //Forget Password
                    //Master.ShowResultMessage("ระบบจะส่งรหัสผ่านใหม่กลับไปยังอีเมล์ของท่าน");
                }
            }

            

            //Page.Form.Action = Page.ResolveClientUrl("~/Account/Login.aspx")


            //RegisterHyperLink.NavigateUrl = "Register";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //if (!String.IsNullOrEmpty(returnUrl))
            //{
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            //}
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            ClickLogin();
        }
        protected void ClickLogin()
        {
            if (Page.IsValid)
            {
                //Clear Session and Previous Ticket
                var sessionStateSection = (System.Web.Configuration.SessionStateSection)System.Configuration.ConfigurationManager.GetSection("system.web/sessionState");
                string sessionCookieName = sessionStateSection.CookieName;

                if (Request.Cookies[sessionCookieName] != null)
                {
                    Response.Cookies[sessionCookieName].Value = string.Empty;
                    Response.Cookies[sessionCookieName].Expires = DateTime.Now.AddMonths(-20);
                }

                if (Request.Cookies[Common.Constants.TICKET_COOKIE_NAME] != null)
                {
                    Response.Cookies[Common.Constants.TICKET_COOKIE_NAME].Value = string.Empty;
                    Response.Cookies[Common.Constants.TICKET_COOKIE_NAME].Expires = DateTime.Now.AddMonths(-20);
                }
                var dt = Session[Common.Constants.SESSION_LOGIN_DATETIME];
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Session[Common.Constants.SESSION_LOGIN_DATETIME] = dt;
                Response.SetCookie(new HttpCookie(Common.Constants.TICKET_COOKIE_NAME, info.TicketID));
                RedirectAuthenticatedUser(info);
            }
        }
        protected void AutoLogin()
        {

            var result = _authSerive.Login(TextBoxUsername.Text.Trim(), TextBoxPassword.Text.Trim());
            if (result.IsCompleted)
            {

                info = result.Data;
                _projService.SaveLogAccess(info.UserID, Common.LOVCode.Logaccess.LOGIN, "I", Request.UserHostAddress);

                var sessionStateSection = (System.Web.Configuration.SessionStateSection)System.Configuration.ConfigurationManager.GetSection("system.web/sessionState");
                string sessionCookieName = sessionStateSection.CookieName;

                if (Request.Cookies[sessionCookieName] != null)
                {
                    Response.Cookies[sessionCookieName].Value = string.Empty;
                    Response.Cookies[sessionCookieName].Expires = DateTime.Now.AddMonths(-20);
                }

                if (Request.Cookies[Common.Constants.TICKET_COOKIE_NAME] != null)
                {
                    Response.Cookies[Common.Constants.TICKET_COOKIE_NAME].Value = string.Empty;
                    Response.Cookies[Common.Constants.TICKET_COOKIE_NAME].Expires = DateTime.Now.AddMonths(-20);
                }
                var dt = Session[Common.Constants.SESSION_LOGIN_DATETIME];
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Session[Common.Constants.SESSION_LOGIN_DATETIME] = dt;
                Response.SetCookie(new HttpCookie(Common.Constants.TICKET_COOKIE_NAME, info.TicketID));
                RedirectAuthenticatedUser(info);
            }
         

        }
        protected void CustomValidatorLogin_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var result = _authSerive.Login(TextBoxUsername.Text.Trim(), TextBoxPassword.Text.Trim());
            if (result.IsCompleted)
            {
          
                info = result.Data;
                _projService.SaveLogAccess(info.UserID, Common.LOVCode.Logaccess.LOGIN, "I", Request.UserHostAddress);
            }
            else if (result.Message.Count > 0)
            {
                string errMsg = result.Message[0];
                CustomValidator validator = (CustomValidator)source;
                validator.ErrorMessage = errMsg;
                validator.Text = errMsg;

            }
            
            args.IsValid = result.IsCompleted;
        }

        private void RedirectAuthenticatedUser(ServiceModels.Security.SecurityInfo info)
        {
            string gotoPage = "";
            //ProjectInfo / ProjectInfoForm ? id = 4927 & activetabindex = 2
            String id = !String.IsNullOrEmpty(Request.QueryString["projectId"]) ? Request.QueryString["projectId"] : "";
            String actId = !String.IsNullOrEmpty(Request.QueryString["activetabindex"]) ? Request.QueryString["activetabindex"] : "";
            if (!string.IsNullOrEmpty(id))
            {
                gotoPage = $"~/ProjectInfo/ProjectInfoForm?id={id}";
                if (!string.IsNullOrEmpty(actId))
                {
                    gotoPage += $"&activetabindex={actId}";
                }
                Response.Redirect(Page.ResolveClientUrl(gotoPage));
            }
            gotoPage = (info.UserGroupCode == "G2") ? "~/ProjectInfo/ProjectInfoList.aspx" : "~/ProjectInfo/DashBoard.aspx";
            String url = (info.Roles.Contains("admin")) ?
                Page.ResolveClientUrl("~/Organization/OrganizationRequestList.aspx") :
                Page.ResolveClientUrl(gotoPage);
            Response.Redirect(url);
        }
    }
}