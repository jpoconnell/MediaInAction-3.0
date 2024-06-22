using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog.Core;

namespace MediaInAction.VideoService.Monitoring;
public class TracingSetup
{
    public static void Init(WebApplicationBuilder builder, Logger logger)
    {
        var otelConfig = builder.Configuration.GetSection("Otel");
        if (!otelConfig.Exists() || !otelConfig.GetValue<bool>("Enabled") || otelConfig["Endpoint"] == null)
        {
            logger.Warning("OpenTelemetry Tracing is disabled, or no endpoint is configured");
            return;
        }

        var serviceName = otelConfig["ServiceName"] ?? "webapi";


        
    }
}