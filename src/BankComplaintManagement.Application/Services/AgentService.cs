using BankComplaintManagement.Application.DTOs.Agents;
using BankComplaintManagement.Application.Exceptions;
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
    public class AgentService : IAgentService
    {


        private readonly IAgentRepository _agentRepository;


        private readonly IUnitOfWork _unitOfWork;


        private readonly IPasswordService _passwordService;



        public AgentService(
            IAgentRepository agentRepository,
            IUnitOfWork unitOfWork,
            IPasswordService passwordService)
        {

            _agentRepository = agentRepository;

            _unitOfWork = unitOfWork;

            _passwordService = passwordService;

        }




        // =====================================================
        // Récupérer tous les agents
        // =====================================================

        public async Task<IReadOnlyList<AgentDto>> GetAllAsync()
        {

            var agents =
                await _agentRepository
                .GetAllAsync();



            var result = new List<AgentDto>();


            foreach (var agent in agents)
            {

                var assigned =
                    await _agentRepository
                    .CountProcessedComplaintsAsync(agent.Id)
                    +
                    await _agentRepository
                    .CountInProgressComplaintsAsync(agent.Id);



                var inProgress =
                    await _agentRepository
                    .CountInProgressComplaintsAsync(agent.Id);



                var rate =
                    await _agentRepository
                    .GetResolutionRateAsync(agent.Id);



                result.Add(agent.ToDto(assigned, inProgress, rate));

            }


            return result;

        }






        // =====================================================
        // Récupérer agent par id
        // =====================================================


        public async Task<AgentDetailsDto> GetByIdAsync(
            Guid agentId)
        {

            var agent =
                await _agentRepository
                .GetByIdAsync(agentId);



            if (agent == null)
            {
                throw new NotFoundException(
                    "Agent introuvable.");
            }



            return agent.ToDetailsDto();

        }







        // =====================================================
        // Profil agent
        // =====================================================


        public async Task<AgentProfileDto> GetProfileAsync(
            Guid agentId)
        {


            var agent =
                await _agentRepository
                .GetByIdAsync(agentId);



            if (agent == null)
            {
                throw new NotFoundException(
                    "Agent introuvable.");
            }



            var processed =
                await _agentRepository
                .CountProcessedComplaintsAsync(agentId);



            var inProgress =
                await _agentRepository
                .CountInProgressComplaintsAsync(agentId);



            var overdue =
                await _agentRepository
                .CountOverdueComplaintsAsync(agentId);



            var rate =
                await _agentRepository
                .GetResolutionRateAsync(agentId);




            return agent.ToProfileDto(
                processed,
                inProgress,
                overdue,
                rate);

        }








        // =====================================================
        // Ajouter agent
        // =====================================================


        public async Task<CreatedAgentDto> CreateAsync(
            CreateAgentRequest request)
        {


            if (await _agentRepository
                .ExistsByEmailAsync(request.Email))
            {
                throw new ConflictException(
                    "Cet email existe déjà.");
            }



            // Génération automatique du mot de passe

            var temporaryPassword =
                GenerateTemporaryPassword();



            var passwordHash =
                _passwordService
                .HashPassword(temporaryPassword);





            var agent =
                new Agent(

                    request.FirstName,

                    request.LastName,

                    request.Email,

                    passwordHash,


                    GenerateMatricule(),


                    "Agent traitement des réclamations",


                    "Réclamations",


                    request.PhoneNumber

                );





            await _agentRepository
                .AddAsync(agent);



            await _unitOfWork
                .SaveChangesAsync();





            return agent.ToCreatedDto(temporaryPassword);

        }







        private string GenerateMatricule()
        {

            return
            $"AG-{DateTime.UtcNow.Year}-{Random.Shared.Next(1000, 9999)}";

        }








        // =====================================================
        // Modifier profil
        // =====================================================


        public async Task UpdateProfileAsync(
            Guid agentId,
            UpdateAgentProfileRequest request)
        {


            var agent =
                await _agentRepository
                .GetByIdAsync(agentId);



            if (agent == null)
                throw new NotFoundException(
                    "Agent introuvable.");




            agent.FirstName =
                request.FirstName;


            agent.LastName =
                request.LastName;


            agent.Email =
                request.Email;


            agent.PhoneNumber =
                request.PhoneNumber;





            _agentRepository
                .Update(agent);




            await _unitOfWork
                .SaveChangesAsync();

        }







        // =====================================================
        // Modifier mot de passe
        // =====================================================


        public async Task ChangePasswordAsync(
            Guid agentId,
            ChangePasswordRequest request)
        {


            var agent =
                await _agentRepository
                .GetByIdAsync(agentId);



            if (agent == null)
                throw new NotFoundException(
                    "Agent introuvable.");




            if (!_passwordService
                .VerifyPassword(
                    request.OldPassword,
                    agent.PasswordHash))
            {

                throw new BadRequestException(
                    "Ancien mot de passe incorrect.");

            }





            if (request.NewPassword !=
               request.ConfirmPassword)
            {

                throw new BadRequestException(
                    "La confirmation du mot de passe est incorrecte.");

            }





            agent.PasswordHash =
                _passwordService
                .HashPassword(request.NewPassword);





            _agentRepository
                .Update(agent);



            await _unitOfWork
                .SaveChangesAsync();


        }









        // =====================================================
        // Modifier statut
        // =====================================================


        public async Task UpdateStatusAsync(
            Guid agentId,
            UpdateAgentStatusRequest request)
        {


            var agent =
                await _agentRepository
                .GetByIdAsync(agentId);



            if (agent == null)
                throw new NotFoundException(
                    "Agent introuvable.");



            agent.Status =
                request.Status;



            _agentRepository
                .Update(agent);



            await _unitOfWork
                .SaveChangesAsync();

        }









        // =====================================================
        // Agents par statut
        // =====================================================


        public async Task<IReadOnlyList<AgentDto>> GetByStatusAsync(
            AgentStatus status)
        {

            var agents =
                await _agentRepository
                .GetActiveAgentsAsync();



            if (status == AgentStatus.Inactive)
            {
                agents =
                await _agentRepository
                .GetInactiveAgentsAsync();
            }



            var result = new List<AgentDto>();


            foreach (var agent in agents)
            {

                var assigned =
                    await _agentRepository
                    .CountProcessedComplaintsAsync(agent.Id)
                    +
                    await _agentRepository
                    .CountInProgressComplaintsAsync(agent.Id);



                var inProgress =
                    await _agentRepository
                    .CountInProgressComplaintsAsync(agent.Id);



                var rate =
                    await _agentRepository
                    .GetResolutionRateAsync(agent.Id);



                result.Add(agent.ToDto(assigned, inProgress, rate));

            }
                return result;

        }








        // =====================================================
        // Statistiques
        // =====================================================


        public async Task<AgentStatisticsDto> GetStatisticsAsync()
        {

            return new AgentStatisticsDto
            {

                TotalAgents =
                await _agentRepository
                .CountTotalAgentsAsync(),



                ActiveAgents =
                await _agentRepository
                .CountActiveAgentsAsync(),



                InactiveAgents =
                await _agentRepository
                .CountInactiveAgentsAsync(),




                AverageWorkload =
                await _agentRepository
                .GetAverageWorkloadAsync()

            };

        }


        private string GenerateTemporaryPassword()
        {

            return
                $"Ag@{Random.Shared.Next(100000, 999999)}";

        }

    }
}
