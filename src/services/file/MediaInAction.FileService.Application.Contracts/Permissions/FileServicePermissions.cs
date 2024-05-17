using Volo.Abp.Reflection;

namespace MediaInAction.FileService.Permissions
{
    public static class FileServicePermissions
    {
        public const string GroupName = "FileService";

        public static class FileEntry
        {
            public const string Default = GroupName + ".FileEntries";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }
        
        public static class FileRequest
        {
            public const string Default = GroupName + ".FileRequests";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(FileServicePermissions));
        }
    }
}