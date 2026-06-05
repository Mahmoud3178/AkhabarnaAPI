using AkhabarnaAPI.Models;

namespace AkhabarnaAPI.Reposatories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int id);
    }
}
