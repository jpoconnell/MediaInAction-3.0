using MediaInAction.TraktService.MongoDb;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(TraktServiceMongoDbTestModule)
        )]
    public class TraktServiceDomainTestModule : AbpModule
    {

    }
}