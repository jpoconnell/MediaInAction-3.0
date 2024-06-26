using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace MediaInAction.CmskitService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(CmskitServiceDomainSharedModule)
)]
[DependsOn(typeof(CmsKitDomainModule))]
    public class CmskitServiceDomainModule : AbpModule
{
}
