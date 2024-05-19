using Volo.Abp.Modularity;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(TraktServiceApplicationModule)
        )]
    public class TraktServiceApplicationTestModule : AbpModule
    {

    }
}