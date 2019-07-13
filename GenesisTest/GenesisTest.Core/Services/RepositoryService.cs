using GenesisTest.Core.Api;
using GenesisTest.Core.Helpers;
using GenesisTest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                var firstRepository = _githubRepositories.GetRepositories(searchString, 1, 1); // I think summing open and closed will give me the total but i need to read the docs to confirm
                var response = await _githubRepositories.GetRepositories(searchString, pageNumber);
                var totalRepositories = GetPageCountFromResponse(await firstRepository);

                if (!response.IsSuccessStatusCode)
                {
                    //throw exception
                }

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
                    TotalCount = totalRepositories
                };

                return pagedResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PullRequestPagedResult> GetPullRequests(int pageNumber, GithubRepository repository)
        {
            try
            {
                // get the first item from each repository state type we are interested in so we can get the paging headers.
                var firstOpenPr = _githubRepositories.GetFirstOpenPullRequest(repository.AuthorUsername, repository.Name);
                var firstClosedPr = _githubRepositories.GetFirstClosedPullRequest(repository.AuthorUsername, repository.Name);
                var firstPr = _githubRepositories.GetPullRequests(repository.AuthorUsername, repository.Name, 1, 1); // I think summing open and closed will give me the total but i need to read the docs to confirm

                // get paged pull requests
                var response = await _githubRepositories.GetPullRequests(repository.AuthorUsername, repository.Name, pageNumber);

                // use the 'last' paging header to determine the total count of each type
                var openPrCount = GetPageCountFromResponse(await firstOpenPr);
                var closedPrCount = GetPageCountFromResponse(await firstClosedPr);
                var totalPrCount = GetPageCountFromResponse(await firstPr);

                if (!response.IsSuccessStatusCode)
                {
                    //throw exception
                }

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

                var pagedResult = new PullRequestPagedResult()
                {
                    Results = pullRequests,
                    TotalCount = totalPrCount,
                    TotalOpen = openPrCount,
                    TotalClosed = closedPrCount
                };

                return pagedResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static int GetPageCountFromResponse<T>(Refit.ApiResponse<T> response) where T : class
        {
            var linkHeader = response.Headers.GetValues("Link").First();
            var links = linkHeader.Split(',');
            var lastPageLink = links.FirstOrDefault(x => x.Contains("last"));

            if (lastPageLink != null)
            {
                var match = Regex.Match(lastPageLink, "[^_](?:page=)[0-9]+", RegexOptions.IgnoreCase);
                int.TryParse(match.Captures[0].Value.Substring(6), out int pageCount);

                return pageCount;
            }
            else
            {
                return 0;
            }
        }
    }
}
