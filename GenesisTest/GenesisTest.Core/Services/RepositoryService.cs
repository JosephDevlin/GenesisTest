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

        public async Task<List<GithubRepository>> GetRepositories(int pageNumber, string searchString)
        {
            var repositories = new List<GithubRepository>();

            try
            {
                var response = await _githubRepositories.GetRepositories(searchString, pageNumber);

                foreach (var item in response.items)
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
                // TODO do some caching of data
                // TODO do some fetching/caching of the images using FF loader

                return repositories;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PagedResult<PullRequest>> GetPullRequests(int pageNumber, GithubRepository repository)
        {
            var pullRequests = new List<PullRequest>();

            try
            {
                var response = await _githubRepositories.GetPullRequests(repository.AuthorUsername, repository.Name, pageNumber);

                if (!response.IsSuccessStatusCode)
                {
                    //throw exception
                }

                var tempTotalCount = 2000;
                var linkHeader = new LinkHeader(response.Headers.GetValues("Link").First()); //use the Last header to calculate the total amount of PRs

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
