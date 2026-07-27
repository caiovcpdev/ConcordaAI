using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Escalas;
using ConcordaAI.Application.Interfaces;
using ConcordaAI.Domain.Entities;
using ConcordaAI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.Services
{
    public class EscalaService : IEscalaService
    {
        private readonly IEscalaRepository _escalaRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IEventoTrabalhadorRepository _eventoTrabalhadorRepository;
        public EscalaService(IEscalaRepository escalaRepository, IEventoRepository eventoRepository, IEventoTrabalhadorRepository eventoTrabalhadorRepository)
        {
            _escalaRepository = escalaRepository;
            _eventoRepository = eventoRepository;
            _eventoTrabalhadorRepository = eventoTrabalhadorRepository;
        }

        public async Task<Result> AdicionarTrabalhadorAsync(Guid escalaId, Guid eventoTrabalhadorId)
        {
            var escala = await _escalaRepository.ObterPorIdAsync(escalaId);

            if (escala is null)
                return Result.Fail("Escala não encontrada.");

            var vinculo = await _eventoTrabalhadorRepository
                .ObterPorIdAsync(eventoTrabalhadorId);

            if (vinculo is null)
                return Result.Fail("Vínculo não encontrado.");

            escala.AdicionarTrabalhador(eventoTrabalhadorId);

            await _escalaRepository.AtualizarAsync(escala);

            return Result.Ok();
        }

        public async Task<Result<EscalaResponse>> CriarAsync(Guid eventoId, CriarEscalaRequest request)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(eventoId);

            if (evento is null)
                return Result<EscalaResponse>.Fail("Evento não encontrado.");

            var escala = new Escala(
                eventoId,
                request.Nome,
                request.Data,
                request.HoraInicio,
                request.HoraFim,
                request.PontoEncontro,
                request.CreatedBy
            );

            await _escalaRepository.AdicionarAsync(escala);

            return Result<EscalaResponse>.Ok(Mapear(escala));
        }

        public async Task<Result<IEnumerable<EscalaResponse>>> ObterPorEventoAsync(Guid eventoId)
        {
            var lista = await _escalaRepository.ObterPorEventoAsync(eventoId);

            return Result<IEnumerable<EscalaResponse>>.Ok(lista.Select(Mapear));
        }

        private static EscalaResponse Mapear(Escala escala)
        {
            return new EscalaResponse
            {
                Id = escala.Id,
                EventoId = escala.EventoId,
                Nome = escala.Nome,
                Data = escala.Data,
                HoraInicio = escala.HoraInicio.ToString(@"hh\:mm"),
                HoraFim = escala.HoraFim.ToString(@"hh\:mm")
            };
        }
    }
}
