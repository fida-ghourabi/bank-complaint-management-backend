using BankComplaintManagement.Application.DTOs.Notifications;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Mappings;
using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using BankComplaintManagement.Domain.Interfaces;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Services
{
    public class NotificationService : INotificationService
    {


        private readonly INotificationRepository _notificationRepository;


        private readonly IComplaintRepository _complaintRepository;


        private readonly IUserRepository _userRepository;


        private readonly IUnitOfWork _unitOfWork;





        public NotificationService(
            INotificationRepository notificationRepository,
            IComplaintRepository complaintRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {

            _notificationRepository = notificationRepository;

            _complaintRepository = complaintRepository;

            _userRepository = userRepository;

            _unitOfWork = unitOfWork;

        }






        // ==========================================
        // Nouvelle réclamation
        // Tous les agents + tous les admins
        // ==========================================

        public async Task NotifyNewComplaintAsync(
            Guid complaintId)
        {


            var users =
                await _userRepository
                .GetAgentsAndAdminsAsync();



            foreach (var user in users)
            {

                var notification =
                    new Notification(
                        NotificationType.NewComplaint,
                        "Nouvelle réclamation",
                        "Une nouvelle réclamation a été créée.",
                        complaintId,
                        user.Id);



                await _notificationRepository
                    .AddAsync(notification);

            }



        }









        // ==========================================
        // Réponse client
        // Notification de l'agent affecté
        // ==========================================


        public async Task NotifyCustomerReplyAsync(
            Guid complaintId)
        {

            var complaint =
                await _complaintRepository
                .GetByIdWithDetailsAsync(complaintId);



            if (complaint == null)
            {
                throw new KeyNotFoundException(
                    "Réclamation introuvable.");
            }




            if (complaint.AssignedAgentId == null)
            {
                return;
            }





            var notification =
                new Notification(
                    NotificationType.CustomerReply,
                    "Réponse client",
                    "Le client a répondu à une réclamation.",
                    complaint.Id,
                    complaint.AssignedAgentId.Value);



            await _notificationRepository
                .AddAsync(notification);




        }









        // ==========================================
        // Réponse agent
        // Notification du client
        // ==========================================


        public async Task NotifyAgentReplyAsync(
            Guid complaintId)
        {


            var complaint =
                await _complaintRepository
                .GetByIdWithDetailsAsync(complaintId);



            if (complaint == null)
            {
                throw new KeyNotFoundException(
                    "Réclamation introuvable.");
            }




            var notification =
                new Notification(
                    NotificationType.AgentReply,
                    "Réponse agent",
                    "Un agent a répondu à votre réclamation.",
                    complaint.Id,
                    complaint.ClientId);



            await _notificationRepository
                .AddAsync(notification);





        }









        // ==========================================
        // Changement statut
        // Notification du client
        // ==========================================


        public async Task NotifyStatusChangedAsync(
            Guid complaintId)
        {

            var complaint =
                await _complaintRepository
                .GetByIdWithDetailsAsync(complaintId);



            if (complaint == null)
            {
                throw new KeyNotFoundException(
                    "Réclamation introuvable.");
            }





            var notification =
                new Notification(
                    NotificationType.StatusChanged,
                    "Modification du statut",
                    $"Votre réclamation est maintenant {complaint.Status}.",
                    complaint.Id,
                    complaint.ClientId,
                    complaint.Status);



            await _notificationRepository
                .AddAsync(notification);





        }









        // ==========================================
        // Récupération utilisateur
        // ==========================================


        public async Task<IReadOnlyList<NotificationDto>> GetByUserAsync(
            Guid userId)
        {

            var notifications =
                await _notificationRepository
                .GetByUserIdAsync(userId);



            return notifications
                .Select(n => n.ToDto())
                .ToList();

        }









        public async Task<int> CountUnreadAsync(
            Guid userId)
        {

            return await _notificationRepository
                .CountUnreadByUserIdAsync(userId);

        }









        public async Task MarkAsReadAsync(
            Guid notificationId)
        {


            var notification =
                await _notificationRepository
                .GetByIdAsync(notificationId);



            if (notification == null)
            {
                throw new KeyNotFoundException(
                    "Notification introuvable.");
            }



            notification.Read = true;



            _notificationRepository
                .Update(notification);



            await _unitOfWork
                .SaveChangesAsync();

        }









        public async Task MarkAllAsReadAsync(
            Guid userId)
        {


            await _notificationRepository
                .MarkAllAsReadAsync(userId);



            await _unitOfWork
                .SaveChangesAsync();

        }


    }
}
