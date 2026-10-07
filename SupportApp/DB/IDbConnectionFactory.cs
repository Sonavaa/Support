using Microsoft.Data.SqlClient;

namespace SupportApp.DB
{
    public interface IDbConnectionFactory
    {
        SqlConnection CreateMsSqlConnection();
        SqlCommand CreateProcedureCommand(string procedureName, SqlConnection connection);
    }
}
