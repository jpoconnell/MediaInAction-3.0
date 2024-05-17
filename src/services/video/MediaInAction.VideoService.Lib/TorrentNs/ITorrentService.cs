using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.TorrentNs.Dtos;

namespace  MediaInAction.VideoService.TorrentNs;

public interface ITorrentService
{
    Task<TorrentDto> GetByHashAsync(string hash);
    Task UpdateAsync(TorrentDto torrent);
    Task<List<TorrentDto>> GetUnMapped();
    
}