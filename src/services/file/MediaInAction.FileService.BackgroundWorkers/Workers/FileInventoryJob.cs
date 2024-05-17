using System.Threading.Tasks;
using MediaInAction.FileService.Lib.FileServicesNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.FileService.BackgroundWorkers.Workers;

public class FileInventoryWorker : QuartzBackgroundWorkerBase
{
    private readonly IFileInventory _fileInventory;

    public FileInventoryWorker( IFileInventory fileInventory)
    {
        JobDetail = JobBuilder.Create<FileInventoryWorker>().WithIdentity(nameof(FileInventoryWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(FileInventoryWorker)).WithSimpleSchedule(s=>s.WithIntervalInMinutes(15).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _fileInventory = fileInventory;
    }
    
    public override Task Execute(IJobExecutionContext context)
    {
        _fileInventory.GetFiles();
        Logger.LogInformation("Executed FileInventoryWorker..!");
        return Task.CompletedTask;
    }
}
