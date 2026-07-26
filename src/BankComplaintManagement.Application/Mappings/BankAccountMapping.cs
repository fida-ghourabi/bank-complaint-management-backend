using BankComplaintManagement.Application.DTOs.BankAccounts;
using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Mappings
{
    public static class BankAccountMapping
    {

        public static BankAccountInfoDto ToInfoDto(
            this BankAccount account)
        {

            return new BankAccountInfoDto
            {

                Id = account.Id,


                AccountNumber =
                    account.AccountNumber,


                Type =
                    account.Type,


                CreatedAt =
                    account.CreatedAt

            };

        }

    }
}
