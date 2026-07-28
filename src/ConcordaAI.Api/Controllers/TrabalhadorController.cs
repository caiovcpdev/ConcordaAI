using Asp.Versioning;
using ConcordaAI.Api.Responses;
using ConcordaAI.Application.DTOs.Eventos;
using ConcordaAI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcordaAI.Api.Controllers
{
    [Authorize(Roles = "Administrador,Supervisor,Operador")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/trabalhadores")]
    public class TrabalhadorController : ControllerBase
    {
        private readonly ITrabalhadorService _trabalhadorService;
        public TrabalhadorController(ITrabalhadorService trabalhadorService)
        {
            _trabalhadorService = trabalhadorService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarTrabalhadorRequest request)
        {
            var result = await _trabalhadorService.CriarAsync(request);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var result = await _trabalhadorService.ObterTodosAsync();
            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var result = await _trabalhadorService.ObterPorIdAsync(id);

            if (!result.Success)
                return NotFound(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> AlterarStatus(Guid id, [FromQuery] string status)
        {
            var result = await _trabalhadorService.AlterarStatusAsync(id, status);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<string>.Ok("Status atualizado com sucesso."));
        }
    }
}
