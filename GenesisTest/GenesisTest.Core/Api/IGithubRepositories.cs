using GenesisTest.Core.Api.PullRequestModels;
using GenesisTest.Core.Api.RepositoryModels;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenesisTest.Core.Api
{
    [Headers("User-Agent: JosephDevlin")]
    public interface IGithubRepositories
    {
        [Get("/search/repositories?q={searchString}+in:name+language:JavaScript&sort=stars&page={pageNumber}&per_page={perPage}")] // return 37 as its a factor of 999. Makes enforcing the 999 limit simple.
        Task<ApiResponse<RepositoryResult>> GetRepositories(string searchString = "", int pageNumber = 1, int perPage = 37);

        [Get("/repos/{owner}/{repo}/pulls?q=page={pageNumber}&per_page={perPage}")]
        Task<ApiResponse<List<PullRequestDto>>> GetPullRequests(string owner, string repo, int pageNumber = 1, int perPage = 37);

        [Get("/repos/{owner}/{repo}/pulls?state=closed&per_page=1&page=1")]
        Task<ApiResponse<List<PullRequestDto>>> GetFirstClosedPullRequest(string owner, string repo);

        [Get("/repos/{owner}/{repo}/pulls?state=open&per_page=1&page=1")]
        Task<ApiResponse<List<PullRequestDto>>> GetFirstOpenPullRequest(string owner, string repo);
    }
}
