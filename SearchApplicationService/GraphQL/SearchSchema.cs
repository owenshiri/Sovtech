using GraphQL.Types;
using GraphQL.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchApplicationService.GraphQL
{
    public class SearchSchema : Schema
    {
        public SearchSchema(IServiceProvider resolver, SearchQuery query) : base(resolver)
        {
            Query = query;
        }

    }
}
