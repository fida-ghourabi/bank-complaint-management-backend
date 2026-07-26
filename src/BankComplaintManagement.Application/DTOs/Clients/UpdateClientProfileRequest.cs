using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Clients
{
    public class UpdateClientProfileRequest
    {


        public string FirstName { get; set; } = null!;



        public string LastName { get; set; } = null!;



        public string Email { get; set; } = null!;



        public string PhoneNumber { get; set; } = null!;


    }
}
