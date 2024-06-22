using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.EmbyItems;

public interface IEmbyItemRepository :  IRepository<EmbyItem, Guid>
{
    Task<EmbyItem> FindByExternalId(string embyItemId);
}