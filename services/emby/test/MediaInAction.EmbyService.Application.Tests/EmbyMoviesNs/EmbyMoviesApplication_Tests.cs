using MediaInAction.EmbyService.EmbyMoviesNs;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Volo.Abp.Users;

namespace MediaInAction.EmbyService.MoviesNs;

public class EmbyMoviesApplication_Tests : EmbyServiceApplicationTestBase
{
    private readonly IEmbyMovieAppService _moviesAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public EmbyMoviesApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _moviesAppService = GetRequiredService<IEmbyMovieAppService>();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

}

