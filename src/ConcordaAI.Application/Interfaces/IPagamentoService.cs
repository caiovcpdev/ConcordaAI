using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Pagamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.Interfaces
{
    public interface IPagamentoService
    {
        Task<Result<PagamentoResponse>> CriarAsync(Guid eventoTrabalhadorId,CriarPagamentoRequest request);
        Task<Result<IEnumerable<PagamentoResponse>>> ObterPorEventoTrabalhadorAsync(Guid eventoTrabalhadorId);
        Task<Result> AprovarAsync(Guid id);
        Task<Result> RegistrarPagamentoAsync(Guid id,RegistrarPagamentoRequest request);
        Task<Result> CancelarAsync(Guid id);
    }
}
