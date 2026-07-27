using BankComplaintManagement.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Interfaces.Services
{
    public interface IAuthService
    {

        Task<LoginResponse> LoginAsync(
            LoginRequest request);


        Task<LoginResponse> RefreshTokenAsync(
            string refreshToken);



        Task LogoutAsync(
            string refreshToken);
    }
}
