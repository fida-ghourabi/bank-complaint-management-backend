using BankComplaintManagement.Application.DTOs.Auth;
using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Mappings
{
    public static class AuthMapping
    {


        public static LoginResponse ToLoginResponse(
            this User user,
            string accessToken,
            string refreshToken,
            DateTime expiration)
        {

            return new LoginResponse
            {

                Token = accessToken,


                RefreshToken = refreshToken,


                Expiration = expiration,


                UserId = user.Id,


                Email = user.Email,


                Role = user.Role,


                FullName =
                    $"{user.FirstName} {user.LastName}"

            };

        }

    }
}
