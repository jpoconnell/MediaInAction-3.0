using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMoviesNs;
using MediaInAction.EmbyService.EmbyMoviesNs.Dtos;
using MediaInAction.EmbyService.MongoDb;
using MongoDB.Driver;
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
    

    public async Task<List<EmbyMovie>> GetEmbyMoviesAsync(
        ISpecification<EmbyMovie> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            
            return await queryable
                .Where(spec.ToExpression())
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<List<EmbyMovie>> GetDashboardAsync(
        ISpecification<EmbyMovie> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            
            return await queryable
                .Where(spec.ToExpression())
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    async Task<EmbyMovie> GetByEmbyMovieNameYearAsync(string name, int year)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(Movie => Movie.Name == name && Movie.FirstAiredYear == year);
    }

    public async Task<EmbyMovie> GetByNameAsync(string movieName)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(m => m.Name == movieName );
    }
}
