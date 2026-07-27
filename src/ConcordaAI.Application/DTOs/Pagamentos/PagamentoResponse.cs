using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Pagamentos
{
    public class PagamentoResponse
    {
        public Guid Id { get; set; }
        public Guid EventoTrabalhadorId { get; set; }
        public decimal ValorPrevisto { get; set; }
        public decimal? ValorPago { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime DataPrevista { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}
