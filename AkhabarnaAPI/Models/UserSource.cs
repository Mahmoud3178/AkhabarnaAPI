namespace AkhabarnaAPI.Models
{
    public class UserSource
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SourceId { get; set; }

        public User User { get; set; }
        public Source Source { get; set; }
    }
}