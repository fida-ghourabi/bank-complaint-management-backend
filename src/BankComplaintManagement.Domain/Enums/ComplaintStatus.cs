using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Enums
{
    public enum ComplaintStatus
    {
        Open = 1,
        InProgress = 2,
        Resolved = 3,
        Closed = 4,
        Rejected = 5
    }
}
