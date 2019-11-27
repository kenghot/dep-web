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
            String url = (UserInfo.Roles.Contains(Common.UserGroupCode.ผู้ดูแลระบบ)) ?
                Page.ResolveClientUrl("~/Organization/OrganizationRequestList") :
                Page.ResolveClientUrl("~/ProjectInfo/ProjectInfoList");
            Response.Redirect(url);
        }
    }
}