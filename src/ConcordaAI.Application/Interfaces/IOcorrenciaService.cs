using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Ocorrencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.Interfaces
{
    public interface IOcorrenciaService
    {
        Task<Result<OcorrenciaResponse>> RegistrarAsync(Guid eventoTrabalhadorId, RegistrarOcorrenciaRequest request);
        Task<Result<IEnumerable<OcorrenciaResponse>>>ObterPorEventoTrabalhadorAsync(Guid eventoTrabalhadorId);
        Task<Result> ResolverAsync(Guid id);
    }
}
