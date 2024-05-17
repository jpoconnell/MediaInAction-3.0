using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.FileEntryNs;
using MediaInAction.VideoService.MediaMatchingServicesNs;
using MediaInAction.VideoService.MovieAliasNs;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.ToBeMappedsNs;
using Microsoft.Extensions.Logging;

namespace  MediaInAction.VideoService.DataMaintenanceNs;

public class ProcessToBeMappeds : IProcessToBeMappeds
{
    private readonly ILogger<ProcessToBeMappeds> _logger;
    private readonly IToBeMappedService _toBeMappedService;
    private readonly ISeriesMatchingService _seriesMatchingService;
    private readonly ISeriesAliasService _seriesAliasService;
    private readonly IMovieAliasLibService _movieAliasService;
    private readonly IMovieMatchingService _movieMatchingService;

    
    public ProcessToBeMappeds( ILogger<ProcessToBeMappeds> logger,
        IToBeMappedService toBeMappedService,
        ISeriesMatchingService seriesMatchingService,
        ISeriesAliasService seriesAliasService,
        IMovieAliasLibService movieAliasService,
        IMovieMatchingService movieMatchingService
      )
    {
        _logger = logger;
        _toBeMappedService = toBeMappedService;
        _seriesMatchingService = seriesMatchingService;
        _seriesAliasService = seriesAliasService;
        _movieAliasService = movieAliasService;
        _movieMatchingService = movieMatchingService;
    }

    public async Task Process()
    {
        _logger.LogInformation("Starting ProcessToBeMapped Service");
        try
        {
            var toBeMappedList = await _toBeMappedService.GetNotProcessed();
            _logger.LogInformation("ToBeMapped Count:" + toBeMappedList.Count.ToString());
            var seriesAliasDtoList = await _seriesAliasService.GetAllByType("name");
            var movieAliasDtoList = await _movieAliasService.GetAllByType("name");
            var matchFound = false;
            foreach (var toBeMapped in toBeMappedList)
            {
                _logger.LogInformation("Alias:" + toBeMapped.Alias);
                try
                {
                    if (toBeMapped.Alias.Length > 0)
                    {
                        matchFound = await _seriesMatchingService.GetBySeriesName(toBeMapped.Alias, seriesAliasDtoList );
                        if (matchFound == false)
                        {
                            matchFound = await _movieMatchingService.GetByMovieName(toBeMapped.Alias, movieAliasDtoList);
                        }
                    }

                    if (matchFound == true)
                    {
                        toBeMapped.Processed = true;
                        _logger.LogInformation("Found:" + toBeMapped.Alias);
                    }
                    toBeMapped.Tries++;
                
                    await _toBeMappedService.UpdateAsync(toBeMapped);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug("ProcessToBeMappeds.Process-1:" + ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug("ProcessToBeMappeds.Process-2:"+ ex.Message);
        }
        _logger.LogInformation("ProcessToBeMapped finished");
    }
   
}