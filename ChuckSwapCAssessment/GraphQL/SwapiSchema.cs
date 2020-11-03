using GraphQL.Types;
using GraphQL.Utilities;
using SwapiApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChuckSwapiCAssessment.API.GraphQL
{
    public class SwapiSchema : Schema
    {
        public SwapiSchema(IServiceProvider resolver, SwapiQuery query) : base(resolver)
        {
            Query = query;
        }
    }
}
