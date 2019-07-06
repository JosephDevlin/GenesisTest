namespace GenesisTest.Core.Models
{
    public class GithubRepository
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AuthorAvatarUrl { get; set; }
        public string AuthorUsername { get; set; }
        public string AuthorFullname { get; set; }
        public int ForksCount { get; set; }
        public int StarsCount { get; set; }

        public GithubRepository(string name, string description, string authorAvatarUrl, string authorUsername, string authorFullname, int forksCount, int starsCount)
        {
            Name = name;
            Description = description;
            AuthorAvatarUrl = authorAvatarUrl;
            AuthorUsername = authorUsername;
            AuthorFullname = authorFullname;
            ForksCount = forksCount;
            StarsCount = starsCount;
        }
    }
}
