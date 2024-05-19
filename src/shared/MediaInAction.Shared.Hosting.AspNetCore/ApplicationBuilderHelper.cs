using System.Threading.Tasks;
using MediaInAction.Shared.Hosting.AspNetCore.Monitoring;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp.Modularity;

namespace MediaInAction.Shared.Hosting.AspNetCore;

public static class ApplicationBuilderHelper
{
    public static async Task<WebApplication> BuildApplicationAsync<TStartupModule>(string[] args)
        where TStartupModule : IAbpModule
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host
            .AddAppSettingsSecretsJson()
            .UseAutofac()
            .UseSerilog();
        
        // Setup logging, tracing and metrics
        var logger = LoggerSetup.Init(builder);
        TracingSetup.Init(builder, logger);
        MetricsSetup.Init(builder, logger);
        await builder.AddApplicationAsync<TStartupModule>();
        return builder.Build();
    }
}