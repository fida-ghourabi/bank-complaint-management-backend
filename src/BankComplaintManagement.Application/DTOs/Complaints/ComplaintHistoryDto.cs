using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Complaints
{
    public class ComplaintHistoryDto
    {

        public Guid Id { get; set; }


        public string ReferenceNumber { get; set; } = null!;


        public string Subject { get; set; } = null!;


        public ComplaintCategory Category { get; set; }


        public string SubCategory { get; set; } = null!;


        public ComplaintStatus Status { get; set; }


        public ComplaintPriority Priority { get; set; }


        public DateTime CreatedAt { get; set; }

    }
}
