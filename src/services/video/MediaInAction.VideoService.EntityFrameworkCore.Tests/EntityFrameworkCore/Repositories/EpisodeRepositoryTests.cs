using System;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Repositories;

public class EpisodeRepositoryTests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly IRepository<Episode, Guid> _episodeRepository;

    public EpisodeRepositoryTests()
    {
        _episodeRepository = GetRequiredService<IRepository<Episode, Guid>>();
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
                var episode = await (await _episodeRepository.GetQueryableAsync())
               // .Where(u => u.UserName == "admin")
                .FirstOrDefaultAsync();

                //Assert
                episode.ShouldNotBeNull();
        });
    }
}
