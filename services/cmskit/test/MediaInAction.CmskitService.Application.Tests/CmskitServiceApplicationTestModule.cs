using Volo.Abp.Modularity;

namespace MediaInAction.CmskitService;

[DependsOn(
    typeof(CmskitServiceApplicationModule),
    typeof(CmskitServiceDomainTestModule)
    )]
public class CmskitServiceApplicationTestModule : AbpModule
{

}
