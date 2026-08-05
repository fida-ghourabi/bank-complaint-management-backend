using BankComplaintManagement.Application.DTOs.Clients;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankComplaintManagement.API.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {

        private readonly ICurrentUserService _currentUser;

        private readonly IClientService _clientService;



        public ClientController(
            IClientService clientService, ICurrentUserService currentUser)
        {
            _clientService = clientService;
            _currentUser = currentUser;
        }







        // =====================================================
        // REGISTER CLIENT
        // Public
        // =====================================================


        [HttpPost("register")]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ClientDto>> Register(
            RegisterClientRequest request)
        {


            var client =
                await _clientService
                .RegisterAsync(request);



            return CreatedAtAction(
                nameof(GetById),
                new { id = client.Id },
                client);

        }









        // =====================================================
        // GET PROFILE
        // Client connecté
        // =====================================================


        [Authorize(Roles = "Client")]
        [HttpGet("profile")]
        [ProducesResponseType(typeof(ClientProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientProfileDto>> GetProfile()
        {


            var clientId =
                _currentUser.UserId;



            var profile =
                await _clientService
                .GetProfileAsync(clientId);




            return Ok(profile);

        }









        // =====================================================
        // UPDATE PROFILE
        // Client connecté
        // =====================================================


        [Authorize(Roles = "Client")]
        [HttpPut("profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProfile(
            UpdateClientProfileRequest request)
        {


            var clientId =
                _currentUser.UserId;



            await _clientService
                .UpdateProfileAsync(
                    clientId,
                    request);



            return NoContent();

        }









        // =====================================================
        // CHANGE PASSWORD
        // Client connecté
        // =====================================================


        [Authorize(Roles = "Client")]
        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePassword(
            ClientChangePasswordRequest request)
        {


            var clientId =
                _currentUser.UserId;



            await _clientService
                .ChangePasswordAsync(
                    clientId,
                    request);



            return NoContent();

        }









        // =====================================================
        // ADMIN : GET ALL CLIENTS
        // =====================================================


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ClientListDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ClientListDto>>> GetAll()
        {


            var clients =
                await _clientService
                .GetAllAsync();



            return Ok(clients);

        }









        // =====================================================
        // ADMIN : GET CLIENT DETAILS
        // =====================================================


        [Authorize(Roles = "Admin")]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ClientDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientDetailsDto>> GetById(
            Guid id)
        {


            var client =
                await _clientService
                .GetDetailsAsync(id);



            return Ok(client);

        }









        // =====================================================
        // ADMIN : UPDATE CLIENT STATUS
        // =====================================================


        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:guid}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(
            Guid id,
            UpdateClientStatusRequest request)
        {


            await _clientService
                .UpdateStatusAsync(
                    id,
                    request);



            return NoContent();

        }









       


    }
}
