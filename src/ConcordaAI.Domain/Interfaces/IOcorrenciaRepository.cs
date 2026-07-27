using ConcordaAI.Domain.Entities;

namespace ConcordaAI.Domain.Interfaces
{
    public interface IOcorrenciaRepository
    {
        Task<Ocorrencia?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Ocorrencia>> ObterPorEventoTrabalhadorAsync(Guid eventoTrabalhadorId);
        Task AdicionarAsync(Ocorrencia ocorrencia);
        Task AtualizarAsync(Ocorrencia ocorrencia);
    }
}
