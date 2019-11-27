using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.Register
{
    public partial class RegisterComplete : Nep.Project.Web.Infra.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.IsAllowAnonymous = true;
        }
    }
}