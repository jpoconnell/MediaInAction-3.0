using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMoviesNs.Dtos;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.EmbyService.MongoDB.EmbyMoviesNs;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * (like default AppUser repository IRepository<AppUser, Guid> here).
 * Only test your custom repository methods.
 */
[Collection(EmbyServiceTestConsts.CollectionDefinitionName)]
public class EmbyMovieRepositoryTests : EmbyServiceMongoDbTestBase
{
    private readonly IRepository<EmbyMovie, Guid> _embyMovieRepository;

    public EmbyMovieRepositoryTests()
    {
        _embyMovieRepository = GetRequiredService<IRepository<EmbyMovie, Guid>>();
    }

    [Fact]
    public async Task Should_Query_AppUser()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var embyMovie = await  _embyMovieRepository.FirstOrDefaultAsync();

            //Assert
            embyMovie.ShouldNotBeNull();
        });
    }
}
