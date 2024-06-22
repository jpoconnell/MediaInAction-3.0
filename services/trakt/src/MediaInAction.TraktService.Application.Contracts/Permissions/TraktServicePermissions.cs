using Volo.Abp.Reflection;

namespace MediaInAction.TraktService.Permissions
{
    public class TraktServicePermissions
    {
        public const string GroupName = "TraktService";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(TraktServicePermissions));
        }
    }
}