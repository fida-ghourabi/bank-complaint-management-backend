using BankComplaintManagement.Application.DTOs.BankAccounts;
using BankComplaintManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankComplaintManagement.API.Controllers
{
    [ApiController]
    [Route("api/bank-accounts")]
    public class BankAccountController : ControllerBase
    {


        private readonly IBankAccountService _bankAccountService;

        private readonly ICurrentUserService _currentUser;



        public BankAccountController(
            IBankAccountService bankAccountService,
            ICurrentUserService currentUser)
        {

            _bankAccountService = bankAccountService;

            _currentUser = currentUser;

        }








        // =====================================================
        // GET CURRENT CLIENT ACCOUNTS
        // Récupérer les comptes du client connecté
        // =====================================================


        [Authorize(Roles = "Client")]
        [HttpGet("my-accounts")]
        [ProducesResponseType(
            typeof(IReadOnlyList<BankAccountInfoDto>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<BankAccountInfoDto>>>
            GetMyAccounts()
        {


            var clientId =
                _currentUser.UserId;



            var accounts =
                await _bankAccountService
                .GetByClientAsync(clientId);



            return Ok(accounts);

        }






    }
}
