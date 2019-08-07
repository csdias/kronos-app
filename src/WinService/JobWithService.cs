using Application;
using Ioc;
using Quartz;

namespace WinService.Jobs
{
    [DisallowConcurrentExecution]
    public class JobWithService : BaseJob<ProductService>
    {
        public JobWithService(ServiceContainer container) :
            base(container)
        {
        }

        public override void Execute(ProductService service)
        {
            service.FazAlgo();
        }
    }
}
