using Volo.Abp.Modularity;

namespace MediaInAction.EmbyService
{
    [DependsOn(
        typeof(EmbyServiceApplicationModule),
        typeof(EmbyServiceDomainTestModule)
        )]
    public class EmbyServiceApplicationTestModule : AbpModule
    {

    }
}
