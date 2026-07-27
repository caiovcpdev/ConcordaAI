using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Pagamentos
{
    public class RegistrarPagamentoRequest
    {
        public decimal ValorPago { get; set; }
        public int FormaPagamento { get; set; }
        public string? ComprovanteUrl { get; set; }
    }
}
