using ConcordaAI.Domain.Entities;
using ConcordaAI.Domain.Interfaces;
using ConcordaAI.Infrastructure.Persistence.Connection;
using Dapper;

namespace ConcordaAI.Infrastructure.Repositories
{
    public class EventoTrabalhadorRepository : IEventoTrabalhadorRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public EventoTrabalhadorRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AdicionarAsync(EventoTrabalhador entidade)
        {
            const string sql = @"
                INSERT INTO EventoTrabalhador
                    (Id, EventoId, TrabalhadorId, TipoTrabalhador, Status, ValorDiaria, Observacoes, CreatedAt, CreatedBy)
                VALUES
                    (@Id, @EventoId, @TrabalhadorId, @TipoTrabalhador, @Status, @ValorDiaria, @Observacoes, @CreatedAt, @CreatedBy);";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                entidade.Id,
                entidade.EventoId,
                entidade.TrabalhadorId,
                TipoTrabalhador = (int)entidade.TipoTrabalhador,
                Status = (int)entidade.Status,
                entidade.ValorDiaria,
                entidade.Observacoes,
                entidade.CreatedAt,
                entidade.CreatedBy
            });
        }

        public async Task AtualizarAsync(EventoTrabalhador entidade)
        {
            const string sql = @"
                UPDATE 
                    EventoTrabalhador
                SET
                    TipoTrabalhador = @TipoTrabalhador,
                    Status = @Status,
                    ValorDiaria = @ValorDiaria,
                    Observacoes = @Observacoes
                WHERE  
                    Id = @Id;";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                entidade.Id,
                TipoTrabalhador = (int)entidade.TipoTrabalhador,
                Status = (int)entidade.Status,
                entidade.ValorDiaria,
                entidade.Observacoes
            });
        }

        public async Task<IEnumerable<EventoTrabalhador>> ObterPorEventoAsync(Guid eventoId)
        {
            const string sql = @"
                SELECT 
                    *
                FROM 
                    EventoTrabalhador
                WHERE 
                    EventoId = @EventoId
                ORDER BY 
                    CreatedAt;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<EventoTrabalhador>(sql, new { EventoId = eventoId });
        }

        public async Task<EventoTrabalhador?> ObterPorEventoETrabalhadorAsync(Guid eventoId, Guid trabalhadorId)
        {
            const string sql = @"
                SELECT 
                    *
                FROM 
                    EventoTrabalhador
                WHERE 
                    EventoId = @EventoId
                    AND TrabalhadorId = @TrabalhadorId;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<EventoTrabalhador>( sql, new { EventoId = eventoId, TrabalhadorId = trabalhadorId });
        }

        public async Task<EventoTrabalhador?> ObterPorIdAsync(Guid id)
        {
            const string sql = @"
                SELECT 
                    *
                FROM 
                    EventoTrabalhador
                WHERE 
                    Id = @Id;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<EventoTrabalhador>(sql, new { Id = id });
        }
    }
}
