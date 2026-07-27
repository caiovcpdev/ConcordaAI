using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace ConcordaAI.Infrastructure.Persistence.Connection
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Connection string não encontrada.");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
