namespace AkhabarnaAPI.DTOs
{
    public class FilterRequest
    {
        public List<Guid>? CategoryIds { get; set; }   
        public List<Guid>? SourceIds { get; set; }
        public string? Period { get; set; } // today | week | month
    }
}
