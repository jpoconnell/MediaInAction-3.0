using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace MediaInAction.EmbyService
{
    [DependsOn(
        typeof(EmbyServiceDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class EmbyServiceApplicationContractsModule : AbpModule
    {

    }
}
