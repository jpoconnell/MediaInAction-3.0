using System;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktEpisodeNs;
using MongoDB.Driver.Linq;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.TraktService.MongoDb.TraktEpisodeNs;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * (like default AppUser repository IRepository<AppUser, Guid> here).
 * Only test your custom repository methods.
 */
[Collection(TraktServiceTestConsts.CollectionDefinitionName)]
public class TraktEpisodeRepositoryTests : TraktServiceMongoDbTestBase
{
    private readonly IRepository<TraktEpisode, Guid> _traktEpisodeRepository;

    public TraktEpisodeRepositoryTests()
    {
        _traktEpisodeRepository = GetRequiredService<IRepository<TraktEpisode, Guid>>();
    }

    [Fact]
    public async Task Should_Query_GetAnyEpisode()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
                //Act
                var adminUser = await (await _traktEpisodeRepository.GetMongoQueryableAsync())
                .FirstOrDefaultAsync();

                //Assert
                adminUser.ShouldNotBeNull();
        });
    }
}
