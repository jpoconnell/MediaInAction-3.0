using Localization.Resources.AbpUi;
using MediaInAction.PaymentService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MediaInAction.PaymentService
{
    [DependsOn(
        typeof(PaymentServiceApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class PaymentServiceHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(PaymentServiceHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<PaymentServiceResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
