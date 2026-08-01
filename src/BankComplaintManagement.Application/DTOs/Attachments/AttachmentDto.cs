using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Attachments
{
    public class AttachmentDto
    {

        public Guid Id { get; set; }


        public string FileName { get; set; } = null!;


        public string DownloadUrl { get; set; } = null!;

        public string ContentType { get; set; } = null!;


        public long FileSize { get; set; }


        public DateTime CreatedAt { get; set; }

    }
}
