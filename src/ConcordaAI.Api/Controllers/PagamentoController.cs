using Asp.Versioning;
using ConcordaAI.Api.Responses;
using ConcordaAI.Application.DTOs.Pagamentos;
using ConcordaAI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConcordaAI.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoService _pagamentoService;
        public PagamentoController(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        [HttpPost("eventos/trabalhadores/{eventoTrabalhadorId:guid}/pagamentos")]
        public async Task<IActionResult> Criar(
        Guid eventoTrabalhadorId,
        [FromBody] CriarPagamentoRequest request)
        {
            var result = await _pagamentoService.CriarAsync(eventoTrabalhadorId, request);

            if (!result.Success)
                return BadRequest(
                    ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpGet("eventos/trabalhadores/{eventoTrabalhadorId:guid}/pagamentos")]
        public async Task<IActionResult> Listar(Guid eventoTrabalhadorId)
        {
            var result = await _pagamentoService.ObterPorEventoTrabalhadorAsync(eventoTrabalhadorId);

            return Ok(ApiResponse<object>.Ok(result.Value));
        }

        [HttpPatch("pagamentos/{id:guid}/aprovar")]
        public async Task<IActionResult> Aprovar(Guid id)
        {
            var result = await _pagamentoService.AprovarAsync(id);

            if (!result.Success)
                return BadRequest(
                    ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<string>.Ok("Pagamento aprovado."));
        }

        [HttpPost("pagamentos/{id:guid}/pagar")]
        public async Task<IActionResult> RegistrarPagamento(Guid id, [FromBody] RegistrarPagamentoRequest request)
        {
            var result = await _pagamentoService.RegistrarPagamentoAsync(id, request);

            if (!result.Success)
                return BadRequest(
                    ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<string>.Ok("Pagamento realizado com sucesso."));
        }

        [HttpPatch("pagamentos/{id:guid}/cancelar")]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            var result = await _pagamentoService.CancelarAsync(id);

            if (!result.Success)
                return BadRequest(
                    ApiResponse<string>.Fail(new[] { result.Error! }));

            return Ok(ApiResponse<string>.Ok("Pagamento cancelado."));
        }

    }
}
