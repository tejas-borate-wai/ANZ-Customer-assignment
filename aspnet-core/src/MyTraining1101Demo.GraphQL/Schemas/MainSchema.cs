using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using MyTraining1101Demo.Queries.Container;
using System;

namespace MyTraining1101Demo.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}