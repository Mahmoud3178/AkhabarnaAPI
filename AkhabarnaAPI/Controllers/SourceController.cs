using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AkhabarnaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SourceController : ControllerBase
    {
        private readonly SourceService _service;

        public SourceController(SourceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sources = await _service.GetAll();
            return Ok(sources);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSourceRequest dto)
        {
            await _service.Add(dto);
            return Ok("Source created");
        }
    }
}
