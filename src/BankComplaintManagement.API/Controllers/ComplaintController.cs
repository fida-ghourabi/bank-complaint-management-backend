using BankComplaintManagement.Application.DTOs.Complaints;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankComplaintManagement.API.Controllers
{
    [ApiController]
    [Route("api/complaints")]
    public class ComplaintController : ControllerBase
    {

        private readonly IComplaintService _complaintService;

        private readonly ICurrentUserService _currentUser;



        public ComplaintController(
            IComplaintService complaintService,
            ICurrentUserService currentUser)
        {
            _complaintService = complaintService;
            _currentUser = currentUser;
        }





        // =====================================================
        // CLIENT
        // Créer une réclamation
        // =====================================================


        [Authorize(Roles = "Client")]
        [HttpPost]
        [ProducesResponseType(typeof(ComplaintDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ComplaintDto>> Create(
            [FromForm] CreateComplaintRequest request)
        {

            var complaint =
                await _complaintService.CreateAsync(
                    _currentUser.UserId,
                    request);



            return CreatedAtAction(
                nameof(GetById),
                new { id = complaint.Id },
                complaint);

        }







        // =====================================================
        // CLIENT
        // Mes réclamations
        // =====================================================


        [Authorize(Roles = "Client")]
        [HttpGet("my")]
        [ProducesResponseType(typeof(IReadOnlyList<ComplaintDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ComplaintDto>>> GetMyComplaints()
        {

            var complaints =
                await _complaintService.GetByClientAsync(
                    _currentUser.UserId);



            return Ok(complaints);

        }







        // =====================================================
        // CLIENT / ADMIN / AGENT
        // Récupérer par ID
        // =====================================================


        [Authorize]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ComplaintDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ComplaintDto>> GetById(
            Guid id)
        {

            var complaint =
                await _complaintService.GetByIdAsync(id);



            return Ok(complaint);

        }







        // =====================================================
        // Détails réclamation
        // =====================================================


        [Authorize]
        [HttpGet("{id:guid}/details")]
        [ProducesResponseType(typeof(ComplaintDetailsDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ComplaintDetailsDto>> GetDetails(
            Guid id)
        {

            var complaint =
                await _complaintService.GetDetailsAsync(id);



            return Ok(complaint);

        }








        // =====================================================
        // Recherche par référence
        // =====================================================


        [Authorize]
        [HttpGet("reference/{reference}")]
        [ProducesResponseType(typeof(ComplaintDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ComplaintDto>> GetByReference(
            string reference)
        {

            var complaint =
                await _complaintService.GetByReferenceAsync(reference);



            return Ok(complaint);

        }









        // =====================================================
        // ADMIN
        // Pagination
        // =====================================================


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ComplaintDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ComplaintDto>>> GetPaged(
            int pageNumber = 1,
            int pageSize = 20)
        {

            var complaints =
                await _complaintService.GetPagedAsync(
                    pageNumber,
                    pageSize);



            return Ok(complaints);

        }









        // =====================================================
        // AGENT
        // Réclamations affectées
        // =====================================================


        [Authorize(Roles = "Agent")]
        [HttpGet("assigned")]
        [ProducesResponseType(typeof(IReadOnlyList<ComplaintDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ComplaintDto>>> GetAssigned()
        {

            var complaints =
                await _complaintService.GetByAgentAsync(
                    _currentUser.UserId);



            return Ok(complaints);

        }








        // =====================================================
        // ADMIN
        // Affecter un agent
        // =====================================================


        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:guid}/assign")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AssignAgent(
            Guid id,
            AssignComplaintRequest request)
        {

            await _complaintService.AssignAgentAsync(
                id,
                request);



            return NoContent();

        }









        // =====================================================
        // AGENT / ADMIN
        // Modifier statut
        // =====================================================


        [Authorize(Roles = "Agent,Admin")]
        [HttpPatch("{id:guid}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeStatus(
            Guid id,
            UpdateComplaintStatusRequest request)
        {

            await _complaintService.ChangeStatusAsync(
                id,
                request);



            return NoContent();

        }









        // =====================================================
        // AGENT / ADMIN
        // Modifier priorité
        // =====================================================


        [Authorize(Roles = "Agent,Admin")]
        [HttpPatch("{id:guid}/priority")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangePriority(
            Guid id,
            ChangeComplaintPriorityRequest request)
        {

            await _complaintService.ChangePriorityAsync(
                id,
                request);



            return NoContent();

        }









        // =====================================================
        // AGENT / ADMIN
        // Transfert service
        // =====================================================


        [Authorize(Roles = "Agent,Admin")]
        [HttpPatch("{id:guid}/transfer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Transfer(
            Guid id,
            TransferComplaintServiceRequest request)
        {

            await _complaintService.TransferToServiceAsync(
                id,
                request);



            return NoContent();

        }









        // =====================================================
        // AGENT / ADMIN
        // Rejeter
        // =====================================================


        [Authorize(Roles = "Agent,Admin")]
        [HttpPatch("{id:guid}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Reject(
            Guid id,
            RejectComplaintRequest request)
        {

            await _complaintService.RejectAsync(
                id,
                request);



            return NoContent();

        }









        // =====================================================
        // ADMIN
        // Réclamations non affectées
        // =====================================================


        [Authorize(Roles = "Admin")]
        [HttpGet("unassigned")]
        public async Task<ActionResult<IReadOnlyList<ComplaintDto>>> GetUnassigned()
        {

            return Ok(
                await _complaintService.GetUnassignedAsync());

        }









        // =====================================================
        // FILTRES
        // =====================================================


        [Authorize(Roles = "Admin")]
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IReadOnlyList<ComplaintDto>>> GetByStatus(
            ComplaintStatus status)
        {

            return Ok(
                await _complaintService.GetByStatusAsync(status));

        }







        [Authorize(Roles = "Admin")]
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IReadOnlyList<ComplaintDto>>> GetByCategory(
            ComplaintCategory category)
        {

            return Ok(
                await _complaintService.GetByCategoryAsync(category));

        }







        [Authorize(Roles = "Admin")]
        [HttpGet("priority/{priority}")]
        public async Task<ActionResult<IReadOnlyList<ComplaintDto>>> GetByPriority(
            ComplaintPriority priority)
        {

            return Ok(
                await _complaintService.GetByPriorityAsync(priority));

        }









        // =====================================================
        // DASHBOARD
        // =====================================================


        [Authorize(Roles = "Admin")]
        [HttpGet("dashboard/count")]
        public async Task<ActionResult<int>> Count()
        {

            return Ok(
                await _complaintService.CountAsync());

        }




        [Authorize(Roles = "Admin")]
        [HttpGet("dashboard/overdue")]
        public async Task<ActionResult<int>> CountOverdue()
        {

            return Ok(
                await _complaintService.CountOverdueAsync());

        }




        [Authorize(Roles = "Admin")]
        [HttpGet("dashboard/unassigned")]
        public async Task<ActionResult<int>> CountUnassigned()
        {

            return Ok(
                await _complaintService.CountUnassignedAsync());

        }





        [Authorize(Roles = "Admin")]
        [HttpGet("dashboard/high-priority")]
        public async Task<ActionResult<int>> CountHighPriority()
        {

            return Ok(
                await _complaintService.CountHighPriorityAsync());

        }




        [Authorize(Roles = "Admin")]
        [HttpGet("dashboard/status/{status}")]
        public async Task<ActionResult<int>> CountByStatus(
            ComplaintStatus status)
        {

            return Ok(
                await _complaintService.CountByStatusAsync(status));

        }




        [Authorize(Roles = "Admin")]
        [HttpGet("dashboard/categories")]
        public async Task<IActionResult> CountByCategory()
        {

            return Ok(
                await _complaintService.CountByCategoryAsync());

        }








        // =====================================================
        // TRI
        // =====================================================


        [Authorize(Roles = "Admin")]
        [HttpGet("sort/latest")]
        public async Task<IActionResult> Latest()
        {
            return Ok(
                await _complaintService.GetLatestAsync());
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("sort/oldest")]
        public async Task<IActionResult> Oldest()
        {
            return Ok(
                await _complaintService.GetOldestAsync());
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("sort/priority")]
        public async Task<IActionResult> PriorityOrder()
        {
            return Ok(
                await _complaintService.GetByPriorityOrderAsync());
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("filter/overdue")]
        public async Task<IActionResult> Overdue()
        {
            return Ok(
                await _complaintService.GetOverdueAsync());
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("filter/high-priority")]
        public async Task<IActionResult> HighPriority()
        {
            return Ok(
                await _complaintService.GetHighPriorityAsync());
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("sort/sla")]
        public async Task<IActionResult> SlaDeadline()
        {
            return Ok(
                await _complaintService.GetBySlaDeadlineAsync());
        }


    }
}
