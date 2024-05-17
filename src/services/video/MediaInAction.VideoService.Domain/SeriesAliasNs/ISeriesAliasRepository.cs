using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace  MediaInAction.VideoService.SeriesAliasNs
{
    public interface ISeriesAliasRepository : IRepository<SeriesAlias, Guid>
    {
        Task<SeriesAlias> GetByIdValue(string idValue);
        Task<List<SeriesAlias>> GetByIdType(string idType);
        Task<SeriesAlias> FindBySeriesIdType(Guid myLink, string folder);
        Task<SeriesAlias> FindBySeriesTypeValueAsync(Guid seriesId, 
            string idType, string alias);

        Task<SeriesAlias> GetBySeriesType(Guid id, string type);
    }
}
