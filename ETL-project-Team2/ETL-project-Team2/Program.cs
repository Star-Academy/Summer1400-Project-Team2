using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ETL_project_Team2.controllers;
using ETL_project_Team2.dao;
using ETL_project_Team2.services;
using Microsoft.Extensions.DependencyInjection;

namespace ETL_project_Team2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped<IFilterService, FilterService>()
                        .AddScoped<IJoinService, JoinService>()
                        .AddScoped<IDBAccessor, DBAccessor>()
                        .AddScoped<IPipelineDBAcessor, PipelineDBAccessor>()
                        .AddScoped<ITablesDBAccessor, TablesDBAccessor>()
                        .AddScoped<IPipelineHandler, PipelineHandler>()
                        .AddScoped<ICSVInputService, CSVInputService>()
                        .AddScoped<ICSVOutputHandler, CSVOutputHandler>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                                   {
                                       webBuilder.UseStartup<Startup>();
                                   });
    }
}
