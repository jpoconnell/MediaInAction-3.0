using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.EpisodeNs;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktEpisodeNs.Dtos;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.TraktService.TraktEpisodesNs;

public class TraktEpisodeApplication_Tests : TraktServiceApplicationTestBase
{
    private readonly ITraktEpisodeAppService _traktEpisodeAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public TraktEpisodeApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _traktEpisodeAppService = GetRequiredService<ITraktEpisodeAppService>();
    }
    
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }


}