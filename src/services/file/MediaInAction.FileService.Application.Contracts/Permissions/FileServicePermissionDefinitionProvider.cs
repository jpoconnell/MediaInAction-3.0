using MediaInAction.FileService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.FileService.Permissions
{
    public class FileServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var fileManagmentGroup = context.AddGroup(FileServicePermissions.GroupName, L("Permission:FileService"));
            
            var fileEntries = fileManagmentGroup.AddPermission(FileServicePermissions.FileEntry.Default, L("Permission:FileEntries"));
            fileEntries.AddChild(FileServicePermissions.FileEntry.Update, L("Permission:FileEntries.Edit"));
            fileEntries.AddChild(FileServicePermissions.FileEntry.Delete, L("Permission:FileEntries.Delete"));
            fileEntries.AddChild(FileServicePermissions.FileEntry.Create, L("Permission:FileEntries.Create"));
            
            var fileRequests = fileManagmentGroup.AddPermission(FileServicePermissions.FileRequest.Default, L("Permission:FileRequests"));
            fileRequests.AddChild(FileServicePermissions.FileRequest.Update, L("Permission:FileRequests.Edit"));
            fileRequests.AddChild(FileServicePermissions.FileRequest.Delete, L("Permission:FileRequests.Delete"));
            fileRequests.AddChild(FileServicePermissions.FileRequest.Create, L("Permission:FileRequests.Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FileServiceResource>(name);
        }
    }
}
