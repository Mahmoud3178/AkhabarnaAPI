namespace AkhabarnaAPI.Models
{
    public class UserPreference
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Country { get; set; }

        public string Language { get; set; }

        public ICollection<UserCategory> UserCategories { get; set; }

        public ICollection<UserSource> UserSources { get; set; }

        public User User { get; set; }
    }
}
