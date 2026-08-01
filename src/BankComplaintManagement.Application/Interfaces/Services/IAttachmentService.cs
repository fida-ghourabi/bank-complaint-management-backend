using BankComplaintManagement.Application.DTOs.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Interfaces.Services
{
    public interface IAttachmentService
    {



        Task<FileDownloadDto>
            DownloadAsync(
                Guid id);

    }
}
