using Localization.Resources.AbpUi;
using MediaInAction.TraktService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(TraktServiceApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class TraktServiceHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(TraktServiceHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<TraktServiceResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
