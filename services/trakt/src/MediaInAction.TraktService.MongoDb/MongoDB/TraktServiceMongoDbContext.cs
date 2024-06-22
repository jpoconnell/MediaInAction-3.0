using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktRequests;
using MediaInAction.TraktService.TraktShowNs;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.TraktService.MongoDb
{
    [ConnectionStringName(TraktServiceDbProperties.ConnectionStringName)]
    public class TraktServiceMongoDbContext : AbpMongoDbContext, ITraktServiceMongoDbContext
    {
        public IMongoCollection<TraktRequest> TraktRequests => Collection<TraktRequest>();
        public IMongoCollection<TraktShow> TraktShows => Collection<TraktShow>();
        public IMongoCollection<TraktMovie> TraktMovies => Collection<TraktMovie>();
        public IMongoCollection<TraktEpisode> TraktEpisodes => Collection<TraktEpisode>();

        
        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureTraktService();
        }
    }
}