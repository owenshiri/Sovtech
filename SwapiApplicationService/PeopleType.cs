using GraphQL.Types;
using System.Collections.Generic;

namespace SwapiApplicationService
{
    public class PeopleType : ObjectGraphType<People>
    {
        public PeopleType()
        {
            Field(x => x.Name).Description("Name of the person.");
            Field(x => x.Height).Description("Height of the person.");
            Field(x => x.Mass).Description("Mass of the person.");
            Field(x => x.HairColor).Description("Color of the hair.");
            Field(x => x.SkinColor).Description("Color of the skin.");
            Field(x => x.EyeColor).Description("Color of the eyes.");
            Field(x => x.BirthYear).Description("Year the person was born.");
            Field(x => x.Gender).Description("Gender of the person.");
            Field(x => x.Homeworld).Description("Homeworld.");
            Field(x => x.Films);
            Field(x => x.Species);
            Field(x => x.Vehicles);
            Field(x => x.Starships);
            Field(x => x.Created).Description("Date record created.");
            Field(x => x.Edited).Description("Date record edited.");
            Field(x => x.Url).Description("Url for the person.");

            // Field<string>(
            //   "categories",
            //   arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "eventId" }),
            //   resolve: context => repository.GetParticipantInfoByEventId(context.Source.EventId)
            //);
        }
    }
    public class PeopleQueryResultType : ObjectGraphType<PeopleQueryResult>
    {
        public PeopleQueryResultType(IPeopleRepository repository)
        {
            Field(x => x.Count).Description("Total records.");
            Field(x => x.Next).Description("Next record.");
            Field(x => x.Previous).Description("Previous record.");
            Field<ListGraphType<PeopleType>>("result",
                resolve: context=>
                {
                    return repository.GetFilteredPeoples(context.Source.Results);
                });
        }
    }

}
