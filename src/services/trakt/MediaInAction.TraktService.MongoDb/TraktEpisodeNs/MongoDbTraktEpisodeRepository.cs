using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.TraktService.MongoDb;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class MongoDbTraktEpisodeRepository
    : MongoDbRepository<TraktServiceMongoDbContext, TraktEpisode, Guid>,
    ITraktEpisodeRepository
{
    public MongoDbTraktEpisodeRepository(
        IMongoDbContextProvider<TraktServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }

    public async Task<List<TraktEpisode>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .WhereIf<TraktEpisode, IMongoQueryable<TraktEpisode>>(
                !filter.IsNullOrWhiteSpace(),
                Episode => Episode.ShowSlug.Contains(filter)
            )
            .OrderBy(sorting)
            .As<IMongoQueryable<TraktEpisode>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<List<TraktEpisode>> GetDashboardAsync(ISpecification<TraktEpisode> spec)
    {
        try
        {
            CancellationToken cancellationToken = default;
            var queryable = await GetMongoQueryableAsync();
            return await (await GetMongoQueryableAsync())
                .Where(spec.ToExpression())
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        catch
        {
            Console.WriteLine();
            return null;
        }
    }

    public async Task<TraktEpisode> GetByTraktShowSlugSeasonEpisodeAsync(
        string slug, int season, int episode)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(Episode => Episode.ShowSlug == slug &&
                                                                  Episode.SeasonNum == season &&
                                                                  Episode.EpisodeNum == episode);
        }
        catch
        {
            Console.WriteLine();
            return null;
        }
    }

    public async Task<List<TraktEpisode>> GetListPagedAsync(
        string filter,
        int skipCount, 
        int maxResultCount, 
        string sorting,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .OrderBy(sorting)
            .As<IMongoQueryable<TraktEpisode>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<TraktEpisode> GetByIdentifier(string slug, int season, int episode)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(Episode => Episode.ShowSlug == slug &&
                                                              Episode.SeasonNum == season &&
                                                              Episode.EpisodeNum == episode);
        
    }

    public async Task<List<TraktEpisode>> GetEpisodesByShow(string slug)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .WhereIf<TraktEpisode, IMongoQueryable<TraktEpisode>>(
                !slug.IsNullOrWhiteSpace(),
                e => e.ShowSlug == slug
            )
            .As<IMongoQueryable<TraktEpisode>>()
            .ToListAsync();
    }
}
