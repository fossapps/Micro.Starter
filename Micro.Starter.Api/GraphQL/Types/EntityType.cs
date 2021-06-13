namespace Micro.Starter.Api.GraphQL.Types
{
    public class EntityType : Micro.GraphQL.Federation.Types.EntityType
    {
        public EntityType()
        {
            Type<WeatherType>();
        }
    }
}
