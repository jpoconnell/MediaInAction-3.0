namespace MediaInAction.TraktService
{
    public static class TraktServiceDbProperties
    {
        public static string DbTablePrefix { get; set; } = "TraktService";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "TraktService";
    }
}
