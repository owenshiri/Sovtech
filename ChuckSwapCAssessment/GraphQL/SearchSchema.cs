using GraphQL.Types;
using GraphQL.Utilities;
using SearchApplicationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChuckSwapCAssessment.API.GraphQL
{
    public class SearchSchema : Schema
    {
        public SearchSchema(IServiceProvider resolver, SearchQuery query) : base(resolver)
        {
            Query = query;
        }

    }
}
