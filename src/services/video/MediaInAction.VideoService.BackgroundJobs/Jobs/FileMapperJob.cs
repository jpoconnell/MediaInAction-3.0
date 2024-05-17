using System.Threading.Tasks;
using MediaInAction.VideoService.BackgroundJobs.JobArgs;
using MediaInAction.VideoService.FileServicesNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService.BackgroundJobs.Jobs
{
    public class FileMapperJob
        : AsyncBackgroundJob<FileMapperArgs>, ITransientDependency
    {
        private IFileMapper _fileMapper;
        private ILogger<FileMapperJob> _logger;
        
        public FileMapperJob( IFileMapper fileMapper,
            ILogger<FileMapperJob> logger)
        {
            _fileMapper = fileMapper;
            _logger = logger;
        }

        public override async Task ExecuteAsync(FileMapperArgs args)
        {
            _logger.LogInformation("FileMapper MapFiles started");
            await _fileMapper.MapFiles();
            _logger.LogInformation("Executed FileMapper MapFiles..!");
        }
    }
}