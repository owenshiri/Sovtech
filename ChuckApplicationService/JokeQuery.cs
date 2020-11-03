using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace ChuckApplicationService
{
    public class JokeQuery : ObjectGraphType<string>
    {
        public JokeQuery(IChuckNorrisService chuckNorrisService
            , IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Name = "jokes";
            Field<ListGraphType<StringGraphType>>(
                "categories",
                //arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "value" }),
                resolve: context => {
                    //var query = context.GetArgument<string>("value");
                    return chuckNorrisService.GetAllCategories();
                }
            );

        }
    }
}
