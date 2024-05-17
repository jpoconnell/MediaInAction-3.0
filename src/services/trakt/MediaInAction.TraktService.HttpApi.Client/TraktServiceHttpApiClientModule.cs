using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(TraktServiceApplicationContractsModule),
        typeof(AbpHttpClientModule)
    )]
    public class TraktServiceHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Trakt";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(
                typeof(TraktServiceApplicationContractsModule).Assembly,
                RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<TraktServiceHttpApiClientModule>();
            });
        }
    }
}
