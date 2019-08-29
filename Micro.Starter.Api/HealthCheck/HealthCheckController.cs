using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Micro.Starter.Api.Models;
using Micro.Starter.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Starter.Api.HealthCheck
{
    [ApiController]
    [Route("api/health")]
    public class HealthCheckController : ControllerBase
    {
        private readonly IWeatherRepository _weather;

        public HealthCheckController(IWeatherRepository weather)
        {
            _weather = weather;
        }

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

        private async Task<bool> GetFakeDbHealth()
        {
            try
            {
                await _weather.Create(new Weather());
                return true;
            }
            catch (Exception)
            {
                // todo: log e
                return false;
            }
        }

        private static Task<bool> GetFakeCacheHealth()
        {
            return Task.Run(() => true);
        }

    }
}
