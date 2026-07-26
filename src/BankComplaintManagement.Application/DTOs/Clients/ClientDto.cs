using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Clients
{
    public class ClientDto
    {
        public Guid Id { get; set; }


        public string FirstName { get; set; } = null!;


        public string LastName { get; set; } = null!;


        public string Email { get; set; } = null!;


        public string CustomerNumber { get; set; } = null!;


        public string CIN { get; set; } = null!;


        public string PhoneNumber { get; set; } = null!;


        public string Status { get; set; } = null!;
    }

}
