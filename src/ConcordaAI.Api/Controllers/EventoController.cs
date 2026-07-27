using Asp.Versioning;
using ConcordaAI.Api.Responses;
using ConcordaAI.Application.DTOs.Eventos;
using ConcordaAI.Application.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace ConcordaAI.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/eventos")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var result = await _eventoService.ObterTodosAsync();
            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var result = await _eventoService.ObterPorIdAsync(id);

            if(!result.Success)
                return NotFound(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarEventoRequest request)
        {
            var result = await _eventoService.CriarAsync(request);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] {result.Error!}));

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = result.Value!.Id },
                ApiResponse<object>.Ok(result.Value)
            );
        }
    }
}
