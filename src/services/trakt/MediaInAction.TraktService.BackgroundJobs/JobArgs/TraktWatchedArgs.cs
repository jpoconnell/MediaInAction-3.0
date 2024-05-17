using Volo.Abp.BackgroundJobs;

namespace MediaInAction.TraktService.BackgroundJobs.JobArgs
{
    [BackgroundJobName("traktsyncwatched")]
    public class TraktWatchedArgs 
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}