using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MediaInAction.FileService;

[DependsOn(
    typeof(FileServiceDomainModule),
    typeof(FileServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule)
)]
public class FileServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<FileServiceApplicationModule>(); });
    }
}