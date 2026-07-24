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
    public class BankCardRepository : IBankCardRepository
    {

        private readonly ApplicationDbContext _context;



        public BankCardRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }






        public async Task<BankCard?> GetByIdAsync(
            Guid id)
        {

            return await _context.BankCards

                .Include(c => c.BankAccount)

                .FirstOrDefaultAsync(
                    c => c.Id == id);

        }







        public async Task<IReadOnlyList<BankCard>>
            GetByBankAccountIdAsync(Guid bankAccountId)
        {

            return await _context.BankCards

                .Where(c =>
                    c.BankAccountId == bankAccountId)

                .ToListAsync();

        }







        public async Task<BankCard?> GetByCardNumberAsync(
            string cardNumber)
        {

            return await _context.BankCards

                .FirstOrDefaultAsync(
                    c => c.CardNumber == cardNumber);

        }







        public async Task<bool> ExistsByCardNumberAsync(
            string cardNumber)
        {

            return await _context.BankCards

                .AnyAsync(
                    c => c.CardNumber == cardNumber);

        }







        public async Task AddAsync(
            BankCard card)
        {

            await _context.BankCards
                .AddAsync(card);

        }







        public void Update(
            BankCard card)
        {

            _context.BankCards.Update(card);

        }







        public void Delete(
            BankCard card)
        {

            _context.BankCards.Remove(card);

        }







        public async Task<int> CountByBankAccountAsync(
            Guid bankAccountId)
        {

            return await _context.BankCards

                .CountAsync(
                    c => c.BankAccountId == bankAccountId);

        }


    }
}

