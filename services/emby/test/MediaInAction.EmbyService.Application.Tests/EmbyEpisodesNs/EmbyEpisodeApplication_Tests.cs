using MediaInAction.EmbyService.EmbyEpisodeNs;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Volo.Abp.Users;

namespace MediaInAction.EmbyService.EmbyEpisodesNs;

public class EmbyEpisodeApplication_Tests : EmbyServiceApplicationTestBase
{
    private readonly IEmbyEpisodeAppService _embyEpisodeAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public EmbyEpisodeApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _embyEpisodeAppService = GetRequiredService<IEmbyEpisodeAppService>();
    }
    
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

}