using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

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
        context.Services.AddAutoMapperObjectMapper<VideoServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<VideoServiceApplicationModule>(validate: true);
        });
    }
}