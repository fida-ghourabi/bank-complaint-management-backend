using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Domain.Interfaces.Repositories
{
    public interface IAgentRepository
    {

        // Recherche
        Task<Agent?> GetByIdAsync(Guid id);

        Task<Agent?> GetByMatriculeAsync(string matricule);


        // Liste
        Task<IReadOnlyList<Agent>> GetAllAsync();

        Task<IReadOnlyList<Agent>> GetActiveAgentsAsync();

        Task<IReadOnlyList<Agent>> GetInactiveAgentsAsync();



        // Service
        Task<IReadOnlyList<Agent>> GetByServiceAsync(
            string service);



        // Pagination
        Task<IReadOnlyList<Agent>> GetPagedAsync(
            int pageNumber,
            int pageSize);



        // Statistiques agents

        Task<int> CountTotalAgentsAsync();

        Task<int> CountActiveAgentsAsync();

        Task<int> CountInactiveAgentsAsync();



        // Statistiques réclamations

        Task<int> CountProcessedComplaintsAsync(
            Guid agentId);


        Task<int> CountInProgressComplaintsAsync(
            Guid agentId);


        Task<int> CountOverdueComplaintsAsync(
            Guid agentId);


        Task<double> GetResolutionRateAsync(
            Guid agentId);



        // Commandes

        Task AddAsync(Agent agent);

        void Update(Agent agent);
    }
}
