using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyApiServicesNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.EmbyService.BackgroundWorkers.Workers;

public class EmbyActivityLogWorker : QuartzBackgroundWorkerBase
{
    private readonly IEmbyProcessActivityLog _embyActivityLog;
    
    public EmbyActivityLogWorker  ( IEmbyProcessActivityLog embyActivityLog)
    {
        JobDetail = JobBuilder.Create<EmbyActivityLogWorker>().WithIdentity(nameof(EmbyActivityLogWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(EmbyActivityLogWorker)).WithSimpleSchedule(s=>s.WithIntervalInHours(1).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _embyActivityLog = embyActivityLog;
    }
    
    public override  Task Execute(IJobExecutionContext context)
    {
        _embyActivityLog.UpdateActivities();
        Logger.LogInformation("Executed EmbyActivityLogWorker..!");
        return Task.CompletedTask;
    }
}
