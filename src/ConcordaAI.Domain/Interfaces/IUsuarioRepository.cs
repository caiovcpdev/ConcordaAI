using ConcordaAI.Domain.Entities;

namespace ConcordaAI.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task<Usuario?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Usuario usuario);
    }
}
