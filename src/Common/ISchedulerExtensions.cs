using Quartz;
using Quartz.Impl.Matchers;
using System;

namespace Common.Extensions
{
    public static class ISchedulerExtensions
    {
        public static void SchedulePerSecondJob<TJob>(this IScheduler scheduler, int secounds, DateTimeOffset? startAt = null) where TJob : IJob
        {
            var jobKey = ScheduleJob<TJob>(
                scheduler,
                trigger => trigger.StartAt(startAt ?? DateTimeOffset.Now)
                                  .WithSimpleSchedule(s => s.RepeatForever().WithIntervalInSeconds(secounds).WithMisfireHandlingInstructionNextWithRemainingCount())
                                  .Build()
            );
        }

        public static void ScheduleRepeatAfterCompletionJob<TJob>(this IScheduler scheduler, int repeatAfterSeconds, DateTimeOffset? startAt = null) where TJob : IJob
        {
            var jobKey = ScheduleJob<TJob>(
                scheduler,
                trigger => trigger.StartAt(startAt ?? DateTimeOffset.Now).Build()
            );

            scheduler.ListenerManager.AddJobListener(new RepeatAfterCompletionJobListener(repeatAfterSeconds, jobKey), KeyMatcher<JobKey>.KeyEquals(jobKey));
        }

        public static void ScheduleDailyJob<TJob>(this IScheduler scheduler, int hour, int minute) where TJob : IJob
        {
            ScheduleJob<TJob>(
                scheduler,
                trigger => trigger.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hour, minute)).Build()
            );
        }

        public static void ScheduleWeeklyJob<TJob>(this IScheduler scheduler, DayOfWeek weekDay, int hour, int minute) where TJob : IJob
        {
            ScheduleJob<TJob>(
                scheduler,
                trigger => trigger.WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(weekDay, hour, minute)).Build()
            );
        }

        private static JobKey ScheduleJob<TJob>(IScheduler scheduler, Func<TriggerBuilder, ITrigger> build) where TJob : IJob
        {
            var jobName = typeof(TJob).Name;
            var jobKey = new JobKey(jobName);
            var triggerKey = new TriggerKey($"{jobName}.trigger");

            var trigger = build(TriggerBuilder.Create().WithIdentity(triggerKey));

            var job = JobBuilder.Create<TJob>()
                                .WithIdentity(jobKey)
                                .Build();

            scheduler.ScheduleJob(job, trigger);

            return jobKey;
        }
    }
}

