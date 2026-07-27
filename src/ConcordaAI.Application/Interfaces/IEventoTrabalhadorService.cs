using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.EventosTrabalhadores;

namespace ConcordaAI.Application.Interfaces
{
    public interface IEventoTrabalhadorService
    {
        Task<Result<EventoTrabalhadorResponse>> VincularAsync(Guid eventoId, VincularTrabalhadorRequest request);
        Task<Result<IEnumerable<EventoTrabalhadorResponse>>> ObterPorEventoAsync(Guid eventoId);
        Task<Result> ConfirmarAsync(Guid id);
        Task<Result> CancelarAsync(Guid id);
    }
}
