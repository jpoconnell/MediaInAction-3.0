using MediaInAction.FileService.FileEntriesNs;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace MediaInAction.FileService.MongoDb
{
    public static class FileServiceMongoDbContextExtensions
    {
        public static void ConfigureFileService(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            
            builder.Entity<FileEntry>(x =>
            {
                x.CollectionName = "FileEntries";
            });
        }
    }
}
