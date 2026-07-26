using BankComplaintManagement.Application.DTOs.Attachments;
using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Mappings
{
    public static class AttachmentMapping
    {


        public static AttachmentDto ToDto(
            this Attachment attachment)
        {

            return new AttachmentDto
            {

                Id = attachment.Id,


                FileName = attachment.FileName,


                FilePath = attachment.FilePath,


                ContentType = attachment.ContentType,


                FileSize = attachment.FileSize,


                CreatedAt = attachment.CreatedAt

            };

        }

    }
}
