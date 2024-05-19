using Volo.Abp.Modularity;

namespace MediaInAction.IdentityService
{
    [DependsOn(
        typeof(IdentityServiceApplicationModule),
        typeof(IdentityServiceDomainTestModule)
        )]
    public class IdentityServiceApplicationTestModule : AbpModule
    {

    }
}