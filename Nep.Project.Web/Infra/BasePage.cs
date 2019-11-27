using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace Nep.Project.Web.Infra
{
    public class BasePage : System.Web.UI.Page
    {
        public ServiceModels.Security.SecurityInfo UserInfo { get; set; }

        private Boolean _isAllowAnonymous = false;
        protected Boolean IsAllowAnonymous
        {
            get
            {
                return _isAllowAnonymous;
            }
            set
            {
                _isAllowAnonymous = value;
            }
        }

        private String[] _functions = new String[0];
        protected String[] Functions
        {
            get
            {
                return _functions;
            }
            set
            {
                if (value == null)
                {
                    _functions = new String[0];
                }
                else
                {
                    _functions = value.Where(x => !String.IsNullOrWhiteSpace(x)).Select(x=>x.Trim()).ToArray();
                }
            }
        }

        public BasePage()
        {
            this.InitComplete += BasePage_InitComplete;
        }

        void BasePage_InitComplete(object sender, EventArgs e)
        {
            if (this.AppRelativeVirtualPath.Contains("DefaultWsdlHelpGenerator.aspx"))
            {
                return;
            }
            
            var isAuthorized = true;
            if (!_isAllowAnonymous && !UserInfo.IsAuthenticated)
            {
                isAuthorized = false;
            }

            if (isAuthorized && _functions.Length >= 1)
            {
                if (!UserInfo.Roles.Any(role => _functions.Contains(role)))
                {
                    isAuthorized = false;
                }
            }

            if (isAuthorized)
            {
                if (!IsAuthorized())
                {
                    isAuthorized = false;
                }
            }

            if (!isAuthorized)
            {
                Response.Redirect(Page.ResolveUrl("~/Account/Login"));
            }
        }

        protected virtual Boolean IsAuthorized()
        {
            return true;
        }

        //protected abstract Boolean CheckAuthorize();
              

        protected void ShowProjectName(string projectName)
        {
            var masterPage = this.Master;
            if (masterPage is Nep.Project.Web.MasterPages.SiteMaster)
            {
                Nep.Project.Web.MasterPages.SiteMaster site = (Nep.Project.Web.MasterPages.SiteMaster)masterPage;
                site.ShowProjectName(projectName);
            }
        }

        #region Reporting Service
        /// <summary>
        ///  สำหรับซ่อน Export drop down mene ของ Reporting Service
        /// </summary>
        /// <param name="ReportViewerID">ReportViewer object</param>
        /// <param name="strFormatName">
        /// 1. To Word option reference to "WORDOPENXML"
        /// 2. To Excel option reference to "EXCELOPENXML"
        /// 3. To PDF option reference to "PDF"
        /// 
        /// </param>
        public void SuppressExportButton(ReportViewer ReportViewerID, string strFormatName)
        {
            FieldInfo info;
            string extName;
            strFormatName = strFormatName.ToUpper();
            foreach (RenderingExtension extension in ReportViewerID.LocalReport.ListRenderingExtensions())
            {
                extName = extension.Name.ToUpper();
                if (extName == strFormatName)
                {
                    info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                    info.SetValue(extension, false);
                }
            }
        }
        #endregion Reporting Service

        #region ShowResultMessage
        protected void ShowResultMessage(String message)
        {
            String scriptTag = @"
                     $(document).ready(function () {
                             var messages = " + Common.Web.WebUtility.ToJSON(message) + @";    
                             c2x.writeSummaryResult(messages, null);
                         });";

            ScriptManager.RegisterStartupScript(base.Page, this.GetType(), "PageShowResultMessage", scriptTag, true);

            //var masterPage = this.Master;
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

        //protected void ShowAllResultMessage(String message)
        //{
        //    //var masterPage = this.Master;
        //    //if (masterPage is Nep.Project.Web.MasterPages.SiteMaster)
        //    //{
        //    //    Nep.Project.Web.MasterPages.SiteMaster site = (Nep.Project.Web.MasterPages.SiteMaster)masterPage;
        //    //    site.ShowAllResultMessage(message);
        //    //}
        //    //else if (masterPage is Nep.Project.Web.MasterPages.Guest)
        //    //{
        //    //    Nep.Project.Web.MasterPages.Guest site = (Nep.Project.Web.MasterPages.Guest)masterPage;
        //    //    site.ShowAllResultMessage(message);
        //    //}
        //    //else if (masterPage is Nep.Project.Web.MasterPages.Content)
        //    //{
        //    //    Nep.Project.Web.MasterPages.Content site = (Nep.Project.Web.MasterPages.Content)masterPage;
        //    //    site.ShowAllResultMessage(message);
        //    //}
        //}

        protected void ShowResultMessage(List<string> messages)
        {
            String scriptTag = @"
                     $(document).ready(function () {
                             var messages = " + Common.Web.WebUtility.ToJSON(messages) + @";    
                             c2x.writeSummaryResult(messages, null);
                         });";

            ScriptManager.RegisterStartupScript(base.Page, this.GetType(), "PageShowResultMessage", scriptTag, true);

            //var masterPage = this.Master;
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

            ScriptManager.RegisterStartupScript(base.Page, this.GetType(), "PageShowErrorMessage", scriptTag, true);

            //var masterPage = this.Master;
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

            ScriptManager.RegisterStartupScript(base.Page, this.GetType(), "PageShowErrorMessage", scriptTag, true);
            //var masterPage = this.Master;
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