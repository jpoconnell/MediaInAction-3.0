using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.DelugeService.Permissions;
using MediaInAction.DelugeService.TorrentNs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;

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

    public async Task<PagedResultDto<TorrentDto>> GetTorrentListPagedAsync(GetTorrentListDto filter)
    {
        ISpecification<Torrent> specification = Specifications.SpecificationFactory.Create(filter.Filter);
        var torrentList = await _torrentRepository.GetListPagedAsync(
            specification, 0,10,"",false);
        if (torrentList.Count > 0)
        {
            var torrentDtoList = CreateTorrentDtoMapping(torrentList);
            return new PagedResultDto<TorrentDto>(torrentList.Count, torrentDtoList);
        }
        else
        {
            return null;
        }
    }

    private List<TorrentDto> CreateTorrentDtoMapping(List<Torrent> torrentList)
    {
        var torrentDtoList = new List<TorrentDto>();
        
        foreach (var torrent in torrentList)
        {
            torrentDtoList.Add(CreateTorrentDtoMapping(torrent)); 
        }

        return torrentDtoList;
    }
    
    private TorrentDto CreateTorrentDtoMapping(Torrent torrent)
    {
       var torrentDto = ObjectMapper.Map<Torrent, TorrentDto>(torrent);
       return torrentDto;
    }
}