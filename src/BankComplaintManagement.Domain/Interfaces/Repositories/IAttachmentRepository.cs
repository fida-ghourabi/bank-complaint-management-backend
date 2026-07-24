using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Interfaces.Repositories
{
    public interface IAttachmentRepository
    {
        // Récupérer une pièce jointe par son Id
        Task<Attachment?> GetByIdAsync(Guid id);



        // Récupérer toutes les pièces jointes d'une réclamation
        Task<IReadOnlyList<Attachment>> GetByComplaintIdAsync(
            Guid complaintId);



        // Récupérer toutes les pièces jointes d'un message
        Task<IReadOnlyList<Attachment>> GetByMessageIdAsync(
            Guid messageId);



        // Ajouter une pièce jointe
        Task AddAsync(
            Attachment attachment);



        // Supprimer une pièce jointe
        void Delete(
            Attachment attachment);



        // Vérifier si une pièce jointe existe
        Task<bool> ExistsAsync(
            Guid id);



        // Compter les pièces jointes d'une réclamation
        Task<int> CountByComplaintAsync(
            Guid complaintId);



        // Compter les pièces jointes d'un message
        Task<int> CountByMessageAsync(
            Guid messageId);
    }
}
