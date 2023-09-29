using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace IUstaFinalProject.Persistence
{
    internal static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

                return configurationBuilder.GetConnectionString("Default");
            }
        }
    }
}