namespace AkhabarnaAPI.Models
{
    public class Source
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public List<Article> News { get; set; }
    }
}
