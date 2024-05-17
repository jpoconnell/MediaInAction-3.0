using MediaInAction.TraktService.MongoDb;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktShowNs;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Quartz;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService.BackgroundJobs
{

    [DependsOn(
        typeof(AbpBackgroundJobsQuartzModule),
        typeof(TraktServiceMongoDbModule) 
    )]
    
    public class TraktServiceBackgroundJobsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {      
            context.Services.TryAddSingleton<ITraktMovieLibService, TraktMovieLibService>();
            context.Services.TryAddSingleton<ITraktShowLibService, TraktShowLibService>();
            context.Services.TryAddSingleton<ITraktEpisodeLibService, TraktEpisodeLibService>();
            context.Services.TryAddSingleton<ITraktMovieLibService, TraktMovieLibService>();
            context.Services.TryAddSingleton<ITraktService, TraktService.Lib.TraktService>();
        }
        
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var configuration = context.GetConfiguration();
        }
    }
}
