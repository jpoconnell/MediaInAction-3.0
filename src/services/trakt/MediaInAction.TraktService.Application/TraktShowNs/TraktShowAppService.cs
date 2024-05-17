using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.TraktService.Permissions;
using MediaInAction.TraktService.TraktShowNs.Dtos;
using MediaInAction.TraktService.TraktShowNs.Specifications;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktShowNs;

[Authorize(TraktServicePermissions.TraktShow.Default)]
public class TraktShowAppService : TraktServiceAppService, ITraktShowAppService
{
    private readonly ILogger<TraktShowAppService> _logger;
    private readonly ITraktShowRepository _traktShowRepository;
    private readonly TraktShowManager _traktShowManager;
    
    public TraktShowAppService(
        ITraktShowRepository traktShowRepository,
        ILogger<TraktShowAppService> logger,
        TraktShowManager traktShowManager)
    {
        _traktShowRepository = traktShowRepository;
        _traktShowManager = traktShowManager;
        _logger = logger;
    }


    [AllowAnonymous]
    public async Task<TraktShowDashboardDto> GetDashboardAsync(TraktShowDashboardInput input)
    {
        return new TraktShowDashboardDto()
        {
            TraktShowStatusDto = await GetCountOfTotalShowStatusAsync(input.Filter)
        };
    }

    public async Task<List<TraktShowDto>> GetTraktShowListAsync(GetTraktShowListInput filter)
    {
        ISpecification<TraktShow> specification = Specifications.SpecificationFactory.Create(filter.Filter);
        var traktShowList = await _traktShowRepository.GetTraktShowBySpec(specification, true);
        if (traktShowList.Count == 1)
        {
            return CreateTraktShowDtoMapping(traktShowList);
        }
        else
        {
            return null;
        }
    }

    public async Task<PagedResultDto<TraktShowDto>> GetTraktShowListPagedAsync(GetTraktShowListInput filter)
    {
        var traktShowDtoList = await GetTraktShowListAsync(filter);
        throw new System.NotImplementedException();
    }

    private List<TraktShowDto> CreateTraktShowDtoMapping(List<TraktShow> traktShow)
    {
        throw new System.NotImplementedException();
    }
    
    private TraktShowDto CreateTraktShowDtoMapping(TraktShow traktShow)
    {
        throw new System.NotImplementedException();
    }

    private async Task<List<TraktShowStatusDto>> GetCountOfTotalShowStatusAsync(string filter)
    {
        var input = "s:";
        if (filter == null)
        {
            input = "s:";
        }
        ISpecification<TraktShow> specification = SpecificationFactory.Create(input);
        var shows = await _traktShowRepository.GetDashboardAsync(specification);
        return CreateShowStatusDtoMapping(shows);
    }

    private List<TraktShowStatusDto> CreateShowStatusDtoMapping(List<TraktShow> shows)
    {
        var showStatus = shows
            .GroupBy(p => p.TraktStatus)
            .Select(p => new TraktShowStatusDto { CountOfStatusShow = p.Count(), ShowStatus = p.Key.ToString() })
            .OrderBy(p => p.CountOfStatusShow)
            .ToList();
        

        showStatus.Add(new TraktShowStatusDto() { ShowStatus = "test", CountOfStatusShow   = 3 });

        return showStatus;
    }
}