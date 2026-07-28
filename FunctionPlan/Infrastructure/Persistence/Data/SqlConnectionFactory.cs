using Application.Abstractions.Data;
using Npgsql;
using System.Data;

namespace Infrastructure.Persistence.Data
{
    internal sealed class SqlConnectionFactory: ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateDbConnection() 
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
