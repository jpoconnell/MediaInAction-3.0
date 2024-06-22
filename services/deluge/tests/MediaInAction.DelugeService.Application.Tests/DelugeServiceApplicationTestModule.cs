using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService
{
    [DependsOn(
        typeof(DelugeServiceApplicationModule),
        typeof(DelugeServiceDomainTestModule)
        )]
    public class DelugeServiceApplicationTestModule : AbpModule
    {

    }
}
