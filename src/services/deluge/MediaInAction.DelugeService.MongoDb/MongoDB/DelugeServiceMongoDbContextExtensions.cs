using MediaInAction.DelugeService.TorrentNs;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace MediaInAction.DelugeService.MongoDb
{
    public static class DelugeServiceMongoDbContextExtensions
    {
        public static void ConfigureDelugeService(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            
            builder.Entity<Torrent>(x =>
            {
                x.CollectionName = "Torrents";
            });
        }
    }
}
