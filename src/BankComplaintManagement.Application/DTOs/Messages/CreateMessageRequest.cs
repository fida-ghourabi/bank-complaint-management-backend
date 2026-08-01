using BankComplaintManagement.Application.DTOs.Attachments;
using Microsoft.AspNetCore.Http;
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



        public List<IFormFile> Attachments { get; set; }
            = new();

    }
}
