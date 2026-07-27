using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Escalas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.Interfaces
{
    public interface IEscalaService
    {
        Task<Result<EscalaResponse>> CriarAsync(Guid eventoId, CriarEscalaRequest request);
        Task<Result<IEnumerable<EscalaResponse>>> ObterPorEventoAsync(Guid eventoId);
        Task<Result> AdicionarTrabalhadorAsync(Guid escalaId, Guid eventoTrabalhadorId);
    }
}
