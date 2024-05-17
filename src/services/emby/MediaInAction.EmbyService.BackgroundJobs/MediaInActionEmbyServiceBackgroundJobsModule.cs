using MediaInAction.EmbyService.MongoDb;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Quartz;
using Volo.Abp.Modularity;

namespace MediaInAction.EmbyService.BackgroundJobs;

[DependsOn(
        typeof(AbpBackgroundJobsQuartzModule), 
        typeof(EmbyServiceMongoDbModule) 
    )]
    
public class EmbyServiceBackgroundJobsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //context.Services.TryAddSingleton<IEmbyService, EmbyService>();

    }
    
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var configuration = context.GetConfiguration();
    }
}

