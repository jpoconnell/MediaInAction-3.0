using Localization.Resources.AbpUi;
using MediaInAction.EmbyService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MediaInAction.EmbyService
{
    [DependsOn(
        typeof(EmbyServiceApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EmbyServiceHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EmbyServiceHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<EmbyServiceResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
