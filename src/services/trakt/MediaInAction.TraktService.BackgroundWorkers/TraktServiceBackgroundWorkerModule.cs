using System.Threading.Tasks;
using MediaInAction.EmbyService.BackgroundWorkers.Workers;
using MediaInAction.TraktService.BackgroundWorkers.Workers;
using MediaInAction.TraktService.Config;
using MediaInAction.TraktService.MongoDb;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktShowNs;
using MediaInAction.TraktService.TraktShowSeasonNs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TraktNet;
using Volo.Abp;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;
using Volo.Abp.Quartz;

namespace MediaInAction.TraktService.BackgroundWorkers;

[DependsOn(
    typeof(AbpBackgroundWorkersQuartzModule),
    typeof(TraktServiceLibModule),
    typeof(TraktServiceDomainSharedModule),
    typeof(TraktServiceMongoDbModule) 
)]
public class TraktServiceBackgroundWorkerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        PreConfigure<AbpQuartzOptions>(options =>
        {
            options.Configurator = configure =>
            {
                configure.UseInMemoryStore(storeOptions =>
                {
                });
            };
        });
    }
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddSingleton<TraktClient, TraktClient>();
        context.Services.TryAddSingleton<ITraktEpisodeLibService, TraktEpisodeLibService>();
        context.Services.TryAddSingleton<ITraktShowLibService, TraktShowLibService>();
        context.Services.TryAddSingleton<ITraktShowSeasonService, TraktShowSeasonService>();
        context.Services.TryAddSingleton<ITraktMovieLibService, TraktMovieLibService>();
        context.Services.TryAddSingleton<ServicesConfiguration, ServicesConfiguration>();
        context.Services.TryAddSingleton<ITraktService, Lib.TraktService>();
        
        Configure<AbpBackgroundWorkerQuartzOptions>(options =>
        {
            options.IsAutoRegisterEnabled = true;
        });
    }
    
    public override async Task OnApplicationInitializationAsync(
        ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<TraktCalendarWorker>();
        await context.AddBackgroundWorkerAsync<TraktCollectionsWorker>();
        await context.AddBackgroundWorkerAsync<EpisodeCleanupWorker>();
        await context.AddBackgroundWorkerAsync<TraktWatchedWorker>();
        await context.AddBackgroundWorkerAsync<TraktWatchListWorker>();
        await context.AddBackgroundWorkerAsync<ResendUnAcceptedMediaWorker>();
    }
}