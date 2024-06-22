using Volo.Abp.Modularity;

namespace MediaInAction.CatalogService
{
    [DependsOn(
        typeof(CatalogServiceApplicationModule),
        typeof(CatalogServiceDomainTestModule)
        )]
    public class CatalogServiceApplicationTestModule : AbpModule
    {

    }
}