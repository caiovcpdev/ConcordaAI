using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Trabalhadores
{
    public class TrabalhadorResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
