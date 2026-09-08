using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Complaints
{
   
        public class ComplaintDto
        {

            // =========================
            // Identification
            // =========================

            public Guid Id { get; set; }


            public string ReferenceNumber { get; set; } = null!;



            // =========================
            // Classification
            // =========================

            public ComplaintCategory Category { get; set; }


            public string SubCategory { get; set; } = null!;




            // =========================
            // Description
            // =========================

            public string Subject { get; set; } = null!;



            public string Description { get; set; } = null!;



            // =========================
            // Workflow
            // =========================


            public ComplaintPriority Priority { get; set; }



            public ComplaintStatus Status { get; set; }



            public DateTime SlaDueDate { get; set; }



            // =========================
            // Client
            // =========================

            public Guid ClientId { get; set; }


            public string ClientName { get; set; } = null!;






            // =========================
            // Agent
            // =========================

            public Guid? AssignedAgentId { get; set; }



            public string? AssignedAgentName { get; set; }



            // =========================
            // Dates
            // =========================


            public DateTime CreatedAt { get; set; }



            public DateTime? UpdatedAt { get; set; }



           

        }
    }
