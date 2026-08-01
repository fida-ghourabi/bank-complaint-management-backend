using BankComplaintManagement.Application.DTOs.Attachments;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankComplaintManagement.API.Controllers
{
    [ApiController]
    [Route("api/attachments")]
    [Authorize]
    public class AttachmentController : ControllerBase
    {


        private readonly IAttachmentService _service;



        public AttachmentController(
            IAttachmentService service)
        {
            _service = service;
        }
















        // GET api/attachments/{id}/download

        [HttpGet("{id:guid}/download")]
        public async Task<IActionResult> Download(
            Guid id)
        {


            var result =
                await _service
                .DownloadAsync(id);



            return File(
                result.Content,
                result.ContentType,
                result.FileName);

        }



    }
}
