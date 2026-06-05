using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Models;
using AkhabarnaAPI.Reposatories;

namespace AkhabarnaAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repo;
        private readonly ImageService imageService;
        private readonly AppDbContext context;

        public CategoryService(ICategoryRepository repo, ImageService imageService, AppDbContext context)
        {
            this.repo = repo;
            this.imageService = imageService;
            this.context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await repo.GetAll();
        }
        public async Task<Category> GetCategoryById(int id)
        {
            return await repo.GetById(id);
        }

        public async Task CreateCategory(CreateCategoryRequest dto)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            if (dto.Image != null)
            {
                category.ImageUrl = await imageService.UploadImageAsync(dto.Image);
            }

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }
    }

}
