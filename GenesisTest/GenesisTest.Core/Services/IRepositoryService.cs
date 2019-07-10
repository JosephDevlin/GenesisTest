using GenesisTest.Core.Helpers;
using GenesisTest.Core.Models;
using System.Threading.Tasks;

namespace GenesisTest.Core.Services
{
    public interface IRepositoryService
    {
        Task<PagedResult<GithubRepository>> GetRepositories(int pageNumber, string searchString);
        Task<PagedResult<PullRequest>> GetPullRequests(int pageNumber, GithubRepository repository);
    }
}
