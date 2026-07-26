using BankComplaintManagement.Application.DTOs.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Interfaces.Services
{
    public interface IMessageService
    {

        Task<MessageDto> CreateAsync(
            Guid complaintId,
            CreateMessageRequest request);





    }
}
