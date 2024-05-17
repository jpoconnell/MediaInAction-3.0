using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MediaInAction.FileService
{
    [DependsOn(
        typeof(FileServiceApplicationContractsModule),
        typeof(AbpHttpClientModule)
    )]
    public class FileServiceHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "File";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(
                typeof(FileServiceApplicationContractsModule).Assembly,
                RemoteServiceName
            );

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<FileServiceHttpApiClientModule>();
            });
        }
    }
}
