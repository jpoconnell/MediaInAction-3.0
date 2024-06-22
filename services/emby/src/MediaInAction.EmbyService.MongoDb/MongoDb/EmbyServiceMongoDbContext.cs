using MediaInAction.EmbyService.ActivityLogEntryNs;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.EmbyItems;
using MediaInAction.EmbyService.EmbyMovieNs;
using MediaInAction.EmbyService.EmbyMoviesNs.Dtos;
using MediaInAction.EmbyService.EmbyShowsNs;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.EmbyService.MongoDb
{
    [ConnectionStringName(EmbyServiceDbProperties.ConnectionStringName)]
    public class EmbyServiceMongoDbContext : AbpMongoDbContext, IEmbyServiceMongoDbContext
    {
        public IMongoCollection<EmbyRequest> EmbyRequests => Collection<EmbyRequest>();
        public IMongoCollection<EmbyShow> EmbyShows => Collection<EmbyShow>();
        public IMongoCollection<EmbyMovie> EmbyMovies => Collection<EmbyMovie>();
        public IMongoCollection<EmbyEpisode> EmbyEpisodes => Collection<EmbyEpisode>();
        public IMongoCollection<EmbyActivityLogEntry> EmbyActivityLogEntries => Collection<EmbyActivityLogEntry>();
        public IMongoCollection<EmbyItem> EmbyItems => Collection<EmbyItem>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureEmbyService();
        }
    }

    public class EmbyRequest
    {
    }
}