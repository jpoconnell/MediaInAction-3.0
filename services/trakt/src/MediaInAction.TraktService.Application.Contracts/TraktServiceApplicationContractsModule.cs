using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(TraktServiceDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class TraktServiceApplicationContractsModule : AbpModule
    {

    }
}
