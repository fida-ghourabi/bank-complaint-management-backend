using BankComplaintManagement.Application.DTOs.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Messages
{
    public class CreateMessageRequest
    {

        public string Text { get; set; } = null!;



        // true = Agent
        // false = Client
        public bool IsAgent { get; set; }



        public List<CreateAttachmentRequest> Attachments { get; set; }
            = new();

    }
}
