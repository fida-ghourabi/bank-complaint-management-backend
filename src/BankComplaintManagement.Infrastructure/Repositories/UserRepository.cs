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
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;


        public UserRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<User>> GetAgentsAndAdminsAsync()
        {
            return await _context.Users
                .Where(u =>
                    u.Role == UserRole.Agent ||
                    u.Role == UserRole.Admin)
                .ToListAsync();
        }
    }
}
