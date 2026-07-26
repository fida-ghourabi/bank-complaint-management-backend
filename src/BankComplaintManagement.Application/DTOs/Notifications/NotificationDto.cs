using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Notifications
{
    public class NotificationDto
    {

        public Guid Id { get; set; }


        public NotificationType Type { get; set; }


        public string Title { get; set; } = null!;


        public string Message { get; set; } = null!;


        public bool Read { get; set; }


        public ComplaintStatus? Status { get; set; }


        public Guid ComplaintId { get; set; }


        public DateTime CreatedAt { get; set; }

    }
}
