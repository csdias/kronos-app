using Ioc;
using Quartz;
using Quartz.Spi;
using System;

namespace WinService
{
    public class JobFactory : IJobFactory
    {
        private readonly ServiceContainer _container;

        public JobFactory(ServiceContainer container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var job = (IJob)Activator.CreateInstance(bundle.JobDetail.JobType, _container);
            return job;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
