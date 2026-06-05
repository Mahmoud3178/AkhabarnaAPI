namespace AkhabarnaAPI.DTOs
{
    public class SetupRequest
    {
        public string Country { get; set; }
        public string Language { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public List<Guid> SourceIds { get; set; }
    }
}
