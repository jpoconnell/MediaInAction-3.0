using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService;

[DependsOn(
    typeof(TraktServiceDomainModule),
    typeof(TraktServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule)
)]
public class TraktServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<TraktServiceApplicationModule>(); });
    }
}