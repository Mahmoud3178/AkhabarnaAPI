using AkhabarnaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AkhabarnaAPI.Reposatories
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);

        Task<User> GetById(Guid id);

        Task Add(User user);
    }
}
