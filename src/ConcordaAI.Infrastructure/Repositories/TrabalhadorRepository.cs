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
    public class TrabalhadorRepository : ITrabalhadorRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public TrabalhadorRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task AdicionarAsync(Trabalhador trabalhador)
        {
            const string sql = @"
                INSERT INTO Trabalhador
                        (Id, Nome, CPF, DataNascimento, Sexo, Telefone, Endereco, Cidade, Estado, CEP, Status, CreatedAt, CreatedBy)
                VALUES
                        (@Id, @Nome, @CPF, @DataNascimento, @Sexo, @Telefone, @Endereco, @Cidade, @Estado, @CEP, @Status, @CreatedAt, @CreatedBy);";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                trabalhador.Id,
                trabalhador.Nome,
                trabalhador.CPF,
                trabalhador.DataNascimento,
                trabalhador.Sexo,
                trabalhador.Telefone,
                trabalhador.Endereco,
                trabalhador.Cidade,
                trabalhador.Estado,
                trabalhador.CEP,
                Status = (int)trabalhador.Status,
                trabalhador.CreatedAt,
                trabalhador.CreatedBy
            });
        }

        public async Task AtualizarAsync(Trabalhador trabalhador)
        {
            const string sql = @"
                UPDATE 
                    Trabalhador
                SET
                    Nome = @Nome,
                    Telefone = @Telefone,
                    Endereco = @Endereco,
                    Cidade = @Cidade,
                    Estado = @Estado,
                    CEP = @CEP,
                    Status = @Status
                WHERE 
                    Id = @Id;";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                trabalhador.Id,
                trabalhador.Nome,
                trabalhador.Telefone,
                trabalhador.Endereco,
                trabalhador.Cidade,
                trabalhador.Estado,
                trabalhador.CEP,
                Status = (int)trabalhador.Status
            });
        }

        public async Task<Trabalhador?> ObterPorCpfAsync(string cpf)
        {
            const string sql = @"
                SELECT 
                    *
                FROM 
                    Trabalhador
                WHERE 
                    CPF = @CPF;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<Trabalhador>(sql, new { CPF = cpf });
        }

        public async Task<Trabalhador?> ObterPorIdAsync(Guid id)
        {
            const string sql = @"
                SELECT 
                    *
                FROM 
                    Trabalhador
                WHERE 
                    Id = @Id;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<Trabalhador>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Trabalhador>> ObterTodosAsync()
        {
            const string sql = @"
            SELECT *
            FROM Trabalhador
            ORDER BY Nome;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Trabalhador>(sql);
        }
    }
}
