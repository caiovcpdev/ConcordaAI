using ConcordaAI.Domain.Entities;
using ConcordaAI.Domain.Interfaces;
using ConcordaAI.Infrastructure.Persistence.Connection;
using Dapper;


namespace ConcordaAI.Infrastructure.Repositories
{
    public class EscalaRepository : IEscalaRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public EscalaRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AdicionarAsync(Escala escala)
        {
            const string sql = @"
                INSERT INTO Escala
                    (Id, EventoId, Nome, Data, HoraInicio, HoraFim, PontoEncontro, Observacoes, CreatedAt, CreatedBy)
                VALUES
                    (@Id, @EventoId, @Nome, @Data, @HoraInicio, @HoraFim, @PontoEncontro, @Observacoes, @CreatedAt, @CreatedBy);";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                escala.Id,
                escala.EventoId,
                escala.Nome,
                escala.Data,
                escala.HoraInicio,
                escala.HoraFim,
                escala.PontoEncontro,
                escala.Observacoes,
                escala.CreatedAt,
                escala.CreatedBy
            });
        }

        public async Task AtualizarAsync(Escala escala)
        {
            using var connection = _connectionFactory.CreateConnection();

            // Atualiza dados principais
            const string updateEscala = @"
                UPDATE 
                    Escala
                SET 
                    Nome = @Nome,
                    Data = @Data,
                    HoraInicio = @HoraInicio,
                    HoraFim = @HoraFim,
                    PontoEncontro = @PontoEncontro,
                    Observacoes = @Observacoes
                WHERE 
                    Id = @Id;";

            await connection.ExecuteAsync(updateEscala, new 
            {
                escala.Id,
                escala.Nome,
                escala.Data,
                escala.HoraInicio,
                escala.HoraFim,
                escala.PontoEncontro,
                escala.Observacoes
            });

            //Inserir trabalhadores
            foreach (var trabalhador in escala.Trabalhadores)
            {
                const string insertEscalaTrabalhador = @"
                    IF NOT EXISTS (
                        SELECT 1 FROM EscalaTrabalhador
                        WHERE EscalaId = @EscalaId
                          AND EventoTrabalhadorId = @EventoTrabalhadorId
                    )
                    INSERT INTO EscalaTrabalhador
                        (Id, EscalaId, EventoTrabalhadorId, HoraInicio, HoraFim, Confirmado, CreatedAt)
                    VALUES
                        (@Id, @EscalaId, @EventoTrabalhadorId, @HoraInicio, @HoraFim, @Confirmado, @CreatedAt);";

                await connection.ExecuteAsync(insertEscalaTrabalhador, new
                {
                    trabalhador.Id,
                    trabalhador.EscalaId,
                    trabalhador.EventoTrabalhadorId,
                    trabalhador.HoraInicio,
                    trabalhador.HoraFim,
                    trabalhador.Confirmado,
                    trabalhador.CreatedAt
                });
            }
        }

        public async Task<IEnumerable<Escala>> ObterPorEventoAsync(Guid eventoId)
        {
            const string sql = @"
                SELECT 
                    *
                FROM 
                    Escala
                WHERE 
                    EventoId = @EventoId
                ORDER BY 
                    Data, 
                    HoraInicio;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Escala>(sql, new { EventoId = eventoId });
        }

        public async Task<Escala?> ObterPorIdAsync(Guid id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sqlEscala = @"
                SELECT 
                    *
                FROM 
                    Escala
                WHERE 
                    Id = @Id;";

            var escala = await connection.QueryFirstOrDefaultAsync<Escala>(sqlEscala, new { Id = id });

            if (escala is null)
                return null;

            const string sqlTrabalhadores = @"
                SELECT 
                    *
                FROM 
                    EscalaTrabalhador
                WHERE 
                    EscalaId = @EscalaId;";

            var trabalhadores = await connection.QueryAsync<EscalaTrabalhador>(sqlTrabalhadores,new { EscalaId = id });

            //Reconstruir agregado manualmente
            foreach (var t in trabalhadores)
            {
                escala.AdicionarTrabalhador(t.EventoTrabalhadorId);
            }

            return escala;
        }
    }
}
