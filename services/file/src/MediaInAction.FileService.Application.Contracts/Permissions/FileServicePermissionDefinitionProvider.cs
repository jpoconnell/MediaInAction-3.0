using MediaInAction.FileService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.FileService.Permissions
{
    public class FileServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var orderManagmentGroup = context.AddGroup(FileServicePermissions.GroupName, L("Permission:FileService"));

            var oders = orderManagmentGroup.AddPermission(FileServicePermissions.FileEntry.Default, L("Permission:Orders"));
            oders.AddChild(FileServicePermissions.FileEntry.SetAsCancelled, L("Permission:Orders.SetAsCancelled"));
            

            orderManagmentGroup.AddPermission(FileServicePermissions.FileEntry.Dashboard, L("Permission:Dashboard"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FileServiceResource>(name);
        }
    }
}