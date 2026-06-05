using System.ComponentModel.DataAnnotations;

namespace AkhabarnaAPI.DTOs
{
    public class RegisterUserRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; } = "user";
    }
}
