using BankComplaintManagement.Application.DTOs.BankCards;
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

    public class BankCardService : IBankCardService
    {


        private readonly IBankCardRepository _bankCardRepository;



        public BankCardService(
            IBankCardRepository bankCardRepository)
        {

            _bankCardRepository = bankCardRepository;

        }







        public async Task<IReadOnlyList<BankCardInfoDto>>
            GetByClientAsync(
                Guid clientId)
        {


            var cards =
                await _bankCardRepository
                .GetByClientIdAsync(clientId);



            return cards
                .Select(c => c.ToInfoDto())
                .ToList();


        }


    }

}
