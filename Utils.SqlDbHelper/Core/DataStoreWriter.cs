using Dapper;
using System.Data;

namespace Utils.SqlDbHelper.Core
{
    public sealed class DataStoreWriter
    {
        #region Using Simple Crud 

        public static int Insert<T>(string connectionString, T poco) where T : class
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.Insert(poco).Value;
            }
        }

        public static int Insert<T>(string connectionString, T poco, IDbTransaction transaction) where T : class
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.Insert(poco, transaction).Value;
            }
        }

        public static int Update<T>(string connectionString, T poco) where T : class
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.Update(poco);
            }
        }
        public static int Update<T>(string connectionString, T poco, IDbTransaction transaction) where T : class
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.Update(poco, transaction);
            }
        }

        #endregion

        #region Pure Dapper

        public static int Execute(string connectionString, string sql, object param = null)
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.Execute(sql, param);
            }
        }
        public static int Execute(string connectionString, string sql, IDbTransaction transaction, object param = null)
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.Execute(sql, param, transaction);
            }
        }

        public async static void ExecuteAsync(string connectionString, string sql, object param = null)
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, param);
            }
        }
        public async static void ExecuteAsync(string connectionString, string sql, IDbTransaction transaction, object param = null)
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, param, transaction);
            }
        }

        #endregion
    }
}
