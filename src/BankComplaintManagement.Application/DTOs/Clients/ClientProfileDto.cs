using BankComplaintManagement.Application.DTOs.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Clients
{
    public class ClientProfileDto
    {

        public Guid Id { get; set; }


        public string CustomerNumber { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string Email { get; set; }


        public string PhoneNumber { get; set; }



        public List<BankAccountInfoDto> Accounts { get; set; }

    }
}
