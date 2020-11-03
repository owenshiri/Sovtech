using ChuckSwapiCAssessment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChuckApplicationService
{
    public class ChuckNorrisService: IChuckNorrisService
    {
        private readonly HttpClient httpClient;

        public ChuckNorrisService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<string>> GetAllCategories()
        {
            var response = await httpClient.GetAsync($"jokes/categories");
            var responseContent = await response.Content.ReadAsStringAsync();
            var jokes = JsonSerializer.Deserialize<List<string>>(responseContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return jokes;
        }
        public async Task<JokeQueryResult> GetAllFilteredJokes(string query)
        {
            var response = await httpClient.GetAsync($"jokes/search?query="+query);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JokeQueryResult>(responseContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return result;
        }
        public async Task<List<Joke>> GetFilteredJokes(string query)
        {
            var data = await GetAllFilteredJokes(query);
            return data.Result;
        }
    }
}
