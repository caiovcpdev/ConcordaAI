using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Auth
{
    public class CriarUsuarioRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int Perfil { get; set; }
    }
}
