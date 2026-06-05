using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Models;

namespace AkhabarnaAPI.Services
{
    public interface IAuthService
    {
        Task<User> Register(User user);

        Task<string> Login(string email, string password);

        Task<User> GetCurrentUser(Guid userId);

        Task SetupUserPreferences(Guid userId, SetupRequest dto);
        Task<User> GetByEmail(string email);

        Task ChangePassword(Guid userId, ChangePasswordRequest dto);
        Task UpdateProfile(Guid userId, UpdateProfileRequest dto);
    }
}
