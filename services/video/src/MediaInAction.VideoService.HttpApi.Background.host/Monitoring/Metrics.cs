using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using Serilog.Core;

namespace MediaInAction.VideoService.Monitoring;
public class MetricsSetup
{
    private static IConfigurationSection _otelConfig;
    public static void Init(WebApplicationBuilder builder, Logger logger)
    {
        _otelConfig = builder.Configuration.GetSection("Otel");
        if (!_otelConfig.Exists() || !_otelConfig.GetValue<bool>("Enabled"))
        {
            logger.Warning("OpenTelemetry Metrics are disabled");
            return;
        }

        builder.Services.AddOpenTelemetry().WithMetrics(metricsOpts => 
                metricsOpts.AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
               
            );
    }


}
