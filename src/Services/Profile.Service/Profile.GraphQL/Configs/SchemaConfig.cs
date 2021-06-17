using HotChocolate;
using HotChocolate.Execution.Configuration;
using HotChocolate.Execution.Options;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.Extensions.DependencyInjection;
using Profile.GraphQL.Inputs;
using Profile.GraphQL.Schema;
using Profile.GraphQL.Types;
using System;

namespace Profile.GraphQL.Configs
{
    public static class SchemaConfig
    {
        private static IRequestExecutorBuilder AddTypes(this IRequestExecutorBuilder builder)
        {
            return builder
                .AddType<PersonProfileType>();
        }

        private static IRequestExecutorBuilder AddInputs(this IRequestExecutorBuilder builder)
        {
            return builder
                .AddType<CreatePersonProfileInput>()
                .AddType<UpdatePersonProfileInput>();
        }

        public static IRequestExecutorBuilder BuildGraphQLSchema(this IServiceCollection services)
        {
            return services
                .AddGraphQLServer()
                .AddQueryType<Queries>()
                .AddMutationType<Mutations>()
            .AddTypes()
            .AddInputs()
            .AddProjections()
                        .AddFiltering()
                        .AddSorting()
                .ModifyRequestOptions(opt =>
                {
                    opt.IncludeExceptionDetails = true;
                    opt.TracingPreference = TracingPreference.Always;
                })
                .ModifyOptions(opt =>
                {
                    opt.SortFieldsByName = true;
                })
                .SetPagingOptions(new PagingOptions
                {
                    MaxPageSize = 100,
                    DefaultPageSize = 10,
                    IncludeTotalCount = true
                })
                .BindRuntimeType<DateTime, DateTimeType>()
                .BindRuntimeType<Guid, UuidType>();
        }
    }
}
