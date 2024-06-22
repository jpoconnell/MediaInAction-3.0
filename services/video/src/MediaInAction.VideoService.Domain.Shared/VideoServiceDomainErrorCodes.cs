namespace MediaInAction.VideoService
{
    public static class VideoServiceDomainErrorCodes
    {
        /* You can add your business exception error codes here, as constants */
        public static string SeriesWithIdNotFound { get; set; }
        public static string EpisodeDoesNotExistException { get; set; }
        public static string EpisodeAlreadyExists { get; set; }
        public static string MovieAlreadyExists { get; set; }
    }
}
