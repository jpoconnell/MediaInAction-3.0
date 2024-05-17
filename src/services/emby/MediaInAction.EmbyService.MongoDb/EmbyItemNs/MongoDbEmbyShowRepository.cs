using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyItems;
using MediaInAction.EmbyService.MongoDb;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
namespace MediaInAction.EmbyService.EmbyItemNs;

public class MongoDbEmbyItemRepository
    : MongoDbRepository<EmbyServiceMongoDbContext, EmbyItem, Guid>,
    IEmbyItemRepository
{
    public MongoDbEmbyItemRepository(
        IMongoDbContextProvider<EmbyServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }

    public async Task<EmbyItem> FindByExternalId(string externalId)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.EmbyItemId == externalId);
    }
}
