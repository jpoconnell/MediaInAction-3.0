using Volo.Abp.BackgroundJobs;

namespace MediaInAction.VideoService.BackgroundJobs.JobArgs
{
    [BackgroundJobName("videoDataMaintenance")]
    public class DataMantenanceArgs 
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}