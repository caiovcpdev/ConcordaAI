using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.Interfaces
{
    public interface IEventoService
    {
        Task<Result<EventoResponse>> CriarAsync(CriarEventoRequest request);
        Task<Result<IEnumerable<EventoResponse>>> ObterTodosAsync();
        Task<Result<EventoResponse>> ObterPorIdAsync(Guid id);
    }
}
