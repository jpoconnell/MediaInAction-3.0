using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowNs.Dtos;
using MediaInAction.EmbyService.EmbyShowsNs;
using MediaInAction.EmbyService.EmbyShowsNs.Specifications;
using MediaInAction.EmbyService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyShowNs;

[Authorize(EmbyServicePermissions.Show.Default)]
public class EmbyShowAppService : EmbyServiceAppService, IEmbyShowAppService
{
    private readonly ILogger<EmbyShowAppService> _logger;
    private readonly IEmbyShowRepository _embyShowRepository;
    private readonly EmbyShowManager _embyShowManager;
    
    public EmbyShowAppService(
        IEmbyShowRepository embyShowRepository,
        ILogger<EmbyShowAppService> logger,
        EmbyShowManager embyShowManager)
    {
        _embyShowRepository = embyShowRepository;
        _embyShowManager = embyShowManager;
        _logger = logger;
    }
    
    public async Task<EmbyShowDto> GetAsync(Guid id)
    {
        var show = await _embyShowRepository.GetAsync(id);
        return ObjectMapper.Map<EmbyShow, EmbyShowDto>(show);
    }

    public async Task<PagedResultDto<EmbyShowDto>> GetListAsync(EmbyGetShowListDto input)
    {
        ISpecification<EmbyShow> specification = SpecificationFactory.Create(input.Filter);

        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(EmbyShow.Name);
        }
        
        var shows = await _embyShowRepository.GetListPagedAsync(
            specification,
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting
        );

        var totalCount = shows.Count;
       
        return new PagedResultDto<EmbyShowDto>(
            totalCount,
            ObjectMapper.Map<List<EmbyShow>, List<EmbyShowDto>>(shows)
        );
    }

    [Authorize(EmbyServicePermissions.Show.Edit)]
    public async Task UpdateAsync(Guid id, EmbyShowCreateDto input)
    {
        var embyShow = await _embyShowRepository.GetAsync(id);
        embyShow.Name = input.Name;
        embyShow.FirstAiredYear = input.Year;
        await _embyShowRepository.UpdateAsync(embyShow);
    }

    [Authorize(EmbyServicePermissions.Show.Create)]
    public async Task<EmbyShowDto> CreateAsync(EmbyShowCreateDto input)
    {
        var showAliases = new List<(string idType, string idValue)>();
        foreach (var aliasDto in input.ShowAliasCreateDtos)
        {
            showAliases.Add((aliasDto.IdType,aliasDto.IdValue));
        }
        var newEmbyShow = await _embyShowManager.CreateAsync(
            input.Name, input.Year, showAliases);
        
        return ObjectMapper.Map<EmbyShow, EmbyShowDto>(newEmbyShow);
    }

    [Authorize(EmbyServicePermissions.Show.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _embyShowRepository.DeleteAsync(id);
    }

    public EmbyShowDto GetByShowNameYear(string show, int year)
    {
        throw new NotImplementedException();
    }
}