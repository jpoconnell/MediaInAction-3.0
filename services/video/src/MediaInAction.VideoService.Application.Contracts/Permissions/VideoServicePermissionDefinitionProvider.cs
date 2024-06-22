using MediaInAction.VideoService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.VideoService.Permissions
{
    public class VideoServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var videoManagmentGroup = context.AddGroup(VideoServicePermissions.GroupName, L("Permission:VideoService"));
            
            var oders = videoManagmentGroup.AddPermission(VideoServicePermissions.Seriess.Default, L("Permission:Series"));
            oders.AddChild(VideoServicePermissions.Seriess.Create, L("Permission:Series.Create"));
            oders.AddChild(VideoServicePermissions.Seriess.Update, L("Permission:Series.Update"));
            oders.AddChild(VideoServicePermissions.Seriess.Delete, L("Permission:Series.Delete"));
            
            var episodes = videoManagmentGroup.AddPermission(VideoServicePermissions.Episodes.Default, L("Permission:Episode"));
            episodes.AddChild(VideoServicePermissions.Episodes.Create, L("Permission:Episode.Create"));
            episodes.AddChild(VideoServicePermissions.Episodes.Update, L("Permission:Episode.Update"));
            episodes.AddChild(VideoServicePermissions.Episodes.Delete, L("Permission:Episode.Delete"));
            
            var oders2 = videoManagmentGroup.AddPermission(VideoServicePermissions.Movies.Default, L("Permission:Movies"));
            oders2.AddChild(VideoServicePermissions.Movies.Create, L("Permission:Movies.Create"));
            oders2.AddChild(VideoServicePermissions.Movies.Update, L("Permission:Movies.Update"));
            oders2.AddChild(VideoServicePermissions.Movies.Delete, L("Permission:Movies.Delete"));
            
            var tobe = videoManagmentGroup.AddPermission(VideoServicePermissions.ToBeMappeds.Default, L("Permission:ToBeMappeds"));
            tobe.AddChild(VideoServicePermissions.ToBeMappeds.Create, L("Permission:ToBeMappeds.Create"));
            tobe.AddChild(VideoServicePermissions.ToBeMappeds.Update, L("Permission:ToBeMappeds.Update"));
            tobe.AddChild(VideoServicePermissions.ToBeMappeds.Delete, L("Permission:ToBeMappeds.Delete"));
            
            var file = videoManagmentGroup.AddPermission(VideoServicePermissions.FileEntries.Default, L("Permission:FileEntries"));
            file.AddChild(VideoServicePermissions.FileEntries.Create, L("Permission:FileEntries.Create"));
            file.AddChild(VideoServicePermissions.FileEntries.Update, L("Permission:FileEntries.Update"));
            file.AddChild(VideoServicePermissions.FileEntries.Delete, L("Permission:FileEntries.Delete"));

            var torrent = videoManagmentGroup.AddPermission(VideoServicePermissions.Torrents.Default, L("Permission:Torrents"));
            torrent.AddChild(VideoServicePermissions.Torrents.Create, L("Permission:Torrents.Create"));
            torrent.AddChild(VideoServicePermissions.Torrents.Update, L("Permission:Torrents.Update"));
            torrent.AddChild(VideoServicePermissions.Torrents.Delete, L("Permission:Torrents.Delete"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<VideoServiceResource>(name);
        }
    }
}