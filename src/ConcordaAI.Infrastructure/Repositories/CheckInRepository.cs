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
    public class CheckInRepository : ICheckInRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public CheckInRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<CheckInDto?> ObterPorEscalaTrabalhadorAsync(Guid escalaTrabalhadorId)
        {
            const string sql = @"
                SELECT 
                    EscalaTrabalhadorId,
                    DataHoraCheckIn,
                    DataHoraCheckOut
                FROM 
                    CheckInEscala
                WHERE 
                    EscalaTrabalhadorId = @EscalaTrabalhadorId;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<CheckInDto>( sql, new { EscalaTrabalhadorId = escalaTrabalhadorId });
        }

        public async Task RegistrarCheckInAsync(Guid escalaTrabalhadorId, decimal latitude, decimal longitude, string dispositivo)
        {
            const string sql = @"
            IF NOT EXISTS (
                SELECT 1 FROM CheckInEscala
                WHERE EscalaTrabalhadorId = @EscalaTrabalhadorId
            )
            BEGIN
                INSERT INTO CheckInEscala
                (   
                    Id, 
                    EscalaTrabalhadorId,
                    DataHoraCheckIn,
                    LatitudeCheckIn,
                    LongitudeCheckIn,
                    DispositivoCheckIn,
                    CreatedAt
                )
                VALUES
                (
                    NEWID(), 
                    @EscalaTrabalhadorId,
                    SYSUTCDATETIME(),
                    @Latitude,
                    @Longitude,
                    @Dispositivo,
                    SYSUTCDATETIME()
                );
            END
            ELSE
            BEGIN
                UPDATE CheckInEscala
                SET 
                    DataHoraCheckIn = SYSUTCDATETIME(),
                    LatitudeCheckIn = @Latitude,
                    LongitudeCheckIn = @Longitude,
                    DispositivoCheckIn = @Dispositivo
                WHERE 
                    EscalaTrabalhadorId = @EscalaTrabalhadorId;
            END";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                EscalaTrabalhadorId = escalaTrabalhadorId,
                Latitude = latitude,
                Longitude = longitude,
                Dispositivo = dispositivo
            });
        }

        public async Task RegistrarCheckOutAsync(Guid escalaTrabalhadorId, decimal latitude, decimal longitude, string dispositivo)
        {
            const string sql = @"
                UPDATE CheckInEscala
                SET 
                    DataHoraCheckOut = SYSUTCDATETIME(),
                    LatitudeCheckOut = @Latitude,
                    LongitudeCheckOut = @Longitude,
                    DispositivoCheckOut = @Dispositivo
                WHERE 
                    EscalaTrabalhadorId = @EscalaTrabalhadorId;";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                EscalaTrabalhadorId = escalaTrabalhadorId,
                Latitude = latitude,
                Longitude = longitude,
                Dispositivo = dispositivo
            });
        }
    }
}
