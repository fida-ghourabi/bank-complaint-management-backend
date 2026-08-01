using BankComplaintManagement.API.Responses;
using BankComplaintManagement.Application.DTOs.Auth;
using BankComplaintManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankComplaintManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService)
        {
            _authService = authService;
        }

        // ==========================================
        // Login
        // POST: /api/auth/login
        // ==========================================

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponse>> Login(
            [FromBody] LoginRequest request)
        {
            var response =
                await _authService.LoginAsync(request);

            return Ok(response);
        }

        // ==========================================
        // Refresh Token
        // POST: /api/auth/refresh-token
        // ==========================================

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponse>> RefreshToken(
            [FromBody] RefreshTokenRequest request)
        {
            var response =
                await _authService.RefreshTokenAsync(
                    request.RefreshToken);

            return Ok(response);
        }

        // ==========================================
        // Logout
        // POST: /api/auth/logout
        // ==========================================

        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Logout(
            [FromBody] RefreshTokenRequest request)
        {
            await _authService.LogoutAsync(
                request.RefreshToken);

            return NoContent();
        }
    }
}
