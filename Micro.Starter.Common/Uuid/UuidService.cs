namespace Micro.Starter.Common.Uuid
{
    public class UuidService : IUuidService
    {
        public string GenerateUuId()
        {
            return System.Guid.NewGuid().ToString();
        }
    }
}
