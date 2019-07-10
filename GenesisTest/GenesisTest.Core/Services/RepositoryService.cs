using GenesisTest.Core.Api;
using GenesisTest.Core.Helpers;
using GenesisTest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenesisTest.Core.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IGithubRepositories _githubRepositories;

        public RepositoryService(IGithubRepositories githubRepositories)
        {
            _githubRepositories = githubRepositories;
        }

        public async Task<PagedResult<GithubRepository>> GetRepositories(int pageNumber, string searchString)
        {
            try
            {
                var response = await _githubRepositories.GetRepositories(searchString, pageNumber);

                if (!response.IsSuccessStatusCode)
                {
                    //throw exception
                }

                var linkHeader = new LinkHeader(response.Headers.GetValues("Link").First()); //use the Last header to calculate the total amount of PRs
                var repositories = new List<GithubRepository>();

                foreach (var item in response.Content.items)
                {
                    repositories.Add(new GithubRepository(
                        item.name,
                        item.description,
                        item.owner.avatar_url,
                        item.owner.login,
                        item.full_name,
                        item.forks_count,
                        item.stargazers_count));
                }

                var pagedResult = new PagedResult<GithubRepository>()
                {
                    Results = repositories,
                    Next = linkHeader.NextLink,
                    TotalCount = response.Content.total_count < 1000 ? response.Content.total_count : 999
                };

                return pagedResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PagedResult<PullRequest>> GetPullRequests(int pageNumber, GithubRepository repository)
        {
            try
            {
                var response = await _githubRepositories.GetPullRequests(repository.AuthorUsername, repository.Name, pageNumber);

                if (!response.IsSuccessStatusCode)
                {
                    //throw exception
                }

                var tempTotalCount = 2000;
                var linkHeader = new LinkHeader(response.Headers.GetValues("Link").First()); //use the Last header to calculate the total amount of PRs
                var pullRequests = new List<PullRequest>();

                foreach (var pullRequest in response.Content)
                {
                    pullRequests.Add(new PullRequest(
                        pullRequest.title,
                        pullRequest.body,
                        pullRequest.user.avatar_url,
                        pullRequest.user.login,
                        pullRequest.created_at,
                        pullRequest.html_url));
                }

                var pagedResult = new PagedResult<PullRequest>()
                {
                    Results = pullRequests,
                    Next = linkHeader.NextLink,
                    TotalCount = tempTotalCount < 1000 ? tempTotalCount : 999
                };

                return pagedResult;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
