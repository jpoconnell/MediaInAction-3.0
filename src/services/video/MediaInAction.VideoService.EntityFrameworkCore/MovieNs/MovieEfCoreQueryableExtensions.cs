using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MediaInAction.VideoService.MovieNs;

public static class MovieEfCoreQueryableExtensions
{
    public static IQueryable<Movie> IncludeDetails(this IQueryable<Movie> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.MovieAliases);
    }
}