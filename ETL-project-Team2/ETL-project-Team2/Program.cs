using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ETL_project_Team2.services;

namespace ETL_project_Team2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            // var s = new YmlFilterParser().ParseQuery(@"             AND : 
            //       - OR :
            //         - q : Name = 'ali'
            //         - q : LastName = 'Alavi'
            //         - OR : 
            //           - q : some_query
            //           - q : some_query
            //       - OR : 
            //         - AND : 
            //           - q : some_query
            //           - q : some_query
            //         - q : FatherName = 'Ali'
            //         - q : MotherName = 'Zahra'");
            //                 Console.WriteLine(s);
            IFilterService filterService = new FilterService(new YmlFilterParser());
            var sqlConnection = new SqlConnection("Trusted_Connection=True;server=(local);");
            var query = @"AND :
- q : date1 >= '2020-10-13'";
            sqlConnection.Open();
            filterService.Filter(query, "[FirstDatabase].dbo.[AMIRS]", "#NEW4", sqlConnection);
            var sqlCommand = new SqlCommand("SELECT * FROM #NEW4", sqlConnection);
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(((IDataRecord)reader)[0]);
            }
            sqlConnection.Close();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
