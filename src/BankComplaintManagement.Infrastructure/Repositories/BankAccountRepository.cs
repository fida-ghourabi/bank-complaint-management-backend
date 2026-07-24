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
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly ApplicationDbContext _context;



        public BankAccountRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }





        public async Task<BankAccount?> GetByIdAsync(
            Guid id)
        {

            return await _context.BankAccounts

                .Include(b => b.BankCards)

                .FirstOrDefaultAsync(
                    b => b.Id == id);

        }







        public async Task<IReadOnlyList<BankAccount>> GetByClientIdAsync(
            Guid clientId)
        {

            return await _context.BankAccounts

                .Where(b => b.ClientId == clientId)

                .Include(b => b.BankCards)

                .ToListAsync();

        }







        public async Task<BankAccount?> GetByAccountNumberAsync(
            string accountNumber)
        {

            return await _context.BankAccounts

                .FirstOrDefaultAsync(
                    b => b.AccountNumber == accountNumber);

        }







        public async Task<BankAccount?> GetByIbanAsync(
            string iban)
        {

            return await _context.BankAccounts

                .FirstOrDefaultAsync(
                    b => b.IBAN == iban);

        }







        public async Task<bool> ExistsByAccountNumberAsync(
            string accountNumber)
        {

            return await _context.BankAccounts

                .AnyAsync(
                    b => b.AccountNumber == accountNumber);

        }







        public async Task<bool> ExistsByIbanAsync(
            string iban)
        {

            return await _context.BankAccounts

                .AnyAsync(
                    b => b.IBAN == iban);

        }







        public async Task AddAsync(
            BankAccount bankAccount)
        {

            await _context.BankAccounts
                .AddAsync(bankAccount);

        }







        public void Update(
            BankAccount bankAccount)
        {

            _context.BankAccounts
                .Update(bankAccount);

        }







        public void Delete(
            BankAccount bankAccount)
        {

            _context.BankAccounts
                .Remove(bankAccount);

        }







        public async Task<int> CountByClientAsync(
            Guid clientId)
        {

            return await _context.BankAccounts

                .CountAsync(
                    b => b.ClientId == clientId);

        }
    }
}
