using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Core;
using System;
using MediaInAction.TraktService.TraktRequests;
using Microsoft.Extensions.Logging;
using MediaInAction.TraktService.TraktMethods;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(TraktServiceDomainModule),
        typeof(TraktServiceApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
    )]
    public class TraktServiceApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<TraktServiceApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<TraktServiceApplicationModule>(validate: true);
            });
            

            context.Services.AddTransient<TraktMethodResolver>(provider => new TraktMethodResolver(
                provider.GetServices<ITraktMethod>(),
                provider.GetRequiredService<ILogger<TraktMethodResolver>>()
            ));
        }
    }
}