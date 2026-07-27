using DbUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Infrastructure.Persistence.Database
{
    public class MigrationRunner
    {
        public static void ExecutarMigration(string connectionString)
        {
            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var resultado = upgrader.PerformUpgrade();

            if (!resultado.Successful) 
            {
                throw new InvalidOperationException($"Falha ao executar migrations: {resultado.Error?.Message}", resultado.Error);
            }
        }
    }
}
