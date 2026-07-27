using ConcordaAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Interfaces
{
    public interface IEscalaRepository
    {
        Task<Escala?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Escala>> ObterPorEventoAsync(Guid eventoId);
        Task AdicionarAsync(Escala escala);
        Task AtualizarAsync(Escala escala);
    }
}
