using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.EventosTrabalhadores
{
    public class VincularTrabalhadorRequest
    {
        public Guid TrabalhadorId { get; set; }
        public int TipoTrabalhador { get; set; }
        public decimal? ValorDiaria { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
