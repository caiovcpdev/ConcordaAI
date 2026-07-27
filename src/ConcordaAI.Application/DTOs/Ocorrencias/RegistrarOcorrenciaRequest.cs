using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Ocorrencias
{
    public class RegistrarOcorrenciaRequest
    {
        public int Tipo { get; set; }
        public int Gravidade { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
