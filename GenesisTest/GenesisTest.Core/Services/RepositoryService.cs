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

        public async Task<List<GithubRepository>> GetRepositories(int pageNumber, string username, string repositoryName)
        {
            var repositories = new List<GithubRepository>();

            try
            {
                var response = await _githubRepositories.GetRepositories(pageNumber);

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

        public async Task<List<object>> GetPullRequests(string repositoryIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
