namespace AkhabarnaAPI.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? ImageUrl { get; set; }


        public ICollection<Article> Articles { get; set; }
    }
}
