using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Models;

namespace AkhabarnaAPI.Services
{
    public interface IArticleService
    {
        Task<List<Article>> GetNews();

        Task<Article> GetNewsById(int id);

        Task AddNews(Article news);

        Task UpdateNews(Article news);

        Task DeleteNews(int id);
        Task<List<Article>> Search(string keyword);
        Task<List<Article>> Filter(FilterRequest dto);
        Task CreateArticle(CreateArticleRequest dto);
    }
}
