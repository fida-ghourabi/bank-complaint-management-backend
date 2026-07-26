using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Enums
{
    public enum NotificationType
    {
        NewComplaint = 1,
        CustomerReply = 2,
        AgentReply = 3,
        //SlaReminder = 4,
        StatusChanged = 5
    }
}
