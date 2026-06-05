using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Models;
using AkhabarnaAPI.Reposatories;
using Microsoft.EntityFrameworkCore;

namespace AkhabarnaAPI.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository repo;
        private readonly ImageService imageService;
        public ArticleService(IArticleRepository repo, ImageService imageService)
        {
            this.repo = repo;
            this.imageService = imageService;
        }

        public async Task<List<Article>> GetNews()
        {
            return await repo.GetAll();
        }

        public async Task<Article> GetNewsById(int id)
        {
            return await repo.GetById(id);
        }

        public async Task AddNews(Article news)
        {
            await repo.Add(news);
        }

        public async Task UpdateNews(Article news)
        {
            await repo.Update(news);
        }

        public async Task DeleteNews(int id)
        {
            await repo.Delete(id);
        }

        public async Task<List<Article>> Search(string keyword)
        {
            return await repo.Search(keyword);
        }

        public async Task<List<Article>> Filter(FilterRequest dto)
        {
            return await repo.GetFiltered(dto);
        }

        public async Task CreateArticle(CreateArticleRequest dto)
        {
            var article = new Article
            {
                Title = dto.Title,
                Content = dto.Content,
                CategoryId = dto.CategoryId,
                SourceId = dto.SourceId,
                PublishedDate = DateTime.UtcNow
            };

            if (dto.Image != null)
            {
                article.ImageUrl = await imageService.UploadImageAsync(dto.Image);
            }

            await repo.Add(article);
        }
    }
}
