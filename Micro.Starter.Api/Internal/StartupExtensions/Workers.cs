using Micro.Starter.Api.Internal.Workers;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.Starter.Api.Internal.StartupExtensions
{
    public static class Workers
    {
        public static void RegisterWorker(this IServiceCollection services)
        {
            services.AddHostedService<Worker>();
        }
    }
}
