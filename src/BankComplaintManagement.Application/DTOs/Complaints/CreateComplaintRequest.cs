using BankComplaintManagement.Application.DTOs.Attachments;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Complaints
{
    public class CreateComplaintRequest
    {


        public ComplaintCategory Category { get; set; }


        public string SubCategory { get; set; } = null!;



        // Compte obligatoire

        public Guid RelatedBankAccountId { get; set; }



        // Carte optionnelle

        public Guid? RelatedBankCardId { get; set; }




        public string Subject { get; set; } = null!;


        public string Description { get; set; } = null!;




        public DateTime IncidentDate { get; set; }


        public TimeSpan IncidentTime { get; set; }



        public string Location { get; set; } = null!;




        public ComplaintChannel Channel { get; set; }



        public string? BranchName { get; set; }



        public decimal? FinancialImpact { get; set; }




        public ComplaintPriority Priority { get; set; }




        public List<CreateAttachmentRequest> Attachments { get; set; }
            = new();

    }
}
