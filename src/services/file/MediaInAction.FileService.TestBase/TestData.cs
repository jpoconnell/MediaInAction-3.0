using System;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService;

public class TestData : ISingletonDependency
{
    public string CurrentUserEmail { get; set; } = "galip.erdem@volosoft.com";
    public Guid CurrentUserId { get; set; } = Guid.NewGuid();
    public string CurrentUserName { get; set; } = "Galip T. ERDEM";

    public string TestUserEmail { get; set; } = "test@user.com";
    public string TestUserName { get; set; } = "Test User";
    
    public string FileName1 { get; set; } = "Test FBI";
    public string FileName2 { get; set; } = "Test Law and Order";
    public string FileName3 { get; set; } = "Test The Lincoln Layer";
    public string FileName4 { get; set; } = "Test SWAT";
}