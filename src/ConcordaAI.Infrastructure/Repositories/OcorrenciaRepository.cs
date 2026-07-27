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
    public class OcorrenciaRepository : IOcorrenciaRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public OcorrenciaRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AdicionarAsync(Ocorrencia ocorrencia)
        {
            const string sql = @"
            INSERT INTO Ocorrencia
                (Id, EventoTrabalhadorId, Tipo, Descricao, DataHora, Gravidade, Resolvida, CreatedAt, CreatedBy)
            VALUES
                (@Id, @EventoTrabalhadorId, @Tipo, @Descricao, @DataHora, @Gravidade, @Resolvida, @CreatedAt, @CreatedBy);";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                ocorrencia.Id,
                ocorrencia.EventoTrabalhadorId,
                Tipo = (int)ocorrencia.Tipo,
                ocorrencia.Descricao,
                ocorrencia.DataHora,
                Gravidade = (int)ocorrencia.Gravidade,
                ocorrencia.Resolvida,
                ocorrencia.CreatedAt,
                ocorrencia.CreatedBy
            });
        }

        public async Task AtualizarAsync(Ocorrencia ocorrencia)
        {
            const string sql = @"
                UPDATE 
                    Ocorrencia
                SET 
                    Resolvida = @Resolvida
                WHERE 
                    Id = @Id;";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                ocorrencia.Id,
                ocorrencia.Resolvida
            });
        }

        public async Task<IEnumerable<Ocorrencia>> ObterPorEventoTrabalhadorAsync(Guid eventoTrabalhadorId)
        {
            const string sql = @"
                SELECT 
                    *
                FROM 
                    Ocorrencia
                WHERE 
                    EventoTrabalhadorId = @EventoTrabalhadorId
                ORDER BY 
                    DataHora DESC;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Ocorrencia>( sql, new { EventoTrabalhadorId = eventoTrabalhadorId });
        }

        public async Task<Ocorrencia?> ObterPorIdAsync(Guid id)
        {
            const string sql = @"
                SELECT 
                    *
                FROM 
                    Ocorrencia
                WHERE 
                    Id = @Id;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<Ocorrencia>(sql, new { Id = id });
        }
    }
}
