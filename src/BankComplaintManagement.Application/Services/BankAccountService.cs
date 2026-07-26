using BankComplaintManagement.Application.DTOs.BankAccounts;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Mappings;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Services
{
    public class BankAccountService : IBankAccountService

    {

        private readonly IBankAccountRepository _bankAccountRepository;



        public BankAccountService(
            IBankAccountRepository bankAccountRepository)
        {

            _bankAccountRepository = bankAccountRepository;

        }




        // ==================================
        // Récupérer comptes d'un client
        // ==================================

        public async Task<IReadOnlyList<BankAccountInfoDto>> GetByClientAsync(
            Guid clientId)
        {


            var accounts =
                await _bankAccountRepository
                .GetByClientIdAsync(clientId);



            return accounts
                .Select(a => a.ToInfoDto())
                .ToList();

        }







        

    }
}
