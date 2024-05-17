using MediaInAction.TraktService.MongoDb;
using MediaInAction.Shared.Hosting.AspNetCore;
using MediaInAction.Shared.Hosting.Microservices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using MediaInAction.Shared.Api;
using MediaInAction.TraktService.BackgroundWorkers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace MediaInAction.TraktService;

[DependsOn(
    typeof(TraktServiceHttpApiModule),
    typeof(TraktServiceApplicationModule),
    typeof(TraktServiceMongoDbModule),
    typeof(TraktServiceBackgroundWorkerModule),
    typeof(MediaInActionSharedHealthCheckModule),
    typeof(MediaInActionSharedHostingMicroservicesModule)
)]
public class TraktServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, "TraktService");

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes: ["TraktService"],
            discoveryEndpoint: configuration["AuthServer:MetadataAddress"],
            apiTitle: "Trakt Service API"
        );

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.Trim().RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(TraktServiceApplicationModule).Assembly, opts =>
            {
                opts.RootPath = "trakt";
                opts.RemoteServiceName = "Trakt";
            });
        });

        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            //Standalone MongoDB servers don't support transactions
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });

        Configure<AbpAntiForgeryOptions>(options => { options.AutoValidate = false; });

        context.Services.AddHealthChecks()
            .AddMongoDb(configuration["ConnectionStrings:TraktService"], tags: new string[] { "mongodb" });
            // TODO: add test for connection to trakt
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCorrelationId();
        app.UseCors();
        app.UseAbpRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpClaimsMap();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Trakt Service API");
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
        });
        app.UseAbpSerilogEnrichers();
        app.UseAuditing();
        app.UseUnitOfWork();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/startup");
            endpoints.MapHealthChecks("/liveness", new HealthCheckOptions { Predicate = _ => false });
            endpoints.MapHealthChecks("/ready", new HealthCheckOptions { Predicate = _ => false });
            endpoints.MapControllers();
        });
        
        app.UseConfiguredEndpoints();
    }
}