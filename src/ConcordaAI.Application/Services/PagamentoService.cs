using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Pagamentos;
using ConcordaAI.Application.Interfaces;
using ConcordaAI.Domain.Entities;
using ConcordaAI.Domain.Enums;
using ConcordaAI.Domain.Interfaces;

namespace ConcordaAI.Application.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IEventoTrabalhadorRepository _eventoTrabalhadorRepository;
        public PagamentoService(IPagamentoRepository pagamentoRepository, IEventoTrabalhadorRepository eventoTrabalhadorRepository)
        {
            _pagamentoRepository = pagamentoRepository;
            _eventoTrabalhadorRepository  = eventoTrabalhadorRepository;
        }
        public async Task<Result> AprovarAsync(Guid id)
        {
            var pagamento = await _pagamentoRepository.ObterPorIdAsync(id);

            if (pagamento is null)
                return Result.Fail("Pagamento não encontrado.");

            pagamento.Aprovar();
            await _pagamentoRepository.AtualizarAsync(pagamento);

            return Result.Ok();
        }
        public async Task<Result> CancelarAsync(Guid id)
        {
            var pagamento = await _pagamentoRepository.ObterPorIdAsync(id);

            if (pagamento is null)
                return Result.Fail("Pagamento não encontrado.");

            pagamento.Cancelar();
            await _pagamentoRepository.AtualizarAsync(pagamento);

            return Result.Ok();
        }
        public async Task<Result<PagamentoResponse>> CriarAsync(Guid eventoTrabalhadorId, CriarPagamentoRequest request)
        {
            var vinculo = await _eventoTrabalhadorRepository
            .ObterPorIdAsync(eventoTrabalhadorId);

            if (vinculo is null)
                return Result<PagamentoResponse>.Fail("Vínculo não encontrado.");

            var pagamento = new Pagamento(
                eventoTrabalhadorId,
                request.ValorPrevisto,
                request.DataPrevista,
                (FormaPagamento)request.FormaPagamento,
                request.CreatedBy);

            await _pagamentoRepository.AdicionarAsync(pagamento);

            return Result<PagamentoResponse>.Ok(Mapear(pagamento));
        }
        public async Task<Result<IEnumerable<PagamentoResponse>>> ObterPorEventoTrabalhadorAsync(Guid eventoTrabalhadorId)
        {
            var lista = await _pagamentoRepository.ObterPorEventoTrabalhadorAsync(eventoTrabalhadorId);

            return Result<IEnumerable<PagamentoResponse>>.Ok(lista.Select(Mapear));
        }
        public async Task<Result> RegistrarPagamentoAsync(Guid id, RegistrarPagamentoRequest request)
        {
            var pagamento = await _pagamentoRepository.ObterPorIdAsync(id);

            if (pagamento is null)
                return Result.Fail("Pagamento não encontrado.");

            pagamento.RegistrarPagamento(
                request.ValorPago,
                (FormaPagamento)request.FormaPagamento,
                request.ComprovanteUrl);

            await _pagamentoRepository.AtualizarAsync(pagamento);

            return Result.Ok();
        }
        private static PagamentoResponse Mapear(Pagamento p)
        {
            return new PagamentoResponse
            {
                Id = p.Id,
                EventoTrabalhadorId = p.EventoTrabalhadorId,
                ValorPrevisto = p.ValorPrevisto,
                ValorPago = p.ValorPago,
                Status = p.Status.ToString(),
                DataPrevista = p.DataPrevista,
                DataPagamento = p.DataPagamento
            };
        }
    }
}
