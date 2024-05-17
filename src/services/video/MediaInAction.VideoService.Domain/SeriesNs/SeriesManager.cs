using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowNs;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.TraktShowNs;
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
        if (seriesCreateDto.FirstAiredYear < 2000)
        {
            seriesCreateDto.FirstAiredYear = 2000;
        }

        var isActive = true;
        
        // Create new series
        var series = new Series(
            id: GuidGenerator.Create(),
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

    public async Task<Series> CreateAsync(
        string name,
        int year,
        List<( Guid? seriesId, string idType, string idValue
                )>
            seriesAliases,
        MediaType type = MediaType.Episode,
        bool isActive = true
    )
    {
        if (year < 2000)
        {
            year = 2000;
        }
        // Create new series
        Series series = new Series(
            id: GuidGenerator.Create(),
            name: name,
            firstAiredYear: year,
            seriesType: type,
            isActive: isActive
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
        
        foreach (var seriesAlias in seriesAliases)
        {
            try
            {
                series.AddSeriesAlias(
                    id: GuidGenerator.Create(),
                    seriesId: series.Id,
                    idType: seriesAlias.idType,
                    idValue: seriesAlias.idValue);
            }
            catch { } 
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
    
    public async Task<Series> CreateAsync(
        string name,
        int year,
        List<( string idType, string idValue)> seriesAliases,
        MediaType type = MediaType.Episode,
        bool isActive = true
    )
    {
        logger.LogInformation("CreateAsync");
        if (year < 1970)
        {
            year = 1970;
        }
        // check for duplicate name
        var dbSeriesSameName = await seriesRepository.GetBySeriesName(name);
        if ((dbSeriesSameName != null) & (dbSeriesSameName.Count > 0))
        {
            name = name + " " + year.ToString();
        }
            // Create new series
        Series series = new Series(
            id: GuidGenerator.Create(),
            name: name,
            firstAiredYear: year,
            seriesType: type,
            isActive: isActive
        );

        // Add new series aliases
        series.AddSeriesAlias(
            id: GuidGenerator.Create(),
            seriesId: series.Id,
            idType: "name",
            idValue: series.Name.ToLower()
        );
        series.AddSeriesAlias(
            id: GuidGenerator.Create(),
            seriesId: series.Id,
            idType: "folder",
            idValue: series.Name
        );
        
        foreach (var seriesAlias in seriesAliases)
        {
            try
            {
                var myAlias = seriesAlias.idValue;
                if (seriesAlias.idType == "name")
                {
                    myAlias = seriesAlias.idValue.ToLower();
                }
                
                series.AddSeriesAlias(
                    id: GuidGenerator.Create(),
                    seriesId: series.Id,
                    idType: seriesAlias.idType,
                    idValue: myAlias);
            }
            catch { } 
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
        if (dbSeries == null)
        {
            var createSeries = await seriesRepository.InsertAsync(series, true);
            //await PublishCreateSeriesEvent(createSeries);
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
            id: GuidGenerator.Create(),
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

    public async Task<Series> AcceptTraktShowAsync(
        string externalId, 
        string slug, 
        string name, 
        int year,
        List<(string idType, string idValue)> aliases
        )
    {
        var series = await seriesRepository.FindBySeriesNameYear(name,year);
        
        if (series == null)
        {
            var aliasesForManager =  aliases;
            var newSeries = await CreateAsync(name, year, aliasesForManager, MediaType.Episode, true);
            return newSeries;
        }
        else  // is an update
        {
            if (slug.Length > 0)
            {
                var dbSeriesAlias = await seriesAliasRepository.GetByIdValue(slug);
                if (dbSeriesAlias != null)
                {
                    var dbSeries = await seriesRepository.GetAsync(dbSeriesAlias.SeriesId);
                    if (dbSeries.SeriesAliases.Count != aliases.Count)
                    {
                        foreach (var alias in aliases)
                        {
                            if (alias.idType.Length > 0)
                            {
                                var found = false;
                                foreach (var seriesAlias in dbSeries.SeriesAliases)
                                {
                                    if ((seriesAlias.IdType == alias.idType) && (seriesAlias.IdValue == alias.idValue))
                                    {
                                        found = true;
                                    }
                                }
                                if (found == false)
                                {
                            
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
    
    public async Task<Series> SetAsInActiveAsync(Guid seriesId)
    {
        var series = await seriesRepository.GetAsync(seriesId);
        if (series == null)
        {
            throw new BusinessException(VideoServiceErrorCodes.SeriesWithIdNotFound)
                .WithData("SeriesId", seriesId);
        }

        series.SetSeriesInactive();

        // Publish series inActive event
        await distributedEventBus.PublishAsync(new SeriesInActiveEto
        {
            SeriesId = series.Id,
            Name = series.Name,
            SeriesAliases = GetSeriesAliasEtoList(series.SeriesAliases)
        });

        return await seriesRepository.UpdateAsync(series, autoSave: true);
    }
    
    public async Task<Series> AcceptEmbyShowAsync(
        EmbyShowCreatedEto eventData)
    {
        try
        {
            if (!Guid.TryParse(eventData.EmbyId, out var traktId))
            {
                throw new BusinessException(VideoServiceErrorCodes.TraktShowIdNotGuid);
            }

            var series = await seriesRepository.GetAsync(traktId);
            if (series != null)
            {
                series.EventStatus = FileStatus.Accepted;
                await seriesRepository.UpdateAsync(series, true);
            }

            return series;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Series> AcceptTraktSeriesAsync(TraktShowAcknowledgeEto eventData)
    {
        try
        {
            if (!Guid.TryParse(eventData.TraktId, out var traktId))
            {
                throw new BusinessException(VideoServiceErrorCodes.TraktShowIdNotGuid);
            }

            var series = await seriesRepository.GetAsync(traktId);
            if (series != null)
            {
                series.EventStatus = FileStatus.Accepted;
                await seriesRepository.UpdateAsync(series, true);
            }

            return series;
        }
        catch
        {
            return null;
        }
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
}
