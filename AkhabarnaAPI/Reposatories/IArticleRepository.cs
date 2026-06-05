using AkhabarnaAPI.DTOs;
using AkhabarnaAPI.Models;

namespace AkhabarnaAPI.Reposatories
{
 
    public interface IArticleRepository
    {
        Task<List<Article>> GetAll();

        Task<Article> GetById(int id);

        Task Add(Article news);

        Task Update(Article news);

        Task Delete(int id);

        Task<List<Article>> Search(string keyword);
        Task<List<Article>> GetFiltered(FilterRequest dto);
    }

}
