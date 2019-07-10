using System.Collections.Generic;

namespace GenesisTest.Core.Helpers
{
    public class PagedResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public string Next { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
