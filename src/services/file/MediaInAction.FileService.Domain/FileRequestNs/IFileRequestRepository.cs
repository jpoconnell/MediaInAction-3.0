using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.FileService.FileRequestNs;

public interface IFileRequestRepository : IRepository<FileRequest, Guid>
{
    Task<FileRequest> FindByServerNameAsync(string server, string filename, string directory);

    Task<List<FileRequest>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );

    Task<FileRequest> GetByIdentifier(Guid refId);
    Task UpdateFileRequestStatus(Guid refId, FileStatus accepted);
}
