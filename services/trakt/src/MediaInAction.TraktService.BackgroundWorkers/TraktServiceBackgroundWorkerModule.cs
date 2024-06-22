using System.Threading.Tasks;
using MediaInAction.TraktService.BackgroundWorkers.Workers;
using MediaInAction.TraktService.Nodb.Lib;
using MediaInAction.TraktService.Nodb.Lib.Config;
using MediaInAction.TraktService.Nodb.Lib.TraktEpisodeNs;
using MediaInAction.TraktService.Nodb.Lib.TraktMovieNs;
using MediaInAction.TraktService.Nodb.Lib.TraktShowNs;
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
    typeof(TraktServiceDomainSharedModule),
    typeof(TraktServiceNodbLibModule)
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
        context.Services.TryAddSingleton<EpisodeClient, EpisodeClient>();
        context.Services.TryAddSingleton<SeriesClient, SeriesClient>();
        context.Services.TryAddSingleton<ServicesConfiguration, ServicesConfiguration>();
        context.Services.TryAddSingleton<ITraktNodbService, TraktNodbService>();
        context.Services.TryAddSingleton<ITraktShowNodbLibService, TraktShowNodbLibService>();
        context.Services.TryAddSingleton<ITraktEpisodeNodbLibService, TraktEpisodeNodbLibService>();
        context.Services.TryAddSingleton<ITraktMovieNodbLibService, TraktMovieNodbLibService>();
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
       // await context.AddBackgroundWorkerAsync<EpisodeCleanupWorker>();
       // await context.AddBackgroundWorkerAsync<TraktWatchedWorker>();
       // await context.AddBackgroundWorkerAsync<TraktWatchListWorker>();
        //await context.AddBackgroundWorkerAsync<ResendUnAcceptedMediaWorker>();
    }
}