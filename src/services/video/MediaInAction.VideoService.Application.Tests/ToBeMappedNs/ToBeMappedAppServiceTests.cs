using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs.Dtos;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.ToBeMappedNs;

public class ToBeMappedAppServiceTests : VideoServiceApplicationTestBase
{
    private readonly IToBeMappedAppService _toBeMappedAppService;

    public ToBeMappedAppServiceTests()
    {
        _toBeMappedAppService = GetRequiredService<IToBeMappedAppService>();
    }

    [Fact]
    public async Task Get_Always_ReturnsAllToBeMapped()
    {
        //Act
        var filter = new GetToBeMappedsInput();
        var result = await _toBeMappedAppService.GetListPagedAsync(filter);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);

    }
}
