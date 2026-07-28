using Asp.Versioning;
using ConcordaAI.Api.Responses;
using ConcordaAI.Application.DTOs.Auth;
using ConcordaAI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcordaAI.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (!result.Success)
                return Unauthorized(
                    ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("usuarios")]
        public async Task<IActionResult> CriarUsuario(CriarUsuarioRequest request)
        {
            var result = await _authService.CriarUsuarioAsync(request);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<object>.Ok(result.Value));
        }
    }
}
