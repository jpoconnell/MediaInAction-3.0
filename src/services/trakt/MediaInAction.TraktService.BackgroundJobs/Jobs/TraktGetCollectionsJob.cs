using System.Threading.Tasks;
using MediaInAction.TraktService.BackgroundJobs.JobArgs;
using MediaInAction.TraktService.Lib;
using Microsoft.Extensions.Logging;
using TraktNet;
using TraktNet.Objects.Authentication;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService.BackgroundJobs.Jobs
{
    public class TraktGetCollectionsJob
        : AsyncBackgroundJob<TraktGetCollectionArgs>, ITransientDependency
    {
        private ITraktService _traktService;

        public TraktGetCollectionsJob( ITraktService traktService)
        {
            _traktService = traktService;
        }

        public override async Task ExecuteAsync(TraktGetCollectionArgs args)
        {
            Logger.LogInformation("Background Job TraktGetCollectionsJob Starting");

            // Get Shows Collection  
            await  _traktService.GetShowCollection();
      
            // Get Movies Collection
            await _traktService.GetMovieCollection();

            // Get Watch List 
            await _traktService.GetWatchedList("Shows");
            await _traktService.GetWatchedList("Movies");
            await _traktService.GetLastActivities();
            Logger.LogInformation("Background Job TraktGetCollectionsJob Finished");
        }
    }
}