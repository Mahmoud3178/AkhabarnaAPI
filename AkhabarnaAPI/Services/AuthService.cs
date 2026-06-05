using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Helper;
using AkhabarnaAPI.Models;
using AkhabarnaAPI.Reposatories;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace AkhabarnaAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepo;
        private readonly JwtService jwtService;
        private readonly IPreferenceRepository preferenceRepo;
        private readonly AppDbContext context;
        private readonly ImageService imageService;
        public AuthService(
            IUserRepository userRepo,
            JwtService jwtService,
            IPreferenceRepository preferenceRepo,
            AppDbContext context,
            ImageService imageService)
        {
            this.userRepo = userRepo;
            this.jwtService = jwtService;
            this.preferenceRepo = preferenceRepo;
            this.context = context;
            this.imageService = imageService;
        }

        public async Task<User> Register(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.Email = user.Email.Trim();
            user.Name = user.Name.Trim();
            user.Password = user.Password.Trim();

            var existing = await userRepo.GetByEmail(user.Email);
            if (existing != null)
                throw new Exception("Email already exists");

            // عملنا hash مع trim لل password
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await userRepo.Add(user);

            return user;
        }

        public async Task<string> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email is required");

            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            var user = await userRepo.GetByEmail(email.Trim());

            if (user == null)
                throw new Exception("Invalid email or user does not exist");

            // Trim + حماية من null لأي شيء
            string dbPassword = user.Password?.Trim() ?? "";
            string inputPassword = password?.Trim() ?? "";

            if (string.IsNullOrEmpty(dbPassword))
                throw new Exception("User password is not set in DB");

            bool valid = BCrypt.Net.BCrypt.Verify(inputPassword, dbPassword);

            if (!valid)
                throw new Exception("Invalid password");

            return jwtService.GenerateToken(user);
        }

        public async Task<User> GetCurrentUser(Guid userId)
        {
            return await userRepo.GetById(userId);
        }
        public async Task SetupUserPreferences(Guid userId, SetupRequest dto)
        {
           
            var exists = await context.UserPreferences
                .AnyAsync(x => x.UserId == userId);

            if (exists)
                throw new Exception("Setup already completed");


            var preference = new UserPreference
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Country = dto.Country,
                Language = dto.Language
            };

            await preferenceRepo.AddPreference(preference);

            var userCategories = dto.CategoryIds.Select(c => new UserCategory
            {
                UserId = userId,
                CategoryId = c
            }).ToList();

            await preferenceRepo.AddUserCategories(userCategories);

            var userSources = dto.SourceIds.Select(s => new UserSource
            {
                UserId = userId,
                SourceId = s
            }).ToList();

            await preferenceRepo.AddUserSources(userSources);

            await context.SaveChangesAsync();
        }
        public async Task<User> GetByEmail(string email)
        {
            return await userRepo.GetByEmail(email.Trim());
        }

        public async Task SendOtp(string email)
        {
            var user = await userRepo.GetByEmail(email);

            if (user == null)
                throw new Exception("User not found");

            var otp = new Random().Next(100000, 999999).ToString();

            user.ResetOtp = otp;
            user.OtpExpiry = DateTime.UtcNow.AddMinutes(10);

            await context.SaveChangesAsync();
        }

        public async Task ChangePassword(Guid userId, ChangePasswordRequest dto)
        {
            var user = await userRepo.GetById(userId);

            if (user == null)
                throw new Exception("User not found");

            // تحقق من الباسورد الحالي
            bool valid = BCrypt.Net.BCrypt.Verify(dto.CurrentPassword.Trim(), user.Password);

            if (!valid)
                throw new Exception("Current password is incorrect");

            // تحقق من الباسورد الجديد
            if (string.IsNullOrWhiteSpace(dto.NewPassword) || dto.NewPassword.Length < 6)
                throw new Exception("New password must be at least 6 characters");

            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword.Trim());

            await context.SaveChangesAsync();
        }
        public async Task UpdateProfile(Guid userId, UpdateProfileRequest dto)
        {
            var user = await userRepo.GetById(userId);

            if (user == null)
                throw new Exception("User not found");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                user.Name = dto.Name.Trim();

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var existing = await userRepo.GetByEmail(dto.Email.Trim());

                if (existing != null && existing.Id != userId)
                    throw new Exception("Email already in use");

                user.Email = dto.Email.Trim();
            }

            // 🔥 Cloudinary Upload
            if (dto.Image != null)
            {
                var imageUrl = await imageService.UploadImageAsync(dto.Image);
                user.ProfileImageUrl = imageUrl;
            }

            await context.SaveChangesAsync();
        }

    }
}