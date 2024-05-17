using Volo.Abp.Modularity;

namespace MediaInAction.VideoService
{
    [DependsOn(
        typeof(VideoServiceApplicationModule),
        typeof(VideoServiceDomainTestModule)
    )]
    public class VideoServiceApplicationTestModule : AbpModule
    {

    }
}