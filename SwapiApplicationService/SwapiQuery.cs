using GraphQL;
using GraphQL.Builders;
using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SwapiApplicationService
{
    public class SwapiQuery : ObjectGraphType<PeopleType>
    {
        private const int MaxPageSize = 10;
        public SwapiQuery(IPeopleRepository peopleRepository)
        {
            this.Name = "Query";
            this.Description = "The query type, represents all of the entry points into our object graph.";

            this.Connection<PeopleType>()
                .Name("peoples")
                .Description("Gets pages of people.")
                // Enable the last and before arguments to do paging in reverse.
                .Bidirectional()
                // Set the maximum size of a page, use .ReturnAll() to set no maximum size.
                .PageSize(MaxPageSize)
                .ResolveAsync(async context => {
                     return await ResolveConnection(peopleRepository, context);
                    });
        }


        private async static Task<object> ResolveConnection(
        IPeopleRepository repository,
        IResolveConnectionContext<PeopleType> context)
    {
        var first = context.First;
        var afterCursor = Cursor.FromCursor<DateTime?>(context.After);
        var last = context.Last;
        var beforeCursor = Cursor.FromCursor<DateTime?>(context.Before);
        var cancellationToken = context.CancellationToken;

        var getPeoplesTask = GetPeoples(repository, first, afterCursor, last, beforeCursor, cancellationToken);
        var getHasNextPageTask = GetHasNextPage(repository, first, afterCursor, cancellationToken);
        var getHasPreviousPageTask = GetHasPreviousPage(repository, last, beforeCursor, cancellationToken);
        var totalCountTask = repository.GetTotalCount(cancellationToken);

        await Task.WhenAll(getPeoplesTask, getHasNextPageTask, getHasPreviousPageTask, totalCountTask);
        var peoples = getPeoplesTask.Result;
        var hasNextPage = getHasNextPageTask.Result;
        var hasPreviousPage = getHasPreviousPageTask.Result;
        var totalCount = totalCountTask.Result;
        var (firstCursor, lastCursor) = Cursor.GetFirstAndLastCursor(peoples, x => x.Created);

        var conn= new Connection<People>()
        {
            Edges = peoples
                .Select(x =>
                    new Edge<People>()
                    {
                        Cursor = Cursor.ToCursor(x.Created),
                        Node = x
                    })
                .ToList(),
            PageInfo = new PageInfo()
            {
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPreviousPage,
                StartCursor = firstCursor,
                EndCursor = lastCursor,
            },
            TotalCount = totalCount
        };
            return conn;
    }

    private static Task<List<People>> GetPeoples(
        IPeopleRepository repository,
        int? first,
        DateTime? afterCursor,
        int? last,
        DateTime? beforeCursor,
        CancellationToken cancellationToken)
    {
        if (first.HasValue)
            return repository.GetPeoples(first, afterCursor, cancellationToken);
        else
            return repository.GetPeoplesReverse(last, beforeCursor, cancellationToken);
    }

    private static async Task<bool> GetHasNextPage(
        IPeopleRepository repository,
        int? first,
        DateTime? afterCursor,
        CancellationToken cancellationToken)
    {
        if (first.HasValue)
            return await repository.GetHasNextPage(first, afterCursor, cancellationToken);
        else
            return false;
    }

    private static async Task<bool> GetHasPreviousPage(
        IPeopleRepository repository,
        int? last,
        DateTime? beforeCursor,
        CancellationToken cancellationToken)
    {
        if (last.HasValue)
            return await repository.GetHasPreviousPage(last, beforeCursor, cancellationToken);
        else
            return false;
    }
    }
}
