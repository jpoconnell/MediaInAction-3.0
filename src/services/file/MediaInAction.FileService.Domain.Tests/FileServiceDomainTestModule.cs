using MediaInAction.FileService.MongoDB;
using Volo.Abp.Modularity;

namespace MediaInAction.FileService
{
    [DependsOn(
        typeof(FileServiceMongoDbTestModule)
        )]
    public class FileServiceDomainTestModule : AbpModule
    {

    }
}