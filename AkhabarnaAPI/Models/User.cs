namespace AkhabarnaAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; } = "User";
        public string? ResetOtp { get; set; }
        public DateTime? OtpExpiry { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public UserPreference Preference { get; set; }
        public ICollection<SavedArticle> SavedArticles { get; set; }
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
