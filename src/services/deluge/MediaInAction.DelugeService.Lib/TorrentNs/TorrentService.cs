using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DelugeRPCClient.Net;
using DelugeRPCClient.Net.Models;
using MediaInAction.DelugeService.TorrentNs.Dtos;
using Microsoft.Extensions.Logging;

namespace MediaInAction.DelugeService.TorrentNs;

public class TorrentService : ITorrentService
{
    private readonly ILogger<TorrentService> _logger;
    private readonly IDelugeService _delugeService;
    private readonly TorrentManager _torrentManager;
    private readonly ITorrentRepository _torrentRepository;
    
    private DelugeClient _delugeClient;
    
    public TorrentService(
        ILogger<TorrentService> logger,
        IDelugeService delugeService,
        TorrentManager torrentManager,
        ITorrentRepository torrentRepository
        )
    {
        _logger = logger;
        _delugeService = delugeService;
        _torrentManager = torrentManager;
        _torrentRepository = torrentRepository;
    }
    
    public async Task GetTorrentCollection()
    {
        _logger.LogInformation("Start GetTorrentCollection");
        _delugeClient = _delugeService.GetClient();
        var loginResult = await _delugeClient.Login();

        if (loginResult == true)
        {
            var myDictionary = new Dictionary<string, string>();
            var torrentList = await _delugeClient.ListTorrents(myDictionary);
            foreach (var torrent in torrentList)
            {
                var torrentDto = MapToDto(torrent);
                await UpdateAddFromDto(torrentDto);
            }
            var logoutResult = await _delugeClient.Logout();
        }
    }

    public async Task UpdateAddFromDto(TorrentDto torrentDto)
    {
        try
        {
            var dbTorrent = await _torrentRepository.GetByHashAsync(torrentDto.Hash);
            if (dbTorrent == null)
            {
                var newTorrent = await _torrentManager.CreateAsync(torrentDto.Comment,
                    torrentDto.Name, torrentDto.IsSeed, torrentDto.Hash,
                    torrentDto.Paused, torrentDto.Ratio, torrentDto.Message,
                    torrentDto.Label, torrentDto.Added, torrentDto.CompleteTime, torrentDto.DownloadLocation);
            }
            else
            {
                var updated = 0;
                var updatedTorrent = dbTorrent;
                updatedTorrent.Ratio = torrentDto.Ratio;
                if (torrentDto.Added != updatedTorrent.Added)
                {
                    updatedTorrent.Added = torrentDto.Added;
                    updated++;
                }
                if (torrentDto.CompleteTime != updatedTorrent.CompleteTime)
                {
                    updatedTorrent.CompleteTime = torrentDto.CompleteTime;
                    updated++;
                }
                if (torrentDto.IsSeed != updatedTorrent.IsSeed)
                {
                    updatedTorrent.IsSeed = torrentDto.IsSeed;
                    updated++;
                }
                if (torrentDto.Paused != updatedTorrent.Paused)
                {
                    updatedTorrent.Paused = torrentDto.Paused;
                    updated++;
                }
                if (updated > 0)
                {
                    await _torrentRepository.UpdateAsync(updatedTorrent, true);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug("UpdateAddFromDto:" +ex.Message);
        }
    }
    
    private TorrentDto MapToDto(DelugeRPCClient.Net.Models.Torrent torrent)
    {
        var torrentDto = new TorrentDto
        {
            Name = torrent.Name,
            Added = torrent.Added,
            Message = torrent.Message,
            Hash = torrent.Hash,
            Label = torrent.Label,
            Comment = torrent.Comment,
            DownloadLocation = torrent.DownloadLocation,
            CompleteTime = torrent.CompleteTime,
            IsSeed = torrent.IsSeed
        };
        return torrentDto;
    }
}

