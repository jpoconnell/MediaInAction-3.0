namespace MediaInAction.VideoService;

public static class VideoServiceErrorCodes
{
    public static string EpisodeAlreadyExists = "Video:00001";
    public static string EpisodeDoesNotExistException  = "Video:00002";
    public static string MovieAlreadyExists  = "Video:00003";
    public static string MovieWithIdNotFound  = "Video:00004";
    public static string SeriesWithIdNotFound = "Video:00005";
    public static string FileEntryIdNotGuid = "Video:00006";
    public static string TraktShowIdNotGuid = "Video:00007";
    public static string TraktMovieIdNotGuid = "Video:00008";
    public static string EmbyShowIdNotGuid = "Video:00009";
    public static string TraktEpisodeEpisodeNumZero = "Video:000010";
    public static string TraktEpisodeSeasonNumZero = "Video:000011";
}