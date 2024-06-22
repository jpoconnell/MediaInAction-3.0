using System.Threading.Tasks;
using MediaInAction.DelugeService.BackgroundWorkers.Workers;
using MediaInAction.DelugeService.Config;
using MediaInAction.DelugeService.MongoDb;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;
using Volo.Abp.Quartz;

namespace MediaInAction.DelugeService.BackgroundWorkers;


[DependsOn(
    typeof(AbpBackgroundWorkersQuartzModule),
    typeof(DelugeServiceLibModule),
    typeof(DelugeServiceDomainSharedModule),
    typeof(DelugeServiceMongoDbModule) 
)]
public class DelugeServiceBackgroundWorkerModule : AbpModule
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
        var test = configuration["Redis:Configuration"];
    }
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddSingleton<IDelugeService, DelugeService>();
        context.Services.TryAddSingleton<DelugeServicesConfiguration, DelugeServicesConfiguration>();

        Configure<AbpBackgroundWorkerQuartzOptions>(options =>
        {
            options.IsAutoRegisterEnabled = true;
        });
    }
    
    public override async Task OnApplicationInitializationAsync(
        ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<GetTorrentsWorker>();

    }
}
