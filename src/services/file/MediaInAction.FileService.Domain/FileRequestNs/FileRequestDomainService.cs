using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace MediaInAction.FileService.FileRequestNs;

public class FileRequestDomainService : DomainService
{
    private readonly IFileRequestRepository _fileRequestRepository;

    public FileRequestDomainService(IFileRequestRepository fileRequestRepository)
    {
        _fileRequestRepository = fileRequestRepository;
    }

    public async Task<FileRequest> UpdateFileRequestStateAsync(
        Guid fileRequestId,
        FileRequestState fileRequestStatus)
    {
        var fileRequest = await _fileRequestRepository.GetAsync(fileRequestId);

        if (fileRequestStatus == FileRequestState.Completed )
        {
            fileRequest.SetAsCompleted();
        }
        else
        {
            fileRequest.SetAsFailed("Failed");
        }

        await _fileRequestRepository.UpdateAsync(fileRequest);

        return fileRequest;
    }
}