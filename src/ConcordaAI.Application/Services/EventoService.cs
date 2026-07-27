using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Eventos;
using ConcordaAI.Application.Interfaces;
using ConcordaAI.Domain.Entidades;
using ConcordaAI.Domain.Interfaces;
using FluentValidation;

namespace ConcordaAI.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IValidator<CriarEventoRequest> _validator;
        public EventoService(IEventoRepository eventoRepository, IValidator<CriarEventoRequest> validator)
        {
            _eventoRepository = eventoRepository;
            _validator = validator;

        }
        public async Task<Result<EventoResponse>> CriarAsync(CriarEventoRequest request)
        {
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                var errors = string.Join(" | ",
                    validation.Errors.Select(e => e.ErrorMessage));

                return Result<EventoResponse>.Fail(errors);
            }

            var evento = new Evento(
                request.Nome,
                request.Cidade,
                request.Estado,
                request.DataInicio,
                request.DataFim,
                request.Organizador,
                request.CreatedBy);

            await _eventoRepository.AdicionarAsync(evento);

            return Result<EventoResponse>.Ok(Mapear(evento));
        }

        public async Task<Result<EventoResponse>> ObterPorIdAsync(Guid id)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(id);

            if (evento is null)
                return Result<EventoResponse>.Fail("Evento não encontrado.");

            return Result<EventoResponse>.Ok(Mapear(evento));
        }

        public async Task<Result<IEnumerable<EventoResponse>>> ObterTodosAsync()
        {
            var eventos = await _eventoRepository.ObterTodosAsync();

            var reponse = eventos.Select(Mapear);

            return Result<IEnumerable<EventoResponse>>.Ok(reponse);
        }

        private static EventoResponse Mapear(Evento evento)
        {
            return new EventoResponse
            {
                Id = evento.Id,
                Nome = evento.Nome,
                Cidade = evento.Cidade,
                Estado = evento.Estado,
                DataInicio = evento.DataInicio,
                DataFim = evento.DataFim,
                Status = evento.Status.ToString()
            };
        }
    }
}
