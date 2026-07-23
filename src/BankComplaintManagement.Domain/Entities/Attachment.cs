using BankComplaintManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Entities
{
    public class Attachment : BaseEntity
    {


        // =========================
        // File information
        // =========================


        public string FileName { get; set; } = null!;



        public string FilePath { get; set; } = null!;



        public string ContentType { get; set; } = null!;



        public long FileSize { get; set; } 




        // =========================
        // Complaint relationship
        // =========================


        public Guid? ComplaintId { get; set; }


        public Complaint? Complaint { get; set; }




        // =========================
        // Message relationship
        // =========================


        public Guid? MessageId { get; set; }


        public Message? Message { get; set; }



        private Attachment()
        {

        }



        public Attachment(
            string fileName,
            string filePath,
            string contentType,
            long fileSize,
            Guid? complaintId = null,
            Guid? messageId = null)
        {

            FileName = fileName;

            FilePath = filePath;

            ContentType = contentType;

            FileSize = fileSize;

            ComplaintId = complaintId;

            MessageId = messageId;

        }

    }
}
