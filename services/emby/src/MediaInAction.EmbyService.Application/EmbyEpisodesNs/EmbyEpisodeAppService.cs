using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeAliasesNs;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.EmbyEpisodeNs.Dtos;
using MediaInAction.EmbyService.EmbyEpisodeNs.Specifications;
using MediaInAction.EmbyService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyEpisodesNs;

[Authorize(EmbyServicePermissions.Episode.Default)]
public class EmbyEpisodeAppService : EmbyServiceAppService, IEmbyEpisodeAppService
{
    private readonly ILogger<EmbyEpisodeAppService> _logger;
    private readonly IEmbyEpisodeRepository _embyEpisodeRepository;
    private readonly EmbyEpisodeManager _embyEpisodeManager;

    public EmbyEpisodeAppService(
        IEmbyEpisodeRepository embyEpisodeRepository,
        ILogger<EmbyEpisodeAppService> logger,
        EmbyEpisodeManager embyEpisodeManager)
    {
        _embyEpisodeRepository = embyEpisodeRepository;
        _embyEpisodeManager = embyEpisodeManager;
        _logger = logger;
    }

    public async Task<EmbyEpisodeDto> GetAsync(Guid id)
    {
        var embyEpisode = await _embyEpisodeRepository.GetAsync(id);
        return ObjectMapper.Map<EmbyEpisode, EmbyEpisodeDto>(embyEpisode);
    }

    public async Task<PagedResultDto<EmbyEpisodeDto>> GetListAsync(GetEmbyEpisodeListDto input)
    {
        ISpecification<EmbyEpisode> specification = SpecificationFactory.Create(input.Filter);
        
        var embyEpisodes = await _embyEpisodeRepository.GetListPagedAsync(
            specification,
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting
        );

        var totalCount = embyEpisodes.Count;
       
        return new PagedResultDto<EmbyEpisodeDto>(
            totalCount,
            ObjectMapper.Map<List<EmbyEpisode>, List<EmbyEpisodeDto>>(embyEpisodes)
        );
    }

    [Authorize(EmbyServicePermissions.Episode.Edit)]
    public async Task UpdateAsync(Guid id, EmbyEpisodeCreateDto input)
    {
        var embyEpisode = await _embyEpisodeRepository.GetAsync(id);

        embyEpisode.SeasonNum = input.SeasonNum;
        embyEpisode.SeasonEpisodeNum = input.EpisodeNum;

        await _embyEpisodeRepository.UpdateAsync(embyEpisode);
    }

    [Authorize(EmbyServicePermissions.Episode.Create)]
    public async Task<EmbyEpisodeDto> CreateAsync(EmbyEpisodeCreateDto input)
    {
        var newEpisode = await _embyEpisodeManager.CreateAsync(input);

        var embyEpisode = new EmbyEpisode();
        await _embyEpisodeRepository.InsertAsync(embyEpisode);

        return ObjectMapper.Map<EmbyEpisode, EmbyEpisodeDto>(embyEpisode);
    }

    public Task<EmbyEpisodeDto> GetEpisodeAsync(GetEmbyEpisodeDto getEpisodeInput)
    {
        throw new NotImplementedException();
    }

    [Authorize(EmbyServicePermissions.Episode.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _embyEpisodeRepository.DeleteAsync(id);
    }
    
    private List<( string idType, string idValue
        )> GetEpisodeAliasTuple(List<EmbyEpisodeAliasCreateDto> createEpisodeAlias)
    {
        var newEpisodeAliases =
            new List<( string idType, string idValue )>();
        foreach (var episodeAlias in createEpisodeAlias)
        {
            newEpisodeAliases.Add(( episodeAlias.IdType, episodeAlias.IdValue));
        }

        return newEpisodeAliases;
    }
}