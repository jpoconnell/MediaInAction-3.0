using MediaInAction.AdministrationService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MediaInAction.AdministrationService
{
    [DependsOn(
        typeof(AdministrationServiceEntityFrameworkCoreTestModule)
        )]
    public class AdministrationServiceDomainTestModule : AbpModule
    {

    }
}