namespace AkhabarnaAPI.DTOs
{
    public class UpdateProfileRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IFormFile? Image { get; set; }
    }
}
