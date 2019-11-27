using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Nep.Project.Web;
using Autofac;
using Autofac.Integration.Web;
using System.Reflection;

namespace Nep.Project.Web
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        static IContainerProvider _containerProvider;
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        List<Infra.JobHost> _jobs = new List<Infra.JobHost>();

        //List<Infra.JobHost<>> job = new List<Infra.JobHost<>>();

        protected void Application_Start(object sender, EventArgs e)
        {            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AuthConfig.RegisterOpenAuth();

            _containerProvider = new ContainerProvider(AutofacConfig.RegisterAutofac());

            //Clear UserAccess every 10 minutes
            _jobs.Add(Infra.JobHost.CreateJobHost<Infra.ClearUserAccessJob>(10 * 60 * 1000L, true));

            _jobs.Add(Infra.JobHost.CreateJobHost<Infra.SetFollowupStatusJob>(new TimeSpan(0, 0, 0)));
            //_jobs.Add(Infra.JobHost.CreateJobHost<Infra.SetFollowupStatusJob>(new TimeSpan(16,55, 10)));
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;

            if (application == null)
                return;

            var request = application.Request;


            // Bug fix for MS SSRS Blank.gif 500 server error missing parameter IterationId
            // https://connect.microsoft.com/VisualStudio/feedback/details/556989/
            if (request.Path.EndsWith("/Reserved.ReportViewerWebControl.axd") &&
             !String.IsNullOrEmpty(request.QueryString["ResourceStreamID"]) &&
                request.QueryString["ResourceStreamID"].ToLower().Equals("blank.gif") &&
                String.IsNullOrEmpty(request.QueryString["IterationId"]))
            {
                Response.Redirect(request.Url.PathAndQuery.ToString() + "&IterationId=1");
            }
        }
       
    }
}
