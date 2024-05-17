using System.Threading.Tasks;
using DelugeRPCClient.Net;
using Shouldly;

namespace MediaInAction.DelugeService;

public class LabelTests
{
    
    [Fact]
    public async Task ListLabels()
    {
        var client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

        var loginResult = await client.Login();
        loginResult.ShouldBe(true);
   
        var labels = await client.ListLabels();
        labels.Count.ShouldBeGreaterThan(0);

        var logoutResult = await client.Logout();
        logoutResult.ShouldBe(true);
    }
}
