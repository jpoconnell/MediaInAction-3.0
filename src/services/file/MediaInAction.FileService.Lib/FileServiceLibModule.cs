using MediaInAction.FileService.MongoDb;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace MediaInAction.FileService.Lib;

[DependsOn(
        typeof(FileServiceDomainModule),
        typeof(FileServiceMongoDbModule) 
    )]
    
public class FileServiceLibModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();
       // Configure<FileServicesConfiguration>(configuration.GetSection("FileService"));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
    }
}

