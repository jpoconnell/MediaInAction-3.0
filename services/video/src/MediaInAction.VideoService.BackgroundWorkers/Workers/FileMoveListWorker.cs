using System.Threading.Tasks;
using MediaInAction.VideoService.FileServicesNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.VideoService.BackgroundWorkers.Workers;

public class FileMoveListWorker : QuartzBackgroundWorkerBase
{
    private readonly IFileMove _fileMove;

    public FileMoveListWorker( IFileMove fileMove)
    {
        JobDetail = JobBuilder.Create<FileMoveListWorker>().WithIdentity(nameof(FileMoveListWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(FileMoveListWorker)).WithSimpleSchedule(s=>s.WithIntervalInMinutes(60).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _fileMove = fileMove;
    }
    
    public override Task Execute(IJobExecutionContext context)
    {
        _fileMove.GetMoveList();
        Logger.LogInformation("Executed FileMoveListWorker..!");
        return Task.CompletedTask;
    }
}
