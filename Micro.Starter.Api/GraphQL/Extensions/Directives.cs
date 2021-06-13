using GraphQL;
using GraphQL.Builders;
using GraphQL.Types;
using Micro.Starter.Api.GraphQL.Directives;

namespace Micro.Starter.Api.GraphQL.Extensions
{
    public static class Directives
    {
        public static FieldType Authorize(this FieldType type)
        {
            return type.ApplyDirective(AuthorizeDirective.DirectiveName);
        }

        public static FieldBuilder<TSourceType, TReturnType> Authorize<TSourceType, TReturnType>(
            this FieldBuilder<TSourceType, TReturnType> type)
        {
            return type.Directive(AuthorizeDirective.DirectiveName);
        }
    }
}
