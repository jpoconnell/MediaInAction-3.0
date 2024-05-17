using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesAliasNs.Dtos;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.SeriesAliasNs;

public class SeriesAliasService: ISeriesAliasService
{
    private readonly ISeriesAliasRepository _seriesAliasRepository;
    private readonly ILogger<SeriesAliasService> _logger;
    
    public SeriesAliasService(
        ISeriesAliasRepository seriesAliasRepository,
        ILogger<SeriesAliasService> logger)
    {
        _seriesAliasRepository = seriesAliasRepository;
        _logger = logger;
    }
    
    public async Task<List<SeriesAliasDto>> GetAllByType(string idType)
    {
        var seriesAliases = await _seriesAliasRepository.GetByIdType(idType);
        if (seriesAliases.Count > 0)
        {
            return MapToSeriesAliasDtos(seriesAliases);
        }
        else
        {
            return null;
        }
    }
    
    public async Task<SeriesAliasDto> FindBySeriesTypeValueAsync(
        Guid seriesId, 
        string idType, 
        string alias)
    {
        var seriesAlias = await _seriesAliasRepository.FindBySeriesTypeValueAsync(seriesId, idType, alias);
        if (seriesAlias == null)
        {
            return null;
        }
        else
        {
            return MapToSeriesAliasDto(seriesAlias);
        }
    }
    
    public async Task CreateSeriesAlias(Guid seriesId, 
        string idType, 
        string alias)
    {
        try
        {
            var seriesAlias = new SeriesAlias
            {
                SeriesId = seriesId,
                IdType = idType,
                IdValue = alias
            };

            var dbSeriesAlias = await _seriesAliasRepository.FindBySeriesTypeValueAsync(seriesId, idType, alias);
            if (dbSeriesAlias == null)
            {
                await _seriesAliasRepository.InsertAsync(seriesAlias);
                _logger.LogInformation("SeriesAlias Inserted:" + seriesAlias.IdValue);
            }
            else  // update Series Alias
            {
               
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
    }

    public async Task<SeriesAliasDto> GetByIdValue(string idValue)
    {
        try
        {
            var seriesAlias = await _seriesAliasRepository.GetByIdValue(idValue);
            if (seriesAlias != null)
            {
                return MapToSeriesAliasDto(seriesAlias);
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    public async Task<SeriesAliasDto> GetBySeriesIdType(Guid id, string type)
    {
        var seriesAlias = await _seriesAliasRepository.GetBySeriesType(id, type);
        return MapToSeriesAliasDto(seriesAlias);
    }

    private List<SeriesAliasDto> MapToSeriesAliasDtos(List<SeriesAlias> seriesAliases)
    {
        var seriesAliasDtos = new List<SeriesAliasDto>();
        foreach (var seriesAlias in seriesAliases)
        {
            seriesAliasDtos.Add(  MapToSeriesAliasDto(seriesAlias));
        }

        return seriesAliasDtos;
    }

    private SeriesAliasDto MapToSeriesAliasDto(SeriesAlias seriesAlias)
    {
        var seriesAliasDto = new SeriesAliasDto
        {
            IdType = seriesAlias.IdType,
            IdValue = seriesAlias.IdValue,
            SeriesId = seriesAlias.SeriesId,
        };
        return seriesAliasDto;
    }
}

