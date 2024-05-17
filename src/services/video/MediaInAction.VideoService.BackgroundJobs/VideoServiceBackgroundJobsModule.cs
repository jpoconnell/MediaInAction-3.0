using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.FileEntryNs;
using MediaInAction.VideoService.FileServicesNs;
using MediaInAction.VideoService.MediaMatchingServicesNs;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.ParserNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.ToBeMappedsNs;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Quartz;
using Volo.Abp.Modularity;

namespace MediaInAction.VideoService.BackgroundJobs;

[DependsOn(
        typeof(AbpBackgroundJobsQuartzModule),
        typeof(VideoServiceEntityFrameworkCoreModule) 
    )]
    
public class VideoServiceBackgroundJobsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {     
        context.Services.TryAddSingleton<IFileMapper, FileMapper>();
        context.Services.TryAddSingleton<IEpisodeService, EpisodeService>();
        context.Services.TryAddSingleton<IMovieService, MovieService>();
        context.Services.TryAddSingleton<ISeriesService, SeriesService>();
        context.Services.TryAddSingleton<IFileEntryLibService, FileEntryLibService>();
        context.Services.TryAddSingleton<ISeriesMatchingService, SeriesMatchingService>();
        context.Services.TryAddSingleton<IParserService, ParserService>();
        context.Services.TryAddSingleton<IToBeMappedService,ToBeMappedService>();
    }
    
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var configuration = context.GetConfiguration();
        
    }
}

