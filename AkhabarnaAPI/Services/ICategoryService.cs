using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Models;

namespace AkhabarnaAPI.Services
{
    public interface ICategoryService
    {
        Task CreateCategory(CreateCategoryRequest dto);
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
    }
}
