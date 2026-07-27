using ConcordaAI.Domain.Entities;


namespace ConcordaAI.Domain.Interfaces
{
    public interface IPagamentoRepository
    {
        Task<Pagamento?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Pagamento>> ObterPorEventoTrabalhadorAsync(Guid eventoTrabalhadorId);
        Task AdicionarAsync(Pagamento pagamento);
        Task AtualizarAsync(Pagamento pagamento);
    }
}
