using System.Threading.Tasks;
using MediaInAction.FileService.FileEntryNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.FileService.BackgroundWorkers.Workers;

public class ResendUnAcceptedFileWorker : QuartzBackgroundWorkerBase
{
    private readonly IFileEntryLibService _fileEntryLibService;

    public ResendUnAcceptedFileWorker( IFileEntryLibService fileEntryLibService)
    {
        JobDetail = JobBuilder.Create<ResendUnAcceptedFileWorker>().WithIdentity(nameof(ResendUnAcceptedFileWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(ResendUnAcceptedFileWorker)).WithSimpleSchedule(s=>s.WithIntervalInHours(24).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _fileEntryLibService = fileEntryLibService;
    }
    
    public override Task Execute(IJobExecutionContext context)
    {
        Logger.LogInformation("Background Worker Resend Starting..!");
        _fileEntryLibService.ResendUnAcceptedFileEntries();
        Logger.LogInformation("Executed Resend FileEntries..!");
        return Task.CompletedTask;
    }
}
