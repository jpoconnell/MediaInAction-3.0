namespace MediaInAction.TraktService
{
    public static class TraktServiceDomainErrorCodes
    {
        //Add your business exception error codes here...
        public static string TraktEpisodeNotInDatabase { get; set; }
        public static string TraktShowIdNotInDatabase { get; set; }
        public static string TraktShowNotInDatabase { get; set; }
        public static string TraktEpisodeIdNotInDatabase { get; set; }
    }
}
