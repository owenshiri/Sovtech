using ChuckApplicationService;
using ChuckSwapiCAssessment.Domain.Model;
using GraphQL.DataLoader;
using SwapiApplicationService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchApplicationService
{
    public class SearchService: ISearchService
    {
        private readonly IChuckNorrisService chuckNorrisService;
        private readonly ISwapiService swapiService;
        public SearchService(IChuckNorrisService chuckNorrisService, ISwapiService swapiService)
        {
            this.chuckNorrisService = chuckNorrisService;
            this.swapiService = swapiService;
        }
        public async Task<SearchResult> Search(string term)
        {
            return new SearchResult { 
                Jokes = await chuckNorrisService.GetFilteredJokes(term), 
                People = await swapiService.GetFilteredPeople(term) 
            };
        }
    }
}
