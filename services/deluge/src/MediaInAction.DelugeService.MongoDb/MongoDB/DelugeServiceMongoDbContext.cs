using MediaInAction.DelugeService.TorrentNs;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.DelugeService.MongoDb
{
    [ConnectionStringName(DelugeServiceDbProperties.ConnectionStringName)]
    public class DelugeServiceMongoDbContext : AbpMongoDbContext, IDelugeServiceMongoDbContext
    {
        public IMongoCollection<Torrent> Torrents => Collection<Torrent>();


        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureDelugeService();
        }
    }
}