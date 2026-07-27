using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Auth
{
    public class LoginResponse
    {

        public string Token { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;
        public DateTime Expiration { get; set; }


        public Guid UserId { get; set; }


        public string Email { get; set; } = null!;


        public UserRole Role { get; set; }


        public string FullName { get; set; } = null!;

    }
}
