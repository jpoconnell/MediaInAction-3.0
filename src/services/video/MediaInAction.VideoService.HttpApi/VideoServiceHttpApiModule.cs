using Localization.Resources.AbpUi;
using MediaInAction.VideoService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MediaInAction.VideoService;

[DependsOn(
    typeof(VideoServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class VideoServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(VideoServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<VideoServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}