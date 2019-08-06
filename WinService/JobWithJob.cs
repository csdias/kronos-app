using Application;
using Ioc;
using Quartz;

namespace WinService.Jobs
{
    [DisallowConcurrentExecution]
    public class JobWithJob : BaseJob<ApplicationJob>
    {
        public JobWithJob(ServiceContainer container) :
            base(container)
        {
        }

        public override void Execute(ApplicationJob service)
        {
            service.ReceiveMessages();
        }
    }
}
