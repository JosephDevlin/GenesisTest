using GenesisTest.Core.Models;

namespace GenesisTest.Core.Helpers
{
    public class PullRequestPagedResult : PagedResult<PullRequest>
    {
        public int TotalOpen { get; set; }
        public int TotalClosed { get; set; }
    }
}
