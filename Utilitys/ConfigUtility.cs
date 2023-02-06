using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace SwiftMigrationTool.Utilitys
{
    public class ConfigUtility
    {
        public static IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(System.AppContext.BaseDirectory))
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        public static IConfiguration Configuration = builder.Build();
        public static readonly string SourceConnectionString = Configuration.GetValue<string>("SourceConnectionString");
        public static readonly string DestinationConnectionString = Configuration.GetValue<string>("DestinationConnectionString");
        public static readonly List<string> MigrationMessageTypes = Configuration.GetSection("MigrationMessageTypes").Get<List<string>>();


    }
}