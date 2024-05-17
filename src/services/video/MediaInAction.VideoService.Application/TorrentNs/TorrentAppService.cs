using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.Permissions;
using MediaInAction.VideoService.TorrentNs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.TorrentNs;

[Authorize(VideoServicePermissions.Torrents.Default)]
public class TorrentAppService : VideoServiceAppService, ITorrentAppService
{
    private readonly TorrentManager _torrentManager;
    private readonly ITorrentRepository _torrentRepository;
    private readonly ILogger<TorrentAppService> _logger;
    
    public TorrentAppService(TorrentManager movieManager,
        ITorrentRepository movieRepository,
        ILogger<TorrentAppService> logger
    )
    {
        _torrentManager = movieManager;
        _torrentRepository = movieRepository;
        _logger = logger;
    }
    
    public async Task<TorrentDto> GetAsync(Guid id)
    {
        var torrent = await _torrentRepository.GetAsync(id);
        return null;
    }

    public async Task<TorrentDto> CreateAsync(TorrentCreatedDto input)
    {
        var torrentCreate = new TorrentCreateDto();
        torrentCreate.Added = input.Added;
        torrentCreate.Comment = input.Comment;
        torrentCreate.CompleteTime = input.CompleteTime;
        torrentCreate.DownloadLocation = input.DownloadLocation;
        torrentCreate.Hash = input.Hash;
        torrentCreate.IsSeed = input.IsSeed;
        torrentCreate.Label = input.Label;  
        torrentCreate.Message = input.Message;
        torrentCreate.Paused = input.Paused;
        torrentCreate.Ratio = input.Ratio;
        torrentCreate.Name = input.Name;
        
        var torrent = await _torrentManager.CreateAsync
            (torrentCreate);

        return MapToDto(torrent);
    }

    public Task<TorrentDto> GetTorrentAsync(GetTorrentInput input)
    {
        throw new NotImplementedException();
    }


    private TorrentDto MapToDto(Torrent torrent)
    {
        var torrentDto = new TorrentDto
        {
            Hash = torrent.Hash,
            Name = torrent.Name,
            Comment = torrent.Comment,
            IsSeed = torrent.IsSeed,
            Paused = torrent.Paused,
            Ratio = torrent.Ratio,
            Message = torrent.Message,
            Label = torrent.Label,
            Added = torrent.Added,
            CompleteTime = torrent.CompleteTime,
            DownloadLocation = torrent.DownloadLocation,
            TorrentStatus = torrent.TorrentStatus,
            Type = torrent.Type,
            MediaLink = torrent.MediaLink,
            EpisodeLink = torrent.EpisodeLink,
            IsMapped = torrent.IsMapped
        };

        return torrentDto;
    }

    public async Task<List<TorrentDto>> GetTorrentsAsync(GetTorrentsInput input)
    {
        ISpecification<Torrent> specification = Specifications.SpecificationFactory.Create("a:");

        var torrents =
            await _torrentRepository.GetListPagedAsync(specification, input.SkipCount,
                input.MaxResultCount, "" );

        var torrentDtoList = new  List<TorrentDto>();
        foreach (var torrent in torrents)
        {
            var torrentDto = MapToDto(torrent);
            torrentDtoList.Add(torrentDto);
        }
        var totalCount = await _torrentRepository.GetCountAsync();
        return torrentDtoList;
    }

    public async Task<PagedResultDto<TorrentDto>> GetListPagedAsync(GetTorrentsInput input)
    {
        var torrentDtoList =  await GetTorrentsAsync(input);
        var totalCount = torrentDtoList.Count;
        return new PagedResultDto<TorrentDto>(totalCount, torrentDtoList);
    }

    private List<TorrentDto> ConvertToDto(List<Torrent> torrentList)
    {
        var torrentDtoList = new List<TorrentDto>();
        foreach (var torrent in torrentList)
        {
            var torrentDto = MapToDto(torrent);
            torrentDtoList.Add(torrentDto);
        }

        return torrentDtoList;
    }
}
