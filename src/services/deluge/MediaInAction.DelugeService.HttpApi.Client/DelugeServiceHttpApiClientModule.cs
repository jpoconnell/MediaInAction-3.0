using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MediaInAction.DelugeService
{
    [DependsOn(
        typeof(DelugeServiceApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class DelugeServiceHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(
                typeof(DelugeServiceApplicationContractsModule).Assembly,
                DelugeServiceRemoteServiceConsts.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DelugeServiceHttpApiClientModule>();
            });

        }
    }
}
