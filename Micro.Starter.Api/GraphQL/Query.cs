using System.Threading.Tasks;
using Micro.GraphQL.Federation;
using Micro.Starter.Api.GraphQL.Types;
using Micro.Starter.Storage;

namespace Micro.Starter.Api.GraphQL
{
    public sealed class Query : Query<EntityType>
    {
        public Query()
        {
            Field<WeatherType, Weather>()
                .Name("weather")
                .ResolveAsync(x => Task.FromResult(new Weather
                {
                    Id = "id",
                    Temperature = 23.3
                }));
        }
    }
}
