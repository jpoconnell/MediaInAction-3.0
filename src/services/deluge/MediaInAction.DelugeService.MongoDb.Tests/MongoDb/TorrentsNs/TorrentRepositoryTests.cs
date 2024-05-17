using System;
using System.Threading.Tasks;
using MediaInAction.DelugeService.TorrentNs;
using MongoDB.Driver.Linq;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.DelugeService.MongoDb.TorrentsNs;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * (like default AppUser repository IRepository<AppUser, Guid> here).
 * Only test your custom repository methods.
 */
[Collection(DelugeServiceTestConsts.CollectionDefinitionName)]
public class TorrentRepositoryTests : DelugeServiceMongoDbTestBase
{
    private readonly IRepository<Torrent, Guid> _torrentRepository;

    public TorrentRepositoryTests()
    {
        _torrentRepository = GetRequiredService<IRepository<Torrent, Guid>>();
    }

    [Fact]
    public async Task Should_Query_torrent()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
                //Act
                var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));       
                var adminUser = await (await _torrentRepository.GetMongoQueryableAsync())
                .FirstOrDefaultAsync(u => u.CreationTime < daysAgo30);

                //Assert
                adminUser.ShouldNotBeNull();
        });
    }
}
