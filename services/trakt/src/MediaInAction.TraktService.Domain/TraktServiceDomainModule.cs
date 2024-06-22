using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(TraktServiceDomainSharedModule)
    )]
    public class TraktServiceDomainModule : AbpModule
    {

    }
}
