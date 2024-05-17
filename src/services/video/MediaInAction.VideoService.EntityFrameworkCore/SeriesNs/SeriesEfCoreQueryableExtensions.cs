using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MediaInAction.VideoService.SeriesNs;

public static class SeriesEfCoreQueryableExtensions
{
    public static IQueryable<Series> IncludeDetails(this IQueryable<Series> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.SeriesAliases);
    }
}