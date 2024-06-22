using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace MediaInAction.DelugeService.MongoDb
{
    [DependsOn(
        typeof(DelugeServiceDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class DelugeServiceMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<DelugeServiceMongoDbContext>(options =>
            {
                options.AddDefaultRepositories();
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
