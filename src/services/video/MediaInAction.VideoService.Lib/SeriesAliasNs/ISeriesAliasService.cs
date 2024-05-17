using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesAliasNs.Dtos;

namespace MediaInAction.VideoService.SeriesAliasNs;

public interface ISeriesAliasService
{
    Task<List<SeriesAliasDto>> GetAllByType(string idType);
    Task<SeriesAliasDto> FindBySeriesTypeValueAsync(Guid seriesId, string idType, string alias);
    Task CreateSeriesAlias(Guid seriesId, string idType, string alias);
    Task<SeriesAliasDto> GetByIdValue(string parserSeriesName);
    Task<SeriesAliasDto> GetBySeriesIdType(Guid id, string type);
}