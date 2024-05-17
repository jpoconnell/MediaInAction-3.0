using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using MediaInAction.TraktService.TraktMovieNs;
using Volo.Abp.Users;

namespace MediaInAction.TraktService.TraktMoviesNs;

public class TraktMoviesApplication_Tests : TraktServiceApplicationTestBase
{
    private readonly ITraktMovieAppService _traktMoviesAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public TraktMoviesApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _traktMoviesAppService = GetRequiredService<ITraktMovieAppService>();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

}
