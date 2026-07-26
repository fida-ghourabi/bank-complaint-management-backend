using BankComplaintManagement.Application.DTOs.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Messages
{
    public class MessageDto
    {

        public Guid Id { get; set; }


        public string Content { get; set; } = null!;

        public bool IsAgent { get; set; }

        public DateTime CreatedAt { get; set; }


        public List<AttachmentDto> Attachments { get; set; }
            = new();

    }
}
