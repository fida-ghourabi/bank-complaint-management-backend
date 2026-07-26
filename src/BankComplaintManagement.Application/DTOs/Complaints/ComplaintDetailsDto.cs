using BankComplaintManagement.Application.DTOs.Attachments;
using BankComplaintManagement.Application.DTOs.BankAccounts;
using BankComplaintManagement.Application.DTOs.BankCards;
using BankComplaintManagement.Application.DTOs.Clients;
using BankComplaintManagement.Application.DTOs.Messages;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Complaints
{
    public class ComplaintDetailsDto
    {


        // ======================
        // Complaint information
        // ======================

        public Guid Id { get; set; }


        public string ReferenceNumber { get; set; } = null!;


        public ComplaintCategory Category { get; set; }


        public string SubCategory { get; set; } = null!;


        public string Subject { get; set; } = null!;


        public string Description { get; set; } = null!;


        public ComplaintStatus Status { get; set; }


        public ComplaintPriority Priority { get; set; }


        public DateTime CreatedAt { get; set; }


        public DateTime SlaDueDate { get; set; }



        // ======================
        // Client
        // ======================


        public ClientInfoDto Client { get; set; } = null!;




        // ======================
        // Account
        // ======================


        public BankAccountInfoDto Account { get; set; } = null!;




        // ======================
        // Card
        // ======================


        public BankCardInfoDto? Card { get; set; }




        // ======================
        // Complaint attachments
        // ======================


        public List<AttachmentDto> Attachments { get; set; }
            = new();




        // ======================
        // Messages
        // ======================


        public List<MessageDto> Messages { get; set; }
            = new();

    }
}
