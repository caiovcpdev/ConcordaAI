using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.CheckIn;
using ConcordaAI.Application.Interfaces;
using ConcordaAI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.Services
{
    public class CheckInService : ICheckInService
    {
        private readonly ICheckInRepository _checkInRepository;

        public CheckInService(ICheckInRepository checkInRepository)
        {
            _checkInRepository = checkInRepository;
        }

        public async Task<Result<PresencaResponse>> ObterPresencaAsync(Guid escalaTrabalhadorId)
        {
            var registro = await _checkInRepository.ObterPorEscalaTrabalhadorAsync(escalaTrabalhadorId);

            if (registro == null)
                return Result<PresencaResponse>.Ok(new PresencaResponse());

            return Result<PresencaResponse>.Ok(new PresencaResponse
            {
                DataHoraCheckIn = registro.DataHoraCheckIn,
                DataHoraCheckOut = registro.DataHoraCheckOut
            });
        }

        public async Task<Result> RegistrarCheckInAsync(Guid escalaTrabalhadorId, RegistrarCheckInRequest request)
        {
            var registro = await _checkInRepository.ObterPorEscalaTrabalhadorAsync(escalaTrabalhadorId);

            if (registro != null && registro.DataHoraCheckIn != null)
                return Result.Fail("Check-in já realizado.");

            await _checkInRepository.RegistrarCheckInAsync(
                escalaTrabalhadorId,
                request.Latitude,
                request.Longitude,
                request.Dispositivo);

            return Result.Ok();
        }

        public async Task<Result> RegistrarCheckOutAsync(Guid escalaTrabalhadorId, RegistrarCheckOutRequest request)
        {
            var registro = await _checkInRepository.ObterPorEscalaTrabalhadorAsync(escalaTrabalhadorId);

            if (registro == null || registro.DataHoraCheckIn == null)
                return Result.Fail("Check-in ainda não realizado.");

            if (registro.DataHoraCheckOut != null)
                return Result.Fail("Check-out já realizado.");

            await _checkInRepository.RegistrarCheckOutAsync(
                escalaTrabalhadorId,
                request.Latitude,
                request.Longitude,
                request.Dispositivo);

            return Result.Ok();
        }
    }
}
