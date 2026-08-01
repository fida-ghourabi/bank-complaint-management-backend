using BankComplaintManagement.Application.DTOs.Messages;
using BankComplaintManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankComplaintManagement.API.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {

        private readonly IMessageService _messageService;

        private readonly ICurrentUserService _currentUser;



        public MessageController(
            IMessageService messageService,
            ICurrentUserService currentUser)
        {
            _messageService = messageService;
            _currentUser = currentUser;
        }





        [Authorize(Roles = "Client,Agent")]
        [HttpPost("{complaintId:guid}")]
        public async Task<ActionResult<MessageDto>> Create(
            Guid complaintId,
           [FromForm] CreateMessageRequest request)
        {


            var userId =
                _currentUser.UserId;



            var role =
                _currentUser.Role;



            bool isAgent =
                role == "Agent";



            var message =
                await _messageService.CreateAsync(
                    complaintId,
                    request,
                    isAgent);



            return Created("", message);

        }

    }
}
