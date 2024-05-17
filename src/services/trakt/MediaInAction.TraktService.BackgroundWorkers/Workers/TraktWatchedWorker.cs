using System.Threading.Tasks;
using MediaInAction.TraktService;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.EmbyService.BackgroundWorkers.Workers;

public class TraktWatchedWorker : QuartzBackgroundWorkerBase
{
    private readonly ITraktService _traktService;

    public TraktWatchedWorker( ITraktService traktService)
    {
        JobDetail = JobBuilder.Create<TraktWatchedWorker>().WithIdentity(nameof(TraktWatchedWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(TraktWatchedWorker)).WithSimpleSchedule(s=>s.WithIntervalInHours(24).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

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
        _traktService.GetWatchedShows();
        _traktService.GetWatchedMovies();
        Logger.LogInformation("Background Worker TraktWatchedWorker Complete");
        return Task.CompletedTask;
    }
}
