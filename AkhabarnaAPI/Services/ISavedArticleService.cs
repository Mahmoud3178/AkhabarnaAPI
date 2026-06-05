using AkhabarnaAPI.Models;

namespace AkhabarnaAPI.Services
{
    public interface ISavedArticleService
    {
        Task Save(Guid userId, int articleId);
        Task Remove(Guid userId, int articleId);
        Task<List<Article>> GetSaved(Guid userId);
    }
}
