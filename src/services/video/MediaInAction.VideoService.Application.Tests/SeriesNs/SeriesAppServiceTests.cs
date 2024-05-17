using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Shouldly;
using Xunit;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesAppServiceTests : VideoServiceApplicationTestBase
{
    private readonly ISeriesAppService _seriesAppService;

    public SeriesAppServiceTests()
    {
        _seriesAppService = GetRequiredService<ISeriesAppService>();
    }
    
    [Fact]
    public async Task Get_Always_ReturnsAllSeries()
    {
        //Act
        var filter = new GetSeriesListInput();
        var result = await _seriesAppService.GetSeriesListAsync(filter);

        //Assert
        result.Count.ShouldBeGreaterThan(0);
    }
    
    [Fact]
    public async Task GetById_IfExists_ReturnsSeries()
    {
        var filter = new GetSeriesInput();
        filter.Filter = "n:Law and Order";
        var result = await _seriesAppService.GetSeriesAsync(filter);
        
        Assert.NotNull(result);
        Assert.Equal("Law and Order", result!.Name);
    }
    
    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        var filter = new GetSeriesInput();
        filter.Filter = "n:Law and DisOrder";
        var result = await _seriesAppService.GetSeriesAsync(filter);
        result.ShouldBeNull();
    }
}