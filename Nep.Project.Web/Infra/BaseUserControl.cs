using Autofac;
using Autofac.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Nep.Project.Web.Infra
{
    public class BaseUserControl : System.Web.UI.UserControl
    {
        public ServiceModels.Security.SecurityInfo UserInfo { get; set; }
               

        #region ShowResultMessage
        protected void ShowResultMessage(String message)
        {
            String scriptTag = @"
                     $(document).ready(function () {
                             var messages = " + Common.Web.WebUtility.ToJSON(message) + @";    
                             c2x.writeSummaryResult(messages, null);
                         });";

            ScriptManager.RegisterStartupScript(base.Page, this.GetType(), "UserControlShowResultMessage", scriptTag, true);
            //var masterPage = this.Page.Master;
            //if (masterPage is Nep.Project.Web.MasterPages.SiteMaster)
            //{
            //    Nep.Project.Web.MasterPages.SiteMaster site = (Nep.Project.Web.MasterPages.SiteMaster)masterPage;
            //    site.ShowResultMessage(message);
            //}
            //else if (masterPage is Nep.Project.Web.MasterPages.Guest)
            //{
            //    Nep.Project.Web.MasterPages.Guest site = (Nep.Project.Web.MasterPages.Guest)masterPage;
            //    site.ShowResultMessage(message);
            //}
            //else if (masterPage is Nep.Project.Web.MasterPages.Content)
            //{
            //    Nep.Project.Web.MasterPages.Content site = (Nep.Project.Web.MasterPages.Content)masterPage;
            //    site.ShowResultMessage(message);
            //}
        }

//        protected void ShowAllResultMessage(String message)
//        {
//            String scriptTag = @"
//                     $(document).ready(function () {
//                             var messages = " + Common.Web.WebUtility.ToJSON(message) + @";    
//                             c2x.writeSummaryResult(messages, null);
//                         });";

//            ScriptManager.RegisterStartupScript(base.Page, this.GetType(), "UserControlShowAllResultMessage", scriptTag, true);
//            //var masterPage = this.Page.Master;
//            //if (masterPage is Nep.Project.Web.MasterPages.SiteMaster)
//            //{
//            //    Nep.Project.Web.MasterPages.SiteMaster site = (Nep.Project.Web.MasterPages.SiteMaster)masterPage;
//            //    site.ShowAllResultMessage(message);
//            //}
//            //else if (masterPage is Nep.Project.Web.MasterPages.Guest)
//            //{
//            //    Nep.Project.Web.MasterPages.Guest site = (Nep.Project.Web.MasterPages.Guest)masterPage;
//            //    site.ShowAllResultMessage(message);
//            //}
//            //else if (masterPage is Nep.Project.Web.MasterPages.Content)
//            //{
//            //    Nep.Project.Web.MasterPages.Content site = (Nep.Project.Web.MasterPages.Content)masterPage;
//            //    site.ShowAllResultMessage(message);
//            //}
//        }

        protected void ShowResultMessage(List<string> messages)
        {
            String scriptTag = @"
                     $(document).ready(function () {
                             var messages = " + Common.Web.WebUtility.ToJSON(messages) + @";    
                             c2x.writeSummaryResult(messages, null);
                         });";

            ScriptManager.RegisterStartupScript(base.Page, this.GetType(), "UserControlShowResultMessage", scriptTag, true);
            //var masterPage = this.Page.Master;
            //if (masterPage is Nep.Project.Web.MasterPages.SiteMaster)
            //{
            //    Nep.Project.Web.MasterPages.SiteMaster site = (Nep.Project.Web.MasterPages.SiteMaster)masterPage;
            //    site.ShowResultMessage(messages);
            //}
            //else if (masterPage is Nep.Project.Web.MasterPages.Guest)
            //{
            //    Nep.Project.Web.MasterPages.Guest site = (Nep.Project.Web.MasterPages.Guest)masterPage;
            //    site.ShowResultMessage(messages);
            //}
            //else if (masterPage is Nep.Project.Web.MasterPages.Content)
            //{
            //    Nep.Project.Web.MasterPages.Content site = (Nep.Project.Web.MasterPages.Content)masterPage;
            //    site.ShowResultMessage(messages);
            //}
        }

        #endregion ShowResultMessage

        #region ShowErrorMessage
        protected void ShowErrorMessage(String message)
        {
            String scriptTag = @"
                     $(document).ready(function () {
                             var errorMessages = " + Common.Web.WebUtility.ToJSON(message) + @";    
                             c2x.writeSummaryResult(null, errorMessages);
                         });";

            ScriptManager.RegisterStartupScript(base.Page, this.GetType(), "UserControlShowErrorMessage", scriptTag, true);

            //var masterPage = this.Page.Master;
            //if (masterPage is Nep.Project.Web.MasterPages.SiteMaster)
            //{
            //    Nep.Project.Web.MasterPages.SiteMaster site = (Nep.Project.Web.MasterPages.SiteMaster)masterPage;
            //    site.ShowErrorMessage(message);
            //}
            //else if (masterPage is Nep.Project.Web.MasterPages.Guest)
            //{
            //    Nep.Project.Web.MasterPages.Guest site = (Nep.Project.Web.MasterPages.Guest)masterPage;
            //    site.ShowErrorMessage(message);
            //}
            //else if (masterPage is Nep.Project.Web.MasterPages.Content)
            //{
            //    Nep.Project.Web.MasterPages.Content site = (Nep.Project.Web.MasterPages.Content)masterPage;
            //    site.ShowErrorMessage(message);
            //}
        }

        protected void ShowErrorMessage(List<string> messages)
        {
            String scriptTag = @"
                     $(document).ready(function () {
                             var errorMessages = " + Common.Web.WebUtility.ToJSON(messages) + @";    
                             c2x.writeSummaryResult(null, errorMessages);
                         });";

            ScriptManager.RegisterStartupScript(base.Page, this.GetType(), "UserControlShowErrorMessage", scriptTag, true);

            //var masterPage = this.Page.Master;
            //if (masterPage is Nep.Project.Web.MasterPages.SiteMaster)
            //{
            //    Nep.Project.Web.MasterPages.SiteMaster site = (Nep.Project.Web.MasterPages.SiteMaster)masterPage;
            //    site.ShowErrorMessage(messages);
            //}
            //else if (masterPage is Nep.Project.Web.MasterPages.Guest)
            //{
            //    Nep.Project.Web.MasterPages.Guest site = (Nep.Project.Web.MasterPages.Guest)masterPage;
            //    site.ShowErrorMessage(messages);
            //}
            //else if (masterPage is Nep.Project.Web.MasterPages.Content)
            //{
            //    Nep.Project.Web.MasterPages.Content site = (Nep.Project.Web.MasterPages.Content)masterPage;
            //    site.ShowErrorMessage(messages);
            //}
        }
        #endregion ShowErrorMessage
    }

    
}