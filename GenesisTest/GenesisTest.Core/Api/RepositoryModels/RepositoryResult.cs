using System.Collections.Generic;

namespace GenesisTest.Core.Api.RepositoryModels
{
    public class RepositoryResult
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public List<Item> items { get; set; }
    }
}
