using Volo.Abp.Reflection;

namespace MediaInAction.VideoService.Permissions
{
    public static class VideoServicePermissions
    {
        public const string GroupName = "VideoService";
        
        public static class Seriess
        {
            public const string Default = GroupName + ".Series";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string SetAsInActive = GroupName + ".SetAsInActive";
            public const string Create = Default + ".Create";
            public const string Dashboard = GroupName + ".Dashboard";
        }

        public static class Episodes
        {
            public const string Default = GroupName + ".Episode";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string SetAsComplete = GroupName + ".SetAsComplete";
            public const string SetAsWatched = GroupName + ".SetAsWatched";
        }
        
        public static class Movies
        {
            public const string Default = GroupName + ".Movies";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string SetAsInActive = GroupName + ".SetAsInActive";
        }

        public static class ToBeMappeds
        {
            public const string Default = GroupName + ".ToBeMappeds";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class FileEntries
        {
            public const string Default = GroupName + ".FileEntries";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class Torrents
        {
            public const string Default = GroupName + ".Torrents";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(VideoServicePermissions));
        }
    }
}