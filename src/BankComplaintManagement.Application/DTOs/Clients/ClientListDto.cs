using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Clients
{
    public class ClientListDto
    {

        public Guid Id { get; set; }


        public string CustomerNumber { get; set; } = null!;

        public string CIN { get; set; } = null!;

        public string FirstName { get; set; } = null!;


        public string LastName { get; set; } = null!;


        public string Email { get; set; } = null!;


        public ClientStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }


        public int TotalComplaints { get; set; }


        public int OpenComplaints { get; set; }

    }
}
