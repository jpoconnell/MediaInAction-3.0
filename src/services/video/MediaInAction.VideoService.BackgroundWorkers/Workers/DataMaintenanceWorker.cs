using System.Threading.Tasks;
using MediaInAction.VideoService.DataMaintenanceNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.VideoService.BackgroundWorkers.Workers;

public class DataMaintenanceWorker : QuartzBackgroundWorkerBase
{
    private readonly IDataMaintenance _dataMaintenance;

    public DataMaintenanceWorker( IDataMaintenance dataMaintenance)
    {
        JobDetail = JobBuilder.Create<DataMaintenanceWorker>().WithIdentity(nameof(DataMaintenanceWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(DataMaintenanceWorker)).WithSimpleSchedule(s=>s.WithIntervalInHours(4).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _dataMaintenance = dataMaintenance;
    }
    
    public override Task Execute(IJobExecutionContext context)
    {
        _dataMaintenance.Process();
        Logger.LogInformation("Executed DataMantenance..!");
        return Task.CompletedTask;
    }
}
