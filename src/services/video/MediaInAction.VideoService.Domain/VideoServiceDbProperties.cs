namespace MediaInAction.VideoService;

public static class VideoServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; }

    public const string ConnectionStringName = "VideoService";
}