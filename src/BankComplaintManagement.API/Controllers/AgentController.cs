using BankComplaintManagement.Application.DTOs.Agents;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        private readonly ICurrentUserService _currentUser;

        public AgentController(
            IAgentService agentService, ICurrentUserService currentUser)
        {
            _agentService = agentService;
            _currentUser = currentUser;
        }

        // =====================================================
        // Récupérer tous les agents
        // =====================================================

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<AgentDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<AgentDto>>> GetAll()
        {
            var agents =
                await _agentService.GetAllAsync();

            return Ok(agents);
        }

        // =====================================================
        // Récupérer un agent
        // =====================================================

        [HttpGet("{agentId:guid}")]
        [ProducesResponseType(typeof(AgentDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AgentDetailsDto>> GetById(
            Guid agentId)
        {
            var agent =
                await _agentService.GetByIdAsync(agentId);


            return Ok(agent);
        }

        // =====================================================
        // Créer un agent
        // =====================================================

        [HttpPost]
        [ProducesResponseType(typeof(CreatedAgentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreatedAgentDto>> Create(
            CreateAgentRequest request)
        {
            var createdAgent =
                await _agentService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { agentId = createdAgent.Id },
                createdAgent);
        }

        // =====================================================
        // Modifier le profil d'un agent
        // =====================================================

        [HttpPut("{agentId:guid}/profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProfile(
            Guid agentId,
            UpdateAgentProfileRequest request)
        {
            await _agentService.UpdateProfileAsync(
                agentId,
                request);

            return NoContent();
        }

        // =====================================================
        // Changer le mot de passe
        // =====================================================

        [Authorize(Roles = "Agent")]
        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(
            ChangePasswordRequest request)
        {
            await _agentService.ChangePasswordAsync(
                _currentUser.UserId,
                request);

            return NoContent();
        }

        // =====================================================
        // Modifier le statut
        // =====================================================

        [HttpPatch("{agentId:guid}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(
            Guid agentId,
            UpdateAgentStatusRequest request)
        {
            await _agentService.UpdateStatusAsync(
                agentId,
                request);

            return NoContent();
        }

        // =====================================================
        // Récupérer les agents par statut
        // =====================================================

        [HttpGet("status/{status}")]
        [ProducesResponseType(typeof(IReadOnlyList<AgentDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<AgentDto>>> GetByStatus(
            AgentStatus status)
        {
            var agents =
                await _agentService.GetByStatusAsync(status);

            return Ok(agents);
        }

        // =====================================================
        // Profil de l'agent connecté
        // =====================================================

        [Authorize(Roles = "Agent")]
        [HttpGet("profile")]
        [ProducesResponseType(typeof(AgentProfileDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<AgentProfileDto>> GetProfile()
        {
            var profile =
                await _agentService.GetProfileAsync(_currentUser.UserId);

            return Ok(profile);
        }

        // =====================================================
        // Statistiques
        // =====================================================

        [HttpGet("statistics")]
        [ProducesResponseType(typeof(AgentStatisticsDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<AgentStatisticsDto>> GetStatistics()
        {
            var statistics =
                await _agentService.GetStatisticsAsync();

            return Ok(statistics);
        }



    
    }
}
