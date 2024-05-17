using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.DelugeService.TorrentNs;

public interface ITorrentRepository : IRepository<Torrent, Guid>
    {
        Task<Torrent> GetByHashAsync(string hash);
    }

