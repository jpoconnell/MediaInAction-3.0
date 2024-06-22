using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace MediaInAction.VideoService;

[DependsOn(
    typeof(VideoServiceDomainSharedModule),
    typeof(AbpDddDomainModule),
    typeof(AbpAutoMapperModule)
)]
public class VideoServiceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<VideoServiceDomainModule>(validate: true); });

        Configure<AbpDistributedEntityEventOptions>(options =>
        {
            //options.AutoEventSelectors.Add<Product>();
            //options.EtoMappings.Add<Product, ProductEto>();
        });
    }
}