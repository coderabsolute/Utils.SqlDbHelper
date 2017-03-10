using System;
using System.Collections.Generic;
using System.Data;
using Utils.SqlDbHelper.Core;

namespace Utils.SqlDbHelper
{
    public abstract class BaseDataStore<T> where T : class
    {
        private readonly string _connection;

        public BaseDataStore(string connectionString)
        {
            _connection = connectionString;
        }

        public int Execute(string sql, object param = null)
        {
            return DataStoreWriter.Execute(_connection, sql, param);
        }

        public int Execute(string sql, IDbTransaction transaction, object param = null)
        {
            return DataStoreWriter.Execute(_connection, sql, transaction, param);
        }

        public IEnumerable<T> Query(string sql, object param = null)
        {
            return DataStoreReader.Query<T>(_connection, sql, param);
        }

        public int Add(T poco)
        {
            return DataStoreWriter.Insert<T>(_connection, poco);
        }

        public int Add(T poco, IDbTransaction transaction)
        {
            return DataStoreWriter.Insert<T>(_connection, poco, transaction);
        }

        public bool Update(T poco)
        {
            var result = DataStoreWriter.Update<T>(_connection, poco);

            return Convert.ToBoolean(result);
        }

        public bool Update(T poco, IDbTransaction transaction)
        {
            var result = DataStoreWriter.Update<T>(_connection, poco, transaction);

            return Convert.ToBoolean(result);
        }

        public bool Exists(int id)
        {
            var result = this.Get(id);

            return (result == null);
        }

        public bool Exists(object whereConditions)
        {
            var result = this.Get(whereConditions);

            return (result != null);
        }

        public T Get(int id)
        {
            return DataStoreReader.Get<T>(_connection, id);
        }

        public T QuerySingleOrDefault(string sql, object param = null)
        {
            return DataStoreReader.QuerySingleOrDefault<T>(_connection, sql, param);
        }

        public T Get(object whereConditions)
        {
            return DataStoreReader.Get<T>(_connection, whereConditions);
        }

        public IEnumerable<T> GetAll()
        {
            return DataStoreReader.GetList<T>(_connection);
        }

        public virtual IEnumerable<T> GetList(object whereConditions)
        {
            return DataStoreReader.GetList<T>(_connection, whereConditions);
        }
    }
}
