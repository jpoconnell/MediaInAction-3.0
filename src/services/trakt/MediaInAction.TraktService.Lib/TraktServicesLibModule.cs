using MediaInAction.TraktService.Config;
using MediaInAction.TraktService.MongoDb;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(TraktServiceDomainModule),
        typeof(TraktServiceMongoDbModule) 
    )]
    public class TraktServiceLibModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {  
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            Configure<ServicesConfiguration>(configuration.GetSection("Trakt"));
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            
            // ... others
        }
    }
}
