using System.Threading.Tasks;
using MediaInAction.TraktService.BackgroundJobs.JobArgs;
using MediaInAction.TraktService.Lib;
using Microsoft.Extensions.Logging;
using TraktNet;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService.BackgroundJobs.Jobs;

public class TraktGetCalendarJob
        : AsyncBackgroundJob<TraktCalendarArgs>, ITransientDependency
    {
        private ITraktService _traktService;
        private readonly ILogger<TraktGetCalendarJob> _logger;

    public TraktGetCalendarJob( ITraktService traktService,
        ILogger<TraktGetCalendarJob> logger)
    {
        _traktService = traktService;
        _logger = logger;
    }

    public override async Task ExecuteAsync(TraktCalendarArgs args)
    {
        Logger.LogInformation("Background Job TraktGetCalendarJob Starting");
        await _traktService.SyncCalendarAsync();
        Logger.LogInformation("Background Job TraktGetCalendarJob Finished");
    }
}
