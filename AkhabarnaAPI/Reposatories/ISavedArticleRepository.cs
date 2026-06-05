using AkhabarnaAPI.Models;

namespace AkhabarnaAPI.Reposatories
{
    public interface ISavedArticleRepository
    {
        Task Save(SavedArticle saved);
        Task Remove(Guid userId, int articleId);
        Task<List<Article>> GetUserSaved(Guid userId);
        Task<bool> Exists(Guid userId, int articleId);
    }
}
