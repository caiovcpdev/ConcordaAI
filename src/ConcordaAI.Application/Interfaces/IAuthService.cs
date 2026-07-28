using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request);
        Task<Result<UsuarioResponse>> CriarUsuarioAsync(CriarUsuarioRequest request);
    }
}
