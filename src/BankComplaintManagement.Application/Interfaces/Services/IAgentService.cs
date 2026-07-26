using BankComplaintManagement.Application.DTOs.Agents;
using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Interfaces.Services
{
    public interface IAgentService
    {


        Task<IReadOnlyList<AgentDto>> GetAllAsync();



        Task<AgentDetailsDto?> GetByIdAsync(
            Guid agentId);



        Task<AgentProfileDto?> GetProfileAsync(
            Guid agentId);




        Task<CreatedAgentDto> CreateAsync(
            CreateAgentRequest request);




        Task UpdateProfileAsync(
            Guid agentId,
            UpdateAgentProfileRequest request);




        Task ChangePasswordAsync(
            Guid agentId,
            ChangePasswordRequest request);




        Task UpdateStatusAsync(
            Guid agentId,
            UpdateAgentStatusRequest request);




        Task<IReadOnlyList<AgentDto>> GetByStatusAsync(
            AgentStatus status);




        Task<AgentStatisticsDto> GetStatisticsAsync();

    }
}
