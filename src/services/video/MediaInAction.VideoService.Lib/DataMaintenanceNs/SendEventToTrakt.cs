using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.VideoService.DataMaintenanceNs.Dtos;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.TraktRequestNs;
using Microsoft.Extensions.Logging;

namespace  MediaInAction.VideoService.DataMaintenanceNs;

public class SendEventsToTrakt : ISendEventsToTrakt
{
    private readonly ILogger<SendEventsToTrakt> _logger;
    private readonly ISeriesService _seriesService;
    private readonly IEpisodeService _episodeService;
    private readonly ITraktRequestService _traktRequestService;
    
    public SendEventsToTrakt( ILogger<SendEventsToTrakt> logger,
        ISeriesService seriesService,
        IEpisodeService episodeService,
        ITraktRequestService traktRequestService
      )
    {
        _logger = logger;
        _seriesService = seriesService;
        _episodeService = episodeService;
        _traktRequestService = traktRequestService;
    }

    public async Task Process()
    {
        _logger.LogInformation("Starting SendEventToTrakt Service");
        var showSeasonList = new List<SeriesSeasonDto>();
        try
        {
            var seriesDtoList = await _seriesService.GetActiveList();

            if ((seriesDtoList != null) && (seriesDtoList.Count > 0))
            {
                foreach (var seriesDto in seriesDtoList)
                {
                    var episodeDtoList = await _episodeService.EpisodesNoDate(seriesDto.Id);

                    foreach (var episodeDto in episodeDtoList)
                    {
                        if (episodeDto.AiredDate < DateTime.Now.AddMonths(-24))
                        {
                            CreateUpdateShowSeason(episodeDto, showSeasonList);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }

        if (showSeasonList.Count > 0)
        {
            await _traktRequestService.SendRequest(showSeasonList);
        }
        _logger.LogInformation("SendEventToTrakt finished");
    }

    private void CreateUpdateShowSeason(EpisodeDto episodeDto, List<SeriesSeasonDto> showSeasonList)
    {
        var found = false;
        var seriesId = Guid.Empty;
        var season = 0;
        if (showSeasonList.Count() > 0)
        {
            foreach (var showSeasonDto in showSeasonList)
            {
                if ((showSeasonDto.Season == episodeDto.SeasonNum) &&
                    (showSeasonDto.SeriesId == episodeDto.SeriesId))
                {
                    found = true;
                    seriesId = episodeDto.SeriesId;
                    season = episodeDto.SeasonNum;
                    break;
                }
            }

            if (found == false)
            {
                var newSeriesSeason = new SeriesSeasonDto();
                newSeriesSeason.SeriesId = episodeDto.SeriesId;
                newSeriesSeason.Season = episodeDto.SeasonNum;;
                showSeasonList.Add(newSeriesSeason);
            }
        }
        else
        {
            var newShowSeason = new SeriesSeasonDto();
            newShowSeason.SeriesId = episodeDto.SeriesId;
            newShowSeason.Season = episodeDto.SeasonNum;
            showSeasonList.Add(newShowSeason);
        }
    }
}