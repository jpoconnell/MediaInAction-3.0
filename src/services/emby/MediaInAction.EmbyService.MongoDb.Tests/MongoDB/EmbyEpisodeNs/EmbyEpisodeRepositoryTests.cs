using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.MongoDB;
using MongoDB.Driver.Linq;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.TraktService.MongoDB.EmbyEpisodeNs;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * (like default AppUser repository IRepository<AppUser, Guid> here).
 * Only test your custom repository methods.
 */
[Collection(EmbyServiceTestConsts.CollectionDefinitionName)]
public class EmbyEpisodeRepositoryTests : EmbyServiceMongoDbTestBase
{
    private readonly IRepository<EmbyEpisode, Guid> _episodeRepository;

    public EmbyEpisodeRepositoryTests()
    {
        _episodeRepository = GetRequiredService<IRepository<EmbyEpisode, Guid>>();
    }

    [Fact]
    public async Task Should_Query_AppUser()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
                //Act
                var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));         
                var adminUser = await (await _episodeRepository.GetMongoQueryableAsync())
                .FirstOrDefaultAsync(u => u.CreationTime <  daysAgo30);

                //Assert
                adminUser.ShouldNotBeNull();
        });
    }
}
