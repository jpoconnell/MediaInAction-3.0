using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MediaInAction.VideoService.SeriesAliasNs;

public class SeriesAliasManager : DomainService
{
    private readonly ISeriesAliasRepository _seriesAliasRepository;
    private readonly ILogger<SeriesAliasManager> _logger;

    public SeriesAliasManager(ISeriesAliasRepository seriesAliasRepository,
        ILogger<SeriesAliasManager> logger)
    {
        _seriesAliasRepository = seriesAliasRepository;
        _logger = logger;
    }

    public async Task<SeriesAlias> CreateAsync(
        Guid seriesId,
        [NotNull] string idType,
        [NotNull] string idValue)
    {
        Check.NotNullOrWhiteSpace(idType, nameof(idType));
        Check.NotNullOrWhiteSpace(idValue, nameof(idValue));

        if (idType == "name")
        {
            idValue = idValue.ToLower();
        }
        var existingSeriesAlias = new SeriesAlias();
        if (idType == "folder")
        {
            existingSeriesAlias = await _seriesAliasRepository.FindBySeriesIdType(seriesId, idType);
        }
        else
        {
            existingSeriesAlias = await _seriesAliasRepository.FindBySeriesTypeValueAsync(seriesId,idType,idValue);
        }
        
        if (existingSeriesAlias != null)
        {
            var name = idType + "-" + idValue;
            _logger.LogInformation(name);
           // throw new SeriesAliasAlreadyExistsException(name);
           return existingSeriesAlias;
        }
        else
        {
            var newSeriesAlias =  new SeriesAlias(
                GuidGenerator.Create(),
                seriesId,
                idType,
                idValue
            );
        
            var createSeries = await _seriesAliasRepository.InsertAsync(newSeriesAlias, true);
            return createSeries;
        }
    }

    public async Task ChangeIdValueAsync(
        [NotNull] SeriesAlias seriesAlias,
        [NotNull] string newValue)
    {
        Check.NotNull(seriesAlias, nameof(seriesAlias));
        Check.NotNullOrWhiteSpace(newValue, nameof(newValue));

        var existingSeriesAlias = await _seriesAliasRepository.FindBySeriesTypeValueAsync(seriesAlias.SeriesId, 
            seriesAlias.IdType,newValue);
        if (existingSeriesAlias != null && existingSeriesAlias.Id != seriesAlias.Id)
        {
            //    throw new SeriesAliasAlreadyExistsException(newValue);
        }

       // seriesAlias.ChangeIdValue(newValue);
    }

    public async  Task<SeriesAlias>  CreateFolderAsync(Guid seriesId, string idValue)
    {
        return await CreateAsync(seriesId, "folder", idValue);
    }

    public async Task<SeriesAlias> CreateNameAsync(Guid seriesId, string idValue)
    {
        var newIdValue =  idValue.ToLower(); 
        return await this.CreateAsync(seriesId, "name", idValue.ToLower());
    }
}

