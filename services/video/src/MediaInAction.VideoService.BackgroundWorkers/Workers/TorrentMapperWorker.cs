using System.Threading.Tasks;
using MediaInAction.VideoService.TorrentNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.VideoService.BackgroundWorkers.Workers;

public class TorrentMapperWorker : QuartzBackgroundWorkerBase
{
    private readonly ITorrentMapper _torrentMapper;

    public TorrentMapperWorker( ITorrentMapper torrentMapper)
    {
        JobDetail = JobBuilder.Create<TorrentMapperWorker>().WithIdentity(nameof(TorrentMapperWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(TorrentMapperWorker)).WithSimpleSchedule(s=>s.WithIntervalInMinutes(15).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _torrentMapper = torrentMapper;
    }
    
    public override Task Execute(IJobExecutionContext context)
    {
        _torrentMapper.MapTorrents();
        Logger.LogInformation("Executed TorrentMapperWorker..!");
        return Task.CompletedTask;
    }
}
