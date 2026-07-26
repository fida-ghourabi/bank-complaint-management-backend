using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Complaints
{
    public class ChangeComplaintPriorityRequest
    {

        public ComplaintPriority Priority { get; set; }

    }
}

