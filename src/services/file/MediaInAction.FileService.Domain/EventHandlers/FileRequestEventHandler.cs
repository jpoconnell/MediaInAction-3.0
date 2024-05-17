using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileRequestNs;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.FileService.EventHandlers
{
    public class FileRequestEventHandler : IDistributedEventHandler<FileRequestEto>, ITransientDependency
    {
        private readonly IFileRequestRepository _fileRequestRepository;
        private readonly ILogger<FileRequestEventHandler> _logger;
        
        public FileRequestEventHandler(IFileRequestRepository fileRequestRepository,
            ILogger<FileRequestEventHandler> logger)
        {
            _fileRequestRepository = fileRequestRepository;
            _logger = logger;
        }

        public async Task HandleEventAsync(FileRequestEto eventData)
        {
            if (eventData.ReferenceId != Guid.Empty)
            {
                // try finding it by 
               var dbFileRequest = await  _fileRequestRepository.GetByIdentifier(eventData.ReferenceId );
               if (dbFileRequest == null)
               {
                   throw new BusinessException(FileServiceDomainErrorCodes.FileRequestAlreadyHave);
               }
               else
               {
                   await _fileRequestRepository.UpdateFileRequestStatus(eventData.ReferenceId, FileStatus.Accepted);
                   _logger.LogInformation("FileRequest Accepted");
                   //TODO: Send accepted event back
               }
            }
        }
    }
}
