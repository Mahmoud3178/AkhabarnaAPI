using AkhabarnaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AkhabarnaAPI.Reposatories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext context;

        public NotificationRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Notification>> GetByUserId(Guid userId)
        {
            return await context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task Add(Notification notification)
        {
            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var n = await context.Notifications.FindAsync(id);
            if (n == null) return;

            context.Notifications.Remove(n);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAll(Guid userId)
        {
            var list = context.Notifications.Where(n => n.UserId == userId);
            context.Notifications.RemoveRange(list);
            await context.SaveChangesAsync();
        }

        public async Task MarkAsRead(Guid id)
        {
            var n = await context.Notifications.FindAsync(id);
            if (n == null) return;

            n.IsRead = true;
            await context.SaveChangesAsync();
        }
    }
}
