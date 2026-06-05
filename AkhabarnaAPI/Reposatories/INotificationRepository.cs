using AkhabarnaAPI.Models;

namespace AkhabarnaAPI.Reposatories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetByUserId(Guid userId);
        Task Add(Notification notification);
        Task Delete(Guid id);
        Task DeleteAll(Guid userId);
        Task MarkAsRead(Guid id);
    }
}
