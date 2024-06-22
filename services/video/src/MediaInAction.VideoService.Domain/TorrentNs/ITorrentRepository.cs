using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.TorrentNs
{
    public interface ITorrentRepository : IRepository<Torrent, Guid>
    {
        Task<Torrent> FindByHash(string hash);
        Task<Torrent> FindByName(string name);
        Task<List<Torrent>> GetMapped(bool isMapped);
        Task<List<Torrent>> GetTorrentStatus(FileStatus status);
        Task<List<Torrent>> GetListPagedAsync(ISpecification<Torrent> specification, 
            int inputSkipCount, 
            int inputMaxResultCount, 
            string empty,
            CancellationToken cancellationToken = default);
    }
}
