using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Ocorrencias;
using ConcordaAI.Application.Interfaces;
using ConcordaAI.Domain.Entities;
using ConcordaAI.Domain.Enums;
using ConcordaAI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.Services
{
    public class OcorrenciaService : IOcorrenciaService
    {
        private readonly IOcorrenciaRepository _ocorrenciaRepository;

        private readonly IEventoTrabalhadorRepository _eventoTrabalhadorRepository;
        public OcorrenciaService(IOcorrenciaRepository ocorrenciaRepository, IEventoTrabalhadorRepository eventoTrabalhadorRepository)
        {
            _ocorrenciaRepository = ocorrenciaRepository;
            _eventoTrabalhadorRepository = eventoTrabalhadorRepository;
        }

        public async Task<Result<IEnumerable<OcorrenciaResponse>>> ObterPorEventoTrabalhadorAsync(Guid eventoTrabalhadorId)
        {
            var lista = await _ocorrenciaRepository.ObterPorEventoTrabalhadorAsync(eventoTrabalhadorId);

            return Result<IEnumerable<OcorrenciaResponse>>.Ok(lista.Select(Mapear));
        }

        public async Task<Result<OcorrenciaResponse>> RegistrarAsync(Guid eventoTrabalhadorId, RegistrarOcorrenciaRequest request)
        {
            var vinculo = await _eventoTrabalhadorRepository.ObterPorIdAsync(eventoTrabalhadorId);

            if (vinculo is null)
                return Result<OcorrenciaResponse>
                    .Fail("Vínculo não encontrado.");

            var ocorrencia = new Ocorrencia(
                eventoTrabalhadorId,
                (TipoOcorrencia)request.Tipo,
                (GravidadeOcorrencia)request.Gravidade,
                request.Descricao,
                request.CreatedBy);

            await _ocorrenciaRepository.AdicionarAsync(ocorrencia);

            return Result<OcorrenciaResponse>.Ok(Mapear(ocorrencia));
        }

        public async Task<Result> ResolverAsync(Guid id)
        {
            var ocorrencia = await _ocorrenciaRepository.ObterPorIdAsync(id);

            if (ocorrencia is null)
                return Result.Fail("Ocorrência não encontrada.");

            ocorrencia.MarcarComoResolvida();
            await _ocorrenciaRepository.AtualizarAsync(ocorrencia);

            return Result.Ok();
        }

        private static OcorrenciaResponse Mapear(Ocorrencia ocorrencia)
        {
            return new OcorrenciaResponse
            {
                Id = ocorrencia.Id,
                EventoTrabalhadorId = ocorrencia.EventoTrabalhadorId,
                Tipo = ocorrencia.Tipo.ToString(),
                Gravidade = ocorrencia.Gravidade.ToString(),
                Descricao = ocorrencia.Descricao,
                DataHora = ocorrencia.DataHora,
                Resolvida = ocorrencia.Resolvida
            };
        }
    }
}
