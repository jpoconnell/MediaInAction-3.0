using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using MediaInAction.VideoService.EntityFrameworkCore;
using Volo.Abp.AutoMapper;

namespace MediaInAction.VideoService.Lib
{
    [DependsOn(
        typeof(VideoServiceDomainModule),
        typeof(VideoServiceEntityFrameworkCoreModule) 
    )]
    public class VideoServiceLibModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {  
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<VideoServiceLibModule>(); });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            
            // ... others
        }
    }
}
