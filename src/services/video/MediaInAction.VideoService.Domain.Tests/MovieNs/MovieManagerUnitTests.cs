
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.MovieNs;

public class MovieManagerUnitTests : VideoServiceDomainTestBase
{
    private readonly MovieManager _movieManager;
    
    public MovieManagerUnitTests()
    {
        _movieManager = GetRequiredService<MovieManager>();
    }

    
    [Fact]
    public async Task Should_CreateMovieAsync1()
    {
        var movieItems =
            new List<( string idType, string idValue)>();
        movieItems.Add(( "Test product", "Code:001"));
        
        var createdMovie = await _movieManager.CreateAsync(
            "Clue",
            2020,
            movieItems,
            MediaType.Movie,
            MediaStatus.New
        );
        
        createdMovie.ShouldNotBeNull();
    }
}
