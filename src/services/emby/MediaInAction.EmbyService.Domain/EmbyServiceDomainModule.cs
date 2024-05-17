using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MediaInAction.EmbyService
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(EmbyServiceDomainSharedModule)
    )]
    public class EmbyServiceDomainModule : AbpModule
    {

    }
}
