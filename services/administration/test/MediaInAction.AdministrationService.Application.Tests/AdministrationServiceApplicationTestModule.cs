using Volo.Abp.Modularity;

namespace MediaInAction.AdministrationService
{
    [DependsOn(
        typeof(AdministrationServiceApplicationModule),
        typeof(AdministrationServiceDomainTestModule)
        )]
    public class AdministrationServiceApplicationTestModule : AbpModule
    {

    }
}