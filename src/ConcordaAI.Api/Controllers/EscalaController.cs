using Asp.Versioning;
using ConcordaAI.Api.Responses;
using ConcordaAI.Application.DTOs.Escalas;
using ConcordaAI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConcordaAI.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class EscalaController : ControllerBase
    {
        private readonly IEscalaService _escalaService;
        public EscalaController(IEscalaService escalaService)
        {
            _escalaService = escalaService;
        }

        [HttpPost("eventos/{eventoId:guid}/escalas")]
        public async Task<IActionResult> Criar(
        Guid eventoId,
        CriarEscalaRequest request)
        {
            var result = await _escalaService.CriarAsync(eventoId, request);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpGet("eventos/{eventoId:guid}/escalas")]
        public async Task<IActionResult> ObterPorEvento(Guid eventoId)
        {
            var result = await _escalaService.ObterPorEventoAsync(eventoId);
            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpPost("escalas/{escalaId:guid}/trabalhadores")]
        public async Task<IActionResult> AdicionarTrabalhador(Guid escalaId, AdicionarTrabalhadorEscalaRequest request)
        {
            var result = await _escalaService.AdicionarTrabalhadorAsync(escalaId, request.EventoTrabalhadorId);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<string>.Ok("Trabalhador adicionado à escala."));
        }
    }
}
