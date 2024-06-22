using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MediaInAction.VideoService.EpisodeNs;
public static class EfCoreEpisodeQueryableExtensions
{
    public static IQueryable<Episode> IncludeDetails(this IQueryable<Episode> queryable, bool include = true)
    {
        return !include
            ? queryable
            : queryable
                .Include(q => q.EpisodeAliases);
    }
}