using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace MediaInAction.FileService.FileRequestNs
{
    public class FileRequestManager : DomainService
    {
        private readonly IRepository<FileRequest, Guid> _fileRequestRepository;

        public FileRequestManager(IRepository<FileRequest, Guid> fileRequestRepository)
        {
            _fileRequestRepository = fileRequestRepository;
        }

        public async Task<FileRequest> CreateAsync(
            FileOperation operation,
            List<FileRequestFile> files
            )
        {
            /*
            var existingFileEntry = await _fileRequestRepository.FindByServerNameAsync(
                p => p.Server == server &&
                p.Filename == filename && p.Directory == directory);
            if (existingFileEntry != null)
            {
                throw new FileEntryAlreadyExistsException(filename);
            }
            */

            return await _fileRequestRepository.InsertAsync(
                new FileRequest(
                    GuidGenerator.Create(),
                    operation
                )
            );
        }
    }
}
