using System.Threading.Tasks;
using Micro.GraphQL.Federation;
using Micro.Starter.Storage;

namespace Micro.Starter.Api.GraphQL.Types
{
    public sealed class WeatherType : ObjectGraphType<Weather>
    {
        public WeatherType()
        {
            Name = "Weather";
            Field("id", x => x.Id);
            Field("temperature", x => x.Temperature);
            ResolveReferenceAsync(ctx => Task.FromResult(new Weather
            {
                Id = "id",
                Temperature = 32.2
            }));
        }
    }
}
