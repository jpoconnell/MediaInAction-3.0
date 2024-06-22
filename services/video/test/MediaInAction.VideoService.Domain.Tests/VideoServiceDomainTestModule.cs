using MediaInAction.CatalogService.MongoDB;
using Volo.Abp.Modularity;

namespace MediaInAction.CatalogService
{
    [DependsOn(
        typeof(CatalogServiceMongoDbTestModule)
        )]
    public class CatalogServiceDomainTestModule : AbpModule
    {

    }
}