using System.Data;
using System.Data.SqlClient;

namespace Utils.SqlDbHelper
{
    /// <summary>
    /// Currently, the support is provided only for Sql Server. But this can be extended for other databases as well.
    /// </summary>
    static class ConnectionFactory
    {
        public static IDbConnection CreateSqlConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
