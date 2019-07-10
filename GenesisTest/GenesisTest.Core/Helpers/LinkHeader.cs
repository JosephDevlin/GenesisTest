using System.Linq;
using System.Text.RegularExpressions;

namespace GenesisTest.Core.Helpers
{
    public class LinkHeader
    {
        public string FirstLink { get; set; }
        public string PrevLink { get; set; }
        public string NextLink { get; set; }
        public string LastLink { get; set; }

        public LinkHeader (string linkHeaderStr)
        {
            if (!string.IsNullOrWhiteSpace(linkHeaderStr))
            {
                string[] linkStrings = linkHeaderStr.Split(',');

                if (linkStrings != null && linkStrings.Any())
                {
                    foreach (string linkString in linkStrings)
                    {
                        var relMatch = Regex.Match(linkString, "(?<=rel=\").+?(?=\")", RegexOptions.IgnoreCase);
                        var linkMatch = Regex.Match(linkString, "(?<=<).+?(?=>)", RegexOptions.IgnoreCase);

                        if (relMatch.Success && linkMatch.Success)
                        {
                            string rel = relMatch.Value.ToUpper();
                            string link = linkMatch.Value;

                            switch (rel)
                            {
                                case "FIRST":
                                    FirstLink = link;
                                    break;
                                case "PREV":
                                    PrevLink = link;
                                    break;
                                case "NEXT":
                                    NextLink = link;
                                    break;
                                case "LAST":
                                    LastLink = link;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
