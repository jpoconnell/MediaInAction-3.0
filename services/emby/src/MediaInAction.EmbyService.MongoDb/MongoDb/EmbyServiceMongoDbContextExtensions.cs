using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.EmbyMovieNs;
using MediaInAction.EmbyService.EmbyMoviesNs.Dtos;
using MediaInAction.EmbyService.EmbyRequestsNs;
using MediaInAction.EmbyService.EmbyShowsNs;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace MediaInAction.EmbyService.MongoDb
{
    public static class EmbyServiceMongoDbContextExtensions
    {
        public static void ConfigureEmbyService(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            
            builder.Entity<Request>(x =>
            {
                x.CollectionName = "EmbyRequest";
            });
            
            builder.Entity<EmbyShow>(x =>
            {
                x.CollectionName = "EmbyShow";
            });
            builder.Entity<EmbyMovie>(x =>
            {
                x.CollectionName = "EmbyMovie";
            });
            builder.Entity<EmbyEpisode>(x =>
            {
                x.CollectionName = "EmbyEpisode";
            });
        }
    }
}
