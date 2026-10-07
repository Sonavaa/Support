using Microsoft.Data.SqlClient;
using System.Data;

namespace SupportApp.DB
{
    public sealed class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection CreateMsSqlConnection()
            => new SqlConnection(_config.GetConnectionString("MsSqlDb"));

        public SqlCommand CreateProcedureCommand(string procedureName, SqlConnection connection)
        {
            var command = new SqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 30
            };

            return command;
        }

    }
}
