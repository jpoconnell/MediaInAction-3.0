using Volo.Abp.Reflection;

namespace MediaInAction.TraktService.Permissions
{
    public class TraktServicePermissions
    {
        public const string GroupName = "TraktService";

        public static class TraktShow
        {
            public const string Default = GroupName + ".Show";
            public const string Delete = Default + ".Delete";
            public const string Edit = Default + ".Edit";
            public const string Create = Default + ".Create";
            public const string Dashboard = Default + ".Dashboard";
        }

        public static class TraktEpisode
        {
            public const string Default = GroupName + ".Episode";
            public const string Delete = Default + ".Delete";
            public const string Edit = Default + ".Edit";
            public const string Create = Default + ".Create";
            public const string Dashboard = Default + ".Dashboard";
        }
        
        public static class TraktMovie
        {
            public const string Default = GroupName + ".Mpvie";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Dashboard = Default + ".Dashboard";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(TraktServicePermissions));
        }
    }
}