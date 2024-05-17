using System.Threading.Tasks;
using MediaInAction.VideoService.MovieNs.Dtos;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.MovieNs;

public class MovieAppServiceTests : VideoServiceApplicationTestBase
{
    private readonly IMovieAppService _movieAppService;
    

    public MovieAppServiceTests()
    {
        _movieAppService = GetRequiredService<IMovieAppService>();
    }


    [Fact]
    public async Task Get_Always_ReturnsAllMovie()
    {
        //Act
        var filter = new GetMoviesInput();
        var result = await _movieAppService.GetListPagedAsync(filter);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);

    }
}
