using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.EmbyService.MongoDb
{
    [ConnectionStringName(EmbyServiceDbProperties.ConnectionStringName)]
    public interface IEmbyServiceMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
