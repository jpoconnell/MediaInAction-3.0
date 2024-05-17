using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;

namespace MediaInAction.Shared.Hosting.AspNetCore;

[DependsOn(
    typeof(MediaInActionSharedHostingModule),
    typeof(MediaInActionSharedLocalizationModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
)]
public class MediaInActionSharedHostingAspNetCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MediaInActionSharedHostingAspNetCoreModule>("MediaInAction.Shared.Hosting.AspNetCore");
        });
    }
}