using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.EmbyService.MongoDb;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyMovieNs;

public class MongoDbEmbyMovieRepository
    : MongoDbRepository<EmbyServiceMongoDbContext, EmbyMovie, Guid>,
    IEmbyMovieRepository
{
    public MongoDbEmbyMovieRepository(
        IMongoDbContextProvider<EmbyServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }

    public async Task<EmbyMovie> FindByNameAsync(string name)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(Movie => Movie.Name == name);
    }
    
    
    public Task<List<EmbyMovie>> GetEmbyMoviesByUserId(Guid userId, ISpecification<EmbyMovie> spec, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyMovie>> GetEmbyMoviesAsync(ISpecification<EmbyMovie> spec)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyMovie>> GetEmbyMoviesAsync(ISpecification<EmbyMovie> spec, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyMovie>> GetDashboardAsync(ISpecification<EmbyMovie> spec, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    async Task<EmbyMovie> GetByEmbyMovieNameYearAsync(string name, int year)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(Movie => Movie.Name == name && Movie.FirstAiredYear == year);
    }
    
    public Task<EmbyMovie> GetByServerNameAsync(string server, string folder, bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<EmbyMovie> GetByNameAsync(string episodeName)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRange(HashSet<EmbyContent> mediaToUpdate)
    {
        throw new NotImplementedException();
    }

    public Task AddRange(HashSet<EmbyContent> mediaToAdd)
    {
        throw new NotImplementedException();
    }

    public Task<EmbyContent> GetByEmbyId(string movieInfoId)
    {
        throw new NotImplementedException();
    }

    public Task<EmbyMovie> GetByMovieId(int theMovieDbId, Guid userId)
    {
        throw new NotImplementedException();
    }
}
