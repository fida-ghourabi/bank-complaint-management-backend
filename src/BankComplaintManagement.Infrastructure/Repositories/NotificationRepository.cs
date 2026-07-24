using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using BankComplaintManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly ApplicationDbContext _context;



        public NotificationRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<Notification?> GetByIdAsync(Guid id)
        {

            return await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id);

        }





        public async Task<IReadOnlyList<Notification>> GetByUserIdAsync(
            Guid userId)
        {

            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

        }





        public async Task<IReadOnlyList<Notification>> GetUnreadByUserIdAsync(
            Guid userId)
        {

            return await _context.Notifications
                .Where(n =>
                    n.UserId == userId &&
                    !n.Read)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

        }





        public async Task<int> CountUnreadByUserIdAsync(
            Guid userId)
        {

            return await _context.Notifications
                .CountAsync(n =>
                    n.UserId == userId &&
                    !n.Read);

        }





        public async Task AddAsync(
            Notification notification)
        {

            await _context.Notifications.AddAsync(notification);

        }





        public void Update(
            Notification notification)
        {

            _context.Notifications.Update(notification);

        }





        public async Task MarkAllAsReadAsync(
            Guid userId)
        {

            var notifications =
                await _context.Notifications
                .Where(n =>
                    n.UserId == userId &&
                    !n.Read)
                .ToListAsync();



            foreach (var notification in notifications)
            {
                notification.Read = true;
            }

        }





        public void Delete(
            Notification notification)
        {

            _context.Notifications.Remove(notification);

        }

    }
}
