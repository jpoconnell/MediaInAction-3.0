using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MediaInAction.EmbyService
{
    [DependsOn(
        typeof(EmbyServiceApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EmbyServiceHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(
                typeof(EmbyServiceApplicationContractsModule).Assembly,
                EmbyServiceRemoteServiceConsts.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EmbyServiceHttpApiClientModule>();
            });

        }
    }
}
