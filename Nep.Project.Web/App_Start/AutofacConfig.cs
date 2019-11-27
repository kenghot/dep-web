using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Web;
using Autofac.Integration.Wcf;

namespace Nep.Project.Web
{
    
    public static class AutofacConfig
    {
        public static IContainer RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.Register<DBModels.Model.NepProjectDBEntities>(f => new DBModels.Model.NepProjectDBEntities()).InstancePerDependency();
            builder.RegisterService<Business.RunningNumberService>();
            builder.RegisterService<Business.OrganizationParameterService>();
            builder.RegisterService<Business.AttachmentService>();            
            builder.RegisterService<Business.ListOfValueService>();
            builder.RegisterService<Business.UserProfileService>();
            builder.RegisterService<Business.RegisterService>();
            builder.RegisterService<Business.AuthenticationService>();
            builder.RegisterService<Business.ProjectInfoService>();
            builder.RegisterService<Business.OrganizationService>();

            builder.RegisterService<Business.ReportBudgetApplicantService>();
            builder.RegisterService<Business.ReportEvaluationService>();
            builder.RegisterService<Business.ReportSummaryTrackingService>();
            builder.RegisterService<Business.ReportOverlapService>();
            builder.RegisterService<Business.ReportOverlapProvinceService>();
            builder.RegisterService<Business.ReportOrganizationClientService>();

         
            builder.RegisterService<Business.ProviceService>();
            builder.RegisterService<Business.SecurityService>();
            builder.RegisterService<Business.JobService>();
            builder.RegisterService<Business.SetFollowupStatusJobService>();

            builder.RegisterService<Business.ReportStatisticSupportService>();
            builder.RegisterService<Business.ReportStatisticClientService>();
            builder.RegisterService<Business.Report4Service>();

            builder.RegisterType<Business.MailService>();
            builder.RegisterType<Business.SmsService>();

            builder.RegisterService<Business.ReportStatisticService>();
            builder.RegisterService<Business.ReportBudgetSummaryService>();
            
            builder.Register(autofac =>
            {
                var cookie = HttpContext.Current.Request.Cookies[Common.Constants.TICKET_COOKIE_NAME];

                var ticket = cookie != null ? cookie.Value : null;
                var result = autofac.Resolve<Nep.Project.IServices.ISecurityService>().UpdateUserAccess(ticket);

                if (!result.Data.IsAuthenticated && ticket != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    cookie.Value = String.Empty;
                }

                return result.Data;
            }).As<ServiceModels.Security.SecurityInfo>().InstancePerRequest();
                     

            builder.RegisterType<Infra.ClearUserAccessJob>();
            builder.RegisterType<Infra.SetFollowupStatusJob>();
           
            return builder.Build();
        }        
    }

    public static class AutofacConfigHelper
    {
        public static ContainerBuilder RegisterService<T>(this ContainerBuilder builder) where T : class
        {           
            builder.RegisterType<T>().AsImplementedInterfaces();
            return builder;
        }
    }
}