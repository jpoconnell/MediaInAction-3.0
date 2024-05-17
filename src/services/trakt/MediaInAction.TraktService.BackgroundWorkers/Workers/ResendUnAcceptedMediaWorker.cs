using System.Threading.Tasks;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktShowNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.EmbyService.BackgroundWorkers.Workers;

public class ResendUnAcceptedMediaWorker : QuartzBackgroundWorkerBase
{
    private readonly ITraktShowLibService _traktShowLibService;
    private readonly ITraktEpisodeLibService _traktEpisodeLibService;
    private readonly ITraktMovieLibService _traktMovieLibService;

    public ResendUnAcceptedMediaWorker( ITraktShowLibService traktShowLibService,
        ITraktEpisodeLibService traktEpisodeLibService,
        ITraktMovieLibService traktMovieLibService)
    {
        JobDetail = JobBuilder.Create<ResendUnAcceptedMediaWorker>().WithIdentity(nameof(ResendUnAcceptedMediaWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(ResendUnAcceptedMediaWorker)).WithSimpleSchedule(s=>s.WithIntervalInHours(24).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _traktShowLibService = traktShowLibService;
        _traktEpisodeLibService = traktEpisodeLibService;
        _traktMovieLibService = traktMovieLibService;
    }
    
    public override Task Execute(IJobExecutionContext context)
    {
        Logger.LogInformation("Background Worker Resend Starting..!");
        _traktShowLibService.ResendUnAcceptedShowsList();
        Logger.LogInformation("Executed Resend Shows..!");
        _traktEpisodeLibService.ResendUnAcceptedEpisodesList();
        Logger.LogInformation("Executed Resend Episodes..!");
        _traktMovieLibService.ResendUnAcceptedMoviesList();
        Logger.LogInformation("Executed Resend Movies..!");
        return Task.CompletedTask;
    }
}
