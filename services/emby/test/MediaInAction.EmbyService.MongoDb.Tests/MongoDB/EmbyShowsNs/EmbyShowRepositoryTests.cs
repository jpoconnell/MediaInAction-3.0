using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowsNs;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.EmbyService.MongoDB.EmbyShowsNs;


[Collection(EmbyServiceTestConsts.CollectionDefinitionName)]
public class EmbyShowRepositoryTests : EmbyServiceMongoDbTestBase
{
    private readonly IEmbyShowRepository _embyShowRepository;

    public EmbyShowRepositoryTests()
    {
        _embyShowRepository = GetRequiredService<IEmbyShowRepository >();
    }

    [Fact]
    public async Task Should_Query_AppUser()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var adminUser = await _embyShowRepository.FirstOrDefaultAsync();

                //Assert
            adminUser.ShouldNotBeNull();
        });
    }
}
