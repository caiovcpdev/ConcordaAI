using ConcordaAI.Domain.Entities;
using ConcordaAI.Domain.Interfaces;
using ConcordaAI.Infrastructure.Persistence.Connection;
using Dapper;

namespace ConcordaAI.Infrastructure.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public PagamentoRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AdicionarAsync(Pagamento pagamento)
        {
            const string sql = @"
                INSERT INTO Pagamento
                    (Id, EventoTrabalhadorId, ValorPrevisto, ValorPago, DataPrevista, DataPagamento, FormaPagamento, Status, ComprovanteUrl, Observacoes, CreatedAt, CreatedBy)
                VALUES
                    (@Id, @EventoTrabalhadorId, @ValorPrevisto, @ValorPago, @DataPrevista, @DataPagamento, @FormaPagamento,@Status, @ComprovanteUrl, @Observacoes, @CreatedAt, @CreatedBy);";
            
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                pagamento.Id,
                pagamento.EventoTrabalhadorId,
                pagamento.ValorPrevisto,
                pagamento.ValorPago,
                pagamento.DataPrevista,
                pagamento.DataPagamento,
                FormaPagamento = (int)pagamento.FormaPagamento,
                Status = (int)pagamento.Status,
                pagamento.ComprovanteUrl,
                pagamento.Observacoes,
                pagamento.CreatedAt,
                pagamento.CreatedBy
            });
        }

        public async Task AtualizarAsync(Pagamento pagamento)
        {
            const string sql = @"
                UPDATE Pagamento
                SET
                    ValorPago = @ValorPago,
                    DataPagamento = @DataPagamento,
                    FormaPagamento = @FormaPagamento,
                    Status = @Status,
                    ComprovanteUrl = @ComprovanteUrl
                WHERE 
                    Id = @Id;";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                pagamento.Id,
                pagamento.ValorPago,
                pagamento.DataPagamento,
                FormaPagamento = (int)pagamento.FormaPagamento,
                Status = (int)pagamento.Status,
                pagamento.ComprovanteUrl
            });
        }

        public async Task<IEnumerable<Pagamento>> ObterPorEventoTrabalhadorAsync(Guid eventoTrabalhadorId)
        {
            const string sql = @"
                SELECT 
                    *
                FROM 
                    Pagamento
                WHERE 
                    EventoTrabalhadorId = @EventoTrabalhadorId
                ORDER BY 
                    CreatedAt DESC;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Pagamento>(sql, new { EventoTrabalhadorId = eventoTrabalhadorId });
        }

        public async Task<Pagamento?> ObterPorIdAsync(Guid id)
        {
            const string sql = @"SELECT * FROM Pagamento WHERE Id = @Id;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<Pagamento>(sql, new { Id = id });
        }
    }
}
