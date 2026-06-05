using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AkhabarnaAPI.Reposatories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext context;

        public ArticleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Article>> GetAll()
        {
            return await context.Articles
       .Include(a => a.Category)
       .Include(a => a.Source)
       .ToListAsync();
        }

        public async Task<Article> GetById(int id)
        {
            return await context.Articles.FindAsync(id);
        }

        public async Task Add(Article news)
        {
            context.Articles.Add(news);
            await context.SaveChangesAsync();
        }

        public async Task Update(Article news)
        {
            context.Articles.Update(news);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var news = await context.Articles.FindAsync(id);

            if (news != null)
            {
                context.Articles.Remove(news);
                await context.SaveChangesAsync();
            }
        }
        public async Task<List<Article>> Search(string keyword)
        {
            return await context.Articles
                .Include(a => a.Category)
                .Include(a => a.Source)
                .Where(a => a.Title.Contains(keyword) || a.Content.Contains(keyword))
                .ToListAsync();
        }

        public async Task<List<Article>> GetFiltered(FilterRequest dto)
        {
            var query = context.Articles.AsQueryable();

            // Filter by Category
            if (dto.CategoryIds != null && dto.CategoryIds.Any())
            {
                query = query.Where(a => dto.CategoryIds.Contains(a.CategoryId));
            }

            // Filter by Source
            if (dto.SourceIds != null && dto.SourceIds.Any())
            {
                query = query.Where(a => dto.SourceIds.Contains(a.SourceId));
            }

            // Filter by Time
            if (!string.IsNullOrEmpty(dto.Period))
            {
                var now = DateTime.UtcNow;

                if (dto.Period == "today")
                    query = query.Where(a => a.PublishedDate.Date == now.Date);

                else if (dto.Period == "week")
                    query = query.Where(a => a.PublishedDate >= now.AddDays(-7));

                else if (dto.Period == "month")
                    query = query.Where(a => a.PublishedDate >= now.AddMonths(-1));
            }

            return await query
                .OrderByDescending(a => a.PublishedDate)
                .ToListAsync();
        }

    }
}
