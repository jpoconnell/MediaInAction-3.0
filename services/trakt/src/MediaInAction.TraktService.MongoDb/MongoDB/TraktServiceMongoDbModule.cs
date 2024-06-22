using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace MediaInAction.TraktService.MongoDb
{
    [DependsOn(
        typeof(TraktServiceDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class TraktServiceMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<TraktServiceMongoDbContext>(options =>
            {
                options.AddDefaultRepositories();
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
