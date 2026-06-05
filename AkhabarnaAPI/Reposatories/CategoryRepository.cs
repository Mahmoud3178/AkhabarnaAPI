using AkhabarnaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AkhabarnaAPI.Reposatories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Category>> GetAll()
        {
            return await context.Categories.ToListAsync();
        }
        public async Task<Category> GetById(int id)
        {
            return await context.Categories.FindAsync(id);
        }
    }

}
