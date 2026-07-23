using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Entities
{
    public class Admin : User
    {


        private Admin()
        {

        }


        public Admin(
        string firstName,
        string lastName,
        string email,
        string passwordHash
        )
        : base(
        firstName,
        lastName,
        email,
        passwordHash,
        UserRole.Admin)
        {

        }


    }
}
