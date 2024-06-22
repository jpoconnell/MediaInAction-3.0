using Localization.Resources.AbpUi;
using MediaInAction.DelugeService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MediaInAction.DelugeService
{
    [DependsOn(
        typeof(DelugeServiceApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class DelugeServiceHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DelugeServiceHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DelugeServiceResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
