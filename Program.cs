using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BuyWebPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((config) =>
                {
                    
                    config.AddJsonFile("config.json", false, true).AddEnvironmentVariables();
                   
                })
            //OR .ConfigureAppConfiguration(StartUpConfiguration)
                .UseStartup<Startup>()
                .Build();

        private static void StartUpConfiguration(WebHostBuilderContext arg1, IConfigurationBuilder builder)
        {
            //cleaning the defaults configurations
            builder.Sources.Clear();

            builder.AddJsonFile("config.json", false, true)
                .AddEnvironmentVariables();
        }
    }
}
