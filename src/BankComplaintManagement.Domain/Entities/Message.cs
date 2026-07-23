using BankComplaintManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Entities
{
    public class Message : BaseEntity
    {

        public string Text { get; set; } = null!;


        public bool IsAgent { get; set; }



        // Date du message
        // Héritée de BaseEntity :
        // CreatedAt



        public Guid ComplaintId { get; private set; }


        public Complaint Complaint { get; private set; } = null!;



        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();



        private Message()
        {

        }



        public Message(
            string text,
            bool isAgent,
            Guid complaintId)
        {

            Text = text;

            IsAgent = isAgent;

            ComplaintId = complaintId;

             

        }

    }
}