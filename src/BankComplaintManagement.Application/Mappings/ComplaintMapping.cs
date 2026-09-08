using BankComplaintManagement.Application.DTOs.Attachments;
using BankComplaintManagement.Application.DTOs.BankAccounts;
using BankComplaintManagement.Application.DTOs.BankCards;
using BankComplaintManagement.Application.DTOs.Clients;
using BankComplaintManagement.Application.DTOs.Complaints;
using BankComplaintManagement.Application.DTOs.Messages;
using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Mappings
{
    public static class ComplaintMapping
    {


        public static ComplaintDto ToDto(
            this Complaint complaint)
        {

            return new ComplaintDto
            {

                Id = complaint.Id,

                ReferenceNumber =
                    complaint.ReferenceNumber,


                Category =
                    complaint.Category,


                SubCategory =
                    complaint.SubCategory,


                Subject =
                    complaint.Subject,


                Description =
                    complaint.Description,


                Priority =
                    complaint.Priority,


                Status =
                    complaint.Status,


                SlaDueDate =
                    complaint.SlaDueDate,


                ClientId =
                    complaint.ClientId,


                ClientName =
                    complaint.Client == null
                    ?
                    ""
                    :
                    $"{complaint.Client.FirstName} {complaint.Client.LastName}",



                AssignedAgentId =
                    complaint.AssignedAgentId,


                AssignedAgentName =
                    complaint.AssignedAgent == null
                    ?
                    null
                    :
                    $"{complaint.AssignedAgent.FirstName} {complaint.AssignedAgent.LastName}",



                CreatedAt =
                    complaint.CreatedAt,


                UpdatedAt =
                    complaint.UpdatedAt,


              

            };

        }






        public static ComplaintDetailsDto ToDetailsDto(
             this Complaint complaint,
             BankAccountInfoDto account,
             BankCardInfoDto? card)
        {

            return new ComplaintDetailsDto
            {

                // ======================
                // Complaint information
                // ======================

                Id = complaint.Id,

                        ReferenceNumber =
                complaint.ReferenceNumber,


                        Category =
                complaint.Category,


                        SubCategory =
                complaint.SubCategory,


                        Subject =
                complaint.Subject,


                        Description =
                complaint.Description,


                        Status =
                complaint.Status,


                        Priority =
                complaint.Priority,


                        IncidentDate =
                complaint.IncidentDate,


                        IncidentTime =
                complaint.IncidentTime,

                Channel =
                complaint.Channel,

                FinancialImpact =
                complaint.FinancialImpact,

                CreatedAt =
                complaint.CreatedAt,


                        SlaDueDate =
                complaint.SlaDueDate,



                        AssignedAgentId =
                complaint.AssignedAgentId,


                        AssignedAgentName =
                complaint.AssignedAgent == null
                ?
                null
                :
                $"{complaint.AssignedAgent.FirstName} {complaint.AssignedAgent.LastName}",



                        TransferTo =
                complaint.TransferTo,


                        RejectionReason =
                complaint.RejectionReason,





                // ======================
                // Client
                // ======================

                Client =
                new ClientInfoDto
                {

                    Id =
                        complaint.Client.Id,


                    CustomerNumber =
                        complaint.Client.CustomerNumber,


                    CIN =
                        complaint.Client.CIN,


                    FirstName =
                        complaint.Client.FirstName,


                    LastName =
                        complaint.Client.LastName,


                    Email =
                        complaint.Client.Email,


                    PhoneNumber =
                        complaint.Client.PhoneNumber

                },




                // ======================
                // Bank Account
                // ======================

                Account = account,




                // ======================
                // Bank Card
                // ======================

                Card = card,




                // ======================
                // Complaint Attachments
                // ======================

                Attachments =
                    complaint.Attachments
                    .Select(a => a.ToDto())
                    .ToList(),





                // ======================
                // Messages
                // ======================

                Messages =
                    complaint.Messages
                    .Select(m => m.ToDto())
                    .ToList()

            };

        }

    }
}
