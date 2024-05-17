using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService.FileEntryNs;

    public class FileEntryLibService : IFileEntryLibService, ITransientDependency
    {
        private readonly ILogger<FileEntryLibService> _logger;
        private readonly FileEntryManager _fileEntryManager;
        private readonly IFileEntryRepository _fileEntryRepository;
        
        public FileEntryLibService(
            FileEntryManager fileEntryManager,
            IFileEntryRepository fileEntryRepository,
            ILogger<FileEntryLibService> logger)

        {
            _fileEntryManager = fileEntryManager;
            _fileEntryRepository = fileEntryRepository;
            _logger = logger;
        }
        
        public async Task CreateFileEntryAsync(CreateFileEntryDto rec)
        {
            await _fileEntryManager.CreateAsync(rec.Server, rec.Filename, rec.Directory, rec.Extn, rec.Size,rec.Sequence,
                rec.ListName);
        }

        public async Task ResendUnAcceptedFileEntries()
        {
            var fileEntries = await _fileEntryRepository.GetListAsync();
            foreach (var fileEntry in fileEntries)
            {
                if (fileEntry.FileStatus == FileStatus.New)
                {
                    await _fileEntryManager.ResendEvent(fileEntry);
                }
            }
        }
    }
