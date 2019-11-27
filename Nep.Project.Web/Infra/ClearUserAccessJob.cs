using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using Autofac;
using Autofac.Integration.Web;

namespace Nep.Project.Web.Infra
{
    public sealed class ClearUserAccessJob : IJob
    {
        IServices.IJobService _service;

        public ClearUserAccessJob(IServices.IJobService service)
        {
            _service = service;
        }

        public void Job()
        {
            _service.ClearUserAccess();
        }
    }
}
