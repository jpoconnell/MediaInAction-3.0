using Volo.Abp.Modularity;

namespace MediaInAction.VideoService
{
    [DependsOn(
        typeof(VideoServiceApplicationModule)
    )]
    public class VideoServiceApplicationTestModule : AbpModule
    {

    }
}