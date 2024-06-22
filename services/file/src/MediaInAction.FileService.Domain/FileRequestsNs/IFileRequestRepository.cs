using System;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.FileService.FileRequestsNs
{
    public interface IFileRequestRepository : IBasicRepository<FileRequest, Guid>
    {
    }
}
