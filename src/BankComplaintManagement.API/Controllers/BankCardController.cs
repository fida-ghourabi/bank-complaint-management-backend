using BankComplaintManagement.Application.DTOs.BankCards;
using BankComplaintManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankComplaintManagement.API.Controllers
{
    [ApiController]
    [Route("api/bank-cards")]
    public class BankCardController : ControllerBase
    {


        private readonly IBankCardService _bankCardService;


        private readonly ICurrentUserService _currentUser;




        public BankCardController(
            IBankCardService bankCardService,
            ICurrentUserService currentUser)
        {

            _bankCardService = bankCardService;

            _currentUser = currentUser;

        }








        // =====================================================
        // GET CURRENT CLIENT CARDS
        // =====================================================


        [Authorize(Roles = "Client")]
        [HttpGet("my-cards")]
        [ProducesResponseType(
            typeof(IReadOnlyList<BankCardInfoDto>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<BankCardInfoDto>>>
            GetMyCards()
        {


            var clientId =
                _currentUser.UserId;




            var cards =
                await _bankCardService
                .GetByClientAsync(clientId);



            return Ok(cards);


        }



    }

}
