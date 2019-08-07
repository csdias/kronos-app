using Quartz;
using Quartz.Listener;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Listener para reagendar a execução de um job após sua conclusão
    /// </summary>
    internal class RepeatAfterCompletionJobListener : JobListenerSupport
    {
        private readonly JobKey jobKey;
        private readonly int seconds;

        public RepeatAfterCompletionJobListener(int seconds, JobKey jobKey)
        {
            this.seconds = seconds;
            this.jobKey = jobKey;
        }

        public override async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            var trigger = context.Trigger.GetTriggerBuilder()
                                         .StartAt(DateTimeOffset.Now.AddSeconds(seconds))
                                         .Build();

            await context.Scheduler.RescheduleJob(context.Trigger.Key, trigger);
        }

        public override string Name => $"RepeatAfterCompletionJobListener_{jobKey.Name}";
    }
}