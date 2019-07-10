namespace GenesisTest.Core.Api.PullRequestModels
{
    public class Links
    {
        public Self self { get; set; }
        public Html html { get; set; }
        public Issue issue { get; set; }
        public Comments comments { get; set; }
        public ReviewComments review_comments { get; set; }
        public ReviewComment review_comment { get; set; }
        public Commits commits { get; set; }
        public Statuses statuses { get; set; }
    }
}
