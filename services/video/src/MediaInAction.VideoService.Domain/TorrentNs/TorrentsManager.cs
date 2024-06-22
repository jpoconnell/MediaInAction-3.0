using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.TorrentNs.Dtos;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.TorrentNs;
public class TorrentManager : DomainService
{
    private readonly ITorrentRepository _torrentRepository;
    private readonly ILogger<TorrentManager> _logger;
    
    public TorrentManager(ITorrentRepository torrentRepository,
        ILogger<TorrentManager> logger,
        IDistributedEventBus distributedEventBus)
    {
        _torrentRepository = torrentRepository;
        _logger = logger;
    }

    public async Task<Torrent> CreateAsync(TorrentCreateDto input)
    {
        // Create new torrent
        var torrent = new Torrent(
            id: GuidGenerator.Create(),
            comment: input.Comment,
            isSeed: input.IsSeed,
            hash: input.Hash,
            paused: input.Paused,
            ratio: input.Ratio,
            message: input.Message,
            name: input.Name,
            label: input.Label,
            added: input.Added,
            completeTime: input.CompleteTime,
            downloadLocation: input.DownloadLocation,
            status: input.Status
        );
        
        var dbTorrent = await _torrentRepository.FindByHash(torrent.Hash);

        if (dbTorrent == null)
        {
            var createdTorrent = await _torrentRepository.InsertAsync(torrent, true);
            return createdTorrent;
        }
        else
        {
            var update = 0;

            if (dbTorrent.Added != torrent.Added)
            {
                dbTorrent.Added = torrent.Added;
                update++;
            }

            if (dbTorrent.CompleteTime != torrent.CompleteTime)
            {
                dbTorrent.CompleteTime = torrent.CompleteTime;
                update++;
            }

            if (dbTorrent.Paused != torrent.Paused)
            {
                dbTorrent.Paused = torrent.Paused;
                update++;
            }

            if (dbTorrent.IsSeed != torrent.IsSeed)
            {
                dbTorrent.IsSeed = torrent.IsSeed;
                update++;
            }
            
            if (update > 0)
            {
                await _torrentRepository.UpdateAsync(dbTorrent);
            }
            return dbTorrent;
        }
    }


    public async Task<Torrent> AcceptTorrentAsync(string comment, bool isSeed, 
        string hash, bool paused, double ratio, string message, string name, 
        string label, long added, double complete, string location, 
        FileStatus status, MediaType mediaType, Guid mediaLink, Guid episodeLink)
    {
        var torrentCreateDto = new TorrentCreateDto
        {
            Comment = comment,
            IsSeed = isSeed,
            Hash = hash,
            Paused = paused,
            Ratio = ratio,
            Message = message,
            Name = name,
            Label = label,
            Added = added,
            CompleteTime = complete,
            DownloadLocation = location,
            Status = status.ToString()
        };
       var torrent = await CreateAsync(torrentCreateDto);
       return torrent;
    }
}
