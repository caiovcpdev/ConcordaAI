using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiracao { get; set; }
    }
}
