using ConcordaAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Interfaces
{
    public interface ICheckInRepository
    {
        Task<CheckInDto?>ObterPorEscalaTrabalhadorAsync(Guid escalaTrabalhadorId);
        Task RegistrarCheckInAsync(Guid escalaTrabalhadorId, decimal latitude, decimal longitude,string dispositivo);
        Task RegistrarCheckOutAsync(Guid escalaTrabalhadorId, decimal latitude,decimal longitude,string dispositivo);
    }
}
