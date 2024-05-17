using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.EmbyService.MongoDb;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

public class MongoDbEmbyEpisodeRepository
    : MongoDbRepository<EmbyServiceMongoDbContext, EmbyEpisode, Guid>,
    IEmbyEpisodeRepository
{
    public MongoDbEmbyEpisodeRepository(
        IMongoDbContextProvider<EmbyServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }
    
    public Task<List<EmbyEpisode>> GetEmbyEpisodesByUserId(Guid userId, ISpecification<EmbyEpisode> spec, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyEpisode>> GetDashboardAsync(ISpecification<EmbyEpisode> spec)
    {
        throw new NotImplementedException();
    }
    
    public Task<List<EmbyEpisode>> GetListPagedAsync(ISpecification<EmbyEpisode> spec, 
        int skipCount, 
        int maxResultCount, 
        string sorting,
        bool includeDetails = false, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<EmbyEpisode> GetBySeriesSeasonEpisodeAsync(
        string showId, 
        int season, int episode)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyEpisode>> GetListPagedAsync(ISpecification<EmbyEpisode> specification, 
        int inputSkipCount, 
        int inputMaxResultCount, 
        string inputSorting)
    {
        throw new NotImplementedException();
    }

    public Task<EmbyEpisode> GetByEmbyShowSeasonEpisodeAsync(string showName, int season, int episode)
    {
        throw new NotImplementedException();
    }
}
