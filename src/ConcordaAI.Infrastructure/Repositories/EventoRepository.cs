using ConcordaAI.Domain.Entidades;
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
    public class EventoRepository : IEventoRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public EventoRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AdicionarAsync(Evento evento)
        {
            const string sql = @"
                    INSERT INTO Evento
                    (Id, Nome, Cidade, Estado, DataInicio, DataFim, Status, Organizador, CreatedAt, CreatedBy)
                    VALUES
                    (@Id, @Nome, @Cidade, @Estado, @DataInicio, @DataFim, @Status, @Organizador, @CreatedAt, @CreatedBy);";
            
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                evento.Id,
                evento.Nome,
                evento.Cidade,
                evento.Estado,
                evento.DataInicio,
                evento.DataFim,
                Status = (int)evento.Status,
                evento.Organizador,
                evento.CreatedAt,
                evento.CreatedBy
            });
        }

        public async Task AtualizarAsync(Evento evento)
        {
            const string sql = @"
                UPDATE Evento SET
                    Nome = @Nome,
                    Cidade = @Cidade,
                    Estado = @Estado,
                    DataInicio = @DataInicio,
                    DataFim = @DataFim,
                    Status = @Status,
                    Organizador = @Organizador
                WHERE Id = @Id;";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new {
                evento.Id,
                evento.Nome,
                evento.Cidade,
                evento.Estado,
                evento.DataInicio,
                evento.DataFim,
                Status = (int)evento.Status,
                evento.Organizador
            });

        }

        public async Task<Evento?> ObterPorIdAsync(Guid id)
        {
            const string sql = @"
                SELECT *
                FROM Evento
                WHERE Id = @Id;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<Evento>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Evento>> ObterTodosAsync()
        {
            const string sql = @"
                SELECT *
                FROM Evento
                ORDER BY DataInicio DESC;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Evento>(sql);
        }
    }
}
