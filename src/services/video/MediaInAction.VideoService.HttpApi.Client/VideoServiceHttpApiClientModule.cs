using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MediaInAction.VideoService;

[DependsOn(
    typeof(VideoServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class VideoServiceHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Video";
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(VideoServiceApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<VideoServiceHttpApiClientModule>();
        });
    }
}