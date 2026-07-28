using ConcordaAI.Domain.Entities;
using ConcordaAI.Domain.Interfaces;
using ConcordaAI.Infrastructure.Persistence.Connection;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public UsuarioRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AdicionarAsync(Usuario usuario)
        {
            const string sql = @"
                INSERT INTO Usuario
                    (Id, Nome, Email, SenhaHash, Perfil, Ativo, CreatedAt)
                VALUES
                    (@Id, @Nome, @Email, @SenhaHash, @Perfil, @Ativo, @CreatedAt);";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.SenhaHash,
                Perfil = (int)usuario.Perfil,
                usuario.Ativo,
                usuario.CreatedAt
            });
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            const string sql = @"
                SELECT 
                    *
                FROM 
                    Usuario
                WHERE 
                    Email = @Email;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Email = email.ToLower() });
        }

        public async Task<Usuario?> ObterPorIdAsync(Guid id)
        {
            const string sql = @"SELECT * FROM Usuario WHERE Id = @Id;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
        }
    }
}
