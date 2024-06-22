using MediaInAction.TraktService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.TraktService.Permissions
{
    public class TraktServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(TraktServicePermissions.GroupName, L("Permission:TraktService"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TraktServiceResource>(name);
        }
    }
}