using BankComplaintManagement.Domain.Common;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Entities
{
    public class Notification : BaseEntity
    {

        public NotificationType Type { get; set; }


        public string Title { get; set; } = null!;


        public string Message { get; set; } = null!;


        public bool Read { get; set; }



        // Etat de la réclamation au moment de la notification

        public ComplaintStatus? Status { get; set; }



        public Guid ComplaintId { get; private set; }


        public Complaint Complaint { get; private set; } = null!;



        public Guid UserId { get; private set; }


        public User User { get; private set; } = null!;



        private Notification()
        {

        }



        public Notification(
            NotificationType type,
            string title,
            string message,
            Guid complaintId,
            Guid userId,
            ComplaintStatus? status = null)
        {

            Type = type;

            Title = title;

            Message = message;

            Status = status;

            ComplaintId = complaintId;

            UserId = userId;

            Read = false;

        }

    }
}
