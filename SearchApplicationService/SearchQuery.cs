using ChuckApplicationService;
using ChuckSwapiCAssessment.Domain.Model;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using SwapiApplicationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchApplicationService
{
    public class SearchQuery: ObjectGraphType<JokeType>
    {
        public SearchQuery(IChuckNorrisService chuckNorrisService, ISwapiService swapiService
            ,IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Name = "search";
            Field<ListGraphType<JokeType>>(
                "jokes",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "value" }),
                resolve: context => {
                    var query = context.GetArgument<string>("value");
                    return chuckNorrisService.GetFilteredJokes(query);
                }
            );

            Field<ListGraphType<PeopleType>>(
                "people",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "value" }),
                resolve: context => {
                    var query = context.GetArgument<string>("value");
                    return swapiService.GetFilteredPeople(query);
                }
            );
        }
    }
}
