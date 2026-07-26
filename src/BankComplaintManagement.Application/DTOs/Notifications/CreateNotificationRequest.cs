using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Notifications
{
    public class CreateNotificationRequest
    {


        public NotificationType Type { get; set; }


        public string Title { get; set; } = null!;


        public string Message { get; set; } = null!;


        public Guid ComplaintId { get; set; }


        public Guid UserId { get; set; }


        public ComplaintStatus? Status { get; set; }


    }
}
