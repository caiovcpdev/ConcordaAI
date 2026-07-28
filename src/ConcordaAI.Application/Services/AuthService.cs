using BCrypt.Net;
using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Auth;
using ConcordaAI.Application.Interfaces;
using ConcordaAI.Domain.Entities;
using ConcordaAI.Domain.Enums;
using ConcordaAI.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ConcordaAI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;


        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<Result<UsuarioResponse>> CriarUsuarioAsync(CriarUsuarioRequest request)
        {
            var existente = await _usuarioRepository.ObterPorEmailAsync(request.Email);

            if (existente != null)
                return Result<UsuarioResponse>.Fail("Email já cadastrado.");

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            var usuario = new Usuario(
                request.Nome,
                request.Email,
                senhaHash,
                (PerfilUsuario)request.Perfil);

            await _usuarioRepository.AdicionarAsync(usuario);

            return Result<UsuarioResponse>.Ok(new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil.ToString(),
                Ativo = usuario.Ativo
            });
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);

            if (usuario is null)
                return Result<LoginResponse>.Fail("Credenciais inválidas.");

            if (!usuario.Ativo)
                return Result<LoginResponse>.Fail("Usuário inativo.");

            //string novaSenha = "caio@12345";
            //string hash = BCrypt.Net.BCrypt.HashPassword(novaSenha);

            var senhaValida = BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash);

            if (!senhaValida)
                return Result<LoginResponse>.Fail("Credenciais inválidas.");

            var token = GerarToken(usuario);

            return Result<LoginResponse>.Ok(token);
        }

        private LoginResponse GerarToken(Usuario usuario)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Perfil.ToString())
        };

            var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpireMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return new LoginResponse
            {
                Token = tokenString,
                Expiracao = expires
            };
        }
    }
}
