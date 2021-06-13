using System;
using Micro.GraphQL.Federation;
using Micro.Starter.Api.GraphQL.Directives;
using Micro.Starter.Api.GraphQL.Types;

namespace Micro.Starter.Api.GraphQL
{
    public class StarterSchema : Schema<EntityType>
    {
        public StarterSchema(IServiceProvider services, Query query) : base(services)
        {
            Query = query;
            Directives.Register(new AuthorizeDirective());
            RegisterVisitor(typeof(AuthorizeDirectiveVisitor));
        }
    }
}
