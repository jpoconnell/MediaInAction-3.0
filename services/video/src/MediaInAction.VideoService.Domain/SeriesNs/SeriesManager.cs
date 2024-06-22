using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.SeriesAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;


namespace MediaInAction.VideoService.SeriesNs;

public class SeriesManager(
    ISeriesRepository seriesRepository,
    ISeriesAliasRepository seriesAliasRepository,
    IDistributedEventBus distributedEventBus,
    ILogger<SeriesManager> logger)
    : DomainService
{
    
    public async Task<Series> CreateAsync(SeriesCreateDto seriesCreateDto)
    {
        if (seriesCreateDto.FirstAiredYear < 1950)
        {
            seriesCreateDto.FirstAiredYear = 1950;
        }

        var isActive = true;
        
        // Create new series
        var series = new Series(
            name: seriesCreateDto.Name,
            firstAiredYear: seriesCreateDto.FirstAiredYear,
            seriesType: MediaType.Episode,
            isActive: isActive,
            imageName: seriesCreateDto.imageName
        );

        // Add new series aliases
        series.AddSeriesAlias(
            id: GuidGenerator.Create(),
            seriesId: series.Id,
            idType: "name",
            idValue: series.Name
        );
        series.AddSeriesAlias(
            id: GuidGenerator.Create(),
            seriesId: series.Id,
            idType: "folder",
            idValue: series.Name
        );

        foreach (var seriesAlias in seriesCreateDto.SeriesAliases)
        {
            series.AddSeriesAlias(
                id: GuidGenerator.Create(),
                seriesId: series.Id,
                idType: seriesAlias.IdType,
                idValue: seriesAlias.IdValue);
        }

        var dbSeries = new Series();

        var dbSeriesList = await seriesRepository.GetBySeriesName(series.Name);
        if (dbSeriesList.Count == 1)
        {
            dbSeries = dbSeriesList[0];
        }
        else
        {
            dbSeries = await seriesRepository.FindBySeriesNameYear(series.Name, series.FirstAiredYear);
        }

        try
        {
            if (dbSeries == null)
            {
                var createSeries = await seriesRepository.InsertAsync(series, true);
                await PublishCreateSeriesEvent(createSeries);
                return createSeries;
            }
            else
            {
                var update = 0;
                if (dbSeries.IsActive != series.IsActive)
                {
                    dbSeries.IsActive = series.IsActive;
                    update++;
                }

                if (dbSeries.Type != series.Type)
                {
                    dbSeries.Type = series.Type;
                    update++;
                }

                foreach (var seriesAlias in series.SeriesAliases)
                {
                    var found = false;
                    foreach (var dbSeriesAlias in dbSeries.SeriesAliases)
                    {
                        if ((dbSeriesAlias.IdType == seriesAlias.IdType) &&
                            (dbSeriesAlias.IdValue == seriesAlias.IdValue))
                        {
                            found = true;
                        }
                    }

                    if (found == false)
                    {
                        dbSeries.AddSeriesAlias(GuidGenerator.Create(), dbSeries.Id, seriesAlias.IdType,
                            seriesAlias.IdValue);
                        update++;
                    }
                }

                if (update > 0)
                {
                    await seriesRepository.UpdateAsync(dbSeries);
                }

                return dbSeries;
            }
        }

        catch (Exception ex)
        {
            logger.LogError(ex, "Error in CreateAsync");
            return null;
        }
    }
    
    public async Task<Series> CreateUpdateSeriesAsync(
        string name,
        int year,
        List<(string idType, string idValue)>
            seriesAliases,
        MediaType type = MediaType.Episode,
        bool isActive = true
    )
    {
        // Create new series
        Series series = new Series(
            name: name,
            firstAiredYear: year,
            seriesType: type,
            isActive: isActive
        );

        // Add new series aliases
        if (seriesAliases.Count == 0)
        {
            series.AddSeriesAlias(
                id: GuidGenerator.Create(),
                seriesId: series.Id,
                idType: "name",
                idValue: series.Name
            );
            series.AddSeriesAlias(
                id: GuidGenerator.Create(),
                seriesId: series.Id,
                idType: "folder",
                idValue: series.Name
            );
        }
        else
        {
            foreach (var seriesAlias in seriesAliases)
            {
                series.AddSeriesAlias(
                    id: GuidGenerator.Create(),
                    seriesId: series.Id,
                    idType: seriesAlias.idType,
                    idValue: seriesAlias.idValue
                );
            }
        }
        var dbSeries = await seriesRepository.FindBySeriesNameYear(series.Name, series.FirstAiredYear);

        if (dbSeries == null)
        {
            var createSeries = await seriesRepository.InsertAsync(series, true);
            return createSeries;
        }
        else
        {
            var update = 0;
            if (dbSeries.IsActive != series.IsActive)
            {
                dbSeries.IsActive = series.IsActive;
                update++;
            }

            if (dbSeries.Type != series.Type)
            {
                dbSeries.Type = series.Type;
                update++;
            }
       
            foreach (var seriesAlias in seriesAliases)
            {
                var found = false;
                foreach (var dbSeriesAlias in dbSeries.SeriesAliases)
                {
                    if ((dbSeriesAlias.IdType == seriesAlias.idType) && (dbSeriesAlias.IdValue == seriesAlias.idValue))
                    {
                        found = true;
                    }
                }

                if (found == false)
                {
                    dbSeries.AddSeriesAlias(GuidGenerator.Create(),dbSeries.Id,seriesAlias.idType,seriesAlias.idValue);
                    update++;
                }
            }
            
            if (update > 0)
            {
                await seriesRepository.UpdateAsync(dbSeries);
            }

            return dbSeries;
        }
    }


    public async Task<Series> SetAsInActiveAsync(Guid seriesId)
    {
        var series = await seriesRepository.GetAsync(seriesId);
        if (series == null)
        {
            throw new BusinessException(VideoServiceDomainErrorCodes.SeriesWithIdNotFound)
                .WithData("SeriesId", seriesId);
        }

        series.SetSeriesInactive();
        return await seriesRepository.UpdateAsync(series, autoSave: true);
    }

    
    private List<SeriesAliasCreatedEto> GetSeriesAliasEtoList(List<SeriesAlias> seriesAliases)
    {
        var etoList = new List<SeriesAliasCreatedEto>();
        foreach (var oItem in seriesAliases)
        {
            etoList.Add(new SeriesAliasCreatedEto()
            {
                SeriesAliasId = oItem.Id,
                SeriesId = oItem.SeriesId,
                IdType = oItem.IdType,
                IdValue = oItem.IdValue
            });
        }

        return etoList;
    }
    
    private async Task PublishCreateSeriesEvent(Series input)
    {
        // Publish Video Series creation event 
        await distributedEventBus.PublishAsync(new SeriesCreatedEto
        {
            SeriesId = input.Id,
            Name = input.Name,
            FirstAiredYear = input.FirstAiredYear,
            Type = input.Type,
            SeriesAliases = GetSeriesAliasEtoList(input.SeriesAliases)
        });
    }

    public async Task AddSeriesAliasAsync(Guid seriesId, SeriesAliasCreateDto seriesAliasCreateDto)
    {
        throw new NotImplementedException();
    }

    public async Task<Series> UpdateAsync(SeriesCreateDto seriesCreateDto)
    {
        var seriesList = await seriesRepository.GetBySeriesName(seriesCreateDto.Name);
        if (seriesList.Count == 1)
        {
            var series = seriesList[0];
           // series.IsActive = true;
           return null;
           //return await seriesRepository.UpdateAsync(series);
        }
        else
        {
            return await CreateAsync(seriesCreateDto);
        }
    }
}
