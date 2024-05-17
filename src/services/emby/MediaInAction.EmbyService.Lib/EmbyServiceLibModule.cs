using MediaInAction.EmbyService.MongoDb;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace MediaInAction.EmbyService
{
    [DependsOn(
        typeof(EmbyServiceDomainModule),
        typeof(EmbyServiceMongoDbModule)
    )]
    
    public class EmbyServiceLibModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            Configure<EmbyServicesConfiguration>(configuration.GetSection("EmbyService"));
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
        }
    }

    public class EmbyServicesConfiguration
    {
    }
}
