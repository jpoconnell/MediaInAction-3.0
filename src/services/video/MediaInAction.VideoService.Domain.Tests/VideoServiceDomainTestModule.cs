using MediaInAction.VideoService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MediaInAction.VideoService
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(VideoServiceEntityFrameworkCoreTestModule)
        )]
    public class VideoServiceDomainTestModule : AbpModule
    {
        
    }
}
