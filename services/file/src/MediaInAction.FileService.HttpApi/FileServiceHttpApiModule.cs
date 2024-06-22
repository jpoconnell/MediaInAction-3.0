using Localization.Resources.AbpUi;
using MediaInAction.FileService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MediaInAction.FileService;

[DependsOn(
    typeof(FileServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class FileServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(FileServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<FileServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}