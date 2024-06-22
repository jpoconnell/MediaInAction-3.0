﻿using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MediaInAction.VideoService;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var assemblyName = typeof(Program).Assembly.GetName().Name;

        SerilogConfigurationHelper.Configure(assemblyName);

        try
        {
            Log.Information($"Starting {assemblyName}.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host
                .UseAutofac()
                .UseSerilog();
            
            builder.AddServiceDefaults();
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5054, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                });
                options.ListenAnyIP(8181, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });

            await builder.AddApplicationAsync<VideoServiceHttpApiHostModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();

            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"{assemblyName} terminated unexpectedly!");
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}