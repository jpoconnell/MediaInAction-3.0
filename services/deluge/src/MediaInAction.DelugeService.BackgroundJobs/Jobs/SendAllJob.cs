using System;
using System.Threading.Tasks;
using MediaInAction.DelugeService.BackgroundJobs.JobArgs;
using MediaInAction.DelugeService.TorrentNs;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraksService.BackgroundJobs.Jobs;

public class SendAllJob
        : AsyncBackgroundJob<SendAllArgs>, ITransientDependency
{
    private ITorrentService _torrentService;
    
    public SendAllJob( ITorrentService torrentService)
    {
        _torrentService = torrentService;
    }
    
    public override Task ExecuteAsync(SendAllArgs args)
    {
        throw new NotImplementedException();
    }
}
