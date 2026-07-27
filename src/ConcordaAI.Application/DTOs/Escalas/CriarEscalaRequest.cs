using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Escalas
{
    public class CriarEscalaRequest
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public string PontoEncontro { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
