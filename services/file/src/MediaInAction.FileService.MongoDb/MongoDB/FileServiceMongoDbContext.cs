using MediaInAction.FileService.FileEntriesNs;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MediaInAction.FileService.MongoDb
{
    [ConnectionStringName(FileServiceDbProperties.ConnectionStringName)]
    public class FileServiceMongoDbContext : AbpMongoDbContext, IFileServiceMongoDbContext
    {
        public IMongoCollection<FileEntry> FileEntries => Collection<FileEntry>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureFileService();
        }
    }
}