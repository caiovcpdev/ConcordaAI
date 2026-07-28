using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Auth
{
    public class UsuarioResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
