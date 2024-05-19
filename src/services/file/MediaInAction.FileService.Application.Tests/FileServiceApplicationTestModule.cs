using Volo.Abp.Modularity;

namespace MediaInAction.FileService
{
    [DependsOn(
        typeof(FileServiceApplicationModule)
        )]
    public class FileServiceApplicationTestModule : AbpModule
    {

    }
}