using ChuckApplicationService;
using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace ChuckSwapiCAssessment.API.GraphQL
{
    public class JokeSchema : Schema
    {
        public JokeSchema(IServiceProvider resolver,JokeQuery query) : base(resolver)
        {
            Query = query;
        }
    }
}
