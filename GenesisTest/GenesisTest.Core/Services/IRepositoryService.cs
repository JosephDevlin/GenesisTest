using GenesisTest.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenesisTest.Core.Services
{
    public interface IRepositoryService
    {
        Task<List<GithubRepository>> GetRepositories(int pageNumber, string username, string repositoryName);
        Task<List<PullRequest>> GetPullRequests(string repositoryIdentifier);
    }
}
