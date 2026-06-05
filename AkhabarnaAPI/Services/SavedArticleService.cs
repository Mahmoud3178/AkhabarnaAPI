using AkhabarnaAPI.Models;
using AkhabarnaAPI.Reposatories;

namespace AkhabarnaAPI.Services
{
    public class SavedArticleService : ISavedArticleService
    {
        private readonly ISavedArticleRepository repo;

        public SavedArticleService(ISavedArticleRepository repo)
        {
            this.repo = repo;
        }

        public async Task Save(Guid userId, int articleId)
        {
            var exists = await repo.Exists(userId, articleId);

            if (exists)
                throw new Exception("Already saved");

            var saved = new SavedArticle
            {
                UserId = userId,
                ArticleId = articleId
            };

            await repo.Save(saved);
        }

        public async Task Remove(Guid userId, int articleId)
        {
            await repo.Remove(userId, articleId);
        }

        public async Task<List<Article>> GetSaved(Guid userId)
        {
            return await repo.GetUserSaved(userId);
        }
    }
}
