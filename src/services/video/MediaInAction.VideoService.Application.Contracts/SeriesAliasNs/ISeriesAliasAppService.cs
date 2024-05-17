using System;
using System.Threading.Tasks;

namespace MediaInAction.VideoService.SeriesAliasNs
{
    public interface ISeriesAliasAppService
    {
        Task<Guid> GetBySlug(string slug);
    }
}