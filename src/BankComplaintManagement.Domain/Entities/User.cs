using BankComplaintManagement.Domain.Common;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Entities
{
    public abstract class User : BaseEntity
    {

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;


        public string Email { get; set; } = null!;


        public string PasswordHash { get; set; } = null!;



        public UserRole Role { get; private set; }

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        protected User()
        {
            // utilisé uniquement par Entity Framework Core
        }


        protected User(
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            UserRole role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;

        }

    }
}
