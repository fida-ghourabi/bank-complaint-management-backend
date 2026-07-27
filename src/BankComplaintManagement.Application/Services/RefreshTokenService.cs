using BankComplaintManagement.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Services
{
    public class RefreshTokenService
         : IRefreshTokenService
    {


        public string Generate()
        {

            var randomBytes =
                RandomNumberGenerator.GetBytes(64);


            return Convert.ToBase64String(
                randomBytes);

        }


    }
}
