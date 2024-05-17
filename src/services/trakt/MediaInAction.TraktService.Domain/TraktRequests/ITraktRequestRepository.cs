using System;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.TraktService.TraktRequests
{
    public interface ITraktRequestRepository : IBasicRepository<TraktRequest, Guid>
    {
    }
}
