using System;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.ToBeMappedNs;

public class ToBeMappedManagerUnitTests : VideoServiceDomainTestBase
{
    private readonly IToBeMappedRepository _toBeMappedRepository;
    private readonly ToBeMappedManager _toBeMappedManager;

    public ToBeMappedManagerUnitTests()
    {
        _toBeMappedRepository = GetRequiredService<IToBeMappedRepository>();
        _toBeMappedManager = GetRequiredService<ToBeMappedManager>();
    }

    [Fact]
    public async Task Should_CreateToBeMappedAsync()
    {    
        var input = new ToBeMappedCreateDto()
        {
            Alias = "alias",
            Processed = false
        };
        
        var createdToBeMapped = await _toBeMappedManager.CreateAsync(input);
        
        createdToBeMapped.ShouldNotBeNull();
    }
}
