using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(TraktServiceApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class TraktServiceHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(
                typeof(TraktServiceApplicationContractsModule).Assembly,
                TraktServiceRemoteServiceConsts.RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<TraktServiceHttpApiClientModule>();
            });

        }
    }
}
