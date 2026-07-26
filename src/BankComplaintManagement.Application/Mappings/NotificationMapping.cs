using BankComplaintManagement.Application.DTOs.Notifications;
using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Mappings
{
    public static class NotificationMapping
    {


        public static NotificationDto ToDto(
            this Notification notification)
        {

            return new NotificationDto
            {

                Id = notification.Id,


                Type = notification.Type,


                Title = notification.Title,


                Message = notification.Message,


                Read = notification.Read,


                Status = notification.Status,


                ComplaintId = notification.ComplaintId,


                CreatedAt = notification.CreatedAt

            };

        }


    }
}
