using MediaInAction.DelugeService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.DelugeService.Permissions
{
    public class DelugeServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var torrentGroup = context.AddGroup(DelugeServicePermissions.GroupName, L("Permission:DelugeService"));
            var torrents = torrentGroup.AddPermission(DelugeServicePermissions.Torrent.Default, L("Permission:Torrent"));
            torrents.AddChild(DelugeServicePermissions.Torrent.Create, L("Permission:Torrent.Create"));
            torrents.AddChild(DelugeServicePermissions.Torrent.Edit, L("Permission:Torrent.Update"));
            torrents.AddChild(DelugeServicePermissions.Torrent.Delete, L("Permission:Torrent.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DelugeServiceResource>(name);
        }
    }
}