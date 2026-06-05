using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Models;
using AkhabarnaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkhabarnaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService service;

        public ArticleController(IArticleService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var news = await service.GetNews();
            return Ok(news);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var news = await service.GetNewsById(id);

            if (news == null)
                return NotFound();

            return Ok(news);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article news)
        {
            await service.AddNews(news);
            return Ok(news);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Article news)
        {
            if (id != news.Id)
                return BadRequest();

            await service.UpdateNews(news);
            return Ok(news);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteNews(id);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { error = "Keyword is required" });

            var result = await service.Search(keyword);

            return Ok(result);
        }
        [HttpPost("filter")]
        public async Task<IActionResult> Filter([FromBody] FilterRequest dto)
        {
            var result = await service.Filter(dto);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("admin")]
        public async Task<IActionResult> CreateAdmin([FromForm] CreateArticleRequest dto)
        {
            await service.CreateArticle(dto);
            return Ok(new { message = "Article created" });
        }
    }
}
