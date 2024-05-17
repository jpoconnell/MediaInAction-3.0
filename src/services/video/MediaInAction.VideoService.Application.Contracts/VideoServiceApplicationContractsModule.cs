using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace MediaInAction.VideoService;

[DependsOn(
    typeof(VideoServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
)]
public class VideoServiceApplicationContractsModule : AbpModule
{

}