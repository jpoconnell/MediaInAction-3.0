using MediaInAction.Shared.Hosting.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace MediaInAction.Shared.Hosting.Gateways
{
    [DependsOn(
        typeof(MediaInActionSharedHostingAspNetCoreModule)
    )]
    public class MediaInActionSharedHostingGatewaysModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddHttpForwarderWithServiceDiscovery();
            
            context.Services.AddReverseProxy()
                .LoadFromConfig(configuration.GetSection("ReverseProxy"))
                .AddServiceDiscoveryDestinationResolver();
        }
    }
}