using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.Config;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktShowNs;
using MediaInAction.TraktService.TraktShowSeasonNs.Dtos;
using Microsoft.Extensions.Logging;
using TraktNet;
using TraktNet.Objects.Authentication;

namespace MediaInAction.TraktService.TraktShowSeasonNs;

public class TraktShowSeasonService : ITraktShowSeasonService
{
    private TraktClient _traktClient;
    private readonly ITraktShowLibService _showService;
    private readonly ITraktEpisodeLibService _episodeService;
    private readonly ILogger<TraktShowSeasonService> _logger;
    private readonly ServicesConfiguration _traktConfig;
    
    public TraktShowSeasonService(
        ITraktShowLibService showService,
        ITraktEpisodeLibService episodeService,
        ServicesConfiguration traktConfig,
        ILogger<TraktShowSeasonService> logger)
    {
        _showService = showService;
        _episodeService = episodeService;
        _logger = logger;
        _traktConfig = traktConfig;
        _traktClient = new TraktClient( _traktConfig.ClientId, _traktConfig.ClientSecret)
        {
            Authorization = TraktAuthorization.CreateWith(_traktConfig.AccessToken, _traktConfig.RefreshToken)
        };
    }
    
    public async Task DoEpisodeCleanup()
    {
        var showSeasonList = await GetEpisodeCleanupList();
    }

    private async Task<List<TraktShowSeasonDto>> GetEpisodeCleanupList()
    {
        var traktShowSeasonList = new List<TraktShowSeasonDto>();
        var showListDto = await _showService.GetActiveShows();
        foreach (var showDto in showListDto)
        {
            var episodeListDto = await _episodeService.GetEpisodeByShow(showDto.Slug);
            if ((episodeListDto != null) && (episodeListDto.Count> 0))
            {
                foreach (var episodeDto in episodeListDto)
                {
                
                }
            }
        }
        return traktShowSeasonList;
    }
}
