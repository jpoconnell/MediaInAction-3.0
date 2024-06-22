using MediaInAction.EmbyService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.EmbyService.Services.Permissions
{
    public class EmbyServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(EmbyServicePermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(EmbyServicePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EmbyServiceResource>(name);
        }
    }
}
