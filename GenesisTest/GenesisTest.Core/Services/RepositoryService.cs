using GenesisTest.Core.Api;
using GenesisTest.Core.Models;
using System;
using System.Collections.Generic;
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
                var response = await _githubRepositories.GetRepositories(pageNumber, searchString);

                foreach (var item in response.items)
                {
                    repositories.Add(new GithubRepository(
                        item.name,
                        item.description,
                        item.owner.avatar_url,
                        item.owner.login,
                        "TODO get full name",
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

        public async Task<List<PullRequest>> GetPullRequests(string owner, string repository)
        {
            var pullRequests = new List<PullRequest>();

            try
            {
                var response = await _githubRepositories.GetPullRequests(owner, repository);

                foreach (var pullRequest in response)
                {
                    pullRequests.Add(new PullRequest(
                        pullRequest.title,
                        pullRequest.body,
                        pullRequest.user.avatar_url,
                        pullRequest.user.login,
                        pullRequest.created_at,
                        pullRequest.html_url));
                }
                // TODO do some caching of data
                // TODO do some fetching/caching of the images using FF loader

                return pullRequests;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
