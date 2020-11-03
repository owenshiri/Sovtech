using ChuckSwapiCAssessment.Domain.Model;
using GraphQL.Types;

namespace ChuckApplicationService
{
    public class JokeType : ObjectGraphType<Joke>
    {
        public JokeType()
        {
            Field(x => x.Categories).Description("Categories.");
            Field(x => x.Id).Description("Id of the joke.");
            Field(x => x.CreatedAt).Description("Date joke created.");
            Field(x => x.UpdatedAt).Description("Date joke updated.");
            Field(x => x.Url).Description("Uri for the joke.");
            Field(x => x.Value).Description("Value of the joke.");
            Field(x => x.IconUrl).Description("Url for the updated.");
        }
    }
}
