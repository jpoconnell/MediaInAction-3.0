using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MediaInAction.VideoService;

[DependsOn(
    typeof(VideoServiceDomainModule),
    typeof(VideoServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule)
)]
public class VideoServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<VideoServiceApplicationModule>(); });
    }
}