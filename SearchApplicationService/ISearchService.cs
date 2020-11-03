using ChuckSwapiCAssessment.Domain.Model;
using System.Threading.Tasks;

namespace SearchApplicationService
{
    public interface ISearchService
    {
        Task<SearchResult> Search(string term);
    }
}
