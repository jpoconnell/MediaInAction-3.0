using System.Threading.Tasks;
using MediaInAction.VideoService.FileServicesNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.VideoService.BackgroundWorkers.Workers;

public class FileMapperWorker : QuartzBackgroundWorkerBase
{
    private readonly IFileMapper _fileMapper;

    public FileMapperWorker( IFileMapper fileMapper)
    {
        JobDetail = JobBuilder.Create<FileMapperWorker>().WithIdentity(nameof(FileMapperWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(FileMapperWorker)).WithSimpleSchedule(s=>s.WithIntervalInMinutes(15).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _fileMapper = fileMapper;
    }
    
    public override  Task Execute(IJobExecutionContext context)
    {
        _fileMapper.MapFiles();
        Logger.LogInformation("Executed FileMapperWorker..!");
        return Task.CompletedTask;
    }
}
