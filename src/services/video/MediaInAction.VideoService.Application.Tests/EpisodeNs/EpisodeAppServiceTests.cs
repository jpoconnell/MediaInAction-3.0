using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using Shouldly;
using Xunit;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeAppServiceTests : VideoServiceApplicationTestBase
{
    private readonly IEpisodeAppService _episodeAppService;

    public EpisodeAppServiceTests()
    {
        _episodeAppService = GetRequiredService<IEpisodeAppService>();
    }

    [Fact]
    public async Task Get_Always_ReturnsAllEpisode()
    {
        //Act
        var filter = new GetEpisodesInput();
        var result = await _episodeAppService.GetListPagedAsync(filter);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
    }
}
