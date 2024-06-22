using System;
using MediaInAction.EmbyService.MongoDb;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace MediaInAction.EmbyService.MongoDB
{
    [DependsOn(
        typeof(EmbyServiceTestBaseModule),
        typeof(EmbyServiceMongoDbModule)
        )]
    public class EmbyServiceMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var stringArray = EmbyServiceMongoDbFixture.ConnectionString.Split('?');
            var connectionString = stringArray[0].EnsureEndsWith('/')  +
                                       "Db_" +
                                   Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}
