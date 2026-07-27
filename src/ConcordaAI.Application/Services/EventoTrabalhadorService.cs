using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.EventosTrabalhadores;
using ConcordaAI.Application.Interfaces;
using ConcordaAI.Domain.Entities;
using ConcordaAI.Domain.Enums;
using ConcordaAI.Domain.Interfaces;


namespace ConcordaAI.Application.Services
{
    public class EventoTrabalhadorService : IEventoTrabalhadorService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly ITrabalhadorRepository _trabalhadorRepository;
        private readonly IEventoTrabalhadorRepository _eventoTrabalhadorRepository;

        public EventoTrabalhadorService(IEventoRepository eventoRepository, ITrabalhadorRepository trabalhadorRepository, IEventoTrabalhadorRepository eventoTrabalhadorRepository)
        {
            _eventoRepository = eventoRepository;
            _trabalhadorRepository = trabalhadorRepository;
            _eventoTrabalhadorRepository = eventoTrabalhadorRepository;
        }

        public async Task<Result> CancelarAsync(Guid id)
        {
            var eventoTrabalhador = await _eventoTrabalhadorRepository.ObterPorIdAsync(id);

            if (eventoTrabalhador is null)
                return Result.Fail("Vínculo não encontrado.");

            eventoTrabalhador.Cancelar();
            await _eventoTrabalhadorRepository.AtualizarAsync(eventoTrabalhador);

            return Result.Ok();
        }

        public async Task<Result> ConfirmarAsync(Guid id)
        {
            var eventoTrabalhador = await _eventoTrabalhadorRepository.ObterPorIdAsync(id);

            if (eventoTrabalhador is null)
                return Result.Fail("Vínculo não encontrado.");

            eventoTrabalhador.Confirmar();
            await _eventoTrabalhadorRepository.AtualizarAsync(eventoTrabalhador);

            return Result.Ok();
        }

        public async Task<Result<IEnumerable<EventoTrabalhadorResponse>>> ObterPorEventoAsync(Guid eventoId)
        {
            var lista = await _eventoTrabalhadorRepository.ObterPorEventoAsync(eventoId);

            return Result<IEnumerable<EventoTrabalhadorResponse>>.Ok(lista.Select(Mapear));
        }

        public async Task<Result<EventoTrabalhadorResponse>> VincularAsync(Guid eventoId, VincularTrabalhadorRequest request)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(eventoId);

            if (evento is null)
                return Result<EventoTrabalhadorResponse>.Fail("Evento não encontrado.");

            var trabalhador = await _trabalhadorRepository.ObterPorIdAsync(request.TrabalhadorId);

            if (trabalhador is null)
                return Result<EventoTrabalhadorResponse>.Fail("Trabalhador não encontrado.");

            var existente = await _eventoTrabalhadorRepository.ObterPorEventoETrabalhadorAsync(eventoId, request.TrabalhadorId);

            if (existente != null)
                return Result<EventoTrabalhadorResponse>.Fail("Trabalhador já vinculado ao evento.");

            var eventoTrabalhador = new EventoTrabalhador(
                eventoId,
                request.TrabalhadorId,
                (TipoTrabalhador)request.TipoTrabalhador,
                request.CreatedBy,
                request.ValorDiaria);

            await _eventoTrabalhadorRepository.AdicionarAsync(eventoTrabalhador);

            return Result<EventoTrabalhadorResponse>.Ok(Mapear(eventoTrabalhador));
        }

        private static EventoTrabalhadorResponse Mapear(EventoTrabalhador e)
        {
            return new EventoTrabalhadorResponse
            {
                Id = e.Id,
                EventoId = e.EventoId,
                TrabalhadorId = e.TrabalhadorId,
                TipoTrabalhador = e.TipoTrabalhador.ToString(),
                Status = e.Status.ToString(),
                ValorDiaria = e.ValorDiaria
            };
        }
    }
}
