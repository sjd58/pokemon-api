using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pokemon_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            string dbConnection = configuration.GetConnectionString("pokemon_db");
            string csvFilePath = configuration.GetConnectionString("pokemon.csv");

            BuildDatabase databaseBuilder = new BuildDatabase(dbConnection, csvFilePath);
            // Check to see if our database has data
            bool dbHasData = databaseBuilder.CheckForData();
            if (!dbHasData)
            {
                // If not, add data
                databaseBuilder.AddData();
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
