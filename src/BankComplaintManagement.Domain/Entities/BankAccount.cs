using BankComplaintManagement.Domain.Common;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Entities
{
   
    public class BankAccount : BaseEntity
    {


        public string AccountNumber { get; set; } = null!;


        public string IBAN { get; set; } = null!;


        public AccountType Type { get; set; }


        public decimal Balance { get; private set; }

        public Guid ClientId { get; private set; }


        public Client Client { get; private set; } = null!;

        public ICollection<BankCard> BankCards { get; set; } = new List<BankCard>();



        private BankAccount()
        {

        }



        public BankAccount(
            string accountNumber,
            string iban,
            AccountType type,
            decimal balance,
            Guid clientId)
        {

            AccountNumber = accountNumber;

            IBAN = iban;

            Type = type;

            ClientId = clientId;

        }

    }
}
