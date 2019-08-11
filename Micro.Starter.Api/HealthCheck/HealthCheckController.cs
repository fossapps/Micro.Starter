using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Starter.Api.HealthCheck
{
    [ApiController]
    [Route("api/health")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HealthData>), StatusCodes.Status200OK)]
        public async Task<HealthData> Get()
        {
            return new HealthData
            {
                FakeCacheHealth = await GetFakeCacheHealth(),
                FakeDbHealth = await GetFakeDbHealth()
            };
        }

        private static Task<bool> GetFakeDbHealth()
        {
            return Task.Run(() => true);
        }

        private static Task<bool> GetFakeCacheHealth()
        {
            return Task.Run(() => true);
        }

    }
}
