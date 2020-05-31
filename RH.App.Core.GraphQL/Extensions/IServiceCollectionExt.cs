using System.Linq;

using HotChocolate;

using Microsoft.Extensions.DependencyInjection;

using RH.App.Common.Extensions;

namespace RH.App.Core.GraphQL.Extensions
{
    public static class IServiceCollectionExt
    {
        public static IServiceCollection RegisterGraphQL(this IServiceCollection services)
        {
            services
                .AddGraphQL(SchemaBuilder.New()
                    .AddQueryType<GraphQLQuery>()
                    .AddMutationType<GraphQLMutation>()
                    .AddTypes(typeof(IGraphQLType).Assembly.GetTypes().Where(w => w.IsImplementedFrom(typeof(IGraphQLType))).ToArray())
                    .AddAuthorizeDirectiveType()
                    .Create()
                );

            services.AddDataLoaderRegistry();

            return services;
        }
    }
}
