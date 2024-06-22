using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.TraktService.MongoDb
{
    [ConnectionStringName(TraktServiceDbProperties.ConnectionStringName)]
    public interface ITraktServiceMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
