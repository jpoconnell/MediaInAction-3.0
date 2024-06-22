using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.DelugeService.TorrentNs;

public interface ITorrentRepository : IRepository<Torrent, Guid>
{
    Task<Torrent> GetByHashAsync(string hash);
    
    Task<List<Torrent>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
    
    Task<List<Torrent>> GetListPagedAsync(
        ISpecification<Torrent> spec,
        int skipCount,
        int maxResultCount,
        string sorting,
        bool includeDetails = false,
        CancellationToken cancellationToken = default
    );
}

