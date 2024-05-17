using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.EmbyService.MongoDb;
using MediaInAction.EmbyService.RequestNs;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyRequestNs;

public class MongoDbEmbyRequestRepository
    : MongoDbRepository<EmbyServiceMongoDbContext, Request, Guid>,
    IRequestRepository
{
    public MongoDbEmbyRequestRepository(
        IMongoDbContextProvider<EmbyServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }
    
    public Task<List<Request>> GetEmbyShowsByUserId(
        Guid userId, 
        ISpecification<Request> spec, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Request>> GetEmbyShowsAsync(
        ISpecification<Request> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Request>> GetDashboardAsync(
        ISpecification<Request> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Request> GetByEmbyShowNameYearAsync(
        string name, 
        int year, 
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Request> FindByServerNameAsync(string server, string filename, string directory)
    {
        throw new NotImplementedException();
    }

    public Task<List<Request>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
        throw new NotImplementedException();
    }

    public Task<Request> GetByIdentifier(Guid refId)
    {
        throw new NotImplementedException();
    }
}
