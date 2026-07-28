using Asp.Versioning;
using ConcordaAI.Api.Responses;
using ConcordaAI.Application.DTOs.EventosTrabalhadores;
using ConcordaAI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcordaAI.Api.Controllers
{
    [Authorize(Roles = "Administrador,Supervisor,Operador")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/eventos")]
    public class EventoTrabalhadorController : ControllerBase
    {
        private readonly IEventoTrabalhadorService _eventoTrabalhadorService;
        public EventoTrabalhadorController(IEventoTrabalhadorService eventoTrabalhadorService)
        {
            _eventoTrabalhadorService = eventoTrabalhadorService;
        }

        [HttpPost("{eventoId:guid}/trabalhadores")]
        public async Task<IActionResult> Vincular(Guid eventoId, VincularTrabalhadorRequest request)
        {
            var result = await _eventoTrabalhadorService.VincularAsync(eventoId, request);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpGet("{eventoId:guid}/trabalhadores")]
        public async Task<IActionResult> ObterPorEvento(Guid eventoId)
        {
            var result = await _eventoTrabalhadorService.ObterPorEventoAsync(eventoId);

            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpPatch("trabalhadores/{id:guid}/confirmar")]
        public async Task<IActionResult> Confirmar(Guid id)
        {
            var result = await _eventoTrabalhadorService.ConfirmarAsync(id);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<string>.Ok("Confirmado com sucesso."));
        }

        [HttpPatch("trabalhadores/{id:guid}/cancelar")]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            var result = await _eventoTrabalhadorService.CancelarAsync(id);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<string>.Ok("Cancelado com sucesso."));
        }
    }
}
