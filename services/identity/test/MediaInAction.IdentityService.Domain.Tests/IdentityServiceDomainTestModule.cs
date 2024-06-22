using MediaInAction.IdentityService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MediaInAction.IdentityService
{
    [DependsOn(
        typeof(IdentityServiceEntityFrameworkCoreTestModule)
        )]
    public class IdentityServiceDomainTestModule : AbpModule
    {

    }
}