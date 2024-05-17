using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesManagerUnitTests : VideoServiceDomainTestBase
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly SeriesManager _seriesManager;

    public SeriesManagerUnitTests()
    {
        _seriesRepository = GetRequiredService<ISeriesRepository>();
        _seriesManager = GetRequiredService<SeriesManager>();
    }
    
    [Fact]
    public async Task Should_Create_A_New_Series()
    {
        var seriesAliases =
            new List<( string idType, string idValue)>();
        seriesAliases.Add(( "Test series", "Code:001"));
        
        var createdSeries = await _seriesManager.CreateAsync(
            "Series Name 1",
            2020,
            seriesAliases,
            MediaType.Episode,
            true
        );
        
        createdSeries.ShouldNotBeNull();
    }
}
