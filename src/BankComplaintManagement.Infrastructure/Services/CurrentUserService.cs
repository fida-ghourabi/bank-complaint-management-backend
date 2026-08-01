using BankComplaintManagement.Application.Exceptions;
using BankComplaintManagement.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BankComplaintManagement.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;


        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        private ClaimsPrincipal? User =>
            _httpContextAccessor.HttpContext?.User;



        public bool IsAuthenticated =>
            User?.Identity?.IsAuthenticated ?? false;




        public Guid UserId
        {
            get
            {

                var userId =
                    User?
                    .FindFirst(ClaimTypes.NameIdentifier)
                    ?.Value;


                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedException(
                        "Utilisateur non authentifié.");
                }


                return Guid.Parse(userId);

            }
        }




        public string Email =>
            User?
            .FindFirst(ClaimTypes.Email)
            ?.Value
            ?? string.Empty;




        public string Role =>
            User?
            .FindFirst(ClaimTypes.Role)
            ?.Value
            ?? string.Empty;

    }
}

