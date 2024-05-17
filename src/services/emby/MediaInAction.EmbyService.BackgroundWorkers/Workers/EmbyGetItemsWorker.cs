using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyApiServicesNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.EmbyService.BackgroundWorkers.Workers;

public class EmbyGetItemsWorker : QuartzBackgroundWorkerBase
{
    private readonly IEmbyItemLookupService _embyItemLookupService;
    
    public EmbyGetItemsWorker  ( IEmbyItemLookupService embyActivityLog)
    {
        JobDetail = JobBuilder.Create<EmbyGetItemsWorker>().WithIdentity(nameof(EmbyGetItemsWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(EmbyGetItemsWorker)).WithSimpleSchedule(s=>s.WithIntervalInMinutes(1).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _embyItemLookupService = embyActivityLog;
    }
    
    public override  Task Execute(IJobExecutionContext context)
    {
        _embyItemLookupService.UpdateActivities();
        Logger.LogInformation("Executed EmbyGetItemsWorker..!");
        return Task.CompletedTask;
    }
}
