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
            CreateHostBuilder(args).Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IFilterService, FilterService>()
                        .AddSingleton<IJoinService, JoinService>()
                        .AddSingleton<IDBAccessor, DBAccessor>()
                        .AddSingleton<IPipelineDBAcessor, PipelineDBAccessor>()
                        .AddSingleton<ITablesDBAccessor, TablesDBAccessor>()
                        .AddTransient<IFilterHandler, FilterHandler>()
                        .AddTransient<IPipelineHandler, PipelineHandler>()
                        .AddTransient<IOperation, JoinHandler>();
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
