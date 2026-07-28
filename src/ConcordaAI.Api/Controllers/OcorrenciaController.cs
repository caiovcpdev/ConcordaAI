using Asp.Versioning;
using ConcordaAI.Api.Responses;
using ConcordaAI.Application.DTOs.Ocorrencias;
using ConcordaAI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcordaAI.Api.Controllers
{
    [Authorize(Roles = "Administrador,Supervisor,Lider")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class OcorrenciaController : ControllerBase
    {
        private readonly IOcorrenciaService _ocorrenciaService;

        public OcorrenciaController(IOcorrenciaService ocorrenciaService)
        {
            _ocorrenciaService = ocorrenciaService;
        }

        [HttpPost("eventos/trabalhadores/{eventoTrabalhadorId:guid}/ocorrencias")]
        public async Task<IActionResult> Registrar(Guid eventoTrabalhadorId, RegistrarOcorrenciaRequest request)
        {
            var result = await _ocorrenciaService.RegistrarAsync(eventoTrabalhadorId, request);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpGet("eventos/trabalhadores/{eventoTrabalhadorId:guid}/ocorrencias")]
        public async Task<IActionResult> Listar(Guid eventoTrabalhadorId)
        {
            var result = await _ocorrenciaService.ObterPorEventoTrabalhadorAsync(eventoTrabalhadorId);

            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpPatch("ocorrencias/{id:guid}/resolver")]
        public async Task<IActionResult> Resolver(Guid id)
        {
            var result = await _ocorrenciaService.ResolverAsync(id);

            if (!result.Success)
                return BadRequest(ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<string>.Ok("Ocorrência resolvida."));
        }
    }
}
