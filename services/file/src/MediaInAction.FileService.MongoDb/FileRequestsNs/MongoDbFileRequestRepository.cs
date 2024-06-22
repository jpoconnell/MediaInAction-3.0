using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.FileService.MongoDb;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace MediaInAction.FileService.FileRequestsNs;

public class MongoDbFileRequestRepository
    : MongoDbRepository<FileServiceMongoDbContext, FileRequest, Guid>,
        IFileRequestRepository
{
    public MongoDbFileRequestRepository(
        IMongoDbContextProvider<FileServiceMongoDbContext> dbContextProvider
    ) : base(dbContextProvider)
    {
    }
    
    public Task<FileRequest> FindByServerNameAsync(
        string server, 
        string filename, 
        string directory)
    {
        throw new NotImplementedException();
    }

    public Task<List<FileRequest>> GetListAsync(int skipCount, 
        int maxResultCount, 
        string sorting, 
        string filter = null)
    {
        throw new NotImplementedException();
    }

    public Task<FileRequest> GetByIdentifier(Guid refId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateFileRequestStatus(Guid refId, FileStatus accepted)
    {
        throw new NotImplementedException();
    }
}