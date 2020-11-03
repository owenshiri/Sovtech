using ChuckSwapiCAssessment.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChuckApplicationService
{
    public interface IChuckNorrisService
    {
        Task<List<string>> GetAllCategories();
        Task<JokeQueryResult> GetAllFilteredJokes(string query);
        Task<List<Joke>> GetFilteredJokes(string query);
    }
}
