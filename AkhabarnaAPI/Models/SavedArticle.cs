namespace AkhabarnaAPI.Models
{
    public class SavedArticle
    {
        // Composite Key: UserId + ArticleId
        public Guid UserId { get; set; }
        public int ArticleId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Article Article { get; set; }

        // Optional: متى حفظ المقال
        public DateTime SavedDate { get; set; } = DateTime.Now;
    }
}