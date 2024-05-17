using System.Collections.Generic;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.VideoService.MovieNs;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace MediaInAction.VideoService
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpBackgroundJobsAbstractionsModule)
    )]
    public class VideoServiceTestBaseModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAlwaysAllowAuthorization();
            
            // Add TestDataSeedContributor to the end of the list so that Domain DataSeedContributor
            // (which is located under EfCore layer because of dbContext seeding) can run first
           
            Configure<AbpDataSeedOptions>(options =>
            {
                options.Contributors.Remove<VideoServiceTestDataSeedContributor>();
                options.Contributors.AddLast(typeof(VideoServiceTestDataSeedContributor));
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            SeedTestData(context);
        }

        private static void SeedTestData(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>()
                        .SeedAsync();
                }
            });
        }
    }
}