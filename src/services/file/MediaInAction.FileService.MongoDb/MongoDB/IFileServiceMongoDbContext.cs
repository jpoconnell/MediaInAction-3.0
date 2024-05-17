using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.FileService.MongoDb
{
    [ConnectionStringName(FileServiceDbProperties.ConnectionStringName)]
    public interface IFileServiceMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
