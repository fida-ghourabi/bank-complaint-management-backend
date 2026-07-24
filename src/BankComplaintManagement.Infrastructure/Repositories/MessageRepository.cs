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
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;



        public MessageRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<Message?> GetByIdAsync(Guid id)
        {
            return await _context.Messages
                .Include(m => m.Complaint)
                .FirstOrDefaultAsync(
                    m => m.Id == id);
        }





        public async Task<IReadOnlyList<Message>> GetByComplaintIdAsync(
            Guid complaintId)
        {

            return await _context.Messages
                .Where(m =>
                    m.ComplaintId == complaintId)
                .OrderBy(m =>
                    m.CreatedAt)
                .ToListAsync();

        }





        public async Task<IReadOnlyList<Message>> GetLatestByComplaintAsync(
            Guid complaintId,
            int count)
        {

            return await _context.Messages
                .Where(m =>
                    m.ComplaintId == complaintId)
                .OrderByDescending(m =>
                    m.CreatedAt)
                .Take(count)
                .ToListAsync();

        }





        public async Task<int> CountByComplaintAsync(
            Guid complaintId)
        {

            return await _context.Messages
                .CountAsync(m =>
                    m.ComplaintId == complaintId);

        }





        public async Task<int> CountAgentMessagesAsync(
            Guid complaintId)
        {

            return await _context.Messages
                .CountAsync(m =>
                    m.ComplaintId == complaintId
                    &&
                    m.IsAgent);

        }





        public async Task<int> CountClientMessagesAsync(
            Guid complaintId)
        {

            return await _context.Messages
                .CountAsync(m =>
                    m.ComplaintId == complaintId
                    &&
                    !m.IsAgent);

        }





        public async Task AddAsync(
            Message message)
        {

            await _context.Messages.AddAsync(message);

        }





        public void Delete(
            Message message)
        {

            _context.Messages.Remove(message);

        }





        public void Update(
            Message message)
        {

            _context.Messages.Update(message);

        }
    }
}
