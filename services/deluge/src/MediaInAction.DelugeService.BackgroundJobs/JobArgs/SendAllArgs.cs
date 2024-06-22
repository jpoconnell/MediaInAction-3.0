using Volo.Abp.BackgroundJobs;

namespace MediaInAction.DelugeService.BackgroundJobs.JobArgs
{
    [BackgroundJobName("delugeSendAll")]
    public class SendAllArgs
    {
    public string ApiKey { get; set; }
    }
}