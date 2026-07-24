using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        // Récupérer un message par son Id
        Task<Message?> GetByIdAsync(Guid id);



        // Récupérer tous les messages d'une réclamation
        Task<IReadOnlyList<Message>> GetByComplaintIdAsync(
            Guid complaintId);



        // Récupérer les messages récents d'une réclamation
        Task<IReadOnlyList<Message>> GetLatestByComplaintAsync(
            Guid complaintId,
            int count);



        // Compter le nombre de messages d'une réclamation
        Task<int> CountByComplaintAsync(
            Guid complaintId);



        // Compter les messages envoyés par les agents
        Task<int> CountAgentMessagesAsync(
            Guid complaintId);



        // Compter les messages envoyés par les clients
        Task<int> CountClientMessagesAsync(
            Guid complaintId);



        // Ajouter un message
        Task AddAsync(
            Message message);



        // Supprimer un message
        void Delete(
            Message message);



        // Sauvegarder les modifications
        void Update(
            Message message);
    }
}
