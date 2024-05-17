using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.DataMaintenanceNs.Dtos;
using MediaInAction.VideoService.SeriesNs;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.TraktRequestNs;

public class TraktRequestService: ITraktRequestService
{
    private readonly ITraktRequestRepository _traktRequestRepository;
    private readonly TraktRequestManager _traktRequestManager;
    private readonly ILogger<TraktRequestService> _logger;
    private readonly ISeriesService _seriesService;
    
    public TraktRequestService(
        TraktRequestManager traktRequestManager,
        ITraktRequestRepository traktRequestRepository,
        ILogger<TraktRequestService> logger ,
        ISeriesService seriesService)
    {
        _logger = logger;
        _seriesService = seriesService;
        _traktRequestManager = traktRequestManager;
        _traktRequestRepository = traktRequestRepository;
    }

    public async Task SendRequest(List<SeriesSeasonDto> showSeasonList)
    {
        try
        {
            var traktRequestItems = new List<TraktRequestItem>();
            var traktRequests = await _traktRequestRepository.GetUnCompleteRequests();
            if (traktRequests.Count > 0)
            {
                foreach (var traktRequest in traktRequests)
                {
                    foreach (var requestItem in traktRequest.RequestItems)
                    {
                        var found = false;
                        foreach (var showSeason in showSeasonList)
                        {
                            var slug = await _seriesService.GetSlugAsync(showSeason.SeriesId);
                            if ((requestItem.Slug == slug) && (requestItem.Season == showSeason.Season))
                            {
                                found = true;
                                break;
                            }
                      
                            if (!found)
                            {
                                var traktRequestItem = new TraktRequestItem(Guid.NewGuid(), slug, requestItem.Season);
                                traktRequestItems.Add(traktRequestItem);
                            }
                        }
                    }
                }
            }

            if (traktRequestItems.Count > 0)
            {
                await _traktRequestManager.CreateAsync("ShowSeason", traktRequestItems);
            }
        }
        catch
        {
            _logger.LogDebug("SendRequest: Failed" );
        }
    }
}