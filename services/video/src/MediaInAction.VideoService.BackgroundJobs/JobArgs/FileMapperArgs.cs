using Volo.Abp.BackgroundJobs;

namespace MediaInAction.VideoService.BackgroundJobs.JobArgs
{
    [BackgroundJobName("videoFileMapper")]
    public class FileMapperArgs 
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}