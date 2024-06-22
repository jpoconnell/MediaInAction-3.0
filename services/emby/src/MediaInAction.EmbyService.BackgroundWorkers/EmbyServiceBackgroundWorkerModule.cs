using System.Configuration;
using System.Threading.Tasks;
using EmbyClient.Dotnet.Client;
using MediaInAction.EmbyService.BackgroundWorkers.Workers;
using MediaInAction.EmbyService.EmbyApiServicesNs;
using MediaInAction.EmbyService.MongoDb;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;
using Volo.Abp.Quartz;

namespace MediaInAction.EmbyService.BackgroundWorkers;


[DependsOn(
    typeof(AbpBackgroundWorkersQuartzModule),
    typeof(EmbyServiceLibModule),
    typeof(EmbyServiceDomainSharedModule),
    typeof(EmbyServiceMongoDbModule) 
)]
public class EmbyServiceBackgroundWorkerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        PreConfigure<AbpQuartzOptions>(options =>
        {
            options.Configurator = configure =>
            {
                configure.UseInMemoryStore(storeOptions =>
                {
                });
            };
        });
    }
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddSingleton<IEmbyProcessActivityLog, EmbyProcessActivityLog>();
        context.Services.TryAddSingleton<IEmbyItemLookupService, EmbyItemLookupService>();
   
        Configure<AbpBackgroundWorkerQuartzOptions>(options =>
        {
            options.IsAutoRegisterEnabled = true;
        });
    }
    
    public  async Task OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var configuration = context.GetConfiguration();

        //await context.AddBackgroundWorkerAsync<EmbyActivityLogWorker>();
        await context.AddBackgroundWorkerAsync<EmbyGetItemsWorker>();
    }
}
