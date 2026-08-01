using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
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
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ApplicationDbContext _context;




        public ComplaintRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }



        // ==============================
        // Recherche
        // ==============================


        public async Task<Complaint?> GetByIdAsync(
            Guid id)
        {
            return await _context.Complaints
                 .Include(c => c.Client)
                 .Include(c => c.AssignedAgent)
                 .FirstOrDefaultAsync(c => c.Id == id);
        }



        public async Task<Complaint?> GetByIdWithDetailsAsync(Guid id)
        {

            return await _context.Complaints

                .Include(c => c.Client)

                .Include(c => c.AssignedAgent)

                .Include(c => c.Messages)
                    .ThenInclude(m => m.Attachments)

                .Include(c => c.Attachments)

                .FirstOrDefaultAsync(
                    c => c.Id == id);

        }



        public async Task<Complaint?> GetByReferenceAsync(
            string referenceNumber)
        {
            return await _context.Complaints
                .FirstOrDefaultAsync(
                    c => c.ReferenceNumber == referenceNumber);
        }



        // ==============================
        // Pagination
        // ==============================


        public async Task<IReadOnlyList<Complaint>> GetPagedAsync(
            int pageNumber,
            int pageSize)
        {
            return await _context.Complaints
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }




        // ==============================
        // Relations
        // ==============================


        public async Task<IReadOnlyList<Complaint>> GetByClientAsync(
            Guid clientId)
        {
            return await _context.Complaints
                .AsNoTracking()
                .Where(c => c.ClientId == clientId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }




        public async Task<IReadOnlyList<Complaint>> GetByAgentAsync(
            Guid agentId)
        {
            return await _context.Complaints
                .AsNoTracking()
                .Where(c => c.AssignedAgentId == agentId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }



        // ==============================
        // Filtrage
        // ==============================


        public async Task<IReadOnlyList<Complaint>> GetByStatusAsync(
            ComplaintStatus status)
        {
            return await _context.Complaints
                .AsNoTracking()
                .Where(c => c.Status == status)
                .ToListAsync();
        }




        public async Task<IReadOnlyList<Complaint>> GetByPriorityAsync(
            ComplaintPriority priority)
        {
            return await _context.Complaints
                .AsNoTracking()
                .Where(c => c.Priority == priority)
                .ToListAsync();
        }




        public async Task<IReadOnlyList<Complaint>> GetByCategoryAsync(
            ComplaintCategory category)
        {
            return await _context.Complaints
                .AsNoTracking()
                .Where(c => c.Category == category)
                .ToListAsync();
        }




        // ==============================
        // Affectation
        // ==============================


        public async Task<IReadOnlyList<Complaint>> GetUnassignedAsync()
        {
            return await _context.Complaints
                .AsNoTracking()
                .Where(c => c.AssignedAgentId == null)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }




        // ==============================
        // SLA
        // ==============================


        public async Task<IReadOnlyList<Complaint>> GetOverdueAsync()
        {
            return await _context.Complaints
                .AsNoTracking()
                .Where(c =>
                    c.SlaDueDate < DateTime.UtcNow
                    &&
                    c.Status != ComplaintStatus.Closed
                    &&
                    c.Status != ComplaintStatus.Rejected)
                .OrderBy(c => c.SlaDueDate)
                .ToListAsync();
        }





        // ==============================
        // Tri
        // ==============================


        public async Task<IReadOnlyList<Complaint>> GetLatestAsync()
        {
            return await _context.Complaints
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }




        public async Task<IReadOnlyList<Complaint>> GetOldestAsync()
        {
            return await _context.Complaints
                .AsNoTracking()
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }





        public async Task<IReadOnlyList<Complaint>> GetByPriorityOrderAsync()
        {
            return await _context.Complaints
                .AsNoTracking()
                .OrderByDescending(c => c.Priority)
                .ThenByDescending(c => c.CreatedAt)
                .ToListAsync();
        }





        public async Task<IReadOnlyList<Complaint>> GetBySlaDeadlineAsync()
        {
            return await _context.Complaints
                .AsNoTracking()
                .Where(c =>
                    c.Status != ComplaintStatus.Closed &&
                    c.Status != ComplaintStatus.Rejected)
                .OrderBy(c => c.SlaDueDate)
                .ToListAsync();
        }




        // ==============================
        // Dashboard
        // ==============================


        public async Task<int> CountAsync()
        {
            return await _context.Complaints
                .CountAsync();
        }





        public async Task<int> CountByStatusAsync(
            ComplaintStatus status)
        {
            return await _context.Complaints
                .CountAsync(c => c.Status == status);
        }





        public async Task<int> CountOverdueAsync()
        {
            return await _context.Complaints
                .CountAsync(c =>
                    c.SlaDueDate < DateTime.UtcNow
                    &&
                    c.Status != ComplaintStatus.Closed
                    &&
                    c.Status != ComplaintStatus.Rejected);
        }





        public async Task<int> CountUnassignedAsync()
        {
            return await _context.Complaints
                .CountAsync(c => c.AssignedAgentId == null);
        }





        public async Task<int> CountHighPriorityAsync()
        {
            return await _context.Complaints
                .CountAsync(c =>
                    c.Priority == ComplaintPriority.Urgent
                    ||
                    c.Priority == ComplaintPriority.Critical);
        }




        public async Task<IReadOnlyList<(ComplaintCategory Category, int Count)>> CountComplaintsByCategoryAsync()
        {
            return await _context.Complaints
                .GroupBy(c => c.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Select(x =>
                    new ValueTuple<ComplaintCategory, int>(
                        x.Category,
                        x.Count))
                .ToListAsync();
        }




        public async Task<IReadOnlyList<Complaint>> GetHighPriorityAsync()
        {
            return await _context.Complaints
                .AsNoTracking()
                .Where(c =>
                    c.Priority == ComplaintPriority.Urgent
                    ||
                    c.Priority == ComplaintPriority.Critical)
                .OrderByDescending(c => c.Priority)
                .ThenBy(c => c.SlaDueDate)
                .ToListAsync();
        }





        // ==============================
        // Commandes
        // ==============================


        public async Task AddAsync(
            Complaint complaint)
        {
            await _context.Complaints.AddAsync(complaint);
        }




        public void Update(
            Complaint complaint)
        {
            _context.Complaints.Update(complaint);
        }

    }
}

