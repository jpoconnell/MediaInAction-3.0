using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowSeasonNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.EmbyService.BackgroundWorkers.Workers;

public class EpisodeCleanupWorker : QuartzBackgroundWorkerBase
{
    private readonly ITraktShowSeasonService _traktShowSeason;

    public EpisodeCleanupWorker( ITraktShowSeasonService traktShowSeason)
    {
        JobDetail = JobBuilder.Create<EpisodeCleanupWorker>().WithIdentity(nameof(EpisodeCleanupWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(EpisodeCleanupWorker)).WithSimpleSchedule(s=>s.WithIntervalInHours(2).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _traktShowSeason = traktShowSeason;
    }
    
    public override Task Execute(IJobExecutionContext context)
    {
        Logger.LogInformation("Background Worker EpisodeCleanupWorker Starting..!");
        _traktShowSeason.DoEpisodeCleanup();
        Logger.LogInformation("Executed EpisodeCleanup..!");
        return Task.CompletedTask;
    }
}
