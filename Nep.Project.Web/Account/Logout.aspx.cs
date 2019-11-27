using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Account
{
    public partial class Logout : Infra.BasePage
    {
        public IServices.IAuthenticationService AuthenticationService { get; set; }
        protected void Page_Init(object sender, EventArgs e)
        {
            IsAllowAnonymous = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticationService.Logout();
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

            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();

            String url = Page.ResolveClientUrl("~/Account/Login.aspx");
            Response.Redirect(url);
        }
    }
}