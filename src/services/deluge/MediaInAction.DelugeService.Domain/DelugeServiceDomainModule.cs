using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DelugeServiceDomainSharedModule)
    )]
    public class DelugeServiceDomainModule : AbpModule
    {

    }
}
