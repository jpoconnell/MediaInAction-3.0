using System.Threading.Tasks;
using MediaInAction.VideoService.BackgroundJobs.JobArgs;
using MediaInAction.VideoService.FileServicesNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService.BackgroundJobs.Jobs
{
    public class DataMantenanceJob
        : AsyncBackgroundJob<DataMantenanceArgs>, ITransientDependency
    {
        private ILogger<DataMantenanceJob> _logger;
        private IFileMapper _fileMapper;
        
        public DataMantenanceJob( IFileMapper fileMapper,
            ILogger<DataMantenanceJob> logger)
        {
            _fileMapper = fileMapper;
            _logger = logger;
        }

        public override async Task ExecuteAsync(DataMantenanceArgs args)
        {
           // await _fileMapper.CreateDefaultAliases();
            _logger.LogInformation("Executed CreateDefaultAliases..!");
           // await _fileMapper.ProcessToBeMapped();
           _logger.LogInformation("Executed ProcessToBeMapped..!");
        }
    }
}