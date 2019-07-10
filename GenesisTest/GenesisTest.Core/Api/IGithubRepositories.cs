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
        [Get("/search/repositories?q={searchString}+in:name+language:JavaScript&sort=stars&page={pageNumber}")]
        Task<RepositoryResult> GetRepositories(string searchString = "", int pageNumber = 1);

        [Get("/repos/{owner}/{repo}/pulls?q=page={pageNumber}")]
        Task<ApiResponse<List<PullRequestDto>>> GetPullRequests(string owner, string repo, int pageNumber = 1);
    }
}
