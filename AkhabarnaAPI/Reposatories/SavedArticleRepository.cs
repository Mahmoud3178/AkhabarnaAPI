using AkhabarnaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AkhabarnaAPI.Reposatories
{
    public class SavedArticleRepository : ISavedArticleRepository
    {
        private readonly AppDbContext context;

        public SavedArticleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Save(SavedArticle saved)
        {
            await context.SavedArticles.AddAsync(saved);
            await context.SaveChangesAsync();
        }

        public async Task Remove(Guid userId, int articleId)
        {
            var item = await context.SavedArticles
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ArticleId == articleId);

            if (item != null)
            {
                context.SavedArticles.Remove(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Article>> GetUserSaved(Guid userId)
        {
            return await context.SavedArticles
                .Where(x => x.UserId == userId)
                .Include(x => x.Article)
                .ThenInclude(a => a.Category)
                .Select(x => x.Article)
                .ToListAsync();
        }

        public async Task<bool> Exists(Guid userId, int articleId)
        {
            return await context.SavedArticles
                .AnyAsync(x => x.UserId == userId && x.ArticleId == articleId);
        }
    }
}
