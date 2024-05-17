using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.Permissions;
using MediaInAction.VideoService.ToBeMappedNs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.ToBeMappedNs;

[Authorize(VideoServicePermissions.ToBeMappeds.Default)]
public class ToBeMappedAppService : VideoServiceAppService, IToBeMappedAppService
{
    private readonly ToBeMappedManager _toBeMappedManager;
    private readonly IToBeMappedRepository _toBeMappedRepository;
    private readonly ILogger<ToBeMappedAppService> _logger;
    
    public ToBeMappedAppService(ToBeMappedManager movieManager,
        IToBeMappedRepository movieRepository,
        ILogger<ToBeMappedAppService> logger
    )
    {
        _toBeMappedManager = movieManager;
        _toBeMappedRepository = movieRepository;
        _logger = logger;
    }
    
    public async Task<ToBeMappedDto> GetAsync(Guid id)
    {
        var movie = await _toBeMappedRepository.GetAsync(id);
        return null;
    }
    
    
    public async Task<ToBeMappedDto> CreateAsync(ToBeMappedCreateDto input)
    {
        var toBeMapped = await _toBeMappedManager.CreateToBeMappedAsync
        (
            alias: input.Alias
        );

        var toBe = new ToBeMappedDto();
        toBe.Alias = toBeMapped.Alias;
        toBe.Processed = false;

        return toBe;
    }
    
    public async Task<List<ToBeMappedDto>> GetToBeMappedsAsync(GetToBeMappedsInput getToBeMappedInput)
    {
        throw new NotImplementedException();
    }

    public async Task<ToBeMappedDto> GetToBeMappedAsync(GetToBeMappedInput input)
    {
        var toBeDb = await _toBeMappedRepository.FindByAlias(input.Alias);
        var toBeOut = new ToBeMappedDto();
        toBeOut.Alias = toBeDb.Alias;
        toBeOut.Processed = toBeDb.Processed;
        return toBeOut;
    }
    
    // code to export ToBeMapped data as a json file

    public async Task<PagedResultDto<ToBeMappedDto>> GetListPagedAsync(GetToBeMappedsInput input)
    {
        ISpecification<ToBeMapped> specification = Specifications.SpecificationFactory.Create("a:");

        var toBeMappedList = await _toBeMappedRepository.GetListPagedAsync(specification, input.SkipCount,
            input.MaxResultCount, "" );

        var totalCount = await _toBeMappedRepository.GetCountAsync();
        
        var toBeMappedDtoMapping = CreateToBeMappedDtoMapping(toBeMappedList);
        return new PagedResultDto<ToBeMappedDto>( totalCount,toBeMappedDtoMapping);
        
    }

    private IReadOnlyList<ToBeMappedDto> CreateToBeMappedDtoMapping(List<ToBeMapped> toBeMappedList)
    {
        _logger.LogInformation("CreateSeriesDtoMapping");
        List<ToBeMappedDto> dtoList = new List<ToBeMappedDto>();
        foreach (var toBeMapped in toBeMappedList)
        {
            dtoList.Add(CreateSeriesDtoMapping(toBeMapped));
        }
        return dtoList;
    }

    private ToBeMappedDto CreateSeriesDtoMapping(ToBeMapped toBeMapped)
    {
        var toBeMappedDto = new ToBeMappedDto();
        toBeMappedDto.Alias = toBeMapped.Alias;
        toBeMappedDto.Processed = toBeMapped.Processed;
        return toBeMappedDto;
    }
}
