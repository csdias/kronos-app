using Ioc;
using Quartz;
using System;
using System.Threading.Tasks;

namespace WinService.Jobs
{
    public abstract class BaseJob<TService> : IJob
    where TService : class
    {
        private readonly ServiceContainer container;

        public BaseJob(ServiceContainer container)
        {
            this.container = container;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                //logger.Info($"Starting {context.JobDetail.Key} job execution");
                container.ExecuteInContainerScope<TService>(Execute);
                //logger.Info($"Finishing {context.JobDetail.Key} job execution");
            }
            catch (Exception ex)
            {
                try
                {
                    //logger.Error($"{context.JobDetail.Key} job error", ex);
                }
                catch { }
            }

            return Task.CompletedTask;
        }

        public abstract void Execute(TService service);
    }
}
