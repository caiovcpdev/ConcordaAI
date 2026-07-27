using ConcordaAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Interfaces
{
    public interface IEquipeRepository
    {
        Task<Equipe?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Equipe>> ObterPorEventoAsync(Guid eventoId);
        Task AdicionarAsync(Equipe equipe);
        Task AtualizarAsync(Equipe equipe);
    }
}
