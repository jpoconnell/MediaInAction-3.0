using MediaInAction.EmbyService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.EmbyService.Permissions
{
    public class EmbyServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var traktManagmentGroup = context.AddGroup(EmbyServicePermissions.GroupName, L("Permission:EmbyService"));
            
            
            var shows = traktManagmentGroup.AddPermission(EmbyServicePermissions.Show.Default, L("Permission:Show"));
            shows.AddChild(EmbyServicePermissions.Show.Create, L("Permission:Show.Create"));
            shows.AddChild(EmbyServicePermissions.Show.Edit, L("Permission:Show.Update"));
            shows.AddChild(EmbyServicePermissions.Show.Delete, L("Permission:Show.Delete"));

            var episodes = traktManagmentGroup.AddPermission(EmbyServicePermissions.Episode.Default, L("Permission:Episode"));
            episodes.AddChild(EmbyServicePermissions.Episode.Create, L("Permission:Episode.Create"));
            episodes.AddChild(EmbyServicePermissions.Episode.Edit, L("Permission:Episode.Update"));
            episodes.AddChild(EmbyServicePermissions.Episode.Delete, L("Permission:Episode.Delete"));

            var movies = traktManagmentGroup.AddPermission(EmbyServicePermissions.Movie.Default, L("Permission:EMovie"));
            movies.AddChild(EmbyServicePermissions.Movie.Create, L("Permission:Movie.Create"));
            movies.AddChild(EmbyServicePermissions.Movie.Update, L("Permission:Movie.Update"));
            movies.AddChild(EmbyServicePermissions.Movie.Delete, L("Permission:Movie.Delete"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EmbyServiceResource>(name);
        }
    }
}