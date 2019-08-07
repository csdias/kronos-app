using System;
using System.Collections.Specialized;
using System.ServiceProcess;
using Quartz;
using Quartz.Impl;
using Ioc;
using WinService.Jobs;
using Common.Extensions;

namespace WinService
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartService();
        }

        protected override void OnStop()
        {
            StopService();

        }

        private void ScheduleJobs(IScheduler scheduler)
        {
            scheduler.ScheduleDailyJob<JobWithService>(09, 00);
            scheduler.ScheduleRepeatAfterCompletionJob<JobWithJob>(30);
        }

        public async void StartService()
        {
            try
            {
                //Inicia container IOC
                var container = HostConfigurator.ConfigWinService(cfg => cfg.WinServiceRegister());

                //Inicia Quartz.NET Schedulers
                var properties = new NameValueCollection { { "quartz.threadPool.threadCount", "25" } };
                var schedulerFactory = new StdSchedulerFactory(properties);

                var scheduler = await schedulerFactory.GetScheduler();
                scheduler.JobFactory = new JobFactory(container);
                await scheduler.Start();

                ScheduleJobs(scheduler);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void StopService()
        {
            try
            {
                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                await scheduler.Shutdown(false);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }

}
