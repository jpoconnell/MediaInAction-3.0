using Volo.Abp.BackgroundJobs;

namespace MediaInAction.FileService.BackgroundJobs.JobArgs
{
    [BackgroundJobName("embySendAll")]
    public class EmbySendAllArgs 
    {
        public string CurrentLocation { get; set; }
    }
}