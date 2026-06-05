using AkhabarnaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AkhabarnaAPI.Controllers
{
    [ApiController]
    [Route("api/saved")]
    public class SavedArticlesController : ControllerBase
    {
        private readonly ISavedArticleService service;

        public SavedArticlesController(ISavedArticleService service)
        {
            this.service = service;
        }

        [Authorize]
        [HttpPost("{articleId}")]
        public async Task<IActionResult> Save(int articleId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await service.Save(userId, articleId);

            return Ok(new { message = "Saved successfully" });
        }

        [Authorize]
        [HttpDelete("{articleId}")]
        public async Task<IActionResult> Remove(int articleId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await service.Remove(userId, articleId);

            return Ok(new { message = "Removed successfully" });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetSaved()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var data = await service.GetSaved(userId);

            return Ok(data);
        }
    }
}
