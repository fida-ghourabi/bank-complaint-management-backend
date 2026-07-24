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
    public class AttachmentRepository : IAttachmentRepository
    {

        private readonly ApplicationDbContext _context;



        public AttachmentRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<Attachment?> GetByIdAsync(
            Guid id)
        {

            return await _context.Attachments
                .FirstOrDefaultAsync(a => a.Id == id);

        }




        public async Task<IReadOnlyList<Attachment>>
            GetByComplaintIdAsync(Guid complaintId)
        {

            return await _context.Attachments
                .Where(a => a.ComplaintId == complaintId)
                .ToListAsync();

        }





        public async Task<IReadOnlyList<Attachment>>
            GetByMessageIdAsync(Guid messageId)
        {

            return await _context.Attachments
                .Where(a => a.MessageId == messageId)
                .ToListAsync();

        }






        public async Task AddAsync(
            Attachment attachment)
        {

            await _context.Attachments.AddAsync(
                attachment);

        }







        public void Delete(
            Attachment attachment)
        {

            _context.Attachments.Remove(
                attachment);

        }






        public async Task<bool> ExistsAsync(
            Guid id)
        {

            return await _context.Attachments
                .AnyAsync(a => a.Id == id);

        }







        public async Task<int> CountByComplaintAsync(
            Guid complaintId)
        {

            return await _context.Attachments
                .CountAsync(a =>
                    a.ComplaintId == complaintId);

        }







        public async Task<int> CountByMessageAsync(
            Guid messageId)
        {

            return await _context.Attachments
                .CountAsync(a =>
                    a.MessageId == messageId);

        }


    }
}
