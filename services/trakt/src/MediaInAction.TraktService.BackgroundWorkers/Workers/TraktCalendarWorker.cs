﻿using System.Threading.Tasks;
using MediaInAction.TraktService.Nodb.Lib;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.TraktService.BackgroundWorkers.Workers;

public class TraktCalendarWorker : QuartzBackgroundWorkerBase
{
    private readonly ITraktNodbService _traktService;

    public TraktCalendarWorker( ITraktNodbService traktService)
    {
        JobDetail = JobBuilder.Create<TraktCalendarWorker>().WithIdentity(nameof(TraktCalendarWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(TraktCalendarWorker)).WithSimpleSchedule(s=>s.WithIntervalInHours(24).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _traktService = traktService;
    }

    public override Task Execute(IJobExecutionContext context)
    {
        Logger.LogInformation("Background Worker TraktCalendarWorker Starting..!");
        _traktService.SyncCalendarAsync();
        Logger.LogInformation("Background Worker TraktCalendarWorker Complete");
        return Task.CompletedTask;
    }
}
