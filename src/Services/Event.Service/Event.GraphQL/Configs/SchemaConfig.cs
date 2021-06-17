using Event.GraphQL.Inputs;
using Event.GraphQL.Schema;
using Event.GraphQL.Types;
using HotChocolate.Execution.Configuration;
using HotChocolate.Execution.Options;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Event.GraphQL.Configs
{
    public static class SchemaConfig
    {
        private static IRequestExecutorBuilder AddTypes(this IRequestExecutorBuilder builder)
        {
            return builder
                .AddType<EventType>();
        }

        private static IRequestExecutorBuilder AddInputs(this IRequestExecutorBuilder builder)
        {
            return builder
                .AddType<CreateEventInput>()
                .AddType<UpdateEventInput>()
                .AddType<AddSpeakerInput>();
        }

        public static IRequestExecutorBuilder BuildGraphQLSchema(this IServiceCollection services)
        {
            return services
                .AddGraphQLServer()
                .AddAuthorization()
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
