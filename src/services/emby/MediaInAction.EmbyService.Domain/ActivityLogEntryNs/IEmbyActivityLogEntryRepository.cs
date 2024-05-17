using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.ActivityLogEntryNs;

public interface IEmbyActivityLogEntryRepository :  IRepository<EmbyActivityLogEntry, Guid>
{
    Task<List<EmbyActivityLogEntry>> GetListPagedAsync( 
        int skipCount, 
        int maxResultCount, 
        string sorting);

    Task<List<EmbyActivityLogEntry>> GetListAsync();
    
    Task<EmbyActivityLogEntry> FindByExternalId(long externalId);
}