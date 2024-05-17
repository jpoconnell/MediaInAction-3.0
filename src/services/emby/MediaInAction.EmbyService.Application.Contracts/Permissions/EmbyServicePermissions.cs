using Volo.Abp.Reflection;

namespace MediaInAction.EmbyService.Permissions
{
    public class EmbyServicePermissions
    {
        public const string GroupName = "EmbyService";

        public static class Show
        {
            public const string Default = GroupName + ".Show";
            public const string Delete = Default + ".Delete";
            public const string Edit = Default + ".Edit";
            public const string Create = Default + ".Create";
        }

        public static class Episode
        {
            public const string Default = GroupName + ".Episode";
            public const string Delete = Default + ".Delete";
            public const string Edit = Default + ".Edit";
            public const string Create = Default + ".Create";
        }
        
        public static class Movie
        {
            public const string Default = GroupName + ".Movie";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(EmbyServicePermissions));
        }
    }
}