using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.TorrentNs;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Repositories;

public class TorrentRepositoryTests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly IRepository<Torrent, Guid> _torrentRepository;

    public TorrentRepositoryTests()
    {
        _torrentRepository = GetRequiredService<IRepository<Torrent, Guid>>();
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
                var torrent = await (await _torrentRepository.GetQueryableAsync())
               // .Where(u => u.UserName == "admin")
                .FirstOrDefaultAsync();

                //Assert
                torrent.ShouldNotBeNull();
        });
    }
    
    //
}
