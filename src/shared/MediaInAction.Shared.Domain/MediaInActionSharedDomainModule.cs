using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.VirtualFileSystem;

namespace MediaInAction.Shared.Domain
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class ServiceDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<ServiceDomainSharedModule>();
            });

        }
    }
}
