using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SwapiApplicationService
{
    public class SwapiService: ISwapiService
    {
        private readonly HttpClient httpClient;
        public SwapiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        
        public async Task<PeopleQueryResult> GetAllPeople(int page)
        {
            var response = await httpClient.GetAsync($"people/?{page}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var people = JsonSerializer.Deserialize<PeopleQueryResult>(responseContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return people;
        }
        public async Task<List<People>> GetAllPeoples()
        {
            var listOfPeople = new List<People>();
            int page = 1;
            bool hasNext = true;
            while (hasNext)
            {
                var pageItems = await GetAllPeople(page);
                if (!string.IsNullOrEmpty(pageItems.Next))
                {
                    page++;
                    hasNext = true;
                    listOfPeople.AddRange(pageItems.Results);
                }
                else
                {
                    hasNext = false;
                }

            }
            return listOfPeople;
        }
        public async Task<PeopleQueryResult> GetAllFilteredPeople(string term)
        {
            var response = await httpClient.GetAsync($"people/?search={term}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var people = JsonSerializer.Deserialize<PeopleQueryResult>(responseContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return people;
        }
        public async Task<List<People>> GetFilteredPeople(string term)
        {
            var jokes = await GetAllFilteredPeople(term);
            return jokes.Results;
        }
    }
    public class PeopleRepository : IPeopleRepository
    {
        private readonly ISwapiService swapiService;
        public PeopleRepository(ISwapiService swapiService)
        {
            this.swapiService = swapiService;
        }

        public async Task<List<People>> GetPeoples(int? first, DateTime? createdAfter, CancellationToken cancellationToken)
        {
            var people = await swapiService.GetAllPeople((int)first);
            return people.Results;
        }
        public async Task<List<People>> GetPeoplesReverse(int? first, DateTime? createdAfter, CancellationToken cancellationToken)
        {
            var people = await swapiService.GetAllPeople((int)first);
            return people.Results;
        }
        public async Task<bool> GetHasNextPage( int? first, DateTime? createdAfter, CancellationToken cancellationToken)
        {
            var people =await swapiService.GetAllPeople((int)first);
            return people.Next!=null;
        }

        public async Task<bool> GetHasPreviousPage(int? last, DateTime? createdBefore, CancellationToken cancellationToken)
        {
            var people = await swapiService.GetAllPeople((int)last);
            return people.Previous != null;
        }

        public async Task<int> GetTotalCount(CancellationToken cancellationToken)
        {
            var people = await swapiService.GetAllPeople(1);
            return people.Count;
        }
        public async Task<List<People>> GetFilteredPeoples(List<People> data)
        {
            return data;
        }
    }

}
