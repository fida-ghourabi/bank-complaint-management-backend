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
                    MaskAccountNumber(account.AccountNumber),


                Type =
                    account.Type,


                CreatedAt =
                    account.CreatedAt

            };

        }



        private static string MaskAccountNumber(
            string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                return string.Empty;


            const int visibleDigits = 4;


            if (accountNumber.Length <= visibleDigits)
                return accountNumber;


            return new string('*',
                    accountNumber.Length - visibleDigits)
                    +
                    accountNumber.Substring(
                        accountNumber.Length - visibleDigits);
        }
    }
}
