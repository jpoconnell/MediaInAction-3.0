using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.TorrentNs.Dtos;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.TorrentNs;

public class TorrentService: ITorrentService
{
    private readonly ITorrentRepository _torrentRepository;
    private readonly TorrentManager _torrentManager;
    private readonly ILogger<TorrentService> _logger;
    
    public TorrentService(
        TorrentManager torrentManager,
        ITorrentRepository torrentRepository,
        ILogger<TorrentService> logger )
    {
        _torrentRepository = torrentRepository;
        _torrentManager = torrentManager;
        _logger = logger;
    }


    public async Task<TorrentDto> GetByHashAsync(string hash)
    {
        var fileList = await _torrentRepository.FindByHash(hash);
        var torrentDto = MapToDto(fileList);
        return torrentDto;
    }

    public async Task UpdateAsync(TorrentDto torrentDto)
    {
        var updates = 0;
        var dbTorrent = await _torrentRepository.FindByHash(torrentDto.Hash);
        if (dbTorrent.EpisodeLink != torrentDto.EpisodeLink)
        {
            dbTorrent.EpisodeLink = torrentDto.EpisodeLink;
            updates++;
        }

        if (dbTorrent.IsMapped != torrentDto.IsMapped)
        {
            dbTorrent.IsMapped = torrentDto.IsMapped;
            updates++;
        }

        if (dbTorrent.MediaLink != torrentDto.MediaLink)
        {
            dbTorrent.MediaLink = torrentDto.MediaLink;
            updates++;
        }

        if (dbTorrent.CompleteTime != torrentDto.CompleteTime)
        {
            dbTorrent.CompleteTime = torrentDto.CompleteTime;
            updates++;
        }

        if (updates > 0)
        {
            await _torrentRepository.UpdateAsync(dbTorrent, true);
        }
    }

    private TorrentDto MapToDto(Torrent torrent)
    {
        var torrentDto = new TorrentDto();
        torrentDto.CompleteTime = torrent.CompleteTime;
        torrentDto.Name = torrent.Name;
        torrentDto.Hash = torrent.Hash;
        return torrentDto;
    }
    
    public async Task<List<TorrentDto>> GetUnMapped()
    {
        var torrentDtoList = new List<TorrentDto>();
        var fileList = await _torrentRepository.GetMapped(false);
        foreach (var torrent in fileList)
        {
            var torrentDto = new TorrentDto
            {
                Name = torrent.Name,
                Hash = torrent.Hash,
                Id = torrent.Id,
                IsSeed = torrent.IsSeed,
                Paused = torrent.Paused,
                Added = torrent.Added,
                CompleteTime = torrent.CompleteTime
            };
            torrentDtoList.Add(torrentDto);
        }

        return torrentDtoList;
    }
}

