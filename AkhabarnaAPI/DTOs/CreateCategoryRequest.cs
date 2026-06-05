namespace AkhabarnaAPI.DTOs
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
