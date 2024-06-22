using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.FileService.FileEntriesNs;

public interface IFileEntryRepository : IRepository<FileEntry, Guid>
{
    Task<FileEntry> FindByServerNameAsync(
        string server,
        string filename, 
        string directory,
        string extn);

    Task<List<FileEntry>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting
    );

    Task<FileEntry> GetByIdentifiers(string server,
        string fileName, 
        string dir, ListType listName);
}
