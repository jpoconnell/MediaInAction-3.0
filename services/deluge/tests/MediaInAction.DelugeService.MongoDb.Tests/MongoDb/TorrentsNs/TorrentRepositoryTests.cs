using System.Threading.Tasks;
using MediaInAction.DelugeService.TorrentNs;
using Shouldly;
using Volo.Abp.Specifications;
using Xunit;

namespace MediaInAction.DelugeService.MongoDb.TorrentsNs;


[Collection(DelugeServiceTestConsts.CollectionDefinitionName)]
public class TorrentRepositoryTests : DelugeServiceMongoDbTestBase
{
    private readonly ITorrentRepository _torrentRepository;

    public TorrentRepositoryTests()
    {
        _torrentRepository = GetRequiredService<ITorrentRepository>();
    }

    [Fact]
    public async Task Should_Query_torrent()
    {

        await WithUnitOfWorkAsync(async () =>
        { 
            //Act
            ISpecification<Torrent> specification = TorrentNs.Specifications.SpecificationFactory.Create("");
            
            var torrentList = await _torrentRepository.GetListPagedAsync(
                specification, 0,10,"",false);
                
            //Assert
            torrentList.ShouldNotBeNull();
        });
    }
}
