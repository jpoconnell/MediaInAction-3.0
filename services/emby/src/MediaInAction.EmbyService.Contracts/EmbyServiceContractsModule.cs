using MediaInAction.EmbyService.Localization;
using MediaInAction.VideoService;
using Volo.Abp.Application;
using Volo.Abp.Authorization;

using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;

namespace MediaInAction.EmbyService;

[DependsOn(
    typeof(VideoServiceApplicationContractsModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpValidationModule)
)]
public class EmbyServiceContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<EmbyServiceResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/EmbyService");

            options.DefaultResourceType = typeof(EmbyServiceResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("EmbyService", typeof(EmbyServiceResource));
        });
    }
}