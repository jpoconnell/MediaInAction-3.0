using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.TraktService.Permissions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Specifications;
using MediaInAction.TraktService.TraktEpisodeNs.Dtos;
using MediaInAction.TraktService.TraktEpisodeNs.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs;

[Authorize(TraktServicePermissions.TraktEpisode.Default)]
public class TraktEpisodeAppService : TraktServiceAppService, ITraktEpisodeAppService
{
    private readonly ILogger<TraktEpisodeAppService> _logger;
    private readonly ITraktEpisodeRepository _traktEpisodeRepository;
    private readonly TraktEpisodeManager _traktEpisodeManager;

    public TraktEpisodeAppService(
        ITraktEpisodeRepository traktEpisodeRepository,
        ILogger<TraktEpisodeAppService> logger,
        TraktEpisodeManager traktEpisodeManager)
    {
        _traktEpisodeRepository = traktEpisodeRepository;
        _traktEpisodeManager = traktEpisodeManager;
        _logger = logger;
    }


    [AllowAnonymous]
    public async Task<EpisodeDashboardDto> GetDashboardAsync(EpisodeDashboardInput input)
    {
        return new EpisodeDashboardDto()
        {
            TraktEpisodeStatusDto = await GetCountOfTotalEpisodeStatusAsync(input.Filter),
        };
    }
    
    private async Task<List<TraktEpisodeStatusDto>> GetCountOfTotalEpisodeStatusAsync(string filter)
    {
        ISpecification<TraktEpisode> specification = SpecificationFactory.Create(filter);
        var orders = await _traktEpisodeRepository.GetDashboardAsync(specification);
        return CreateEpisodeStatusDtoMapping(orders);
    }
    
    private List<TraktEpisodeStatusDto> CreateEpisodeStatusDtoMapping(List<TraktEpisode> episodes)
    {
        var episodeStatus = episodes
            .GroupBy(p => p.TraktStatus)
            .Select(p => new TraktEpisodeStatusDto { CountOfStatusEpisode = p.Count(), EpisodeStatus = p.Key.ToString() })
            .OrderBy(p => p.CountOfStatusEpisode)
            .ToList();
        

        episodeStatus.Add(new TraktEpisodeStatusDto() { EpisodeStatus = "test", CountOfStatusEpisode   = 3 });

        return episodeStatus;
    }
}