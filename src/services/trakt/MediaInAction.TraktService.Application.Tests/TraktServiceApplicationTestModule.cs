using Volo.Abp.Modularity;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(TraktServiceApplicationModule),
        typeof(TraktServiceDomainTestModule)
        )]
    public class TraktServiceApplicationTestModule : AbpModule
    {

    }
}