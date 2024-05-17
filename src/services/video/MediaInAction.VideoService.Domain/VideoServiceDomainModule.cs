using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MediaInAction.VideoService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(VideoServiceDomainSharedModule)
)]
public class VideoServiceDomainModule : AbpModule
{

}