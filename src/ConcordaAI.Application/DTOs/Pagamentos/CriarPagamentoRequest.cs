using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Pagamentos
{
    public class CriarPagamentoRequest
    {
        public decimal ValorPrevisto { get; set; }
        public DateTime DataPrevista { get; set; }
        public int FormaPagamento { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
