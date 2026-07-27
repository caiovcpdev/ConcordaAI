using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Ocorrencias
{
    public class OcorrenciaResponse
    {
        public Guid Id { get; set; }
        public Guid EventoTrabalhadorId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Gravidade { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public bool Resolvida { get; set; }
    }
}
