namespace MediaInAction.FileService;

public static class FileServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; }

    public const string ConnectionStringName = "FileService";
}