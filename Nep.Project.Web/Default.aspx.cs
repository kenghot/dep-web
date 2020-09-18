using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web
{
    public partial class _Default : Nep.Project.Web.Infra.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string gotoPage = "";
            gotoPage = (UserInfo.UserGroupCode == "G2") ? "~/ProjectInfo/ProjectInfoList.aspx" : "~/ProjectInfo/DashBoard.aspx";
            String url = (UserInfo.Roles.Contains("admin")) ?
                Page.ResolveClientUrl("~/Organization/OrganizationRequestList.aspx") :
                Page.ResolveClientUrl(gotoPage);
            Response.Redirect(url);

   
        }
    }
}