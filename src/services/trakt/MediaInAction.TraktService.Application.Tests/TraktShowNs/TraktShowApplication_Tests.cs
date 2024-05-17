using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShowApplication_Tests : TraktServiceApplicationTestBase
{
    private readonly ITraktShowAppService _traktShowAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public TraktShowApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _traktShowAppService = GetRequiredService<ITraktShowAppService>();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

    [Fact]
    public async Task Get_Always_ReturnsAllSeries()
    {
        //Act
        var filter = new GetTraktShowListInput();
        var result = await _traktShowAppService.GetTraktShowListPagedAsync(filter);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
    }

}