using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Microsoft.Extensions.Options;
using System;
using Microsoft.Extensions.Logging;

namespace MediaInAction.DelugeService
{
    [DependsOn(
        typeof(DelugeServiceDomainModule),
        typeof(DelugeServiceApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
    )]
    public class DelugeServiceApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DelugeServiceApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<DelugeServiceApplicationModule>(validate: true);
            });
            
        }
    }
}