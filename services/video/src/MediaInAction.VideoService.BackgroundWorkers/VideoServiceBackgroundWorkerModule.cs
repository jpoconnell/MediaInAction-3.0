using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.BackgroundWorkers.Workers;
using MediaInAction.VideoService.DataMaintenanceNs;
using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.FileEntryNs;
using MediaInAction.VideoService.FileServicesNs;
using MediaInAction.VideoService.Lib;
using MediaInAction.VideoService.MediaMatchingServicesNs;
using MediaInAction.VideoService.MovieAliasNs;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.ParserNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.ToBeMappedsNs;
using MediaInAction.VideoService.TorrentNs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
using Volo.Abp;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;
using Volo.Abp.Quartz;

namespace MediaInAction.VideoService.BackgroundWorkers;

[DependsOn(
    typeof(AbpBackgroundWorkersQuartzModule),
    typeof(VideoServiceLibModule),
    typeof(VideoServiceDomainSharedModule),
    typeof(VideoServiceEntityFrameworkCoreModule) 
)]
public class VideoServiceBackgroundWorkerModule : AbpModule
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
        context.Services.TryAddSingleton<IFileMapper,FileMapper>();
        context.Services.TryAddSingleton<IFileMove,FileMove>();
        context.Services.TryAddSingleton<IEpisodeService,EpisodeService>();
        context.Services.TryAddSingleton<IToBeMappedService,ToBeMappedService>();
        
        context.Services.TryAddSingleton<ISeriesService,SeriesService>();
        context.Services.TryAddSingleton<ISeriesAliasService,SeriesAliasService>();   
        context.Services.TryAddSingleton<ISeriesMatchingService,SeriesMatchingService>();
        
        context.Services.TryAddSingleton<IMovieService,MovieService>();
        context.Services.TryAddSingleton<IMovieAliasRepository,EfCoreMovieAliasRepository>();
        
        context.Services.TryAddSingleton<IMovieAliasLibService,MovieAliasLibService>();
        context.Services.TryAddSingleton<IMovieMatchingService,MovieMatchingService>();
        
        context.Services.TryAddSingleton<IParserService,ParserService>();
        context.Services.TryAddSingleton<IFileEntryLibService,FileEntryLibService>();
        context.Services.TryAddSingleton<ITorrentService, TorrentService>();
        context.Services.TryAddSingleton<ITorrentMapper,TorrentMapper>();
        context.Services.TryAddSingleton<IDataMaintenance,DataMaintenance>();
        context.Services.TryAddSingleton<IProcessToBeMappeds,ProcessToBeMappeds>();
        context.Services.TryAddSingleton<IProcessMappedFiles,ProcessMappedFiles>();
        //context.Services.TryAddSingleton<ISendEventsToTrakt,SendEventsToTrakt>();
        //context.Services.TryAddSingleton<ITraktRequestService,TraktRequestService>();
        
        
        Configure<AbpBackgroundWorkerQuartzOptions>(options =>
        {
            options.IsAutoRegisterEnabled = true;
        });
    }
    
    public override async Task OnApplicationInitializationAsync(
        ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<DataMaintenanceWorker>();
        await context.AddBackgroundWorkerAsync<FileMapperWorker>();
        await context.AddBackgroundWorkerAsync<TorrentMapperWorker>();
        await context.AddBackgroundWorkerAsync<FileMoveListWorker>();
    }
}


