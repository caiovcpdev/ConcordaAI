using ConcordaAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Interfaces
{
    public interface IEventoTrabalhadorRepository
    {
        Task<EventoTrabalhador?> ObterPorIdAsync(Guid id);
        Task<EventoTrabalhador?> ObterPorEventoETrabalhadorAsync(Guid eventoId, Guid trabalhadorId);
        Task<IEnumerable<EventoTrabalhador>> ObterPorEventoAsync(Guid eventoId);
        Task AdicionarAsync(EventoTrabalhador entidade);
        Task AtualizarAsync(EventoTrabalhador entidade);
    }
}
