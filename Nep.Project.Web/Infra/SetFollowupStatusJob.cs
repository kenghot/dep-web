using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nep.Project.Web.Infra
{
    public class SetFollowupStatusJob : IJob
    {
        IServices.ISetFollowupStatusJobService _service;

        public SetFollowupStatusJob(IServices.ISetFollowupStatusJobService service)
        {
            _service = service;
        }

        public void Job()
        {
            Common.Logging.LogInfo("Set Follwup Status Job Service", "Start Set Follwup Status");            
            _service.SetFollowupStatus();
            Common.Logging.LogInfo("Set Follwup Status Job Service", "End Set Follwup Status");

        }
    }
}