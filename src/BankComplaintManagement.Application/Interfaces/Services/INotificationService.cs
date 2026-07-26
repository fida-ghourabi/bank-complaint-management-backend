using BankComplaintManagement.Application.DTOs.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Interfaces.Services
{
    public interface INotificationService
    {


        // ============================
        // Création automatique
        // ============================


        Task NotifyNewComplaintAsync(
            Guid complaintId);



        Task NotifyCustomerReplyAsync(
            Guid complaintId);



        Task NotifyAgentReplyAsync(
            Guid complaintId);



        Task NotifyStatusChangedAsync(
            Guid complaintId);





        // ============================
        // Consultation
        // ============================


        Task<IReadOnlyList<NotificationDto>> GetByUserAsync(
            Guid userId);



        Task<int> CountUnreadAsync(
            Guid userId);





        // ============================
        // Lecture
        // ============================


        Task MarkAsReadAsync(
            Guid notificationId);



        Task MarkAllAsReadAsync(
            Guid userId);

    }
}
