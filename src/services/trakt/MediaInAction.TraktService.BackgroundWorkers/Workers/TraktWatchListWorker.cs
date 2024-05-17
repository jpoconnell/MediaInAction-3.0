using System.Threading.Tasks;
using MediaInAction.TraktService;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.EmbyService.BackgroundWorkers.Workers;

public class TraktWatchListWorker : QuartzBackgroundWorkerBase
{
    private readonly ITraktService _traktService;

    public TraktWatchListWorker( ITraktService traktService)
    {
        JobDetail = JobBuilder.Create<TraktWatchListWorker>().WithIdentity(nameof(TraktWatchListWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(TraktWatchListWorker)).WithSimpleSchedule(s=>s.WithIntervalInHours(4).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

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
        Logger.LogInformation("Background Worker TraktWatchedWorker Starting..!");
        _traktService.GetWatchedList("Shows");
        _traktService.GetWatchedList("Movies");
        Logger.LogInformation("Background Worker TraktWatchedWorker Complete");
        return Task.CompletedTask;
    }
}
