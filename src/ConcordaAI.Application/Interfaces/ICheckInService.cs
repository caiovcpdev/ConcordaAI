using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.CheckIn;

namespace ConcordaAI.Application.Interfaces
{
    public interface ICheckInService
    {
        Task<Result> RegistrarCheckInAsync(Guid escalaTrabalhadorId, RegistrarCheckInRequest request);
        Task<Result> RegistrarCheckOutAsync(Guid escalaTrabalhadorId,RegistrarCheckOutRequest request);
        Task<Result<PresencaResponse>>ObterPresencaAsync(Guid escalaTrabalhadorId);
    }
}
