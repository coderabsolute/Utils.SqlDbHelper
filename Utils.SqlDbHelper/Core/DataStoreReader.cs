using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.SqlDbHelper.Core
{
    public sealed class DataStoreReader
    {
        #region Using Simple Crud

        public static IEnumerable<T> GetList<T>(string connectionString, object whereConditions) where T : class
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.GetList<T>(whereConditions);
            }
        }

        public static bool IsRowExist<T>(string connectionString, object whereConditions) where T : class
        {
            return Convert.ToBoolean(RecordCount<T>(connectionString, whereConditions));
        }

        public static IEnumerable<T> GetList<T>(string connectionString) where T : class
        {
            return GetList<T>(connectionString, null);
        }

        public static T Get<T>(string connectionString, object whereConditions) where T : class
        {
            return GetList<T>(connectionString, whereConditions).SingleOrDefault();
        }

        public static T Get<T>(string connectionString, int id) where T : class
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.Get<T>(id);
            }
        }

        public static int RecordCount<T>(string connectionString, object whereConditions) where T : class
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.RecordCount<T>(whereConditions);
            }
        }

        #endregion

        #region Using Pure Dapper

        public static IEnumerable<T> Query<T>(string connectionString, string sql, object param = null)
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.Query<T>(sql, param);
            }
        }

        public static T QuerySingleOrDefault<T>(string connectionString, string sql, object param = null)
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.QuerySingleOrDefault<T>(sql, param);
            }
        }

        public static bool RowExist(string connectionString, string sql, object param = null)
        {
            var result = (ExecuteScalar(connectionString, sql, param) != null);

            return result;
        }

        public static object ExecuteScalar(string connectionString, string sql, object param = null)
        {
            using (var connection = ConnectionFactory.CreateSqlConnection(connectionString))
            {
                return connection.ExecuteScalar(sql, param);
            }
        }

        #endregion
    }
}
