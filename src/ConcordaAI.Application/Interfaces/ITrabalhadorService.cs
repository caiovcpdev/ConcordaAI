using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Eventos;
using ConcordaAI.Application.DTOs.Trabalhadores;

namespace ConcordaAI.Application.Interfaces
{
    public interface ITrabalhadorService
    {
        Task<Result<TrabalhadorResponse>> CriarAsync(CriarTrabalhadorRequest request);
        Task<Result<IEnumerable<TrabalhadorResponse>>> ObterTodosAsync();
        Task<Result<TrabalhadorResponse>> ObterPorIdAsync(Guid id);
        Task<Result> AlterarStatusAsync(Guid id, string novoStatus);
    }
}
