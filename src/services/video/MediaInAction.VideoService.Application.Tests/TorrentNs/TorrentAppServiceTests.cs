using System.Threading.Tasks;
using MediaInAction.VideoService.TorrentNs.Dtos;
using Shouldly;
using Xunit;

namespace MediaInAction.VideoService.TorrentNs;

public class TorrentAppServiceTests : VideoServiceApplicationTestBase
{
    private readonly ITorrentAppService _torrentAppService;

    public TorrentAppServiceTests()
    {
        _torrentAppService = GetRequiredService<ITorrentAppService>();
    }

    [Fact]
    public async Task Get_Always_ReturnsAllTorrent()
    {
        //Act
        var filter = new GetTorrentsInput();
        var result = await _torrentAppService.GetListPagedAsync(filter);
            
        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
    }
}
