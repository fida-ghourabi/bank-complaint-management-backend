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
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Client?> GetByIdAsync(Guid id)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client?> GetByCustomerNumberAsync(string customerNumber)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c =>
                    c.CustomerNumber == customerNumber);
        }


        public async Task<Client?> GetByEmailAsync(string email)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(
                    c => c.Email == email);
        }


        public async Task<IReadOnlyList<Client>> GetByStatusAsync(ClientStatus status)
        {
            return await _context.Clients
                .AsNoTracking()
                .Where(c => c.Status == status)
                .OrderBy(c => c.LastName)
                .ToListAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Clients
                .AnyAsync(c => c.Email == email);
        }


        public async Task<Client?> GetByCinAsync(string cin)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(
                    c => c.CIN == cin);
        }

        public async Task<bool> ExistsByCinAsync(string cin)
        {
            return await _context.Clients
                .AnyAsync(c => c.CIN == cin);
        }

        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
        }


        public void Update(Client client)
        {
            _context.Clients.Update(client);
        }

        public async Task<IReadOnlyList<Client>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Clients
                .AsNoTracking()
                .OrderBy(c => c.LastName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<IReadOnlyList<Client>> GetAllAsync()
        {

            return await _context.Clients

                .AsNoTracking()

                .OrderBy(c => c.LastName)

                .ToListAsync();

        }

        public async Task<IReadOnlyList<Client>> SearchAsync(string keyword)
        {
            return await _context.Clients
                .AsNoTracking()
                .Where(c =>
                    c.FirstName.Contains(keyword) ||
                    c.LastName.Contains(keyword) ||
                    c.Email.Contains(keyword) ||
                    c.CIN.Contains(keyword) ||
                    c.CustomerNumber.Contains(keyword))
                .OrderBy(c => c.LastName)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Clients
                .CountAsync();
        }


        public async Task<int> CountByStatusAsync(ClientStatus status)
        {
            return await _context.Clients
                .CountAsync(
                    c => c.Status == status);
        }

        public async Task<int> CountTotalClientsAsync()
        {
            return await _context.Clients
                .CountAsync();
        }

        public async Task<int> CountActiveClientsAsync()
        {
            return await _context.Clients
                .CountAsync(c =>
                    c.Status == ClientStatus.Active);
        }

        public async Task<int> CountBlockedClientsAsync()
        {
            return await _context.Clients
                .CountAsync(c =>
                    c.Status == ClientStatus.Blocked);
        }

        public async Task<int> CountByClientAsync(Guid clientId)
        {
            return await _context.Complaints
                .CountAsync(c =>
                    c.ClientId == clientId);
        }

        public async Task<int> CountOpenByClientAsync(Guid clientId)
        {
            return await _context.Complaints
                .CountAsync(c =>
                    c.ClientId == clientId
                    &&
                    c.Status == ComplaintStatus.Open);
        }

    }
}
