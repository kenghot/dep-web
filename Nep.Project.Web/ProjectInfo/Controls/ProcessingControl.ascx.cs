﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class ProcessingControl : Nep.Project.Web.Infra.BaseUserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState.Clear();
               
            }
        }

        
    }
}