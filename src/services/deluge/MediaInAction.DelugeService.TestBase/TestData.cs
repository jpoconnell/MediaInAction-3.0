using System;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.DelugeService;

public class TestData : ISingletonDependency
{
    public string CurrentUserEmail { get; set; } = "galip.erdem@volosoft.com";
    public Guid CurrentUserId { get; set; } = Guid.NewGuid();
    public string CurrentUserName { get; set; } = "Galip T. ERDEM";
    public Guid TestUserId { get; set; } = Guid.NewGuid();
    
    public string TorrentName1 { get; set; } = "Test FBI";
    public string TorrentName2 { get; set; } = "Test Law and Order";
    public string TorrentName3 { get; set; } = "Test The Lincoln Layer";
    public string TorrentName4 { get; set; } = "Test SWAT";
    
}