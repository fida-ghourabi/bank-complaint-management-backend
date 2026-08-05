using BankComplaintManagement.Application.DTOs.Notifications;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankComplaintManagement.API.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [Authorize]
    public class NotificationController : ControllerBase
    {

        private readonly INotificationService _notificationService;
        private readonly ICurrentUserService _currentUser;


        public NotificationController(
            INotificationService notificationService, ICurrentUserService currentUser)
        {
            _notificationService = notificationService;
            _currentUser = currentUser;

        }



        // ==========================================
        // GET : toutes les notifications utilisateur
        // ==========================================

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<NotificationDto>>> Get()
        {

            var userId = _currentUser.UserId;



            var notifications =
                await _notificationService.GetByUserAsync(userId);


            return Ok(notifications);
        }






        // ==========================================
        // PATCH : une notification lue
        // ==========================================

        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkAsRead(
            Guid id)
        {

            await _notificationService
                .MarkAsReadAsync(id);


            return NoContent();
        }






        // ==========================================
        // PATCH : toutes les notifications lues
        // ==========================================

        [HttpPatch("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {

            var userId = _currentUser.UserId;
                 
                


            await _notificationService
                .MarkAllAsReadAsync(userId);



            return NoContent();
        }

    }
}
