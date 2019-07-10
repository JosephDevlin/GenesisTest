namespace GenesisTest.Core.Api.PullRequestModels
{
    public class Base
    {
        public string label { get; set; }
        public string @ref { get; set; }
        public string sha { get; set; }
        public User3 user { get; set; }
        public Repo2 repo { get; set; }
    }
}
