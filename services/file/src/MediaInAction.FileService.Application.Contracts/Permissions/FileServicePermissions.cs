using Volo.Abp.Reflection;

namespace MediaInAction.FileService.Permissions;

public static class FileServicePermissions
{
    public const string GroupName = "FileService";

    public static class FileEntry
    {
        public const string Default = GroupName + ".Orders";
        public const string SetAsCancelled = GroupName + ".SetAsCancelled";
        public const string Dashboard = GroupName + ".Dashboard";
        public const string Create = GroupName + ".Create";
        public const string Update = GroupName + ".Update";
        public const string Delete = GroupName + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FileServicePermissions));
    }
}