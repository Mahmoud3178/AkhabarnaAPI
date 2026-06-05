using System.ComponentModel.DataAnnotations;

namespace AkhabarnaAPI.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }
        
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid SourceId { get; set; }
        public Source Source { get; set; }
        public ICollection<SavedArticle> SavedArticles { get; set; }
        public DateTime PublishedDate { get; set; }

    }
}
