using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using BankComplaintManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Infrastructure.Repositories
{
    public class AgentRepository : IAgentRepository
    {

        private readonly ApplicationDbContext _context;


        public AgentRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }



        // =====================================================
        // Recherche
        // =====================================================


        public async Task<Agent?> GetByIdAsync(
            Guid id)
        {
            return await _context.Agents
                .FirstOrDefaultAsync(a => a.Id == id);
        }




        public async Task<Agent?> GetByMatriculeAsync(
            string matricule)
        {
            return await _context.Agents
                .FirstOrDefaultAsync(
                    a => a.Matricule == matricule);
        }




        public async Task<bool> ExistsByEmailAsync(
            string email)
        {
            return await _context.Agents
                .AnyAsync(a => a.Email == email);
        }




        public async Task<bool> ExistsByMatriculeAsync(
            string matricule)
        {
            return await _context.Agents
                .AnyAsync(a => a.Matricule == matricule);
        }



        // =====================================================
        // Récupération des agents
        // =====================================================


        public async Task<IReadOnlyList<Agent>> GetAllAsync()
        {
            return await _context.Agents
                .AsNoTracking()
                .OrderBy(a => a.LastName)
                .ToListAsync();
        }





        public async Task<IReadOnlyList<Agent>> GetActiveAgentsAsync()
        {
            return await _context.Agents
                .AsNoTracking()
                .Where(a =>
                    a.Status == AgentStatus.Active)
                .OrderBy(a => a.LastName)
                .ToListAsync();
        }





        public async Task<IReadOnlyList<Agent>> GetInactiveAgentsAsync()
        {
            return await _context.Agents
                .AsNoTracking()
                .Where(a =>
                    a.Status == AgentStatus.Inactive)
                .OrderBy(a => a.LastName)
                .ToListAsync();
        }





        public async Task<IReadOnlyList<Agent>> GetByServiceAsync(
            string service)
        {
            return await _context.Agents
                .AsNoTracking()
                .Where(a => a.Service == service)
                .ToListAsync();
        }





        // =====================================================
        // Pagination
        // =====================================================


        public async Task<IReadOnlyList<Agent>> GetPagedAsync(
            int pageNumber,
            int pageSize)
        {
            return await _context.Agents
                .AsNoTracking()
                .OrderBy(a => a.LastName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }



        // =====================================================
        // Statistiques globales agents
        // =====================================================


        // Nombre total des agents

        public async Task<int> CountTotalAgentsAsync()
        {
            return await _context.Agents
                .CountAsync();
        }





        // Nombre agents actifs

        public async Task<int> CountActiveAgentsAsync()
        {
            return await _context.Agents
                .CountAsync(a =>
                    a.Status == AgentStatus.Active);
        }





        // Nombre agents désactivés

        public async Task<int> CountInactiveAgentsAsync()
        {
            return await _context.Agents
                .CountAsync(a =>
                    a.Status == AgentStatus.Inactive);
        }





        // =====================================================
        // Statistiques réclamations par agent
        // =====================================================



        // Réclamations traitées
        // Résolues + Fermées

        public async Task<int> CountProcessedComplaintsAsync(
            Guid agentId)
        {

            return await _context.Complaints
                .CountAsync(c =>
                    c.AssignedAgentId == agentId
                    &&
                    (
                        c.Status == ComplaintStatus.Resolved
                        ||
                        c.Status == ComplaintStatus.Closed
                    ));
        }






        // Réclamations actuellement en cours

        public async Task<int> CountInProgressComplaintsAsync(
            Guid agentId)
        {

            return await _context.Complaints
                .CountAsync(c =>
                    c.AssignedAgentId == agentId
                    &&
                    c.Status == ComplaintStatus.InProgress);
        }






        // Réclamations dépassant le SLA

        public async Task<int> CountOverdueComplaintsAsync(
            Guid agentId)
        {

            return await _context.Complaints
                .CountAsync(c =>
                    c.AssignedAgentId == agentId
                    &&
                    c.SlaDueDate < DateTime.UtcNow
                    &&
                    c.Status != ComplaintStatus.Closed
                    &&
                    c.Status != ComplaintStatus.Rejected);
        }







        // Taux de résolution %

        public async Task<double> GetResolutionRateAsync(
            Guid agentId)
        {

            var total =
                await _context.Complaints
                .CountAsync(c =>
                    c.AssignedAgentId == agentId);



            if (total == 0)
            {
                return 0;
            }



            var resolved =
                await CountProcessedComplaintsAsync(agentId);



            return Math.Round(
                ((double)resolved / total) * 100,
                2);
        }






        // =====================================================
        // Commandes
        // =====================================================


        public async Task AddAsync(
            Agent agent)
        {
            await _context.Agents.AddAsync(agent);
        }





        public void Update(
            Agent agent)
        {
            _context.Agents.Update(agent);
        }


    }
}
