﻿using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace MediaInAction.FileService;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var assemblyName = typeof(Program).Assembly.GetName().Name;

        SerilogConfigurationHelper.Configure(assemblyName);

        try
        {
            Log.Information($"Starting {assemblyName}.");

            var app = await ApplicationBuilderHelper.BuildApplicationAsync<FileServiceHttpApiHostModule>(args);
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
            Log.CloseAndFlush();
        }
    }
}