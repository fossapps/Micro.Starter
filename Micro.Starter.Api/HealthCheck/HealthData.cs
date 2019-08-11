namespace Micro.Starter.Api.HealthCheck
{
    public class HealthData
    {
        public bool Healthy => FakeDbHealth && FakeCacheHealth;

        public bool FakeDbHealth { set; get; }
        public bool FakeCacheHealth { set; get; }
    }
}
