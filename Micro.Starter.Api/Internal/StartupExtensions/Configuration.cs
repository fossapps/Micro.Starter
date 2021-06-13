using Micro.Starter.Api.Internal.Configs;
using Micro.Starter.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Starter.Api.Internal.StartupExtensions
{
    public static class Configuration
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfig>(configuration.GetSection("DatabaseConfig"));
            services.Configure<Services>(configuration.GetSection("Services"));
            services.Configure<SlackLoggingConfig>(configuration.GetSection("Logging").GetSection("Slack"));
        }
    }
}
