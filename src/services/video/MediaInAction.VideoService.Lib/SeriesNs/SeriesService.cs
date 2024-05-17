using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.SeriesAliasNs.Dtos;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesService: ISeriesService
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly ILogger<SeriesService> _logger;
    
    public SeriesService(
        ISeriesRepository seriesRepository,
        ILogger<SeriesService>  logger)
    {
        _seriesRepository = seriesRepository;
        _logger = logger;
    }
    
    public async Task<SeriesDto> GetByIdAsync(Guid seriesId)
    {
        try
        {
            var series = await _seriesRepository.GetAsync(seriesId, true);
            return MapToSeriesDto(series);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("SeriesService.GetByIdAsync" + ex.Message);
            return null;
        }
    }

    private SeriesDto MapToSeriesDto(Series series)
    {
        var seriesDto = new SeriesDto
        {
            Id = series.Id,
            Name = series.Name,
            FirstAiredYear = series.FirstAiredYear,
            IsActive = series.IsActive
        };
        
        if (series.SeriesAliases != null)
        {
            seriesDto.SeriesAliasDtos = new List<SeriesAliasDto>();
            foreach (var seriesAlias in series.SeriesAliases )
            {
                var newSeriesAliasDto = new SeriesAliasDto();
                newSeriesAliasDto.IdType = seriesAlias.IdType;
                newSeriesAliasDto.IdValue = seriesAlias.IdValue;
                newSeriesAliasDto.SeriesId = seriesAlias.SeriesId;
                seriesDto.SeriesAliasDtos.Add(newSeriesAliasDto);
            }
        }
        return seriesDto;
    }
    
    public async Task<List<SeriesDto>> GetActiveList()
    {
        var seriesList = await _seriesRepository.GetActiveList();
        var seriesDtos = new List<SeriesDto>();
        foreach (var series in seriesList)
        {
            var seriesDto = new SeriesDto
            {
                Id = series.Id,
                Name = series.Name,
                FirstAiredYear = series.FirstAiredYear,
                IsActive = series.IsActive
            };
            seriesDtos.Add(seriesDto);
        }

        return seriesDtos;
    }
    
    public async Task<List<SeriesDto>> GetAllNoDefault()
    {
        var seriesList = await _seriesRepository.GetNoDefault();
        var seriesDtos = new List<SeriesDto>();
        foreach (var series in seriesList)
        {
            var found = false;
            foreach (var sa in series.SeriesAliases)
            {
                if (sa.IdType == "name")
                {
                    found = true;
                }
            }

            if (found == false)
            {
                var seriesDto = new SeriesDto
                {
                    Id = series.Id,
                    Name = series.Name,
                    FirstAiredYear = series.FirstAiredYear,
                    IsActive = series.IsActive
                };
                seriesDtos.Add(seriesDto);
            }
        }
        return seriesDtos;
    }

    public async Task<string> GetSlugAsync(Guid seriesId)
    {
        var seriesDto = await GetByIdAsync(seriesId);
        var slug = "";
        foreach (var seriesAliasDto in seriesDto.SeriesAliasDtos)
        {
            if (seriesAliasDto.IdType == "slug")
            {
                slug = seriesAliasDto.IdValue;
                break;
            }
        }

        return slug;
    }
}

