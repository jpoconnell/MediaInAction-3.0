using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace MediaInAction.DelugeService
{
    [DependsOn(
        typeof(DelugeServiceDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class DelugeServiceApplicationContractsModule : AbpModule
    {

    }
}
