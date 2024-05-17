using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.SeriesAliasNs.Dtos;
using MediaInAction.VideoService.SeriesNs;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.MediaMatchingServicesNs;

public class SeriesMatchingService : ISeriesMatchingService
{
    private readonly ISeriesAliasService _seriesAliasService;
    private readonly ISeriesService _seriesService;
    private readonly ILogger<SeriesMatchingService> _logger;

    public SeriesMatchingService(
        ISeriesAliasService seriesAliasService,
        ISeriesService seriesService,
        ILogger<SeriesMatchingService> logger)
    {
        _seriesAliasService = seriesAliasService;
        _seriesService = seriesService;
        _logger = logger;
    }

    public async Task<bool> GetBySeriesName(string alias, List<SeriesAliasDto> seriesAliasList)
    {
        if (!seriesAliasList.IsNullOrEmpty())
        {
            if (seriesAliasList.Count > 0)
            {
                var masterFound = false;
                LevenshteinDistance ld = new LevenshteinDistance();
                string idType = "name";
                int dist = 0;
                int fuzzy = (alias.Length / 4);
                if (fuzzy < 1)
                {
                    fuzzy = 1;
                }

                var myAlias = alias.ToLower();
                if (myAlias.Contains("vegas"))
                {
                    
                }
                var seriesAliasValue = "";
                try
                {
                    foreach (var seriesAlias in seriesAliasList)
                    {
                        var found = false;
                        if (myAlias.Contains("afi"))
                        {
                            seriesAliasValue = "afi " + seriesAlias.IdValue.ToLower();
                        }
                        else
                        {
                            seriesAliasValue = seriesAlias.IdValue.ToLower();
                        }

                        dist = ld.ComputeDistance(myAlias, seriesAliasValue);
                        if (dist == 0)
                        {
                            await SeriesAliasCreateIfNeeded(seriesAlias.SeriesId, idType, myAlias);
                            masterFound = true;
                            break;
                        }

                        if (dist <= fuzzy)
                        {
                            await SeriesAliasCreateIfNeeded(seriesAlias.SeriesId, idType, myAlias);
                            masterFound = true;
                            break;
                        }

                        if (found == false)
                        {
                            var seriesDto = await _seriesService.GetByIdAsync(seriesAlias.SeriesId);
                            seriesAliasValue = seriesAliasValue + " " + seriesDto.FirstAiredYear.ToString();
                            dist = ld.ComputeDistance(myAlias, seriesAliasValue);

                            if (dist == 0)
                            {
                                await SeriesAliasCreateIfNeeded(seriesAlias.SeriesId, idType, myAlias);
                                masterFound = true;
                                break;
                            }

                            if (dist <= fuzzy)
                            {
                                await SeriesAliasCreateIfNeeded(seriesAlias.SeriesId, idType, myAlias);
                                masterFound = true;
                                break;
                            }
                        }
                    }
                    return masterFound;
                }
                catch (Exception ex)
                {
                    _logger.LogDebug("SeriesMatchingService.GetBySeriesName:" + ex.Message);
                }
            }
            else
            {
                _logger.LogDebug("seriesAlias count is zero");
                return false;
            }
        }
        else
        {
            _logger.LogDebug("seriesAlias count is zero");
            return false;
        }
        return false;
    }

    private async Task SeriesAliasCreateIfNeeded(Guid seriesId, string idType, string alias)
    {
        try
        {     
            var seriesAlias = await _seriesAliasService.FindBySeriesTypeValueAsync(seriesId,idType, alias);

            if (seriesAlias == null)
            { 
                await _seriesAliasService.CreateSeriesAlias(seriesId,idType, alias);
            }   
        }
        catch (Exception ex) 
        {
           _logger.LogDebug("SeriesMatchingService.SeriesAliasCreateIfNeeded:" + ex.Message);
        } 
    }
}
