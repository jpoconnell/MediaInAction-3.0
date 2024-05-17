using MediaInAction.TraktService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MediaInAction.TraktService.Permissions
{
    public class TraktServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var traktManagmentGroup = context.AddGroup(TraktServicePermissions.GroupName, L("Permission:TraktService"));
            
            
            var shows = traktManagmentGroup.AddPermission(TraktServicePermissions.TraktShow.Default, L("Permission:TraktShow"));
            shows.AddChild(TraktServicePermissions.TraktShow.Create, L("Permission:TraktShow.Create"));
            shows.AddChild(TraktServicePermissions.TraktShow.Edit, L("Permission:TraktShow.Update"));
            shows.AddChild(TraktServicePermissions.TraktShow.Delete, L("Permission:TraktShow.Delete"));
            shows.AddChild(TraktServicePermissions.TraktShow.Dashboard, L("Permission:TraktShow.Dashboard"));

            var episodes = traktManagmentGroup.AddPermission(TraktServicePermissions.TraktEpisode.Default, L("Permission:TraktEpisode"));
            episodes.AddChild(TraktServicePermissions.TraktEpisode.Create, L("Permission:TraktEpisode.Create"));
            episodes.AddChild(TraktServicePermissions.TraktEpisode.Edit, L("Permission:TraktEpisode.Update"));
            episodes.AddChild(TraktServicePermissions.TraktEpisode.Delete, L("Permission:TraktEpisode.Delete"));
            episodes.AddChild(TraktServicePermissions.TraktEpisode.Dashboard, L("Permission:TraktEpisode.Dashboard")); 
            
            var movies = traktManagmentGroup.AddPermission(TraktServicePermissions.TraktMovie.Default, L("Permission:TraktMovie"));
            movies.AddChild(TraktServicePermissions.TraktMovie.Create, L("Permission:TraktMovie.Create"));
            movies.AddChild(TraktServicePermissions.TraktMovie.Update, L("Permission:TraktMovie.Update"));
            movies.AddChild(TraktServicePermissions.TraktMovie.Delete, L("Permission:TraktMovie.Delete"));
            movies.AddChild(TraktServicePermissions.TraktMovie.Dashboard, L("Permission:TraktMovie.Dashboard"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TraktServiceResource>(name);
        }
    }
}