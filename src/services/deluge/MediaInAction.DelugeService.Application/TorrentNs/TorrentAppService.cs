using System;
using System.Threading.Tasks;
using MediaInAction.DelugeService.Permissions;
using MediaInAction.DelugeService.TorrentNs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace MediaInAction.DelugeService.TorrentNs;

[Authorize(DelugeServicePermissions.Torrent.Default)]
public class TorrentAppService : DelugeServiceAppService, ITorrentAppService
{
    private readonly ILogger<TorrentAppService> _logger;
    private readonly ITorrentRepository _torrentRepository;
    private readonly TorrentManager _torrentManager;
    
    public TorrentAppService(
        ITorrentRepository torrentRepository,
        ILogger<TorrentAppService> logger,
        TorrentManager torrentManager)
    {
        _torrentRepository = torrentRepository;
        _torrentManager = torrentManager;
        _logger = logger;
    }
    
    public async Task<TorrentDto> GetAsync(Guid id)
    {
        var torrent = await _torrentRepository.GetAsync(id);
        return ObjectMapper.Map<Torrent, TorrentDto>(torrent);
    }
    
}