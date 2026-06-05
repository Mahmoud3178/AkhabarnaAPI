using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Models;
using AkhabarnaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AkhabarnaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly AppDbContext context;
        public AuthController(IAuthService authService, AppDbContext context)
        {
            this.authService = authService;
            this.context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name.Trim(),
                Email = dto.Email.Trim(),
                Password = dto.Password.Trim(),
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                var newUser = await authService.Register(user);
                return Ok(new
                {
                    message = "User registered successfully",
                    userId = newUser.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { error = "Email and Password are required" });

            try
            {
                var token = await authService.Login(dto.Email, dto.Password);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("setup")]
        public async Task<IActionResult> Setup([FromBody] SetupRequest dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Invalid data" });

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized(new { error = "Invalid token" });

            var userId = Guid.Parse(userIdClaim.Value);

            try
            {
                await authService.SetupUserPreferences(userId, dto);

                return Ok(new { message = "Setup completed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest dto)
        {
            var user = await authService.GetByEmail(dto.Email);

            if (user == null)
                return BadRequest(new { error = "User not found" });

            var otp = new Random().Next(100000, 999999).ToString();

            user.ResetOtp = otp;
            user.OtpExpiry = DateTime.UtcNow.AddMinutes(10);

            await context.SaveChangesAsync();

            // هنا تبعت OTP بالإيميل (mock حاليا)
            return Ok(new { message = "OTP sent", otp }); // شيل otp في production
        }
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest dto)
        {
            var user = await authService.GetByEmail(dto.Email);

            if (user == null || user.ResetOtp != dto.Otp)
                return BadRequest(new { error = "Invalid OTP" });

            if (user.OtpExpiry < DateTime.UtcNow)
                return BadRequest(new { error = "OTP expired" });

            return Ok(new { message = "OTP verified" });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest dto)
        {
            var user = await authService.GetByEmail(dto.Email);

            if (user == null)
                return BadRequest(new { error = "User not found" });

            if (user.ResetOtp != dto.Otp || user.OtpExpiry < DateTime.UtcNow)
                return BadRequest(new { error = "Invalid or expired OTP" });

            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.ResetOtp = null;
            user.OtpExpiry = null;

            await context.SaveChangesAsync();

            return Ok(new { message = "Password reset successful" });
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            try
            {
                await authService.ChangePassword(userId, dto);

                return Ok(new { message = "Password changed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileRequest dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            try
            {
                await authService.UpdateProfile(userId, dto);

                return Ok(new { message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

      
    }
}