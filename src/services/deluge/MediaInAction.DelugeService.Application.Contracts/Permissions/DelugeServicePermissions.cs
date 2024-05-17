using Volo.Abp.Reflection;

namespace MediaInAction.DelugeService.Permissions
{
    public class DelugeServicePermissions
    {
        public const string GroupName = "DelugeService";

        public static class Torrent
        {
            public const string Default = GroupName + ".Torrent";
            public const string Delete = Default + ".Delete";
            public const string Edit = Default + ".Edit";
            public const string Create = Default + ".Create";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(DelugeServicePermissions));
        }
    }
}