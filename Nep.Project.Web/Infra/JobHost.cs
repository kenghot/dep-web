using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Autofac;
using Autofac.Integration.Web;

namespace Nep.Project.Web.Infra
{
    public interface IJob
    {
        void Job();
    }

    public sealed class JobHost : IRegisteredObject
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private Timer _timer = null;
        private IContainerProvider _container = null;
        private Type _jobType = null;

        private void Init()
        {
            var cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
            var cp = cpa.ContainerProvider;
            _container = cp;
            HostingEnvironment.RegisterObject(this);
        }

        private JobHost (Int64 intervalTime, Boolean isImmediateStart, Type jobType)
        {
            Init();
            _jobType = jobType;

            _timer = new Timer(this.OnTimerElapsed, null, isImmediateStart?0L:intervalTime, intervalTime);
        }

        public static JobHost CreateJobHost<T>(Int64 intervalTime, Boolean isImmediateStart) where T: IJob
        {
            return new JobHost(intervalTime, isImmediateStart, typeof(T));
        }

        private JobHost(TimeSpan runTime, Type jobType)
        {
            Init();
            _jobType = jobType;

            var currentTime = DateTime.Now;
            var diffTime = (runTime - currentTime.TimeOfDay);

            if (diffTime.Ticks < 0)
            {
                diffTime = diffTime.Add(new TimeSpan(1, 0, 0, 0, 0));
            }
            _timer = new Timer(this.OnTimerElapsed, null, diffTime, new TimeSpan(1, 0, 0, 0, 0));
        }

        public static JobHost CreateJobHost<T>(TimeSpan runTime) where T : IJob
        {
            return new JobHost(runTime, typeof(T));
        }

        public void Stop(bool immediate)
        {
            lock (_lock)
            {
                _shuttingDown = true;
            }
            HostingEnvironment.UnregisterObject(this);
        }

        private void OnTimerElapsed(Object sender)
        {
            lock (_lock)
            {
                if (_shuttingDown)
                {
                    return;
                }
                try
                {
                    IJob jobInstance = (IJob) _container.ApplicationContainer.Resolve(_jobType);
                    jobInstance.Job();
                }
                catch (Exception ex)
                {
                    Common.Logging.LogError(Common.Logging.ErrorType.WebError, "Job", ex);
                }
            }
        }
    }
}
