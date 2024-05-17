using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.VideoService.TraktRequestNs
{
    public interface ITraktRequestRepository : IRepository<TraktRequest, Guid>
    {
        Task<List<TraktRequest>> GetUnCompleteRequests();
    }
}
