using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace MediaInAction.IdentityService
{
    [DependsOn(
        typeof(IdentityServiceDomainSharedModule),
        typeof(AbpIdentityDomainModule)
    )]
    public class IdentityServiceDomainModule : AbpModule
    {
    }
}