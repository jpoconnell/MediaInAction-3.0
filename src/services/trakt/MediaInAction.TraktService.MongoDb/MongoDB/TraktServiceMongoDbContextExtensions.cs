using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktRequests;
using MediaInAction.TraktService.TraktShowNs;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace MediaInAction.TraktService.MongoDb
{
    public static class TraktServiceMongoDbContextExtensions
    {
        public static void ConfigureTraktService(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            
            builder.Entity<TraktRequest>(x =>
            {
                x.CollectionName = "TraktRequests";
            });
            
            builder.Entity<TraktShow>(x =>
            {
                x.CollectionName = "TraktShows";
            });
            builder.Entity<TraktMovie>(x =>
            {
                x.CollectionName = "TraktMovies";
            });
            builder.Entity<TraktEpisode>(x =>
            {
                x.CollectionName = "TraktEpisodes";
            });
        }
    }
}
