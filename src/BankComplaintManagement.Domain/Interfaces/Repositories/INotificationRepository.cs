using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Interfaces.Repositories
{
    public interface INotificationRepository
    {

        // Récupérer une notification par Id
        Task<Notification?> GetByIdAsync(Guid id);



        // Récupérer les notifications d'un utilisateur
        Task<IReadOnlyList<Notification>> GetByUserIdAsync(
            Guid userId);



        // Récupérer les notifications non lues
        Task<IReadOnlyList<Notification>> GetUnreadByUserIdAsync(
            Guid userId);



        // Compter les notifications non lues
        Task<int> CountUnreadByUserIdAsync(
            Guid userId);



        // Ajouter une notification
        Task AddAsync(Notification notification);



        // Modifier une notification
        void Update(Notification notification);



        // Marquer toutes les notifications d'un utilisateur comme lues
        Task MarkAllAsReadAsync(
            Guid userId);



        // Supprimer une notification
        void Delete(Notification notification);

    }
}
