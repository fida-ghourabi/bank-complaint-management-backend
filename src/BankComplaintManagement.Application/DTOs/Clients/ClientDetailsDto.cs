using BankComplaintManagement.Application.DTOs.Complaints;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Clients
{
    public class ClientDetailsDto
    {

        public Guid Id { get; set; }


        public string CustomerNumber { get; set; }


        public string CIN { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string Email { get; set; }


        public string PhoneNumber { get; set; }



        public ClientStatus Status { get; set; }



        public List<ComplaintHistoryDto> Complaints { get; set; }

    }
}
