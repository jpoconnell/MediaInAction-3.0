using System.Threading.Tasks;
using MediaInAction.FileService.BackgroundWorkers.Workers;
using MediaInAction.FileService.FileEntryNs;
using MediaInAction.FileService.Lib;
using MediaInAction.FileService.Lib.Config;
using MediaInAction.FileService.Lib.FileEntriesNs;
using MediaInAction.FileService.Lib.FileServicesNs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;
using Volo.Abp.Quartz;

namespace MediaInAction.FileService.BackgroundWorkers;


[DependsOn(
    typeof(AbpBackgroundWorkersQuartzModule),
    typeof(FileServiceLibModule),
    typeof(FileServiceDomainSharedModule)
)]
public class FileServiceBackgroundWorkerModule : AbpModule
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
        context.Services.TryAddSingleton<IFileInventory, FileInventory>();
        context.Services.TryAddSingleton<IFileEntryLibService, FileEntryLibService>();
        context.Services.TryAddSingleton<FileServicesConfiguration, FileServicesConfiguration>();
 
        Configure<AbpBackgroundWorkerQuartzOptions>(options =>
        {
            options.IsAutoRegisterEnabled = true;
            
        });
    }
    public override async Task OnApplicationInitializationAsync(
        ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<FileInventoryWorker>();
      //  await context.AddBackgroundWorkerAsync<ResendUnAcceptedFileWorker>();
    }
}


