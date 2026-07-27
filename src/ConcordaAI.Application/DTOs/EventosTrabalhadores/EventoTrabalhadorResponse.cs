using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.EventosTrabalhadores
{
    public class EventoTrabalhadorResponse
    {
        public Guid Id { get; set; }
        public Guid EventoId { get; set; }
        public Guid TrabalhadorId { get; set; }
        public string TipoTrabalhador { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal? ValorDiaria { get; set; }
    }
}
