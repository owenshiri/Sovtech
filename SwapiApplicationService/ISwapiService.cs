using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SwapiApplicationService
{
    public interface ISwapiService
    {
        Task<PeopleQueryResult> GetAllPeople(int page);
        Task<List<People>> GetAllPeoples();
        Task<PeopleQueryResult> GetAllFilteredPeople(string term);
        Task<List<People>> GetFilteredPeople(string term);
    }

}
