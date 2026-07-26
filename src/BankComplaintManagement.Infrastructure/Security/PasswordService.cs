using BankComplaintManagement.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Infrastructure.Secutity
{
    public class PasswordService : IPasswordService
    {


        public bool VerifyPassword(
            string password,
            string passwordHash)
        {

            return BCrypt.Net.BCrypt
                .Verify(
                    password,
                    passwordHash);

        }



        public string HashPassword(
            string password)
        {

            return BCrypt.Net.BCrypt
                .HashPassword(password);

        }


    }
}
