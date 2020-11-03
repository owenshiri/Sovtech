using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IPeopleRepository
{
    Task<List<People>> GetPeoples(
        int? first,
        DateTime? createdAfter,
        CancellationToken cancellationToken);

    Task<List<People>> GetPeoplesReverse(
        int? first,
        DateTime? createdAfter,
        CancellationToken cancellationToken);

    Task<bool> GetHasNextPage(
        int? first,
        DateTime? createdAfter,
        CancellationToken cancellationToken);
    Task<List<People>> GetFilteredPeoples(List<People> data);

    Task<bool> GetHasPreviousPage(
        int? last,
        DateTime? createdBefore,
        CancellationToken cancellationToken);

    Task<int> GetTotalCount(CancellationToken cancellationToken);
}
