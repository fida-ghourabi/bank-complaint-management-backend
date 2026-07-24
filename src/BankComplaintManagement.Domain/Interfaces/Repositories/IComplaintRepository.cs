using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Interfaces.Repositories
{
    public interface IComplaintRepository
    {

        // ==============================
        // Recherche
        // ==============================


        Task<Complaint?> GetByIdAsync(Guid id);



        Task<Complaint?> GetByReferenceAsync(
            string referenceNumber);



        // ==============================
        // Pagination
        // ==============================


        Task<IReadOnlyList<Complaint>> GetPagedAsync(
            int pageNumber,
            int pageSize);



        // ==============================
        // Relations
        // ==============================


        Task<IReadOnlyList<Complaint>> GetByClientAsync(
            Guid clientId);



        Task<IReadOnlyList<Complaint>> GetByAgentAsync(
            Guid agentId);



        // ==============================
        // Filtrage
        // ==============================


        Task<IReadOnlyList<Complaint>> GetByStatusAsync(
            ComplaintStatus status);



        Task<IReadOnlyList<Complaint>> GetByPriorityAsync(
            ComplaintPriority priority);



        Task<IReadOnlyList<Complaint>> GetByCategoryAsync(
            ComplaintCategory category);



        // ==============================
        // Affectation
        // ==============================


        Task<IReadOnlyList<Complaint>> GetUnassignedAsync();



        // ==============================
        // SLA
        // ==============================


        Task<IReadOnlyList<Complaint>> GetOverdueAsync();



        // ==============================
        // Tri
        // ==============================


        Task<IReadOnlyList<Complaint>> GetLatestAsync();



        Task<IReadOnlyList<Complaint>> GetOldestAsync();



        Task<IReadOnlyList<Complaint>> GetByPriorityOrderAsync();



        Task<IReadOnlyList<Complaint>> GetBySlaDeadlineAsync();



        // ==============================
        // Dashboard - statistiques
        // ==============================


        Task<int> CountAsync();



        Task<int> CountByStatusAsync(
            ComplaintStatus status);



        Task<int> CountOverdueAsync();



        Task<int> CountUnassignedAsync();



        Task<int> CountHighPriorityAsync();


        Task<IReadOnlyList<(ComplaintCategory Category, int Count)>>
            CountComplaintsByCategoryAsync();


        Task<IReadOnlyList<Complaint>> GetHighPriorityAsync();



        // ==============================
        // Commandes
        // ==============================


        Task AddAsync(
            Complaint complaint);



        void Update(
            Complaint complaint);

    }
}
