using ConcordaAI.Domain.Entities;

namespace ConcordaAI.Domain.Interfaces
{
    public interface ITrabalhadorRepository
    {
        Task<Trabalhador?> ObterPorIdAsync(Guid id);
        Task<Trabalhador?> ObterPorCpfAsync(string cpf);
        Task<IEnumerable<Trabalhador>> ObterTodosAsync();
        Task AdicionarAsync(Trabalhador trabalhador);
        Task AtualizarAsync(Trabalhador trabalhador);
    }
}
