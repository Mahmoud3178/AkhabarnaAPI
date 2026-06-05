using AkhabarnaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AkhabarnaAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService service;

        public NotificationController(NotificationService service)
        {
            this.service = service;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = GetUserId();
            return Ok(await service.Get(userId));
        }

        [HttpPost("read/{id}")]
        public async Task<IActionResult> Read(Guid id)
        {
            await service.Read(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await service.Delete(id);
            return Ok();
        }

        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll()
        {
            var userId = GetUserId();
            await service.DeleteAll(userId);
            return Ok();
        }
    }
}
