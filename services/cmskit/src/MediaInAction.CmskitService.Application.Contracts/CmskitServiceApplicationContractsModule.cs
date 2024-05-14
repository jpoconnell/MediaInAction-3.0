using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace MediaInAction.CmskitService;

[DependsOn(
    typeof(CmskitServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(CmsKitApplicationContractsModule))]
public class CmskitServiceApplicationContractsModule : AbpModule
{

}
