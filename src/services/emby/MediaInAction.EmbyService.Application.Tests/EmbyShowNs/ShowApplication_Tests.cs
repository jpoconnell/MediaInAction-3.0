using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Volo.Abp.Users;

namespace MediaInAction.EmbyService.EmbyShowNs;

public class EmbyShowApplication_Tests : EmbyServiceApplicationTestBase
{
    private readonly IEmbyShowAppService _embyShowAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public EmbyShowApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _embyShowAppService = GetRequiredService<IEmbyShowAppService>();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

}