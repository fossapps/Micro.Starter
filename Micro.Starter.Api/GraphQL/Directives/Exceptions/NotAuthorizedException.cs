using System;

namespace Micro.Starter.Api.GraphQL.Directives.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base("This operation requires logging in")
        {
        }
    }
}
