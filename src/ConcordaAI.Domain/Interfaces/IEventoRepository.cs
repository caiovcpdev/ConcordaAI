using ConcordaAI.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Interfaces
{
    public interface IEventoRepository
    {
        Task<Evento?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Evento>> ObterTodosAsync();
        Task AdicionarAsync(Evento evento);
        Task AtualizarAsync(Evento evento);
    }
}
