namespace GenesisTest.Core.Api.PullRequestModels
{
    public class Head
    {
        public string label { get; set; }
        public string @ref { get; set; }
        public string sha { get; set; }
        public User2 user { get; set; }
        public Repo repo { get; set; }
    }
}
