namespace AkhabarnaAPI.DTOs
{
    public class CreateArticleRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SourceId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
