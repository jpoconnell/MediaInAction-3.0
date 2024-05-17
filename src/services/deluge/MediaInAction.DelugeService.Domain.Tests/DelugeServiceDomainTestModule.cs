using MediaInAction.DelugeService.MongoDb;
using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService
{

    [DependsOn(
        typeof(DelugeServiceMongoDbTestModule)
    )]
    public class DelugeServiceDomainTestModule : AbpModule
    {
        
    }
}
