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
    public class RefreshTokenRepository : IRefreshTokenRepository
    {


        private readonly ApplicationDbContext _context;



        public RefreshTokenRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }





        public async Task<RefreshToken?> GetByTokenAsync(
            string token)
        {

            return await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r =>
                    r.Token == token);

        }








        public async Task AddAsync(
            RefreshToken refreshToken)
        {

            await _context.RefreshTokens
                .AddAsync(refreshToken);

        }








        public void Update(
            RefreshToken refreshToken)
        {

            _context.RefreshTokens
                .Update(refreshToken);

        }


    }
}
