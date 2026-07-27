using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Eventos
{
    public class CriarTrabalhadorRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty ;
        public DateTime DataNascimento { get; set; }
        public int Sexo { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;

    }
}
