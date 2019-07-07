using System;
using System.Collections.Generic;
using System.Text;

namespace GenesisTest.Core.Models
{
    public class PullRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorAvatarUrl { get; set; }
        public string AuthorUsername { get; set; }
        public DateTime PullRequestDate { get; set; }

        public PullRequest(string title, string description, string authorAvatarUrl, string authorUsername, DateTime pullRequestDate)
        {
            Title = title;
            Description = description;
            AuthorAvatarUrl = authorAvatarUrl;
            AuthorUsername = authorUsername;
            PullRequestDate = pullRequestDate;
        }
    }
}
