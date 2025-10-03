using Microsoft.Extensions.Configuration;
using Nyx.Server.Database;

namespace Nyx.Server
{
    public sealed class ServerSettings
    {
        public ServerSettings()
        {
            new ConfigurationBuilder()
                .AddJsonFile("ServerConfigrations.json")
                .AddEnvironmentVariables("Server") // or .AddEnvironmentVariables("Database_") if you want a prefix
                .AddEnvironmentVariables("Database") // or .AddEnvironmentVariables("Database_") if you want a prefix
                .AddEnvironmentVariables("Application") // or .AddEnvironmentVariables("Database_") if you want a prefix
                .Build()
                .Bind(this);
        }
        public DatabaseConfiguration Database { get; set; }

    }
}
