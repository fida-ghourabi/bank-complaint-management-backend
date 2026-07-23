using BankComplaintManagement.Domain.Common;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Entities
{
    public class Complaint : BaseEntity
    {

        // =========================
        // Identification
        // =========================


        public string ReferenceNumber { get; private set; } = null!;



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
        // Incident information
        // =========================


        public DateTime IncidentDate { get; private set; }


        public TimeSpan IncidentTime { get; private set; }


        public string Location { get; set; } = null!;



        public ComplaintChannel Channel { get; set; }


        public string? BranchName { get; set; }



        // =========================
        // Financial impact
        // =========================


        public decimal? FinancialImpact { get; private set; }



        // =========================
        // Workflow
        // =========================


        public ComplaintPriority Priority { get; private set; }


        public ComplaintStatus Status { get; private set; }



        public DateTime SlaDueDate { get; private set; }



        // =========================
        // Transfer
        // =========================


        public ComplaintTransferService? TransferTo { get; private set; }



        public string? RejectionReason { get; private set; }



        // =========================
        // Unread messages
        // =========================


        public bool Unread { get; set; }



        // =========================
        // Client relationship
        // =========================


        public Guid ClientId { get; private set; }


        public Client Client { get; private set; } = null!;



        // =========================
        // Agent relationship
        // =========================


        public Guid? AssignedAgentId { get; private set; }


        public Agent? AssignedAgent { get; private set; }



        // =========================
        // Collections
        // =========================


        public ICollection<Message> Messages { get; private set; } = new List<Message>();


        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();


        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();



        private Complaint()
        {

        }




        public Complaint(
            Guid clientId,
            ComplaintCategory category,
            string subCategory,
            string subject,
            string description,
            DateTime incidentDate,
            TimeSpan incidentTime,
            string location,
            ComplaintChannel channel,
            ComplaintPriority priority)
        {


            ReferenceNumber = GenerateReference();


            ClientId = clientId;


            Category = category;


            SubCategory = subCategory;


            Subject = subject;


            Description = description;


            ChangeIncidentInformation(incidentDate, incidentTime);



            Location = location;


            Channel = channel;


            Priority = priority;


            Status = ComplaintStatus.Open;


            Unread = true;


            SlaDueDate = CalculateSla(priority);


        }





        private string GenerateReference()
        {

            return $"REC-{DateTime.UtcNow.Year}-{Guid.NewGuid()
                .ToString()
                .Substring(0, 6)
                .ToUpper()}";
        }





        private DateTime CalculateSla(ComplaintPriority priority)
        {
            return priority switch
            {
                ComplaintPriority.Critical
                    => DateTime.UtcNow.AddHours(24),


                ComplaintPriority.Urgent
                    => DateTime.UtcNow.AddHours(48),


                ComplaintPriority.Normal
                    => DateTime.UtcNow.AddDays(5),


                _ => DateTime.UtcNow.AddDays(5)
            };
        }


        //Changer le statut avec une règle
        public void ChangeStatus(ComplaintStatus newStatus)
        {
            if (!CanChangeStatus(newStatus))
            {
                throw new InvalidOperationException(
                    $"Impossible de passer de {Status} à {newStatus}");
            }

            Status = newStatus;
        }



        private bool CanChangeStatus(ComplaintStatus newStatus)
        {
            return Status switch
            {
                ComplaintStatus.Open =>
                    newStatus == ComplaintStatus.InProgress
                    || newStatus == ComplaintStatus.Rejected,

                ComplaintStatus.InProgress =>
                    newStatus == ComplaintStatus.Resolved
                    || newStatus == ComplaintStatus.Rejected,

                ComplaintStatus.Resolved =>
                    newStatus == ComplaintStatus.Closed,

                ComplaintStatus.Closed =>
                    false,

                ComplaintStatus.Rejected =>
                    false,

                _ => false
            };
        }

        //Une réclamation rejetée doit avoir une raison.
        public void Reject(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentException(
                    "La raison du rejet est obligatoire.");
            }


            ChangeStatus(ComplaintStatus.Rejected);


            RejectionReason = reason;
        }


        public void ChangePriority(ComplaintPriority newPriority)
        {
            if (Status == ComplaintStatus.Closed ||
               Status == ComplaintStatus.Rejected)
            {
                throw new InvalidOperationException(
                    "Impossible de modifier la priorité d'une réclamation terminée.");
            }


            Priority = newPriority;


            SlaDueDate = CalculateSla(newPriority);
        }

        //L'impact financier ne peut pas être négatif
        public void SetFinancialImpact(decimal? amount)
        {
            if (amount.HasValue && amount.Value < 0)
            {
                throw new ArgumentException(
                    "L'impact financier ne peut pas être négatif.");
            }

            FinancialImpact = amount;

        }

        //Impossible de transférer une réclamation fermée ou rejetée
        public void TransferToService(ComplaintTransferService service)
        {
            if (Status == ComplaintStatus.Closed ||
                Status == ComplaintStatus.Rejected)
            {
                throw new InvalidOperationException(
                    "Impossible de transférer une réclamation terminée.");
            }

            TransferTo = service;
        }


        // Impossible d'ajouter un message après fermeture
        public void AddMessage(Message message)
        {
            if (Status == ComplaintStatus.Closed ||
                Status == ComplaintStatus.Rejected)
            {
                throw new InvalidOperationException(
                    "Impossible d'ajouter un message à une réclamation terminée.");
            }

            Messages.Add(message);

            Unread = true;
        }


        public void ChangeIncidentInformation(
          DateTime incidentDate,
          TimeSpan incidentTime)
        {
            // La date de l'incident ne peut pas être dans le futur
            if (incidentDate.Date > DateTime.UtcNow.Date)
            {
                throw new ArgumentException(
                    "La date de l'incident ne peut pas être dans le futur.");
            }

            // Si l'incident est aujourd'hui,
            // l'heure ne peut pas être dans le futur
            if (incidentDate.Date == DateTime.UtcNow.Date &&
                incidentTime > DateTime.UtcNow.TimeOfDay)
            {
                throw new ArgumentException(
                    "L'heure de l'incident ne peut pas être dans le futur.");
            }

            IncidentDate = incidentDate;
            IncidentTime = incidentTime;
        }


        public void AssignAgent(Agent agent)
        {
            if (Status == ComplaintStatus.Closed ||
                Status == ComplaintStatus.Rejected)
            {
                throw new InvalidOperationException(
                    "Impossible d'assigner un agent à une réclamation terminée.");
            }


            agent.EnsureCanReceiveComplaint();


            AssignedAgent = agent;

            AssignedAgentId = agent.Id;
        }
    }
}
