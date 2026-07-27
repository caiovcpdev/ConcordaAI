using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Escalas
{
    public class EscalaResponse
    {
        public Guid Id { get; set; }
        public Guid EventoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string HoraInicio { get; set; } = string.Empty;
        public string HoraFim { get; set; } = string.Empty;
    }
}
