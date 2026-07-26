using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.BankAccounts
{
    public class BankAccountInfoDto
    {

        public Guid Id { get; set; }


        public string AccountNumber { get; set; } = null!;


        public AccountType Type { get; set; }

        public decimal Balance { get; set; }



        public DateTime CreatedAt { get; set; }

    }
}
