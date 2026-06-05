using AkhabarnaAPI.Models;
using AkhabarnaAPI.Reposatories;

namespace AkhabarnaAPI.Services
{
    public class NotificationService
    {
        private readonly INotificationRepository repo;

        public NotificationService(INotificationRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<Notification>> Get(Guid userId)
        {
            return await repo.GetByUserId(userId);
        }

        public async Task Add(Guid userId, string title, string body)
        {
            var n = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = title,
                Body = body
            };

            await repo.Add(n);
        }

        public async Task Read(Guid id)
        {
            await repo.MarkAsRead(id);
        }

        public async Task Delete(Guid id)
        {
            await repo.Delete(id);
        }

        public async Task DeleteAll(Guid userId)
        {
            await repo.DeleteAll(userId);
        }
    }
}
