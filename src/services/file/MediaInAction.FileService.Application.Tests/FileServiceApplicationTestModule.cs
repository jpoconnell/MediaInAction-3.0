using Volo.Abp.Modularity;

namespace MediaInAction.FileService
{
    [DependsOn(
        typeof(FileServiceApplicationModule),
        typeof(FileServiceDomainTestModule)
        )]
    public class FileServiceApplicationTestModule : AbpModule
    {

    }
}