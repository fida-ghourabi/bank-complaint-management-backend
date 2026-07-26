using BankComplaintManagement.Application.DTOs.Messages;
using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Mappings
{
    public static class MessageMapping
    {


        public static MessageDto ToDto(
            this Message message)
        {

            return new MessageDto
            {

                Id = message.Id,


                Content = message.Text,


                IsAgent = message.IsAgent,


                CreatedAt = message.CreatedAt,



                Attachments =
                    message.Attachments
                    .Select(a => a.ToDto())
                    .ToList()

            };

        }
    }
}