using System.Threading;
using System.Threading.Tasks;
using Micro.Starter.Storage;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Micro.Starter.Api.Internal.HealthCheck
{
    public class ConnectionToDbCheck : IHealthCheck
    {
        private readonly ApplicationContext _db;

        public ConnectionToDbCheck(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var canConnect = await _db.Database.CanConnectAsync(cancellationToken);
                return canConnect ? new HealthCheckResult(HealthStatus.Healthy, "Database can connect") : new HealthCheckResult(HealthStatus.Unhealthy, "database can't connect");
            }
            catch
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, "database isn't reachable");
            }
        }
    }
}
