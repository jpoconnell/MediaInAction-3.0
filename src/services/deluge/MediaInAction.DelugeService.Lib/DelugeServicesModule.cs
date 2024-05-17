using MediaInAction.DelugeService.Config;
using MediaInAction.DelugeService.MongoDb;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService
{
    [DependsOn(
        typeof(DelugeServiceDomainModule),
        typeof(DelugeServiceMongoDbModule) 
    )]
    
    public class DelugeServiceLibModule : AbpModule
    {
        
        public override void ConfigureServices(ServiceConfigurationContext context)
        {  
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            Configure<DelugeServicesConfiguration>(configuration.GetSection("Deluge"));
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            
            // ... others
        }
        
    }
}
