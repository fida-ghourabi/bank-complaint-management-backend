using BankComplaintManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {

        public string Token { get; private set; } = null!;


        public DateTime ExpiresAt { get; private set; }


        public bool IsRevoked { get; private set; }



        public Guid UserId { get; private set; }


        public User User { get; private set; } = null!;




        private RefreshToken()
        {

        }




        public RefreshToken(
            string token,
            DateTime expiresAt,
            Guid userId)
        {

            Token = token;

            ExpiresAt = expiresAt;

            UserId = userId;

            IsRevoked = false;

        }





        public void Revoke()
        {

            IsRevoked = true;

        }




        public bool IsExpired()
        {

            return DateTime.UtcNow >= ExpiresAt;

        }

    }
}
