using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Eventos
{
    public class CriarEventoRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Organizador { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
