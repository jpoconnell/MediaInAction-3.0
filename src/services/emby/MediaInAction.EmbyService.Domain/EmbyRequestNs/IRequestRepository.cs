using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.RequestNs;

public interface IRequestRepository : IRepository<Request, Guid>
{
    Task<Request> FindByServerNameAsync(string server, string filename, string directory);

    Task<List<Request>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );

    Task<Request> GetByIdentifier(Guid refId);
 //   Task UpdateEmbyRequestStatus(Guid refId, EmbyStatus accepted);
}
