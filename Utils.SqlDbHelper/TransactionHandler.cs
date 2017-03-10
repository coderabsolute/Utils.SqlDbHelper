using System;
using System.Data;
using System.Data.SqlClient;

namespace Utils.SqlDbHelper
{
    /// <summary>
    /// <para>This class assists you to manage the Transactions automatically and efficiently.</para>
    /// </summary>
    public sealed class TransactionHandler : IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection = null;
        private IDbTransaction _transaction = null;

        public IDbConnection Connection
        {
            get { return _connection; }
        }

        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        public TransactionHandler(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(connectionString);
            }

            _connectionString = connectionString;
        }

        public void BeginTransaction()
        {
            OpenConnection();

            if (!IsTransactionAlive)
            {
                _transaction = _connection.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            if (IsTransactionAlive)
            {
                _transaction.Commit();
            }
        }

        public void RollbackTransaction()
        {
            if (IsTransactionAlive)
            {
                _transaction.Rollback();
            }
        }

        private void OpenConnection()
        {
            if (!IsConnectionAlive)
            {
                _connection = new SqlConnection(_connectionString);
            }

            _connection.Open();
        }

        private void CloseConnection()
        {
            if (IsConnectionAlive)
            {
                _connection.Close();
            }
        }

        private bool IsTransactionAlive
        {
            get
            {
                return (_transaction.IsolationLevel == IsolationLevel.ReadCommitted);
            }
        }

        private bool IsConnectionAlive
        {
            get
            {
                return (_connection.State == ConnectionState.Open);
            }
        }

        public void Dispose()
        {
            _transaction.Dispose();
            _connection.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
