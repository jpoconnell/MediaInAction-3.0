using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Repositories;


public class SeriesRepositoryTests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly IRepository<Series, Guid> _seriesRepository;

    public SeriesRepositoryTests()
    {
        _seriesRepository = GetRequiredService<IRepository<Series, Guid>>();
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
                var series = await (await _seriesRepository.GetQueryableAsync())
               // .Where(u => u.UserName == "admin")
                .FirstOrDefaultAsync();

                //Assert
                series.ShouldNotBeNull();
        });
    }
    
    //
}
