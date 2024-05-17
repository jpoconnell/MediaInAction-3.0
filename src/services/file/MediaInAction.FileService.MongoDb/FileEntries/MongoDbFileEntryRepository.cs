using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MediaInAction.FileService.FileEntryNs;
using MediaInAction.FileService.MongoDb;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace MediaInAction.FileService.FileEntries;

public class MongoDbFileEntryRepository
    : MongoDbRepository<FileServiceMongoDbContext, FileEntry, Guid>,
    IFileEntryRepository
{
    public MongoDbFileEntryRepository(
        IMongoDbContextProvider<FileServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }

    public async Task<FileEntry> FindByNameAsync(string name)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(file => file.Filename == name);
    }

    public async Task<FileEntry> FindByServerNameAsync(
        string server,
        string filename, 
        string directory,
        string extn)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(file => file.Server == server && 
                                                           file.Filename == filename && 
                                                           file.Directory == directory &&
                                                           file.Extn == extn);
    }

    public async Task<List<FileEntry>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .OrderBy(sorting)
            .As<IMongoQueryable<FileEntry>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<FileEntry> GetByIdentifiers(string server, 
        string fileName, string dir, 
        ListType listName)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(file => file.Server == server && 
                                                           file.Filename == fileName && 
                                                           file.Directory == dir &&
                                                           file.ListName == listName);
    }
}
