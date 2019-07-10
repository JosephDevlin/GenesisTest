using System;
using System.Collections.Generic;

namespace GenesisTest.Core.Api.PullRequestModels
{
    public class PullRequestDto
    {
        public string url { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string html_url { get; set; }
        public string diff_url { get; set; }
        public string patch_url { get; set; }
        public string issue_url { get; set; }
        public int number { get; set; }
        public string state { get; set; }
        public bool locked { get; set; }
        public string title { get; set; }
        public User user { get; set; }
        public string body { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object closed_at { get; set; }
        public object merged_at { get; set; }
        public string merge_commit_sha { get; set; }
        public Assignee assignee { get; set; }
        public List<object> assignees { get; set; }
        public List<object> requested_reviewers { get; set; }
        public List<object> requested_teams { get; set; }
        public List<object> labels { get; set; }
        public object milestone { get; set; }
        public string commits_url { get; set; }
        public string review_comments_url { get; set; }
        public string review_comment_url { get; set; }
        public string comments_url { get; set; }
        public string statuses_url { get; set; }
        public Head head { get; set; }
        public Base @base { get; set; }
        public Links _links { get; set; }
        public string author_association { get; set; }
    }
}
