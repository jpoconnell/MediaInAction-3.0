using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeManagerUnitTests : VideoServiceDomainTestBase
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly EpisodeManager _episodeManager;

    public EpisodeManagerUnitTests()
    {
        _episodeRepository = GetRequiredService<IEpisodeRepository>();
        _episodeManager = GetRequiredService<EpisodeManager>();
    }
    
    [Fact]
    public async Task Should_Create_A_New_Episode()
    {
        var episodeAliases =
            new List<( string idType, string idValue)>();
        episodeAliases.Add(( "Test episode", "Code:001"));
        
        var createdEpisode = await _episodeManager.CreateAsync(
            Guid.NewGuid(), 
            1,
            1,
            episodeAliases,
            DateTime.Now
        );
        
        createdEpisode.ShouldNotBeNull();
    }
}