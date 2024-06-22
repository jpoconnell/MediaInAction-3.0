using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.MediaFoldersNs;

public interface IMediaFolderRepository :  IRepository<MediaFolder, Guid>
{
    Task<MediaFolder> GetByServerNameAsync(string server,string folder,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);
}